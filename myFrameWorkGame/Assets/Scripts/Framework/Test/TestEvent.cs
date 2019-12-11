using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JIANING;
using System;

public class TestEvent : MonoBehaviour
{
    private void Start()
    {

        GameEntry.Event.CommonEvent.AddEventListener(CommonEventID.RegComplete,OnRegConplete);
       
    }

    private void OnRegConplete(object userData)
    {
        Debug.Log(userData);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameEntry.Event.CommonEvent.Dispatch(1001,123);
        }
    }

    private void OnDestroy()
    {
        GameEntry.Event.CommonEvent.RemoveEventListener(CommonEventID.RegComplete, OnRegConplete);
    }
}
