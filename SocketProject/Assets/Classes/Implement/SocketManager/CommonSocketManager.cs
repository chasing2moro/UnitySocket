using System;


	public class CommonSocketManager : NetSocket.CSocketManager
	{
	 	static CommonSocketManager _instance = new CommonSocketManager();
		public static CommonSocketManager Instance 
		{
			get {return _instance;}
		}
	}


