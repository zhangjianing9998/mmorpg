using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TestMethod();
        }
    }

    private void TestMethod()
    {
        for (int i = 0; i < 5000; i++)
        {
            Debug.LogError(i);
        }
    }

}
