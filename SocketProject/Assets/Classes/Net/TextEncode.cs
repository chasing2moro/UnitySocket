using System;
using System.Text;

namespace TextEncode
{
	public class Convert
	{
		 //GB2312转换成unicode编码 
        static public string GB2Unicode(string str) 
        { 
            string Hexs = ""; 
            string HH; 
            Encoding GB = Encoding.GetEncoding("GB2312"); 
            //Encoding unicode = Encoding.Unicode; 
            byte[] GBBytes = GB.GetBytes(str); 
            for (int i = 0; i < GBBytes.Length; i++) 
            { 
                HH = "%" + GBBytes[i].ToString("X"); 
                Hexs += HH; 
            } 
            return Hexs; 
        }
		//GB2312转换成unicode编码 
        static public string GB2UnicodeFromBin(byte[] GBBytes) 
        { 
            string Hexs = ""; 
            string HH; 
            //Encoding GB = Encoding.GetEncoding("GB2312"); 
            //Encoding unicode = Encoding.Unicode; 
            for (int i = 0; i < GBBytes.Length; i++) 
            { 
                HH = "%" + GBBytes[i].ToString("X"); 
                Hexs += HH; 
            } 
            return Hexs; 
        } 
        //unicode编码转换成GB2312汉字 
        static public string UtoGB(string str) 
        { 
            string[] ss = str.Split('%'); 
            byte[] bs = new Byte[ss.Length - 1]; 
            for (int i = 1; i < ss.Length; i++) 
            { 
                bs[i - 1] = System.Convert.ToByte(Convert2Hex(ss[i]));   //ss[0]为空串   
            } 
            char[] chrs = System.Text.Encoding.GetEncoding("GB2312").GetChars(bs); 
            string s = ""; 
            for (int i = 0; i < chrs.Length; i++) 
            { 
                s += chrs[i].ToString(); 
            } 
            return s; 
        } 
        static private string Convert2Hex(string pstr) 
        { 
            if (pstr.Length == 2) 
            { 
                pstr = pstr.ToUpper(); 
                string hexstr = "0123456789ABCDEF"; 
                int cint = hexstr.IndexOf(pstr.Substring(0, 1)) * 16 + hexstr.IndexOf(pstr.Substring(1, 1)); 
                return cint.ToString(); 
            } 
            else 
            { 
                return ""; 
            } 
        }
		
		
		static public string GBK2UnicodeFromBin(byte[] data)
		{
			Encoding gbkencoding = Encoding.GetEncoding(936);
			byte[] buf2 = Encoding.Convert(gbkencoding,Encoding.Unicode, data);
			string atext =Encoding.Unicode.GetString(buf2);
			return atext;
		}
		
		static public byte[] Unicode2GBKBin(string gbk)
		{
			Encoding gbkencoding = Encoding.GetEncoding(936);
			byte[] gbkBytes = gbkencoding.GetBytes(gbk);
			return gbkBytes;
		}
		
		static public string ServerBin2UTFString(byte[] data)
		{
			return Encoding.UTF8.GetString(data);
		}
		
		static public byte[] UTFString2ServerBin(string str)
		{
			return Encoding.UTF8.GetBytes(str);
		}
	}
}

