using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JIANING;
public class TestSocket : MonoBehaviour
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
            GameEntry.Socket.ConnectToMainSocket("10.211.55.5",1038);


        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Task_SearchTaskProto proto = new Task_SearchTaskProto();
            //GameEntry.Socket.SendMsg(proto);
            //for (int i = 0; i < 100; i++)
            //{
            //    System_SendLocalTimeProto proto = new System_SendLocalTimeProto();

            //    GameEntry.Socket.SendMsg(proto);
            //}
        }
    }
}
