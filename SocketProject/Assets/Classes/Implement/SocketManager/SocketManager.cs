using System;


	public class SocketManager
	{
		public static NetSocket.CSocketManager currentSceneSocketManager{
			get{
				//暂时 用 登录层代替
				return LoginSocketManager.Instance;
			}
		}
	}


