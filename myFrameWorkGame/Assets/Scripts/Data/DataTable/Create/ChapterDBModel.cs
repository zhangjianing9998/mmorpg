//===================================================
//作    者：家宁  http://www.zhangjianing.net
//创建时间：2019-12-15 19:49:56
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using JIANING;

/// <summary>
/// Chapter数据管理
/// </summary>
public partial class ChapterDBModel : DataTableDBModelBase<ChapterDBModel, ChapterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Chapter"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            ChapterEntity entity = new ChapterEntity();
            entity.Id = ms.ReadInt();
            entity.ChapterName = ms.ReadUTF8String();
            entity.GameLevelCount = ms.ReadInt();
            entity.BG_Pic = ms.ReadUTF8String();
            entity.Uvx = ms.ReadFloat();
            entity.Uvy = ms.ReadFloat();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}