
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
/// Message数据管理
/// </summary>
public partial class MessageDBModel : DataTableDBModelBase<MessageDBModel, MessageEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Message"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            MessageEntity entity = new MessageEntity();
            entity.Id = ms.ReadInt();
            entity.Msg = ms.ReadUTF8String();
            entity.Module = ms.ReadUTF8String();
            entity.Description = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}