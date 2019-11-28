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
        }

        public override void Shutdown()
        {
            m_EventManager.Dispose();
            m_EventManager = null;
        }
    }
}