using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventComponent : JIANINGBaseComponent
    {
        /// <summary>
        /// 事件管理器
        /// </summary>
        private EventManager m_EventManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_EventManager = new EventManager();
            SocketEvent = m_EventManager.SocketEvent;
            CommonEvent = m_EventManager.CommonEvent;
        }

        public override void Shutdown()
        {
            m_EventManager.Dispose();
        }

        /// <summary>
        /// Socket事件
        /// </summary>
        public SocketEvent SocketEvent;

        /// <summary>
        /// 通用事件
        /// </summary>
        public CommonEvent CommonEvent;

    }
}
