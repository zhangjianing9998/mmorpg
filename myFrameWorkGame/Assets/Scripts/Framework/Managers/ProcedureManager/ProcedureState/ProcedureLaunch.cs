using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class ProcedureLaunch : FsmState<ProcedureManager>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("ProcedureLaunch Enter");
            //访问账号服务器
            string url = GameEntry.Http.RealWebAccountUrl + "";

            Dictionary<string, object> dic = GameEntry.Pool.DequeueClassObject<Dictionary<string, object>>();
            dic.Clear();
            dic["ChanneId"] = 0;
            dic["InnerVersion"] = 1001;
            GameEntry.Http.SendData(url,OnWebAccountInit,true,false,dic);


        }

        private void OnWebAccountInit(HttpCallbackArgs args)
        {
            Debug.Log("HasError=" + args.HasError);
            Debug.Log("Value=" + args.Value);
        }

        public override void OnUpdate()
        {

        }

        public override void OnLeave()
        {
            Debug.Log("ProcedureLaunch OnLeave");
        }

        public override void OnDestory()
        {
            Debug.Log("ProcedureLaunch OnDestory");
        }

    }
}