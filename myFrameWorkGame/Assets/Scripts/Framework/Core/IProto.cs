using UnityEngine;
using System.Collections;

/// <summary>
/// 协议接口
/// </summary>
public interface IProto
{
    //协议编号
    ushort ProtoCode { get; }
     byte[] ToArray(bool isChild = false);
     string ProtoEnName { get; }
}