using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class DataComponent : JIANINGBaseComponent
    {
        /// <summary>
        /// 临时缓存数据
        /// </summary>
        public CacheData CacheData
        {
            get;
            private set;
        }

        /// <summary>
        /// 系统相关数据
        /// </summary>
        public SysData SysDataManager
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户相关数据
        /// </summary>
        public UserData UserData
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            UserData = new UserData();
            SysDataManager = new SysData();
            CacheData = new CacheData();

        }


        public override void Shutdown()
        {
            CacheData.Dispose();
            SysDataManager.Dispose();
            UserData.Dispose();

        }
    }
}