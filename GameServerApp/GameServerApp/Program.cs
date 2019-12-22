using GameServerApp.Controller;
using GameServerApp.PVP.WorldMap;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServerApp
{
    class Program
    {
        private static string m_ServerIP;
        private static int m_Port;

        private static Socket m_ServerSocket;

        /// <summary>
        /// 初始化所有控制器
        /// </summary>
        static void InitAllController()
        {
            ////角色控制器初始化
            //RoleController.Instance.Init();

            //世界地图场景管理器初始化
            WorldMapSceneMgr.Instance.Init();
        }


        static void Main(string[] args)
        {
            m_ServerIP = System.Configuration.ConfigurationManager.AppSettings["serverIP"];
            m_Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["serverPort"]);
            Console.WriteLine(m_ServerIP);
            Console.WriteLine(m_Port);

            //加载所有表格
            DataTableManager.Instance.LoadDataTable();

            InitAllController();

            //实例化socket
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //向操作系统申请一个可用的ip和端口用来通讯
            m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(m_ServerIP), m_Port));

            //设置最多3000个排队连接请求
            m_ServerSocket.Listen(3000);

            Console.WriteLine("启动监听{0}成功", m_ServerSocket.LocalEndPoint.ToString());

            Thread mThread = new Thread(ListenClientCallBack);
            mThread.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private static void ListenClientCallBack()
        {
            while (true)
            {
                //接收客户端请求
                Socket socket = m_ServerSocket.Accept();

                Console.WriteLine("客户端{0}已经连接", socket.RemoteEndPoint.ToString());

                //Console.WriteLine("客户端{0}已经连接", ((IPEndPoint)socket.RemoteEndPoint).Address.ToString());

                if (((IPEndPoint)socket.RemoteEndPoint).Address.ToString() == m_ServerIP)
                {
                    RechargeCtrl.Instance.Recharge(socket);
                }
                else
                {

                    //一个角色 就相当于一个客户端
                    Role role = new Role();
                    ClientSocket clientSocket = new ClientSocket(socket, role);

                    //把角色添加到角色管理
                    RoleMgr.Instance.AllRole.Add(role);
                }
            }
        }
    }
}
