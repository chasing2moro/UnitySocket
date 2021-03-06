using System;

namespace twp
{
	namespace app
	{
		// ClietSession 的 UID 定义
		// 1. 此 UID 与 AccountIndex、CharacterIndex 无关，只用于标识唯一的一次客户端、服务器会话
		// 2. 此 UID 由fep server 生成
		// 3. 选择角色进入游戏后CharacterIndex跟UID绑定 使其一一对应
		public class ClientUID // 8
		{
			public uint fepsrv_uid;  	// fep 服务器uid
			public uint fepsession_uid;  // client 在fep上面的唯一id
		}

	}
}

