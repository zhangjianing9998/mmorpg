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
    /// ��ʼ��DBModel
    /// </summary>
    private void InitDBModel()
    {
        //ÿ����new

    }

    /// <summary>
    /// ���ر��
    /// </summary>
    public void LoadDataTable()
    {
        //ÿ���� LoadData

    }
}