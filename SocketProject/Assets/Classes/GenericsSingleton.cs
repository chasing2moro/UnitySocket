using UnityEngine;
using System.Collections;

public class Singleton<T> where T : new()
{
	internal static readonly  T _instance = new T();
	

	public static T Instance 
	{
		get {return _instance;}
	}
}

