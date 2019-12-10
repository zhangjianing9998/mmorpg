using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class ProcedureLaunch : FsmState<ProcedureManager>
    {
        public override void OnEnter()
        {
            Debug.Log("ProcedureLaunch Enter");
        }

        public override void OnUpdate()
        {
            Debug.Log("ProcedureLaunch OnUpdate");
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