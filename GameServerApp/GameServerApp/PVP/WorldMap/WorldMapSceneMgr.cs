using GameServerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.PVP.WorldMap
{
    /// <summary>
    /// 世界地图场景管理器
    /// </summary>
    public class WorldMapSceneMgr : Singleton<WorldMapSceneMgr>, IDisposable
    {
        private Dictionary<int, WorldMapSceneController> m_WorldMapSceneControllerDic;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
         


            //客户端发送角色复活消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.test, OnTest);

        }

        private void OnTest(Role role, byte[] buffer)
        {
            testProto proto = testProto.GetProto(role.SocketReceiveMS, buffer);
            Console.WriteLine(proto.hp);


            testProto proto1 = new testProto();
            proto1.hp = 50;
            role.Client_Socket.SendMsg(proto1.ToArray(role.SocketSendMS));
        }



        
    }
}
