using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using JIANING;
public class TestPool : MonoBehaviour
{
    void Start()
    {
        GameEntry.Pool.SetClassObjectResideCount<StringBuilder>(3);
        GameEntry.Pool.SetClassObjectResideCount<StringBuilder>(3);
        GameEntry.Pool.SetClassObjectResideCount<StringBuilder>(3);
        GameEntry.Pool.SetClassObjectResideCount<StringBuilder>(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StringBuilder str = GameEntry.Pool.DequeueClassObject<StringBuilder>();
            str.Length = 0;
            str.Append("123");

            aa data1 = GameEntry.Pool.DequeueClassObject<aa>();
            bb data2 = GameEntry.Pool.DequeueClassObject<bb>();
            cc data3 = GameEntry.Pool.DequeueClassObject<cc>();


            StartCoroutine(EnqueueClassObject(str));
            StartCoroutine(EnqueueClassObject(data1));
            StartCoroutine(EnqueueClassObject(data2));
            StartCoroutine(EnqueueClassObject(data3));

        }
    }

    private IEnumerator EnqueueClassObject(object obj)
    {
        yield return new WaitForSeconds(5f);
        GameEntry.Pool.EnqueueClassObject(obj);
    }
}

public class aa{
    
    }
public class bb
{

}

public class cc
{

}

