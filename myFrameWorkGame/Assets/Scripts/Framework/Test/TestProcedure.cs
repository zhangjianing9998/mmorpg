using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JIANING;
public class TestProcedure : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //VarInt a = VarInt.Alloc();

        //StartCoroutine(asd(a));
        //a.Release();
        //Debug.Log(33);
    }

    IEnumerator asd(VarInt a)
    {
        //a.Retain();

        yield return new WaitForSeconds(2f);
        //Debug.LogError("a=" + a.Value);

        //Debug.Log("在携程中释放");
        a.Release();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    VarInt a = VarInt.Alloc(10);
        //    int x = a;
        //    StartCoroutine(asd(a));
        //    Debug.Log("释放");
        //    //a.Release();
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameEntry.Procedure.SetDate("code" , 12);
            GameEntry.Procedure.SetDate("name" , "家宁");
            Debug.Log("当前的流程 = " + GameEntry.Procedure.CurrProcedure);

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEntry.Procedure.ChangeState(ProcedureState.EnterGame);


        }
    }
}
