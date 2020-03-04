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
        //1.项目流程管理器里接入语音和主持人引导声音片段。
        //2.流程管理器加入播放语音功能，现在按步骤执行可以放声音片段，可以配表执行。
        //3.界面中加入显示当前步骤的ui。
        //4.

            //1.按照demo流程文档，测试每一条功能点；
            //2.测试按步骤执行会不会有bug产生；
            //3.开发部分还未贯穿流程功能；

        /// <summary>
        /// 关闭方法
        /// </summary>
        public abstract void Shutdown();

    }
}