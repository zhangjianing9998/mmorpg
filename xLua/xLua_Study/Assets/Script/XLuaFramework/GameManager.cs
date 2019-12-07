using UnityEngine;
using System.Collections;

namespace XLuaFramework
{
    /// <summary>
    /// xLua框架的主入口
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            //启动的时候 在自身挂上 LuaManager 脚本
            gameObject.AddComponent<LuaManager>();
        }

        // Use this for initialization
        void Start()
        {
            LuaManager.Instance.DoString("require'XLuaLogic/Main'");
        }
    }
}