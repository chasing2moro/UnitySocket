using UnityEngine;
using System.Collections;

public class Handler : Singleton<Handler> {

	// Use this for initialization
	public Handler () {
	}
	
	//注册接受服务器消息的函数
	public void registerEvent () {
		//测试多个函数 注册同一个服务器消息
		LoginSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, repEncryptInfo0);
		LoginSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, repEncryptInfo1);
		LoginSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, repEncryptInfo2);
		LoginSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, repEncryptInfo);
		//删除 注册消息的 函数
		LoginSocketManager.Instance.unPlug(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, repEncryptInfo2);

		//kMSGIDX_REP_LOGIN 信息本来是 登录场景的消息，这里为了演示才在 CommonSocketManager 注册消息
		//LoginSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_LOGIN, repLogin);
		CommonSocketManager.Instance.setPlug(twp.protocol.fep2c.kMSGIDX_REP_LOGIN, repLogin);
	}
	
	//测试多个函数 注册同一个服务器消息
	void repEncryptInfo0(System.Object msg){
		Debug.Log("repEncryptInfo0 handle:" + (int)twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO);
	}
	void repEncryptInfo1(System.Object msg){
		Debug.Log("repEncryptInfo1 handle:" + (int)twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO);
	}
	void repEncryptInfo2(System.Object msg){
		Debug.Log("repEncryptInfo2 handle:" + (int)twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO);
	}
	
	//解密信息返回
	void repEncryptInfo(System.Object msg){
		Debug.Log("repEncryptInfo handle:" + (int)twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO);
		
		EncryptInfo info = msg as EncryptInfo;

		//替换本地秘钥
		yw.YwEncrypt.SetKey (info.key);
		
		Sender.sendLoginFirstAccountInfo();
	}
	
	//登录返回
	void repLogin(System.Object msg){
		
		RepLoginInfo info = msg as RepLoginInfo;
		
		Debug.Log("Login login_type:" + info.login_type);
		Debug.Log("Login login_result:" + info.login_result);
		
		twp.protocol.login.LoginResult loginResult = info.login_result;
		switch (loginResult) 
		{
		case twp.protocol.login.LoginResult.E_SUCCESS:
			{
				//请求角色列表信息
			    Sender.RequestCharacterList();
				break;
			}
			
		case twp.protocol.login.LoginResult.E_CONTINUE:
			{
			    //第二次请求登录
			    Sender.sendAccountAndPasswdToGameServer();
			    break;
			}
			
		default:
			{	
				Debug.Log("Login Result: unhandle " + loginResult);
			break;
			}	
		}

	}
	
}