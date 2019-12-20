
//===================================================
//作    者：家宁  http://www.zhangjianing.net
//创建时间：2019-12-15 19:49:56
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using JIANING;

/// <summary>
/// GameLevelMonster实体
/// </summary>
public partial class GameLevelMonsterEntity : DataTableEntityBase
{
    /// <summary>
    /// 游戏关卡Id
    /// </summary>
    public int GameLevelId;

    /// <summary>
    /// 难度等级
    /// </summary>
    public int Grade;

    /// <summary>
    /// 区域Id
    /// </summary>
    public int RegionId;

    /// <summary>
    /// 精灵Id
    /// </summary>
    public int SpriteId;

    /// <summary>
    /// 精灵数量
    /// </summary>
    public int SpriteCount;

    /// <summary>
    /// 掉落经验
    /// </summary>
    public int Exp;

    /// <summary>
    /// 掉落金币
    /// </summary>
    public int Gold;

    /// <summary>
    /// 掉落装备
    /// </summary>
    public string DropEquip;

    /// <summary>
    /// 掉落道具
    /// </summary>
    public string DropItem;

    /// <summary>
    /// 掉落材料
    /// </summary>
    public string DropMaterial;

}
