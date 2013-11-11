using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	//IP
	static public string IP = "192.168.1.240";
	//Port
	static public int port = 6666;
	//账号
	static public string account = "test10";
	//密码
	static public string password = "1";
	
	void Start () {
		//（1）注册解析类
		ClassRegister.register();
		
		//（2）注册接受服务器消息的函数
		Handler.Instance.registerEvent();

		//生成socket
		NetSocket.CBoard.Instance.socket = new NetSocket.CSocket();
	}
	

	void Update () {	
		NetSocket.CBoard.Instance.Update();
	}
	
	void OnGUI () {
		if (GUILayout.Button("connect"))
		{
			Debug.Log("Request Socket Connect");
			//（3）请求socket 链接
			NetSocket.CBoard.Instance.connectToServer(IP, port);
		}	
		if(GUILayout.Button("login"))
		{
			Debug.Log("Request Login");
			//开始登录
			twp.protocol.c2fep.ReqEncryptInfo reqEncryptInfo = new twp.protocol.c2fep.ReqEncryptInfo();
			NetSocket.CBoard.Instance.send(reqEncryptInfo.ToBin());
		}
	}
}
