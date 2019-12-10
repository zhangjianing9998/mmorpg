using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JIANING;
public class TestProcedure : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("当前的流程 = " + GameEntry.Procedure.CurrProcedure);

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameEntry.Procedure.ChangeState( ProcedureState.CheckVersion);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.EnterGame);


        }
    }
}
