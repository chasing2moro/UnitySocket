using System;
using System.Collections.Generic;

namespace NetSocket
{
	public class CBoard : Singleton<CBoard>
	{
	   
		
		private CSocket _socket;
#if false
		private static bool _is = true;
	    private var funAr:Array = [];
	    private var plugAr:Array = [];//插板数组
	    private var plugDic:Dictionary;//插板  哈希表
#endif
	    
		//修改socket引用值
		public CSocket socket {
			get {
				return 	_socket;
			}
			set {
				if (_socket != null) {
					throw new System.Exception("已经写入过Socket属性");
				}
				_socket = value;
			}
		}
		
		//请求socket 链接
		public void connectToServer(string ip, int port){
			if(opened)
				_socket.Start(ip, port);
		}
		
		//获取SOCKET是否开启
		public bool opened {
			get {
				return _socket != null;
			}
		}
	    
		/**
	     * 发送数据  
	     * @param _number  //命令注册号号
	     * @param _stream  //发送数据
	     * 
	     */	    
		public void send (byte[] _stream = null, int cmd = 0)
		{
	    	  if(_socket != null)
		      {
		         if(_socket.connected)
	             {
					_socket.SendData(_stream);
					 return;
	             }
				 UnityEngine.Debug.Log("socket haven't connected");
				 return;
		      }
			  UnityEngine.Debug.Log("socket haven't request");
		}
		
		public void Update()
		{
			if(opened)
				_socket.Update();
		}
	   
	}
}

#if false
onUnitEnterBack（CLASSA）

TMF.regSocketClass(ServiceID.WS_ENTER_GAME_BACK,CLASSA); //首次请求进入游戏 返回信息

#endif