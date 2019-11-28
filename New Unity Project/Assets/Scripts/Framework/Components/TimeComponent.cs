using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class TimeComponent : JIANINGBaseComponent, IUpdataComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
        }
        public void OnUpdate()
        {
            Debug.Log("时间组件Update");
        }
        public override void Shutdown()
        {
        }
    }
}