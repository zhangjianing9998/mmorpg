using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class EventManager : ManagerBase, System.IDisposable
    {

        /// <summary>
        /// Socket事件
        /// </summary>
        public SocketEvent SocketEvent
        {
            private set;
            get;
        }

        /// <summary>
        /// 通用事件
        /// </summary>
        public CommonEvent CommonEvent
        {
            private set;
            get;
        }

        public EventManager()
        {
            SocketEvent = new SocketEvent();
            CommonEvent = new CommonEvent();
        }



        public void Dispose()
        {
            SocketEvent.Dispose();
            CommonEvent.Dispose();
        }
    }
}