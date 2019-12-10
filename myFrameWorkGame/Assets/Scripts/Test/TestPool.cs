using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using JIANING;
public class TestPool : MonoBehaviour
{
    public Transform a;
    public Transform b;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.B))
        {

            StartCoroutine(CreatObj());

        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            GameEntry.Pool.InitGameObjectPool();

        }
    }


    private IEnumerator CreatObj()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameEntry.Pool.GameObjectSpawn(1,a,(Transform instance)=> {
                instance.transform.localPosition += new Vector3(0,0,i*2);
                instance.gameObject.SetActive(true);
                StartCoroutine(Despwan(1,instance));
            });


            GameEntry.Pool.GameObjectSpawn(2, b, (Transform instance) => {
                instance.transform.localPosition += new Vector3(0, 5, i * 2);
                instance.gameObject.SetActive(true);
                StartCoroutine(Despwan(2, instance));
            });
        }
        
    }

    private IEnumerator Despwan(byte poolId,Transform instance)
    {
        yield return new WaitForSeconds(20f);
      GameEntry.Pool.GameObjectDespawn(poolId,instance);
      
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

