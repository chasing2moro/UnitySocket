using System;
using System.Collections;
using System.Collections.Generic;

namespace NetSocket
{
	/// <summary>
	/// 解析从网络层接收到的数据流
	/// </summary>
		public class ByteArray
	{
		public int mRP;
		private List<byte> mData;
		
		public int Length
		{
			get { return mData.Count; }
		}
		
		public static void ArraySet<T>(T[] array, T v)
		{
			for (int i=0; i<array.Length; ++i)
			{
				array[i] = v;
			}
		}
		
		public ByteArray()
		{
			mRP = 0;
			mData = new List<byte>();
		}
		
		public ByteArray(byte[] data)
		{
			mRP = 0;
			mData = new List<byte>();
			
			Put(data);
		}
		
		public ByteArray(char[] data)
		{
			mRP = 0;
			mData = new List<byte>();
			
			for (int i=0; i<data.Length; i++)
			{
				byte b = Convert.ToByte(data[i]);
				mData.Add(b);
			}
		}
		
		public void Clear()
		{
			mRP = 0;
			mData.Clear();
		}
		
		/*
		public void PutMeta(object o)
		{
			if (o.GetType().IsArray)
			{
				Object[] o_array = (Object[])o;
				foreach(Object v in o_array)
				{
					PutMeta(v);
				}
			}
			else
			{
				if (o.GetType() == typeof(ushort))
				{
					ushort v = (ushort)o;
					Put(v);
				}
				else if (o.GetType() == typeof(short))
				{
					short v = (short)o;
					Put(v);
				}
				else if (o.GetType() == typeof(uint))
				{
					uint v = (uint)o;
					Put(v);
				}
				else if (o.GetType() == typeof(int))
				{
					int v = (int)o;
					Put(v);
				}
				else if (o.GetType() == typeof(char))
				{
					char v = (char)o;
					Put(v);
				}
				else if (o.GetType() == typeof(byte))
				{
					byte v = (byte)o;
					Put(v);
				}
			}
		}
		
		public void Recv<T>(T t)
		{
			System.Reflection.FieldInfo[] fields = t.GetType().GetFields();
			for (int i=0; i<fields.Length; ++i)
			{
				System.Reflection.FieldInfo field = fields[i];
				
				object o = field.GetValue(t);
					
				PutMeta(o);
			}
		}
		*/
		
		public byte[] GetData()
		{
			return mData.ToArray();
		}
		
		public byte[] GetRange(int start)
		{
			return mData.GetRange(start, mData.Count-start).ToArray();
		}
		
		public byte[] GetRange(int start, int count)
		{
			return mData.GetRange(start, count).ToArray();
		}
		
		public void Put(byte[] v)
		{
			for (int i=0; i<v.Length; i++)
			{
				mData.Add(v[i]);
			}
		}
		
		public void Put(char[] v)
		{
			for (int i=0; i<v.Length; i++)
			{
				byte v_ = System.Convert.ToByte(v[i]);
				mData.Add(v_);
			}
		}
		
		public void Put(byte[] v, int len)
		{
			for (int i=0; i<v.Length && i<len; i++)
			{
				mData.Add(v[i]);
			}
		}
		
		public void Put(long v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(ulong v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(int v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(uint v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(short v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(ushort v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			
			Put(_v);
		}
		
		public void Put(byte v)
		{
			mData.Add(v);
		}
		
		public void Put(sbyte v)
		{						
			mData.Add((byte)v);
		}
		
		public void Put(bool v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			Put(_v);
		}
		
		public void Put(string str)
		{
			for(int i=0; i<str.Length; ++i)
			{
				Put((byte)str[i]);
			}
		}
		
		public void Put(float v)
		{
			byte[] _v = BitConverter.GetBytes(v);
			Put(_v);
		}
		
		public void Write(byte[] data, int pos)
		{
			for (int i=0; i<data.Length; i++)
			{
				mData[pos+i] = data[i];
			}
		}
		
		public void Write(char[] data, int pos)
		{
			for (int i=0; i<data.Length; i++)
			{
				byte v_ = System.Convert.ToByte(data[i]);
				mData[pos+i] = v_;
			}
		}
		
		public void Write(int v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		public void Write(uint v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		public void Write(short v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		public void Write(ushort v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		public void Write(byte v, int pos)
		{
			mData[pos] = v;
		}
		
		public void Write(float v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		public void Write(sbyte v, int pos)
		{
			byte[] data = BitConverter.GetBytes(v);
			Write(data, pos);
		}
		
		
		public void Get(out long res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(long)).ToArray();
			
			res = BitConverter.ToInt64(data, 0);
		}
		
		public void Get(out ulong res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(ulong)).ToArray();
			
			res = BitConverter.ToUInt64(data, 0);
		}
		
		public void Get(out int res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(int)).ToArray();
			
			res = BitConverter.ToInt32(data, 0);
		}
		
		public void Get(out uint res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(uint)).ToArray();
			
			res = BitConverter.ToUInt32(data, 0);
		}
		
		public void Get(out short res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(short)).ToArray();
			
			res = BitConverter.ToInt16(data, 0);
		}
		
		public void Get(out ushort res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(ushort)).ToArray();
			try
			{
				res = BitConverter.ToUInt16(data, 0);
			}
			catch(Exception e)
			{
				res = 0;
				UnityEngine.Debug.Log("Exception:" + e);	
			}
		}
		
		public void Get(out bool res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(bool)).ToArray();
			
			res = BitConverter.ToBoolean(data, 0);
		}
		
		public void Get(out byte res, int pos)
		{
			res = mData[pos];
		}
		
		public void Get(out float res, int start)
		{
			byte[] data = mData.GetRange(start, sizeof(float)).ToArray();
			
			res = BitConverter.ToSingle(data, 0);
		}
		
		public void Get(out sbyte res, int start)
		{
			res = (sbyte)mData[start];
		}
		
		/*
		public void Get(out char res, int pos)
		{
			byte v = mData[pos];
			res = System.Convert.ToChar(v);
		}
		*/
		
		public void Get_(out long res)
		{
			Get(out res, mRP);
			mRP += sizeof(long);
		}
		
		public void Get_(out ulong res)
		{
			Get(out res, mRP);
			mRP += sizeof(ulong);
		}
		
		public void Get_(out int res)
		{
			Get(out res, mRP);
			mRP += sizeof(int);
		}
		
		public void Get_(out uint res)
		{
			Get(out res, mRP);
			mRP += sizeof(uint);
		}
		
		public void Get_(out short res)
		{
			Get(out res, mRP);
			mRP += sizeof(short);
		}
		
		public void Get_(out ushort res)
		{
			Get(out res, mRP);
			mRP += sizeof(ushort);
		}
		
		public void Get_(out bool res)
		{
			Get(out res, mRP);
			mRP += sizeof(bool);
		}
		
		public void Get_(out byte res)
		{
			Get(out res, mRP);
			mRP += sizeof(byte);
		}
		
		public void Get_(out float res)
		{
			Get(out res, mRP);
			mRP += sizeof(float);
		}
		
		public void Get_(out sbyte res)
		{
			Get(out res, mRP);
			mRP += sizeof(sbyte);
		}
	
		/*
		public void Get_(out char res)
		{
			Get(out res, mRP);
			mRP += sizeof(byte);
		}
		*/
		
		public byte[] GetByteArray_(int len)
		{
			int pos = mRP;
			mRP += len;
			return mData.GetRange(pos, len).ToArray();
		}
		
		public string GetStringData(int len)
		{
			byte[] buf = GetByteArray_(len);
			string str = TextEncode.Convert.ServerBin2UTFString(buf);
			
			int strLen = 0;
			for (int i=0; i<str.Length; ++i) {
				if (str [i] != '\0')
					strLen++;
				else
					break;
			}
			str = str.Substring(0,strLen);
			
			return str;
		}
		
		public void CopyTo(byte[] dst)
		{
			mData.CopyTo(dst);
		}
	
		// Added by bobo start 2013-8-28
		/// <summary>ByteArray后面追加字节量</summary>
		/// <param name='byteLength'>字节长度</param>
		public void appendMargin(byte byteLength)
		{
			for (byte i = 0; i < byteLength; ++i) 
			{
				this.Put((byte)0);
			}
		}
		// Added by bobo end 2013-8-28
		
		//跳过moveSizeLen个字节
		public void Move(byte moveSizeLen)
		{
			mRP += moveSizeLen;
		}
		// Added by bobo end 2013-9-11
		
		
		
	}

}

