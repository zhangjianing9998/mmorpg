using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public abstract class JIANINGBaseComponent : JIANINGComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            //把自己加入基础组件列表
            GameEntry.RegisterBaseComponent(this);
        }


        /// <summary>
        /// 关闭方法
        /// </summary>
        public abstract void Shutdown();

    }
}