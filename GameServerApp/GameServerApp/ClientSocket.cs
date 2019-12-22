﻿using GameServerApp.Common;
using GameServerApp.PVP.WorldMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServerApp
{
    /// <summary>
    /// 客户端连接对象 负责和客户端进行通讯的
    /// </summary>
    public class ClientSocket
    {
        //所属角色
        private Role m_Role;

        //客户端Socket
        private Socket m_Socket;

        //接收数据的线程
        private Thread m_ReceiveThread;


        #region 接收消息所需变量
        //接收数据包的字节数组缓冲区
        private byte[] m_ReceiveBuffer = new byte[2048];

        //接收数据包的缓冲数据流
        private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

        /// <summary>
        /// 发送用的MS
        /// </summary>
        private MMO_MemoryStream m_SocketSendMS = new MMO_MemoryStream();

        /// <summary>
        /// 接收用的MS
        /// </summary>
        private MMO_MemoryStream m_SocketReceiveMS = new MMO_MemoryStream();

        #endregion

        #region 发送消息所需变量
        //发送消息队列
        private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

        //检查队列的委托
        private Action m_CheckSendQuene;

        //压缩数组的长度界限
        private const int m_CompressLen = 200;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="role"></param>
        public ClientSocket(Socket socket, Role role)
        {
            m_Socket = socket;
            m_Role = role;
            m_Role.Client_Socket = this;

            //启动线程 进行接收数据
            m_ReceiveThread = new Thread(ReceiveMsg);
            m_ReceiveThread.Start();

            m_CheckSendQuene = OnCheckSendQueueCallBack;
        }

        #region ReceiveMsg 接收数据
        /// <summary>
        /// 接收数据
        /// </summary>
        private void ReceiveMsg()
        {
            //异步接收数据
            m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Socket);
        }
        #endregion

        #region ReceiveCallBack 接收数据回调
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int len = m_Socket.EndReceive(ar);

                if (len > 0)
                {
                    //已经接收到数据

                    //把接收到数据 写入缓冲数据流的尾部
                    m_ReceiveMS.Position = m_ReceiveMS.Length;

                    //把指定长度的字节 写入数据流
                    m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                    //如果缓存数据流的长度>2 说明至少有个不完整的包过来了
                    //为什么这里是2 因为我们客户端封装数据包 用的ushort 长度就是2
                    if (m_ReceiveMS.Length > 2)
                    {
                        //进行循环 拆分数据包
                        while (true)
                        {
                            //把数据流指针位置放在0处
                            m_ReceiveMS.Position = 0;

                            //currMsgLen = 包体的长度
                            int currMsgLen = m_ReceiveMS.ReadUShort();

                            //currFullMsgLen 总包的长度=包头长度+包体长度
                            int currFullMsgLen = 2 + currMsgLen;

                            //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                            if (m_ReceiveMS.Length >= currFullMsgLen)
                            {
                                //至少收到一个完整包

                                //定义包体的byte[]数组
                                byte[] buffer = new byte[currMsgLen];

                                //把数据流指针放到2的位置 也就是包体的位置
                                m_ReceiveMS.Position = 2;

                                //把包体读到byte[]数组
                                m_ReceiveMS.Read(buffer, 0, currMsgLen);

                                //===================================================
                                //异或之后的数组
                                byte[] bufferNew = new byte[buffer.Length - 3];

                                bool isCompress = false;
                                ushort crc = 0;

                                MMO_MemoryStream ms1 = this.m_ReceiveMS;
                                ms1.SetLength(0);
                                ms1.Write(buffer, 0, buffer.Length);
                                ms1.Position = 0;

                                isCompress = ms1.ReadBool();
                                crc = ms1.ReadUShort();
                                ms1.Read(bufferNew, 0, bufferNew.Length);

                                //using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                                //{
                                //    isCompress = ms.ReadBool();
                                //    crc = ms.ReadUShort();
                                //    ms.Read(bufferNew, 0, bufferNew.Length);
                                //}

                                //先crc
                                int newCrc = Crc16.CalculateCrc16(bufferNew);
                                if (newCrc == crc)
                                {
                                    //异或 得到原始数据
                                    bufferNew = SecurityUtil.Xor(bufferNew);

                                    if (isCompress)
                                    {
                                        bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                                    }

                                    //协议编号
                                    ushort protoCode = 0;
                                    byte[] protoContent = new byte[bufferNew.Length - 2];

                                    MMO_MemoryStream ms2 = this.m_ReceiveMS;
                                    ms2.SetLength(0);
                                    ms2.Write(bufferNew, 0, bufferNew.Length);
                                    ms2.Position = 0;

                                    protoCode = ms2.ReadUShort();
                                    ms2.Read(protoContent, 0, protoContent.Length);

                                    //using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                                    //{
                                    //    protoCode = ms.ReadUShort();
                                    //    ms.Read(protoContent, 0, protoContent.Length);
                                    //}

                                    EventDispatcher.Instance.Dispatch(protoCode, m_Role, protoContent);
                                }

                                //==============处理剩余字节数组===================

                                //剩余字节长度
                                int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                                if (remainLen > 0)
                                {
                                    //把指针放在第一个包的尾部
                                    m_ReceiveMS.Position = currFullMsgLen;

                                    //定义剩余字节数组
                                    byte[] remainBuffer = new byte[remainLen];

                                    //把数据流读到剩余字节数组
                                    m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                    //清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);

                                    //把剩余字节数组重新写入数据流
                                    m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                    remainBuffer = null;
                                }
                                else
                                {
                                    //没有剩余字节

                                    //清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);

                                    break;
                                }
                            }
                            else
                            {
                                //还没有收到完整包
                                break;
                            }
                        }
                    }

                    //进行下一次接收数据包
                    ReceiveMsg();
                }
                else
                {
                    //客户端断开连接
                    Console.WriteLine("客户端{0}断开连接", m_Socket.RemoteEndPoint.ToString());
                    RoleMgr.Instance.AllRole.Remove(m_Role);
                }
            }
            catch(Exception ex)
            {
                //客户端断开连接
                Console.WriteLine("客户端{0}断开连接 原因{1}", m_Socket.RemoteEndPoint.ToString(), ex.Message);
                RoleMgr.Instance.AllRole.Remove(m_Role);
            }
        }
        #endregion


        //========================================================


        #region OnCheckSendQueueCallBack 检查队列的委托回调
        /// <summary>
        /// 检查队列的委托回调
        /// </summary>
        private void OnCheckSendQueueCallBack()
        {
            lock (m_SendQueue)
            {
                //如果队列中有数据包 则发送数据包
                if (m_SendQueue.Count > 0)
                {
                    //发送数据包
                    Send(m_SendQueue.Dequeue());
                }
            }
        }
        #endregion

        #region MakeData 封装数据包
        /// <summary>
        /// 封装数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] MakeData(byte[] data)
        {
            byte[] retBuffer = null;

            //1.如果数据包的长度 大于了m_CompressLen 则进行压缩
            bool isCompress = data.Length > m_CompressLen ? true : false;
            if (isCompress)
            {
                data = ZlibHelper.CompressBytes(data);
            }

            //2.异或
            data = SecurityUtil.Xor(data);

            //3.Crc校验 压缩后的
            ushort crc = Crc16.CalculateCrc16(data);

            Console.WriteLine("crc="+ crc);
            MMO_MemoryStream ms = this.m_SocketSendMS;
            ms.SetLength(0);

            ms.WriteUShort((ushort)(data.Length + 3));
            ms.WriteBool(isCompress);
            ms.WriteUShort(crc);
            ms.Write(data, 0, data.Length);

            retBuffer = ms.ToArray();
            return retBuffer;
        }
        #endregion

        #region SendMsg 发送消息 把消息加入队列
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer"></param>
        public void SendMsg(byte[] buffer)
        {
            //得到封装后的数据包
            byte[] sendBuffer = MakeData(buffer);

            lock (m_SendQueue)
            {
                //把数据包加入队列
                m_SendQueue.Enqueue(sendBuffer);

                //启动委托（执行委托）
                m_CheckSendQuene.BeginInvoke(null, null);
            }
        }
        #endregion

        #region Send 真正发送数据包到服务器
        /// <summary>
        /// 真正发送数据包到服务器
        /// </summary>
        /// <param name="buffer"></param>
        private void Send(byte[] buffer)
        {
            m_Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Socket);
        }
        #endregion

        #region SendCallBack 发送数据包的回调
        /// <summary>
        /// 发送数据包的回调
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallBack(IAsyncResult ar)
        {
            m_Socket.EndSend(ar);

            //继续检查队列
            OnCheckSendQueueCallBack();
        }
        #endregion
    }
}