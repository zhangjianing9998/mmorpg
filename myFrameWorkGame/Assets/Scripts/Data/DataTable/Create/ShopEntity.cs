
//===================================================
//作    者：家宁  http://www.zhangjianing.net
//创建时间：2019-12-15 19:49:57
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using JIANING;

/// <summary>
/// Shop实体
/// </summary>
public partial class ShopEntity : DataTableEntityBase
{
    /// <summary>
    /// 物品分类编号
    /// </summary>
    public int ShopCategoryId;

    /// <summary>
    /// 物品类型
    /// </summary>
    public int GoodsType;

    /// <summary>
    /// 物品Id
    /// </summary>
    public int GoodsId;

    /// <summary>
    /// 原价
    /// </summary>
    public int OldPrice;

    /// <summary>
    /// 售价
    /// </summary>
    public int Price;

    /// <summary>
    /// 促销状态 0=无 1=打折 2=新品 3=热卖 4=限时
    /// </summary>
    public int SellStatus;

}
