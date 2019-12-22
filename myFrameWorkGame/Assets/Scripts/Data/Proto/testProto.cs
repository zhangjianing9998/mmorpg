//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-12-22 17:44:35
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using JIANING;

[Serializable]
/// <summary>
/// 新建协议
/// </summary>
public struct testProto : IProto
{
    public ushort ProtoCode { get { return 6666; } }
    public string ProtoEnName { get { return "test"; } }

    public int hp; //

    public byte[] ToArray(bool isChild = false)
    {
        MMO_MemoryStream ms = null;
        
        if (!isChild)
        {
            ms = GameEntry.Socket.SocketSendMS;
            ms.SetLength(0);
            ms.WriteUShort(ProtoCode);
        }
        else
        {
            ms = GameEntry.Pool.DequeueClassObject<MMO_MemoryStream>();
            ms.SetLength(0);
        }

        ms.WriteInt(hp);

        byte[] retBuffer = ms.ToArray();
        if (isChild)
        {
            GameEntry.Pool.EnqueueClassObject(ms);
        }
        return retBuffer;
    }

    public static testProto GetProto(byte[] buffer, bool isChild = false)
    {
        testProto proto = new testProto();

        MMO_MemoryStream ms = null;
        if (!isChild)
        {
            ms = GameEntry.Socket.SocketSendMS;
        }
        else
        {
            ms = GameEntry.Pool.DequeueClassObject<MMO_MemoryStream>();
        }
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.hp = ms.ReadInt();

        if (isChild)
        {
            GameEntry.Pool.EnqueueClassObject(ms);
        }
        return proto;
    }
}