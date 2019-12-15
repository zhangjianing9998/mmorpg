using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace JIANING
{
    public class DataTableManager : ManagerBase
    {

        
        public int TotalTableCount;


        public DataTableManager()
        {
            InitDBModel();
        }


        /// <summary>
        /// 初始化DBModel
        /// </summary>
        private void InitDBModel()
        {

        }


        public void LoadDataTable()
        {


            //所有表格load完毕
            GameEntry.Event.CommonEvent.Dispatch(SysEventId.LoadDataTableComplete);
        }



        /// <summary>
        /// 异步加载表格
        /// </summary>
        public void LoadDataTableAsync()
        {
            Task.Factory.StartNew(LoadDataTable);

        }


        public void Clear()
        {

        }

    }
}
