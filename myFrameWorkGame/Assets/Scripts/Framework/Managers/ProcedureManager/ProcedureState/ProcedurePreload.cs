using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class ProcedurePreload : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            GameEntry.Event.CommonEvent.AddEventListener(
                SysEventId.LoadDataTableComplete,
                OnLoadDataTableComplete
                );

            GameEntry.Event.CommonEvent.AddEventListener(
       SysEventId.LoadOneDataTableComplete,
       OnLoadOneDataTableComplete
       );

            GameEntry.DataTable.DataTableManager.LoadDataTableAsync();
        }

    

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLeave()
        {
            base.OnLeave();

            GameEntry.Event.CommonEvent.RemoveEventListener(
             SysEventId.LoadDataTableComplete,
             OnLoadDataTableComplete
             );

            GameEntry.Event.CommonEvent.RemoveEventListener(
     SysEventId.LoadOneDataTableComplete,
     OnLoadOneDataTableComplete
     );


        }

        public override void OnDestory()
        {
            base.OnDestory(); 
        }

        /// <summary>
        /// 加载所有表格完毕
        /// </summary>
        /// <param name="userData"></param>
        private void OnLoadDataTableComplete(object userData)
        {
            Debug.Log("加载所有表完毕");
        }

        /// <summary>
        /// 加载单一表完毕
        /// </summary>
        /// <param name="userData"></param>
        private void OnLoadOneDataTableComplete(object userData)
        {
            Debug.Log("tableName = " + userData);
        }
    }
}