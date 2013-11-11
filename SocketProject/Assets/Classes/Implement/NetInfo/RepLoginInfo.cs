using System;

public class RepLoginInfo
{
	// 登录结果
	public twp.protocol.login.LoginResult login_result;
				
	// 登录类型				
	public twp.protocol.login.LoginType login_type;
				
	// 登录数据长度
	public UInt16 data_len = 0;
	
	// 登录数据(帐号 密码等 变长数据)
	public string data; // = new byte[(int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH];
		
	public RepLoginInfo (NetSocket.ByteArray bin)
	{
		bin.Move(NetSocket.CSocketManager.headerLen);
			
		int login_result_;
		bin.Get_ (out login_result_);
		login_result = (twp.protocol.login.LoginResult)login_result_;
					
		int login_type_;
		bin.Get_ (out login_type_);
		login_type = (twp.protocol.login.LoginType)login_type_;
					
		bin.Get_ (out data_len);
		
		data = bin.GetStringData((int)data_len);
	}
}


