using GameServerApp.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataTableManager : Singleton<DataTableManager>, IDisposable
{
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

    }

    /// <summary>
    /// 加载表格
    /// </summary>
    public void LoadDataTable()
    {
        //每个表都 LoadData

    }
}