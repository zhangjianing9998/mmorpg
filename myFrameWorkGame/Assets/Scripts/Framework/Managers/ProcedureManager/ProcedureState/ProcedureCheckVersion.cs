using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class ProcedureCheckVersion : ProcedureBase
    {
        public override void OnEnter()
        {
            Debug.Log("ProcedureCheckVersion Enter");
        }

        public override void OnUpdate()
        {
            Debug.Log("ProcedureCheckVersion OnUpdate");
        }

        public override void OnLeave()
        {
            Debug.Log("ProcedureCheckVersion OnLeave");
        }

        public override void OnDestory()
        {
            Debug.Log("ProcedureCheckVersion OnDestory");
        }

    }
}