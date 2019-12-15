using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace JIANING
{
    /// <summary>
    /// Lua管理器
    /// </summary>
    public class LuaManager : ManagerBase
    {
        /// <summary>
        /// 全局的xLua引擎
        /// </summary>
        public static LuaEnv luaEnv;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //1. 实例化 xLua引擎
            luaEnv = new LuaEnv();

#if DISABLE_ASSETBUNDLE && UNITY_EDITOR
            //2.设置xLua的脚本路径
            luaEnv.DoString(string.Format("package.path = '{0}/?.bytes'", Application.dataPath + "/Download/xLuaLogic/"));
            DoString("require 'Main'");
#else
            ////1.添加自定义Loader
            //luaEnv.AddLoader(MyLoader);

            ////2.加载Bundle
            //LoadLuaAssetBundle();
#endif
        }

        /// <summary>
        /// 当前xLua脚本的资源包
        /// </summary>
        private AssetBundle m_CurrAssetBundle;

        /// <summary>
        /// 加载xlua的AssetBundle
        /// </summary>
        private void LoadLuaAssetBundle()
        {
            //GameEntry.Resource.ResourceLoaderManager.LoadAssetBundle(ConstDefine.XLuaAssetBundlePath, onComplete: (AssetBundle bundle) =>
            //{
            //    m_CurrAssetBundle = bundle;
            //    DoString("require 'Main'");
            //});
        }

        /// <summary>
        /// 自定义的Loader
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        //private byte[] MyLoader(ref string filePath)
        //{
            //string path = GameEntry.Resource.GetLastPathName(filePath);
            //TextAsset asset = m_CurrAssetBundle.LoadAsset<TextAsset>(path);
            //byte[] buffer = asset.bytes;
            //if (buffer[0] == 239 && buffer[1] == 187 && buffer[2] == 191)
            //{
            //    // 处理UTF-8 BOM头
            //    buffer[0] = buffer[1] = buffer[2] = 32;
            //}
            //return buffer;
        //}

        /// <summary>
        /// 执行Lua脚本
        /// </summary>
        /// <param name="str"></param>
        public void DoString(string str)
        {
            luaEnv.DoString(str);
        }
    }
}