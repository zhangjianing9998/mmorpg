using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace JIANING
{
    public static class LuaGenConfig
    {
        //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(JIANING.GameEntry),
                //typeof(JIANING.LuaComponent),
                typeof(JIANING.EventComponent),
                typeof(JIANING.SocketEvent),
                typeof(JIANING.CommonEvent),
                //typeof(JIANING.BaseParams),
            };

        //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {

        };
    }
}