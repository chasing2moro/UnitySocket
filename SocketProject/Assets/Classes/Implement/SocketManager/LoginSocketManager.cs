using System;


	public class LoginSocketManager : NetSocket.CSocketManager
	{
		static  LoginSocketManager _instance = new LoginSocketManager();
		public static LoginSocketManager Instance 
		{
			get {return _instance;}
		}
	}


