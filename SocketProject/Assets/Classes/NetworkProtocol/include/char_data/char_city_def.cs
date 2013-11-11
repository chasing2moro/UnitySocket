using UnityEngine;
using System;
using System.Collections;

/*
* Copyright (c) 2013,广州要玩娱乐网络技术有限公司
* All rights reserved.
*
* 文件名称: char_city_def.h
* 文件标识:
* 摘要: 玩家携带区域主城信息
*
* 当前版本: 1.1

* 作者: LiuJun
* 完成日期: 2013年8月17日
*
* 取代版本:1.0

* 原作者 : LiuJun
* 完成日期: 2013年8月17日
*/



namespace twp
{
	namespace app
	{
		namespace unit
		{
			public enum ECharCityLimit
			{
				MAX_CHAR_BIN_CITY_COUNT = 7, // 玩家携带的主城数据
			};
			
			public enum CityOperateType : byte//城镇操作类型
			{
    AREA_CITY_OPERATION_TYPE_INVALID = 0,  // 无效操作
    AREA_CITY_OPERATION_TYPE_CONSTRUCT = 1,// 初始化主城
    PLAYER_OPERATE_CITY_BUY          = 2,  // 购买城市
    PLAYER_OPERATE_CITY_CONSTRUCT,       // 建造
    PLAYER_OPERATE_CITY_LOOT,    // 抢占
			};
			
			public enum LootIdentity : byte//抢占身份
			{
				LOOT_PLAYER_AUTHORITY_NULL,    // 错误身份
				LOOT_PLAYER_AUTHORITY_OPERATE,    // 操作方（抢占方）
				LOOT_PLAYER_AUTHORITY_DEFEND ,    // 防守方（被占方）
			};
			
			public enum LootType : byte// 是否占领
			{
				LOOT_OPERATE_CAPTURE,     // 占领
				LOOT_OPERATE_OVERRIDE,            // 放弃
			};

			// 传输使用主城数据
			public class DeliverCityData
			{
				public UInt64 city_idx; // 主城id
				public UInt32 col;
				public UInt32 row;
			}; // 4
			
			// 主城数据
			public class CityData
			{
				public UInt64 city_idx; // 主城id
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out city_idx);
				}
			}; // 4
			
			// 玩家携带的主城信息
			public class CharacterCityDatas
			{
				public CityData[] city = new CityData[(int)ECharCityLimit.MAX_CHAR_BIN_CITY_COUNT];

				public void FromBin (NetSocket.ByteArray bin)
				{
					for (uint i = 0; i < (uint)ECharCityLimit.MAX_CHAR_BIN_CITY_COUNT; ++i) {
						city [i] = new twp.app.unit.CityData ();
						city [i].FromBin (bin);
					}
				}
			}; // 4 * 7 = 28

		}
	}
}
