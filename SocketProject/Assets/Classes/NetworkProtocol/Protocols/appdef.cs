using System;

namespace twp
{
	namespace app
	{
		enum EDef
		{
			LIMIT_ACCOUNT_STR_LENGTH	    =	32,		// 帐号最大长度
			LIMIT_PASSWORD_STR_LENGTH	    =	24,     // 密码最大长度
			LIMIT_PASSWORD_MD5_STR_LENGTH	=	32,     // 密码MD5最大长度
			LIMIT_ACCOUNT_MD5_STR_LENGTH	=	32,     // 帐号MD5最大长度
			LIMIT_AUTHENTICATION_STRING_LENGTH = 32,	// 后台验证字符串最大长度
			LIMIT_IP_ADDRESS_LENGTH		    =	15,     // ip最大长度
			LIMIT_LOGIN_DATA_LENGTH		    =	300,    // 登录数据最大长度
			LIMIT_KEY_LENGTH				=	32,		// key的固定长度
		};
	}
}

