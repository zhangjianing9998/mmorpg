using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class TestScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject obj = LuaHelper.ResourcesMgr.Load("UIPrefab/UIWindows/TaskItemView");

        obj.transform.parent = null;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one; ;

        Transform trans = null;

        trans.gameObject.SetActive(true);

        Button btn = null;

        int a = 1;
        btn.onClick.AddListener(() =>
        {
            print(a);
        });

        Text txt = null;

        txt.DOText("", 0.5f);

        UnityEngine.CanvasGroup cg;

    }

    // Update is called once per frame
    void Update()
    {

    }
}