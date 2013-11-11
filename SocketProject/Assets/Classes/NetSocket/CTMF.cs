using System;
using System.Collections.Generic;

namespace NetSocket
{
	public class CTMF
	{
#if false
		 		System.Type t = System.Type.GetType(screenName);
		Iscreen screen = (Iscreen)System.Activator.CreateInstance(t);
#endif
		
		public static System.Collections.Generic.Dictionary<int, System.Type> cmdId2Type = new System.Collections.Generic.Dictionary<int, System.Type>();
		
		public static System.Type getSocketClassType(int cmdId){
			System.Type type;
			if(!cmdId2Type.TryGetValue(cmdId, out type))
				return null;
			return type;
		}
		
		public static void registerSocketClassType(int cmdId, string type){
			cmdId2Type[cmdId] = System.Type.GetType(type);
		}
	}
}

