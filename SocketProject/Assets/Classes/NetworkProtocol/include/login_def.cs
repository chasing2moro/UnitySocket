using System;

namespace twp
{
	namespace protocol
	{
		namespace login {
			
            // 登录结果
            public enum LoginResult
            {
                E_FAILED_UNKNOWNERROR			= 0,
                E_SUCCESS						= 1,	// success
                E_SUCCESS_QUEUEDLOGIN,					// 登录成功,进入队列.
                E_SUCCESS_QUEUECOMPLETE,				// 队列完成,进入游戏.
                E_FAILED_ALREADYLOGIN,					// 同帐号已经登录
                E_FAILED_SERVERINTERNALERROR,			//	e.g. no available ls/dp server
                E_FAILED_INVALIDACCOUNTORPASSWORD,
                E_FAILED_INVALIDACCOUNT,
                E_FAILED_INVALIDPASSWORD,
                E_FAILED_ACCOUNTDISABLED,				// 帐号已经封停
                E_FAILED_INVALIDVERSION,				// 错误的版本号
                E_CONTINUE,								// 继续下一步登录过程
                E_FAILED_ONLINELIMIT,					// 服务器人数已满
                E_FAILED_LOGINQUEUE_IS_FULL,			// 连接已满
				E_FAILED_LOGIN_TIMEOUT,					// 登录超时
				E_FAILED_NOT_ALLOWED,					// 不允许操作
				E_FAILED_LIMIT_INVALID_PASSWORD,		// 密码错误次数太多
				E_FAILED_NOT_LOGIN_TENCENT,				// 未登录到腾讯平台
				E_FAILED_INVALID_CLIENT_VERSION,		// 错误的客户端版本号
            }

           public enum EDef
           {
              LIMIT_KICKOUT_ACCOUNT_DESC_LENGHT = 64, // 
           }
			
			
            //
            // client 登出原因
            //
            public enum LogoutClientReason
            {
                LOC_REASON_UNKOWN = 0,						// 未知原因
                LOC_REASON_ACCOUNT_REPEATLOGIN_SERVERSET,	// 当前服务器组帐号重登陆
                LOC_REASON_ACCOUNT_REPEATLOGIN_SERVERREALM,	// 当前服务器区帐号重登陆
                LOC_REASON_OTHER_SERVER,					// 其他服务器请求
                LOC_REASON_CLIENT_DISCONNECTED,				// 客户端连接断开
                LOC_REASON_CLIENT_REQUEST_EXITGAME,			// 客户端请求退出游戏
                LOC_REASON_SERVERINTERNALERROR,				// 服务器内部错误
                LOC_REASON_SS_DISCONNECTED,					// ss断开连接
                LOC_REASON_LAWLESS,							// 非法客户端
                LOC_REASON_BACKSTAGEMGR,					// 后台管理GM踢人
                LOC_REASON_ONLINEGM,						// 在线GM 踢人
                LOC_REASON_SPEED_HACK,						// 加速
                LOC_REASON_SS_CRASH,						// ss crash
                LOC_REASON_LS_CRASH,						// ls crash
                LOC_REASON_NEED_RENAME,						// 需要改名
                LOC_REASON_ENTERSCENE_FAILED,               // 进入场景失败
                LOC_REASON_LEAVESCENE_FAILED,               // 离开场景失败
            }

            //
            // 登出信息
            // 
            public class LogoutClientInfo
            {
                // 原因
               	public LogoutClientReason logout_reason;
                // 原因不同 参数意义不同
                public uint param;

                public LogoutClientInfo()
                {
                    logout_reason = LogoutClientReason.LOC_REASON_UNKOWN;
                    param = 0;
                }

                LogoutClientInfo(LogoutClientReason lr, uint param_ = 0)
                {
                    logout_reason = lr;
                    param = param_;
                }
            }

			//
			// 帐号标记
			//
			public enum AccountFlags
			{
				ACCOUNT_FLAG_NONE = 0x00000000,
				// 正式注册验证帐号
				ACCOUNT_FLAG_VERIFIED = 0x00000001,
				// 本机绑定帐号（绑定移动设备）
				ACCOUNT_FLAG_BOUND_THIS_MOBILE_DEVICE = 0x00000002,
			}

            // 登录类型
            public enum LoginType
            {
                LT_DEFAULT,				// 默认类型,由LS根据配置决定
                LT_NAIVE,				// 原始明文密码
                LT_PASSWORD_MD5,		// md5加密密码
                LT_AUTHENTICATION,      // 后台验证
				LT_TENCENT,				// 腾讯平台
				LT_MOBILE,				// APPLE 等移动平台
            }

            // 压入数据类型
            public enum InputDataType
            {
                IDT_NONE,					// 
                IDT_PASSWORD,				// 用户名
                IDT_PASSWORD_MD5,			// possword md5
            }
			
#if false
            // 压入的数据
            union InputData
            {
                struct Password
                {
                    char password[twp::app::LIMIT_PASSWORD_STR_LENGTH+1];
                }password;

               struct PasswordMd5
               {
                    char password_md5[twp::app::LIMIT_PASSWORD_MD5_STR_LENGTH+1];
               }password_md5;
			   
			   struct AuthentiCation
			   {
				   char authenti_cation[twp::app::LIMIT_AUTHENTICATION_STRING_LENGTH+1];
			   }authenti_cation;
            }
#endif
            //
            // 帐号数据
            //
            public struct AccountData
            {
                uint account_idx;
                byte[] password_md5;//[LIMIT_PASSWORD_MD5_STR_LENGTH+1];
                /*uint8*/byte account_type;
            }

			//
			// 防沉迷
			//
			public enum AdultType
			{
				ADULT_TYPE_ADULT = 0,	// 成年人
				ADULT_TYPE_PRE_ADULT,	// 审核中的成年人
				ADULT_TYPE_IMMATURE,	// 未成年
			}
			
			public enum AntiTime
			{
				ANTI_TIME_MAX_ONLINE_TIME = 3 * 60 * 60,
				ANTI_TIME_TIME_INTERVAL = 5 * 60 * 60,
			}
		}
	}
}
