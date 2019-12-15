using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class DataTableComponent : JIANINGBaseComponent
    {



        /// <summary>
        /// 表格管理器
        /// </summary>
        public DataTableManager DataTableManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            DataTableManager = new DataTableManager();
        }

        /// <summary>
        /// 异步加载表格
        /// </summary>
        public void LoadDataTableAsync()
        {
            DataTableManager.LoadDataTableAsync();

        }



        public override void Shutdown()
        {
            //DataTableManager

        }
    }
}