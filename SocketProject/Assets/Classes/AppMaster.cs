using UnityEngine;   
//using UnityEditor;
using System.Collections;   
using System.Collections.Generic;   
using System.Xml;   
using System.IO;   
using System.Text;
using System;

public struct AppRunConfig{
	// 登陆服务器地址
   public string loginServerHost;
    // push message 服务器地址
   public string pushServerHost;
	// 更新下载服务器地址
   public string updateServerHost;
}

//平台类型
public enum TargetPlatform
{
    kTargetWindows,
    kTargetLinux,
    kTargetMacOS,
    kTargetAndroid,
    kTargetIphone,
    kTargetIpad,
    kTargetBlackBerry,
    kTargetNaCl,
}


public class AppMaster  {
	
	string _deviceHash = null;
	//singleton
	static protected AppMaster _instance;
	static public AppMaster GetInstance()
	{
			if (_instance == null)
				_instance = new AppMaster();
			return _instance;
	}
	
	//runConfig
	public AppRunConfig runConfig = new AppRunConfig();
	public string loginServerHost
	{
		get
		{
			return runConfig.loginServerHost;
		}	
	}
	
	//Volume
	//音效音量
	public float TDXVolume = 1.0F;
	
	//write buffer to runConfig
	bool ParseApprunconfig()
	{
		TextAsset textAsset = Resources.Load("Config/apprunconfig", typeof(TextAsset)) as TextAsset;
		if(textAsset != null)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(new MemoryStream(textAsset.bytes));
			XmlNodeList nodeList = xmlDoc.SelectSingleNode("yaowan").ChildNodes;
			if (nodeList.Count == 1)
			{
				XmlElement xe = (XmlElement)nodeList.Item(0); 
				runConfig.loginServerHost = xe.GetAttribute("loginServer");
				runConfig.pushServerHost = xe.GetAttribute("pushServer");
				//runConfig.updateServerHost = xe.GetAttribute("updateServer") + PlayerSettings.bundleVersion + @"/package/" ;
				runConfig.updateServerHost = xe.GetAttribute("updateServer") + "/1.0" + @"/package" ;
				return true;
			}
			else
			{
					Debug.LogError("apprunconfig.xml config is not unique");
					return false;
			}
		}
		else
		{
			Debug.LogError("Cannot find Config/apprunconfig");
			return false;
		}		
	}
	
	public AppMaster()
	{
		ParseApprunconfig();
	}

    public string hash()
	{
/*		
	    // 获取本机udid
	    string udid = UDID();
	    // 获取本机mac地址
	    string mac = MACAddress();

		string tempString =string.Format("::';;!#&09{0}dsd@234*&912))*(2{1}", udid, mac);
		_deviceHash = MD5ByString(tempString); 
		return _deviceHash;
*/
		
		string hash = PlayerPrefs.GetString("_deviceHash", "");
		if(hash != "")
		{
			return hash;
		}
		
		hash = AppMaster.TimeSince1970.ToString() + UDID() + "sei@9k5(#2e";
	    _deviceHash = MD5ByString(hash);
		
		return _deviceHash;
	}
	
	public void saveHash()
	{
		PlayerPrefs.SetString("_deviceHash", _deviceHash);
	}
	
	// Hash an input string and return the hash as     
	// a 32 character hexadecimal string.
	static public string MD5ByString(string input)
	{
		// Create a new instance of the MD5CryptoServiceProvider object. 
        System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();  
        // Convert the input string to a byte array and compute the hash. 
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));  
        // Create a new Stringbuilder to collect the bytes        
		// and create a string. 
        StringBuilder sBuilder = new StringBuilder();
		 
        // Loop through each byte of the hashed data         
		// and format each one as a hexadecimal string.         
		for (int i = 0; i < data.Length; i++)        
		{ 
            sBuilder.Append(data[i].ToString("x2"));     
		}  
        // Return the hexadecimal string.  
		
		return sBuilder.ToString(); 
	}
	
	static public string MD5ByPathName(string pathName)
	{
		  try
            {
                FileStream file = new FileStream(pathName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("MD5ByPathName() fail,error:" + ex.Message);
            }
	}
	
	static public string MACAddress()
	{
#if false
		// !UNITY_STANDALONE_WIN
		System.Net.NetworkInformation.NetworkInterfaceType Type = 0;

		string MacAddress = "AA:AA:AA:AA:AA:AA";//ModSupBase.EMPTY_STRING;

		try
		{
		    System.Net.NetworkInformation.NetworkInterface[] theNetworkInterfaces = 
		        System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
		
		    foreach (System.Net.NetworkInformation.NetworkInterface currentInterface in theNetworkInterfaces)
		    {
		        Type = currentInterface.NetworkInterfaceType;
		
		        if (Type == System.Net.NetworkInformation.NetworkInterfaceType.Ethernet 
		            || Type == System.Net.NetworkInformation.NetworkInterfaceType.GigabitEthernet 
		            || Type == System.Net.NetworkInformation.NetworkInterfaceType.FastEthernetFx)
		        {
		            MacAddress = currentInterface.GetPhysicalAddress().ToString();		
		            break;
		        }
		    }	
		}
		catch (System.Exception ex)
		{
		//    ModErrorHandle.Error_Handler(ex);
			Debug.LogError("obtaining MAC address failed:" + ex.Message);
		 //ModSupBase.EMPTY_STRING;
		}
		return MacAddress;
#else
		return "AA:AA:AA:AA:AA:AA";
#endif
	}
	
	static public string UDID()
	{
		return SystemInfo.deviceUniqueIdentifier;
	}
	
	static public string Version()
	{
		return "1.0";
	}
	
	static public string gameName
	{
		get {return "yaowan_tdcoc_ios";}
	}
	
	static public UInt32 TimeSince1970
	{
		get 
		{
			System.DateTime baseDate = new System.DateTime(1970, 1, 1);
			System.TimeSpan duration = System.DateTime.Now - baseDate;
			UInt32 currentTime = (UInt32)(duration.Seconds+duration.Minutes*60 + duration.Hours* 3600+duration.Days*86400);//duration.TotalSeconds
			return currentTime;
		}
	}
	
	/// <summary></summary>
	static public UInt64 D_ValueTime;//服务器时间 - 本地时间 
	static public UInt64 TimeSince1970CloseToServer
	{
		get 
		{
			UInt64 currentTime = TimeSince1970 - D_ValueTime;
			return currentTime;
		}
	}
	
	static public string ToBase64(string src)
	{
		byte[] bytes=Encoding.Default.GetBytes(src);
		return Convert.ToBase64String(bytes);	
	}
	
	static public int targetPlatform
	{
		get
		{
			TargetPlatform platform = TargetPlatform.kTargetIpad;
			switch(Application.platform)
			{
				case RuntimePlatform.IPhonePlayer:
				{
					platform = TargetPlatform.kTargetIpad;
					break;
				}
				case RuntimePlatform.Android:
				{
					platform = TargetPlatform.kTargetAndroid;
					break;
				}
				case RuntimePlatform.WindowsWebPlayer:
				case RuntimePlatform.WindowsPlayer:
				{
					platform = TargetPlatform.kTargetWindows;
					break;
				}
				default:
					break;
			}
			return (int)platform;
		}	
	}
}


		/*unity3d 调用ios的api 方法
			 首先，在C#脚本中使用
		[System.Runtime.InteropServices.DllImport("__Internal")]
		extern static public int AwesomeFunction(int awesomeParameter);
		AwesomeFunction是脚本名
		然后在unity导出的xcode工程中的C/C++/objective-C 文件(我是新建一个unityplugin.mm)中的任意位置
		int AwesomeFunction(int awesomeParameter)
		{
		   // My awesome code goes here.
		 
		  return somethingAwesome;
		}
		在这个类里面你可以用ios的api
		然后需要转一下类型，我是放在unityplugin.h里的
		extern "C" void AwesomeFunction();*/
