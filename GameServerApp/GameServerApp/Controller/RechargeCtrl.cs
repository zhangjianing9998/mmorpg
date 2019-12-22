using GameServerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServerApp.Controller
{
    public class RechargeCtrl : Singleton<RechargeCtrl>, IDisposable
    {
        private byte[] m_ReceiveBuffer = new byte[1024];

        public void Recharge(Socket socket)
        {
            
            int len = socket.Receive(m_ReceiveBuffer);

       
        }
    }
}