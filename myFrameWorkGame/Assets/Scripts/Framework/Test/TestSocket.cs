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
            GameEntry.Socket.ConnectToMainSocket("192.168.0.111",1038);


        }
    }
}
