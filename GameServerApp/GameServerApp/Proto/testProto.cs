//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-12-22 17:44:35
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 新建协议
/// </summary>
public struct testProto : IProto
{
    public ushort ProtoCode { get { return 6666; } }
    public string ProtoEnName { get { return "test"; } }

    public int hp; //

    public byte[] ToArray(MMO_MemoryStream ms, bool isChild = false)
    {
        ms.SetLength(0);
        if (!isChild)
        {
            ms.WriteUShort(ProtoCode);
        }

        ms.WriteInt(hp);

        return ms.ToArray();
    }

    public static testProto GetProto(MMO_MemoryStream ms, byte[] buffer)
    {
        testProto proto = new testProto();
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.hp = ms.ReadInt();

        return proto;
    }
}