using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JIANING;
public class TestTime : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TimeAction action = GameEntry.Time.CreateTimeAction();
            action.Init(5f, 1, 8,
                () => {
                    Debug.Log("start");
                },
                (int loop) => {
                    Debug.Log("update loop = " + loop);
                },
                () =>{
                    Debug.Log("complete");
                }
                ).Run();
        }

    }

    
}
