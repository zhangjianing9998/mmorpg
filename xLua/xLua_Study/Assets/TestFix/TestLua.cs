using UnityEngine;
using System.Collections;
using XLua;

public class TestLua : MonoBehaviour
{

    public TextAsset luaScript;

    [XLua.CSharpCallLua]
    public delegate double LuaMax(double a, double b);

    private LuaEnv luaenv;

    // Use this for initialization
    void Start()
    {
        luaenv = new LuaEnv();
        //luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
        luaenv.DoString(luaScript.text);

        //LuaMax max = luaenv.Global.GetInPath<LuaMax>("math.max");
        //Debug.Log("max:" + max(32, 12));

        TestFix fix = new TestFix();

        Debug.Log("计算结果=" + fix.Add(10, 20));






    }

    void Destroy()
    {
        luaenv.Dispose(); //用lua修正方法的话，不能立刻释放 要等destroy后释放
    }

    // Update is called once per frame
    void Update()
    {

    }
}
