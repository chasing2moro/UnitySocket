using UnityEngine;
using System.Collections;

public class ClassRegister  {
	public static void register()
	{
		//注册解析类...
		NetSocket.CTMF.registerSocketClassType(twp.protocol.fep2c.kMSGIDX_REP_ENCRYPT_INFO, "EncryptInfo"); 
		NetSocket.CTMF.registerSocketClassType(twp.protocol.fep2c.kMSGIDX_REP_LOGIN, "RepLoginInfo"); 
		//...
		//...
		//...
	}
}
