using System;
using System.Collections;
using System.Collections.Generic;

namespace NetSocket
{
	/// <summary>支持多个函数注册同一条服务器消息 </summary>
	public class CSocketManager
	{
		
		public static byte headerLen = (byte)sizeof(UInt16);
		//消息插板集合
		private Dictionary<int, HashSet<CPlug>> cmdPlugDic = new Dictionary<int, HashSet<CPlug>> ();
		private HashSet<CPlug> temp_plugSet;
		private CPlug temp_plug;
		
		public CSocketManager ()
		{
		}
			
		/// <summary> 设置插线板  </summary>
		public bool setPlug (int _number, CPlug.ReceiveMsgFunc _fun)
		{
			// plug 集合
			if (!cmdPlugDic.TryGetValue (_number, out temp_plugSet)) {
				temp_plugSet = new HashSet<CPlug> ();
				cmdPlugDic [_number] = temp_plugSet;
			}
			
			foreach (CPlug plug in temp_plugSet) {
				if (plug.output == _fun) {
					return false;//已经存在
				}
			}
			
			//生成 plug
			temp_plug = new CPlug ();
			temp_plug.output = _fun;
			
			temp_plugSet.Add (temp_plug);
			
			return true;
		}
		
		public bool unPlug (int _number, CPlug.ReceiveMsgFunc _fun = null)
		{
			if (_fun == null) {
				return cmdPlugDic.Remove (_number);
			} else {
				// plug 集合
				if (!cmdPlugDic.TryGetValue (_number, out temp_plugSet)) {
					return false;
				}
				//找出需删除的 plug
				CPlug plugWillDelete = null;
				foreach (CPlug plug in temp_plugSet) {
					if (plug.output == _fun) {
						plugWillDelete = plug;
						break;
					}
				}
				
				//删除
				if (plugWillDelete == null)
					return false;
				else {
					//UnityEngine.Debug.Log ("删除");
					return temp_plugSet.Remove (plugWillDelete);
				}
			}
			
			
		}

		/// <summary> 是否创建插板  </summary>
		public bool isSetPlug (int _number)
		{
			return cmdPlugDic.ContainsKey (_number);
		}
		
		//
		public HashSet<CPlug> getPlugByCmd (int _number)
		{
			HashSet<CPlug> plugSet;
			if (!cmdPlugDic.TryGetValue (_number, out plugSet))
				return null;
			return plugSet;
		} 
	}
}