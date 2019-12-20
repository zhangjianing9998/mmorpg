
//===================================================
//作    者：家宁  http://www.zhangjianing.net
//创建时间：2019-12-15 19:49:57
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using JIANING;

/// <summary>
/// Sys_Effect数据管理
/// </summary>
public partial class Sys_EffectDBModel : DataTableDBModelBase<Sys_EffectDBModel, Sys_EffectEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Sys_Effect"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            Sys_EffectEntity entity = new Sys_EffectEntity();
            entity.Id = ms.ReadInt();
            entity.Desc = ms.ReadUTF8String();
            entity.PrefabId = ms.ReadInt();
            entity.KeepTime = ms.ReadFloat();
            entity.SoundId = ms.ReadInt();
            entity.Type = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}