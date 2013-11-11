using System;

namespace NetSocket
{
	public class CPlug
	{
		public CPlug ()
		{
		}
		    
		public delegate void ReceiveMsgFunc (System.Object msg);
		
		//数据输出
		public ReceiveMsgFunc output;
#if false	
		//数据流
		public NetSocket.ByteArray stream; //没有用到
		
		//是否通电
		public bool power = true; //没有用到
	          
		//获取是否通电
		public bool input {//没有用到
			get {
				return (stream != null) && power;
			}
		}
#endif
	}
}

