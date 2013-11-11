using System;

public class EncryptInfo
{
	// 参数
	public uint param;
	public byte[] key = new byte[yw.YwEncrypt.LIMIT_KEY_LENGTH];//加解密key
	
	public EncryptInfo (NetSocket.ByteArray bin)
	{
		// 跳过两个字节
		bin.Move (NetSocket.CSocketManager.headerLen);
		
		bin.Get_ (out param);
		for (uint i = 0; i < yw.YwEncrypt.LIMIT_KEY_LENGTH; ++i) {
			bin.Get_ (out key [i]);
		}
	}
}


