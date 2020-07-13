using JIANING;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameEntry.UI.OpenUIForm(UIFormId.UI_Task);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEntry.UI.CloseUIForm(UIFormId.UI_Task);
        }
    }


}
