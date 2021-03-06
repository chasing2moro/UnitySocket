using System;
using System.Runtime.InteropServices;

namespace twp
{
	namespace protocol
	{
		public class fep2c
		{
			public const ushort kMSGIDX_REP_LOGIN = (ushort)protocol.ERange.MSG_BASE_FEP2C + 1;
			public const ushort kMSGIDX_REP_ENCRYPT_INFO = (ushort)protocol.ERange.MSG_BASE_FEP2C + 2;
			
			  // 
            // 返回登录结果
            //
            public class RepLogin : PacketBase
            {
                // 登录结果
				public twp.protocol.login.LoginResult login_result;
				
                // 登录类型				
                public twp.protocol.login.LoginType login_type;
				
                // 登录数据长度
                public UInt16 data_len;
                // 登录数据(帐号 密码等 变长数据)
                public byte[] data;//LIMIT_LOGIN_DATA_LENGTH

                public RepLogin()//   :PacketBase(kMSGIDX_REP_LOGIN),data_len(0)
                {
					header = kMSGIDX_REP_LOGIN;
					data_len = 0;
					data = new byte[(int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH];
				}
				
				public new void FromBin (NetSocket.ByteArray bin)
				{
					base.FromBin (bin);
					
					int login_result_;
					bin.Get_ (out login_result_);
					login_result = (twp.protocol.login.LoginResult)login_result_;
					
					int login_type_;
					bin.Get_ (out login_type_);
					login_type = (twp.protocol.login.LoginType)login_type_;
					
					bin.Get_ (out data_len);
					for(int i = 0; i < data_len; ++i)
					{
						bin.Get_ (out 	data[i]);
					}
				}
				

               // inline uint32 get_pak_length()const { return sizeof(RepLogin) - LIMIT_LOGIN_DATA_LENGTH + data_len; }
            };
			
			//
			// 返回加解密信息
			//
			public class RepEncryptInfo : PacketBase
			{
				// 参数
				public uint param;
				public byte[] key;//char key[LIMIT_KEY_LENGTH];//加解密key

				public RepEncryptInfo ()
                   // :PacketBase(kMSGIDX_REP_ENCRYPT_INFO)
				{
					header = kMSGIDX_REP_ENCRYPT_INFO;
					key = new byte[yw.YwEncrypt.LIMIT_KEY_LENGTH];
				}
				
				public new void FromBin (NetSocket.ByteArray bin)
				{
					base.FromBin (bin);
				
					bin.Get_ (out param);
					for(uint i = 0; i < yw.YwEncrypt.LIMIT_KEY_LENGTH; ++i)
					{
						bin.Get_ (out key[i]);
					}
					
				}
				

			}
			
		}
	}
}