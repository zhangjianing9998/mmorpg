using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace JIANING
{
    public class DataTableManager : ManagerBase
    {

        
        public int TotalTableCount;

        /// <summary>
        /// 章表
        /// </summary>
        public ChapterDBModel ChapterDBModel { get; private set; }

        /// <summary>
        /// 关卡表
        /// </summary>
        public GameLevelDBModel GameLevelDBModel { get; private set; }

        /// <summary>
        /// ui表
        /// </summary>
        public Sys_UIFormDBModel Sys_UIFormDBModel { get; private set; }

        public DataTableManager()
        {
            InitDBModel();
        }


        /// <summary>
        /// 初始化DBModel
        /// </summary>
        private void InitDBModel()
        {
            //每个表都new
            ChapterDBModel = new ChapterDBModel();
            GameLevelDBModel = new GameLevelDBModel();
            Sys_UIFormDBModel = new Sys_UIFormDBModel();
        }


        public void LoadDataTable()
        {
            ChapterDBModel.LoadData();
            GameLevelDBModel.LoadData();
            Sys_UIFormDBModel.LoadData();

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
            //每个表都clear
            ChapterDBModel.Clear();
            GameLevelDBModel.Clear();
            Sys_UIFormDBModel.Clear();
        }

    }
}
