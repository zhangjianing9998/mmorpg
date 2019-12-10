using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    /// <summary>
    /// framework base
    /// </summary>
    public class JIANINGComponent : MonoBehaviour
    {
        /// <summary>
        /// 组件实例编号
        /// </summary>
        private int m_InstanceId;

      
        private void Awake()
        {
            m_InstanceId = GetInstanceID();

          
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        public int InstanceID
        {
            get { return m_InstanceId; }
        }


        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }

        
    }

   
}
