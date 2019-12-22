using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp
{
    public class Role
    {
        public Role()
        {
            SocketSendMS = new MMO_MemoryStream();
            SocketReceiveMS = new MMO_MemoryStream();
        }

        #region 基本属性

        /// <summary>
        /// 当前角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色昵称
        /// </summary>
        public string NickName { get; set; }


      
        #endregion

        /// <summary>
        /// 发送用的MS
        /// </summary>
        public MMO_MemoryStream SocketSendMS
        {
            get;
            private set;
        }

        /// <summary>
        /// 接收用的MS
        /// </summary>
        public MMO_MemoryStream SocketReceiveMS
        {
            get;
            private set;
        }

        public ClientSocket Client_Socket { get; set; }

    


  
    }
}