using JIANING;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationDBModel : DataTableDBModelBase<LocalizationDBModel, DataTableEntityBase>
{

    public override string DataTableName { get { return "Localization/" + GameEntry.Localization.CurrLanguage.ToString(); } }

    /// <summary>
    /// µ±Ç°ÓïÑÔ×Öµä
    /// </summary>
    public Dictionary<string, string> LocalizationDic = new Dictionary<string, string>();

    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int colums = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            LocalizationDic[ms.ReadUTF8String()] = ms.ReadUTF8String();
        }


    }
}

