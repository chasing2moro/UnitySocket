#define USE_MULTIPACK
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using yw;

namespace NetSocket
{
	public class CSocket
	{
		public string mServerIP;
		public int mServerPort;
		public int mConnectMaxWaitTime = 10000;
		public int mConnectWaitTime = 0;
		
		MutiPacketHelp mMutiPacketHelp = null;
		
		
		public interface IListener
		{
			void OnConnect();
			void OnDisconnect(int e);
			void OnDataReply(byte[] data);
		}
		
		//public IListener mListener;
		
		public enum ENetState
		{
			ES_UnInit = 0,	// 未连接
			ES_Connecting,	// 正在连接
			ES_Connected,	// 已连接
			ES_Disconnect,	// 断开连接
		}
		protected ENetState mNetState;
		
		public ENetState NetState
		{
			get { return mNetState; }
		}
		
		// Packet header define.
		public struct NetCoreHeader
		{
			public ushort length;	// header + content
			public ushort encrypt;	// 24 single, 1 multi
		};
		
		// Request list.
		protected struct Request
		{
			public List<byte> Data;
			public int SendedByte;
		}
		protected object mLock;	// For multi-thread.
		protected LinkedList<Request> mRequestList;
		
		public enum EReplyType
		{
			RT_Connected = 0,
			RT_Disconnect,
			RT_DataReply,
		}
		
		public struct Reply
		{
			public EReplyType Type;
			public List<byte> Data;
			public int e;
		}
		protected LinkedList<Reply> mReplyList;
		//start  Added by bobo 2013-8-16  System.BitConverter.ToUInt16(replyNode.Value.Data.ToArray(), 0); 
		static public UInt16 headerByReply(ref Reply reply)
		{
			return BitConverter.ToUInt16(reply.Data.ToArray(), 0);
		}
		//end Added by bobo 2013-8-16
		
		protected List<byte> mRecvBuffer;
		protected Socket mSocket = null;
		protected Thread mThread;
		protected bool mFirstConnect;
		protected int mPackNum;
		
		private IAsyncResult m_ar_Connect = null;
        private IAsyncResult m_ar_Recv = null;
        private IAsyncResult m_ar_Send = null;
	
		public bool connected = false;
		/*
		static protected CSocket msInstance;
		static public CSocket GetInstance()
		{
			if (NetClient.msInstance == null)
				NetClient.msInstance = new NetClient();
			return NetClient.msInstance;
		}
		*/
		
		public CSocket()
		{
			mPackNum = 0;
			mLock = new object();
			
			mRequestList = new LinkedList<Request>();
			mRecvBuffer = new List<byte>();
			mReplyList = new LinkedList<Reply>();
			
			mNetState = ENetState.ES_UnInit;
			
			mMutiPacketHelp = new MutiPacketHelp(this);
// add at 2013-9-29			
			mSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		}
		
		public void Start(string ip, int port)
		{
			lock(mLock)
			{
				if (mNetState == ENetState.ES_Connecting || mNetState == ENetState.ES_Connecting)
					return;
				
				YwEncrypt.ResetKey();
				mRequestList.Clear();
				mRecvBuffer.Clear();
				mPackNum = 0;
				
				mNetState = ENetState.ES_UnInit;
				
// comment at 2013-9-29
//				if (mSocket == null)
//					mSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			
				//mThread = new Thread(new ThreadStart(_thread_run));
				//mThread.IsBackground = true;
				//mThread.Start();
				
				if (ip == mServerIP && port == mServerPort && mSocket.Connected)
				{
					//mListener.OnConnect();
					this.OnConnect();
					return;
				}
			
				mServerIP = ip;
				mServerPort = port;
				Connect();
				
				mNetState = ENetState.ES_Connecting;
				mConnectWaitTime = 0;
			}
		}
		
		void Clear()
		{
			if (m_ar_Connect != null)
				m_ar_Connect.AsyncWaitHandle.Close();
			if (m_ar_Recv != null)
				m_ar_Recv.AsyncWaitHandle.Close();
			if (m_ar_Send != null)
				m_ar_Send.AsyncWaitHandle.Close();
			
			YwEncrypt.ResetKey();
			mRequestList.Clear();
			mRecvBuffer.Clear();
			mPackNum = 0;
		}
		
		public void Stop()
		{
			lock(mLock)
			{
				Clear();
				
				if (mSocket != null)
				{
					if (mSocket.Connected)
					{
						mSocket.Shutdown(SocketShutdown.Both);
						mSocket.Close();
					}
					
					mSocket = null;
				}
				mNetState = ENetState.ES_UnInit;
			}
		}
		
		public virtual byte[] WrapPacket(byte[] data)
		{		
			byte[] encode_buffer = new byte[YwEncrypt.MSG_MAX_STC_PACK_LEN];
			int encode_buffer_len = YwEncrypt.Encode(mPackNum, encode_buffer, data);
			
			ByteArray final_buffer = new ByteArray();
			
			// Fill head.
			final_buffer.Put((ushort)encode_buffer_len);
			final_buffer.Put((ushort)24);
			
			// Fill real content.
			final_buffer.Put(encode_buffer, encode_buffer_len);
			
			mPackNum++;
			
			return final_buffer.GetData();
		}
		
		public class MutiPacketHelp
		{
			CSocket mParent;
			ByteArray mCacheData = new ByteArray();
			byte mCount = 0;
			
			public MutiPacketHelp(CSocket p)
			{
				mParent = p;
			}
			
			public void Push(byte[] data)
			{
				byte[] encode_buffer = new byte[YwEncrypt.MSG_MAX_STC_PACK_LEN];
				int encode_buffer_len = YwEncrypt.Encode(mParent.mPackNum, encode_buffer, data);
				
				ByteArray final_buffer = new ByteArray();
				
				// Fill content len.
				final_buffer.Put((ushort)encode_buffer_len);
				
				// Fill real content.
				final_buffer.Put(encode_buffer, encode_buffer_len);
				
				mParent.mPackNum++;

				mCacheData.Put(final_buffer.GetData());
				mCount++;
			}
			
			public void Send()
			{
				if (mCacheData.GetData().Length <= 0)
					return;
				
				// |--总长(2)--|--label(2)--|--num(1)--|--single len--|--single packet...--|
				ByteArray send = new ByteArray();
				byte[] data = mCacheData.GetData();
				send.Put((ushort)(data.Length+1));	// 总长
				send.Put((ushort)1);				// 多包
				send.Put((byte)mCount);				// 包的个数
				send.Put(data); 
				
				//mParent.mPackNum++;
			
				//mParent.PushRequest(send.GetData());
				mParent.Send(send.GetData());
				
				// 发送完后，清除数据
				mCacheData.Clear();
				mCount = 0;
			}
		}
		
		public void PushRequest(byte[] wrapData)
		{
			lock(mLock)
			{
				if (mNetState != ENetState.ES_Connected)
					return;

				if (wrapData != null)
				{
					CSocket.Request req = new CSocket.Request();
					req.Data = new List<byte>();
					req.SendedByte = 0;
					for (int i=0; i<wrapData.Length; i++)
					{
						req.Data.Add(wrapData[i]);
					}
					
					mRequestList.AddLast(req);
					
					if (mRequestList.Count == 1)
						this.Send(wrapData);
				}
			}
		}
		
		public void SendData(byte[] oriData)
		{
			if (mSocket == null)
				return;

#if false
	    	ushort header = BitConverter.ToUInt16(oriData, 0);
			if ( !ReqCDTimeModel.Instance.IsOverCDTime(header) )
			{
				NotificationCenter.sharedNotificationCenter().postNotification(ReqCDTimeModel.NM_NOT_OVER_REQ_CD, (Object)header);
				return;
			}
			else
			{
				ReqCDTimeModel.Instance.UpdateReqTime(header);
			}
#endif
			
#if USE_MULTIPACK
			mMutiPacketHelp.Push(oriData);
#else
			byte[] data = WrapPacket(oriData);
			PushRequest(data);
#endif
		}
		
		protected void Connect()
		{
			mFirstConnect = true;
			IPAddress ipAddress = IPAddress.Parse(mServerIP);
	        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, mServerPort);
			
	        m_ar_Connect = mSocket.BeginConnect(ipEndpoint, new AsyncCallback(_scb_connect), mSocket);
		}
		
		protected void Recv()
		{
			try
			{
				byte[] buf = new byte[0x1000];
				this.m_ar_Recv = mSocket.BeginReceive(buf, 0, 0x1000, SocketFlags.None, new AsyncCallback(this._scb_recv), buf);
			}
			catch (SocketException e)
			{
				this.OnDisconnect(e.ErrorCode);
			}
		}
		
		protected void Send(byte[] data)
		{
			try
            {
                this.m_ar_Send = mSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(this._scb_send), mSocket);
            }
            catch (SocketException e)
            {
                this.OnDisconnect(e.ErrorCode);
            }
		}
		
		void _scb_send(IAsyncResult ar)
		{
			try
            {
                ar.AsyncWaitHandle.Close();
                ((Socket) ar.AsyncState).EndSend(ar);
				this.m_ar_Send = null;
				
				/* BeginSend一定会将所有数据发送完毕才调用回调，因此，这里不用再判断发送字节数
				LinkedListNode<Request> req = mRequestList.First;
				Request newRequest = new Request();
				newRequest.Data = req.Value.Data;
				newRequest.SendedByte = req.Value.SendedByte + len;
				req.Value = newRequest;
				*/
				
				// 发送成功了，继续发送队列中的下一条消息
				lock(this.mLock)
				{
					mRequestList.RemoveFirst();
					
					if (mRequestList.Count > 0)
					{
						Request req = mRequestList.First.Value;
						this.Send(req.Data.ToArray());
					}
				}
				
            }
            catch (SocketException e)
            {
                this.OnDisconnect(e.ErrorCode);
            }
		}
		
		void _scb_recv(IAsyncResult ar)
		{
			try
			{
				ar.AsyncWaitHandle.Close();
	            byte[] buf = (byte[]) ar.AsyncState;
	            int len = mSocket.EndReceive(ar);
				this.m_ar_Recv = null;
	            if (len > 0)
	            {
					for (int i=0; i<len; i++)
					{
						mRecvBuffer.Add(buf[i]);
					}
					
					int proc = OnSplitPacket();
					if (proc > 0)
						mRecvBuffer.RemoveRange(0, proc);
	            }

				mSocket.BeginReceive(buf, 0, 0x1000, SocketFlags.None, new AsyncCallback(this._scb_recv), buf);
			}
			catch (SocketException e)
            {
                this.OnDisconnect(e.ErrorCode);
            }
		}
		
		protected void _scb_connect(IAsyncResult ar)
		{
			try
            {
                ar.AsyncWaitHandle.Close();
                mSocket.EndConnect(ar);
				this.m_ar_Connect = null;
				
				this.OnConnect();
				
                mSocket.Blocking = false;
                if (mSocket != null)
                {
                    mSocket.ReceiveTimeout = 0xbb8;
                    mSocket.SendTimeout = 0xbb8;
                }
                this.Recv();
            }
            catch (SocketException e)
            { 
				UnityEngine.Debug.Log("SocketException:" + e.Message);
                this.OnDisconnect(e.ErrorCode);
            }
			
		}
		
		// 保留老版的单独线程轮询模式
		protected void _thread_run()
		{
			bool run = true;
			while (run)
			{
				if (mSocket.Poll(5, SelectMode.SelectWrite))
				{
					if (mFirstConnect)
					{
						mFirstConnect = false;
						OnConnect();
					}

					// Send data.
					lock(mLock)
					{
						bool hasErr = false;
						while (!hasErr && mRequestList.Count > 0)
						{
							LinkedListNode<Request> req = mRequestList.First;
							
							byte[] buffer = new byte[req.Value.Data.Count - req.Value.SendedByte];
							req.Value.Data.CopyTo(buffer, req.Value.SendedByte);
							
							try
							{
								int len = mSocket.Send(buffer);
							
								Request newRequest = new Request();
								newRequest.Data = req.Value.Data;
								newRequest.SendedByte = req.Value.SendedByte + len;
								req.Value = newRequest;
								
								if (req.Value.SendedByte >= req.Value.Data.Count)
									mRequestList.RemoveFirst();
							}
							catch (SocketException e)
							{
								if (e.SocketErrorCode != SocketError.WouldBlock)
								{
									OnDisconnect(e.ErrorCode);
									run = false;
								}
								else
								{
									hasErr = true;
								}
							}
							catch
							{
								hasErr = true;
							}
						}
					}
				}
				
				if (mSocket.Poll(5, SelectMode.SelectRead))
				{
					// Recv data.
					try
					{
						byte[] buf = new byte[0x1000];
						int len = mSocket.Receive(buf);
						//mSocket.BeginReceive(buf, 0, 0x1000, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), buf);
						
						for (int i=0; i<len; i++)
						{
							mRecvBuffer.Add(buf[i]);
						}
						
						int proc = OnSplitPacket();
						if (proc > 0)
							mRecvBuffer.RemoveRange(0, proc);
					}
					catch (SocketException e)
					{
						if (e.SocketErrorCode != SocketError.WouldBlock)
						{
							OnDisconnect(e.ErrorCode);
							run = false;
						}
					}
				}
			}
		}
		
		protected virtual void OnConnect()
		{
			lock(mLock)
			{
				mNetState = ENetState.ES_Connected;
				
				Reply reply = new Reply();
				reply.Type = EReplyType.RT_Connected;
				mReplyList.AddLast(reply);
			}
		}
		
		protected virtual void OnDisconnect(int e)
		{
			lock(mLock)
			{
				mNetState = ENetState.ES_Disconnect;
					
				Reply reply = new Reply();
				reply.Type = EReplyType.RT_Disconnect;
				reply.e = e;
				mReplyList.AddLast(reply);
			}
		}
		
		static public int _get_header_size()
		{
			return sizeof(ushort)+sizeof(ushort);
		}
		
		static public int _get_packet_total_len(NetCoreHeader h)
		{
			return sizeof(ushort)+sizeof(ushort)+h.length;
		}
		
		protected virtual int OnSplitPacket()
		{	
			byte[] recv_buf = mRecvBuffer.ToArray();
			ByteArray total_buffer = new ByteArray(recv_buf);
			
			int header_size = _get_header_size();
			int read = 0;
			int size_remain = recv_buf.Length;
			while (size_remain > header_size)	// Recv data len must contain one header at least.
			{
				byte[] cur_buf = total_buffer.GetRange(read, header_size);
				ByteArray packet = new ByteArray(cur_buf);
				
				// Get header info.
				NetCoreHeader h = new NetCoreHeader();
				packet.Get_(out h.length);
				packet.Get_(out h.encrypt);			
		
				// Get packet's len.
				int packet_len = _get_packet_total_len(h);
				
				if (packet_len <= size_remain)
				{
					byte[] body = total_buffer.GetRange(read + _get_header_size(), h.length);
	
					if (h.encrypt == 24)
						ParseSinglePack(body);
					else
						ParseMutiPack(body);
		
					size_remain -= packet_len;
					read += packet_len;
				}
				else
				{
					// Wait for more data.
					break;
				}
			}
		
			return read;
		}
		
		protected void ParseSinglePack(byte[] body)
		{	
			// Decode buffer.
			byte[] decode_buffer = new byte[YwEncrypt.MSG_MAX_STC_PACK_LEN];
			
			int len = YwEncrypt.Decode(decode_buffer, body);
			
			byte[] body_decode = new byte[len];
			Array.Copy(decode_buffer, body_decode, len);
		
			OnDataReply(body_decode);
		}
		
		protected void ParseMutiPack(byte[] body)
		{
			ByteArray bin_buf = new ByteArray(body);
			
			byte num = 0;
			bin_buf.Get_(out num);
			
			for (int i=0; i<num; i++)
			{
				ushort sinle_pack_len = 0;
				bin_buf.Get_(out sinle_pack_len);
				
				byte[] buf = new byte[sinle_pack_len];
				Array.Copy(bin_buf.GetByteArray_(sinle_pack_len), buf, sinle_pack_len);
				
				ParseSinglePack(buf);
			}
			
			/* C++ code.
			bin_buf.get(num);
		
			for (int i=0; i<num; ++i)
			{
				USHORT single_pack_len = 0;
				bin_buf.get(single_pack_len);
		
				static std::vector<UCHAR> buf;
				buf.clear();
				buf.resize(single_pack_len);
				
				bin_buf.read(&buf[0], single_pack_len);
		
				ParseSinglePack(&buf[0], single_pack_len);
			}
			*/
		}
		
		protected virtual void OnDataReply(byte[] data)
		{
			lock(mLock)
			{
				Reply reply = new Reply();
				reply.Type = EReplyType.RT_DataReply;
				reply.Data = new List<byte>();
				for (int i=0; i<data.Length; ++i)
				{
					reply.Data.Add(data[i]);
				}
				mReplyList.AddLast(reply);
			}
		}
		
		public void Update()
		{
		//	if (mListener == null)
		//		return;
			
			lock(mLock)
			{	
				if (mNetState == ENetState.ES_Connecting)
				{
					mConnectWaitTime += (int)(UnityEngine.Time.deltaTime*1000.0f);
					if (mConnectWaitTime > mConnectMaxWaitTime)
					{
						mNetState = ENetState.ES_Disconnect;
						
						if (mSocket != null)
						{
							mSocket.Close();							
							mSocket = null;
						}
						//mListener.OnDisconnect(-1);
					}
				}
				
				while(mReplyList.Count > 0)
				{
					LinkedListNode<Reply> replyNode = mReplyList.First;
					switch (replyNode.Value.Type)
					{
					case EReplyType.RT_Connected:
					{
						UnityEngine.Debug.Log("RT_Connected : Socket Connect Succeed");
						connected = true;
#if false
						twp.protocol.c2fep.ReqEncryptInfo reqEncryptInfo = new twp.protocol.c2fep.ReqEncryptInfo();
						CBoard.Instance.send(reqEncryptInfo.ToBin());
#endif
						break;
					}
					case EReplyType.RT_Disconnect:
					{
						UnityEngine.Debug.LogWarning("RT_Disconnect");	
						break;
					}	
					case EReplyType.RT_DataReply:
					{
						//UnityEngine.Debug.LogWarning("RT_DataReply");
						UnityEngine.Debug.Log("===packet header:  " + System.BitConverter.ToUInt16(replyNode.Value.Data.ToArray(), 0) + "===");
						
						//处理消息
						processStram(new NetSocket.ByteArray(replyNode.Value.Data.ToArray()));
						
						break;
					}
					
					default:break;
					}
					
					mReplyList.RemoveFirst();
				}
				
				if (mNetState == ENetState.ES_Disconnect)
					Clear();
				
#if USE_MULTIPACK
				mMutiPacketHelp.Send();
#endif
			}
		}
		
		void processStram(NetSocket.ByteArray stream){
			UInt16 temp_cmdID ;
			stream.Get_ (out temp_cmdID);
			//还原解析位置
			stream.mRP -= sizeof(UInt16);
			
			//当前场景的 SocketManager
			HashSet<CPlug> plugSet = SocketManager.currentSceneSocketManager.getPlugByCmd((int)temp_cmdID);
			if (plugSet == null || plugSet.Count == 0){//找不到注册消息的函数
				//通用 SocketManager
				plugSet = CommonSocketManager.Instance.getPlugByCmd((int)temp_cmdID);	
				if (plugSet == null || plugSet.Count == 0){//找不到注册消息的函数
					UnityEngine.Debug.LogError("cannot find the register function for ==packet header: " + temp_cmdID + "==");
					return;
				}
			}
					
			System.Type type = CTMF.getSocketClassType((int)temp_cmdID);
			foreach (CPlug plug in plugSet) {
				if(type != null){
					plug.output(System.Activator.CreateInstance(type, stream));
					stream.mRP = 0;//还原解析位置
				}
				else
					plug.output(stream);//备用
			}	
		}
	}
}

