using System.Collections;

namespace twp
{
	namespace app 
	{
		namespace chat
		{
			public enum ChatDefine
			{
				LIMIT_CHAT_TEXT_LENGTH = 512,		// 聊天内容最大值
				LIMIT_SENDER_NAME_LENGTH = 32,	// 聊天发送者最大名字上限
			};
			
			public enum ChatType
			{
				// 系统频道(全世界范围)
                CT_SYSTEM = 1,
                // 私聊频道(玩家间的)
                CT_PRIVATE = 2,
                // 世界频道
                CT_WORLD = 3,
				//
				CT_CURRENT = 4,
				//
				CT_GROUP = 5,
				//联盟聊天
				CT_LEAGUE = 6,
				//
				CT_GUILD_RECRUIT = 7,
				//
				CT_GROUP_ROOM = 8,
			};
			
			public enum ChatSender
			{
				// 游戏内系统
				CS_SYSTEM = twp.app.unit.EUnitLimit.UNIT_INTERNAL_SYSTEM_ID,
				// 后台系统(gm kf等)
				CS_GM_SYSTEM = twp.app.unit.EUnitLimit.UNIT_GM_SYSTEM_ID,
				// 游戏事件（超级广播）
				CS_GAME_EVENT = twp.app.unit.EUnitLimit.UNIT_GAME_EVENT_ID,
			};
		}
	}
}
