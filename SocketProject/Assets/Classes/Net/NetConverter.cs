#if false
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace Net
{
	public class NetConverter 
	{ 
	    static public Byte[] StructToBytes(System.Object structure) 
	    {
	
	        Int32 size = Marshal.SizeOf(structure); 
	        Console.WriteLine(size); 
	        IntPtr buffer = Marshal.AllocHGlobal(size); 
	        try 
	        { 
	            Marshal.StructureToPtr(structure, buffer, false); 
	            Byte[] bytes = new Byte[size]; 
	            Marshal.Copy(buffer, bytes, 0, size); 
	            return bytes; 
	        } 
	        finally 
	        { 
	            Marshal.FreeHGlobal(buffer); 
	        } 
	    }
	
	    static public System.Object BytesToStruct(Byte[] bytes, Type strcutType) 
	    { 
	        Int32 size = Marshal.SizeOf(strcutType); 
	        IntPtr buffer = Marshal.AllocHGlobal(size); 
	        try 
	        { 
	            Marshal.Copy(bytes, 0, buffer, size); 
	            return Marshal.PtrToStructure(buffer, strcutType); 
	        } 
	        finally 
	        { 
	            Marshal.FreeHGlobal(buffer); 
	        } 
	    } 
	}
}
#endif

#if false
 * 
 * http://www.cppblog.com/erran/archive/2011/06/29/149751.html
 * 一、c#结构体
 
1、定义与C++对应的C#结构体

 
在c#中的结构体不能定义指针，不能定义字符数组，只能在里面定义字符数组的引用。 
C++的消息结构体如下： 
//消息格式 4+16+4+4= 28个字节 
struct cs_message
{ 
    u32_t        cmd_type; 
    char username[16]; 
    u32_t        dstID; 
    u32_t        srcID; 
};
 
C#定义的结构体如下:
 
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct my_message 
{ 
    public UInt32  cmd_type;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
    public string username;    

    public UInt32  dstID;

    public UInt32  srcID;

    public my_message(string s)
    {
        cmd_type = 0;
        username = s;
        dstID = 0;
        srcID = 0;
    } 
}
 
在C++的头文件定义中，使用了 #pragma pack 1 字节按1对齐，所以C#的结构体也必须要加上对应的特
性，LayoutKind.Sequential属性让结构体在导出到非托管内存时按出现的顺序依次布局,而对于C++的
char数组类型，C#中可以直接使用string来对应，当然了，也要加上封送的特性和长度限制。

2、结构体与byte[]的互相转换
 
定义一个类，里面有2个方法去实现互转：
 
public class Converter 
{ 
    public Byte[] StructToBytes(Object structure) 
    {

        Int32 size = Marshal.SizeOf(structure); 
        Console.WriteLine(size); 
        IntPtr buffer = Marshal.AllocHGlobal(size); 
        try 
        { 
            Marshal.StructureToPtr(structure, buffer, false); 
            Byte[] bytes = new Byte[size]; 
            Marshal.Copy(buffer, bytes, 0, size); 
            return bytes; 
        } 
        finally 
        { 
            Marshal.FreeHGlobal(buffer); 
        } 
    }

    public Object BytesToStruct(Byte[] bytes, Type strcutType) 
    { 
        Int32 size = Marshal.SizeOf(strcutType); 
        IntPtr buffer = Marshal.AllocHGlobal(size); 
        try 
        { 
            Marshal.Copy(bytes, 0, buffer, size); 
            return Marshal.PtrToStructure(buffer, strcutType); 
        } 
        finally 
        { 
            Marshal.FreeHGlobal(buffer); 
        } 
    } 
}
 
3、测试结果：
 
static void Main(string[] args) 
{ 
    //定义转换类的一个对象并初始化 
    Converter Convert = new Converter();

    //定义消息结构体 
    my_message m;

    //初始化消息结构体 
    m = new my_message("yanlina"); 
    m.cmd_type = 1633837924; 
    m.srcID = 1633837924; 
    m.dstID = 1633837924;

    //使用转换类的对象的StructToBytes方法把m结构体转换成Byte 
    Byte[] message = Convert.StructToBytes(m); 
    //使用转换类的对象的BytesToStruct方法把Byte转换成m结构体 
    my_message n = (my_message)Convert.BytesToStruct(message, m.GetType()); 
    //输出测试 
    Console.WriteLine(Encoding.ASCII.GetString(message)); 
    Console.WriteLine(n.username); 
}
 
结构体的size是28个字节和c++的结构体一样，同时可以将结构体和字节数组互转，方便UDP的发送和接收。

 

本文来自CSDN博客，转载请标明出处：
http://blog.csdn.net/huxiangyang4/archive/2010/08/31/5853247.aspx
#endif