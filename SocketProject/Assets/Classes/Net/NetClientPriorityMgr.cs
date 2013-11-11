
#if false
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Net
{
	public class NetClientPriorityMgr : Singleton<NetClientPriorityMgr> {
		
		//低优先级的集合
		HashSet<UInt16> _lowPriorityHeaderSet = new HashSet<UInt16>();
		
		public NetClientPriorityMgr()
		{
			//_lowPriorityHeaderSet.Add(低优先级协议号);
		}
		
		/// <summary>
		/// Reorders the reply list.
		/// </summary>
		/// 
		/// <returns>
		/// true : 改变了 replyList 的排列顺序
		/// </returns>
		/// 
		/// <param name='replyList'>
		/// NetSocket.NetClient 接收到的协议包队列
		/// </param>
		public bool reorderReplyList(LinkedList<NetSocket.NetClient.Reply> replyList)
		{
			//只有一个协议包，没必要做处理
			if(replyList.Count <= 1)
			{
				return false;
			}
			
			//第一个reply的协议号
			NetSocket.NetClient.Reply reply = replyList.First.Value;
			UInt16 firstNodeheader = NetSocket.NetClient.headerByReply(ref reply);
			
			if(_lowPriorityHeaderSet.Contains(firstNodeheader))
			{
				//第一个reply 放到最後
				LinkedListNode<NetSocket.NetClient.Reply> firstNode = replyList.First;
				replyList.RemoveFirst();
				replyList.AddLast(firstNode);
				return true;
			}
			else
			{
				return false;
			}
		}
		 
	}
 
}
#endif
