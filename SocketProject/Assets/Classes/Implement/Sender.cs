using System;
using twp.protocol;
public class Sender
{
		//c2fep
	//第1次请求登录 
	static public void sendLoginFirstAccountInfo ()
	{
		c2fep.ReqLogin reqLogin = new c2fep.ReqLogin ();
		
		// 客户端版本
		string appVersion = AppMaster.Version();
		byte[] appVersionBytes = TextEncode.Convert.UTFString2ServerBin (appVersion);
		System.Array.Copy(appVersionBytes, reqLogin.client_version, appVersionBytes.Length);

		
		//用户名 
		string account = Game.account;//"TD" +  System.Convert.ToString ( AppMaster.TimeSince1970 );
		byte[] accountBytes = TextEncode.Convert.UTFString2ServerBin (account);
		//最大长度
		int accountLength = (int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH - 1;
		if (account.Length < (int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH)
			accountLength = account.Length;

		System.Array.Copy(accountBytes, reqLogin.data, accountLength);

		reqLogin.data [accountLength] = System.Convert.ToByte ('\0');
		reqLogin.data_len = (UInt16)(accountLength + 1);

		//send
		NetSocket.CBoard.Instance.send(reqLogin.ToBin());
	}
	
		//第2次请求登录 
	static public void sendAccountAndPasswdToGameServer ()
	{
		c2fep.ReqLogin reqLogin = new c2fep.ReqLogin ();
		
		// 客户端版本
		string appVersion = AppMaster.Version ();
		byte[] appVersionBytes = TextEncode.Convert.UTFString2ServerBin (appVersion);

		System.Array.Copy(appVersionBytes, reqLogin.client_version, appVersionBytes.Length);
		
		
		//密码
		string password = AppMaster.MD5ByString (Game.password);
		byte[] passwordBytes = TextEncode.Convert.UTFString2ServerBin (password);
		int passwordLength = (int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH - 1;
		if (password.Length < (int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH)
			passwordLength = password.Length;

		System.Array.Copy(passwordBytes, reqLogin.data, passwordLength);

		reqLogin.data [passwordLength] = System.Convert.ToByte ('\0');
		reqLogin.data_len = (UInt16)(passwordLength + 1);
		
		//send 
		NetSocket.CBoard.Instance.send (reqLogin.ToBin ());
	}
	
		//c2ls
	//发送请求角色列表信息
	static public void RequestCharacterList ()
	{
		c2ls.ReqCharacterList elem = new twp.protocol.c2ls.ReqCharacterList ();
		//send 
		NetSocket.CBoard.Instance.send (elem.ToBin ());
	}
}


