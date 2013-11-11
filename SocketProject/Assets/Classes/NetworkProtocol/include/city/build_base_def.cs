using UnityEngine;
using System;
using System.Collections;

namespace twp
{
	namespace app
	{
		namespace build
		{

			// 建筑类型
			public enum BuildsType : byte
			{
				BUILD_TYPE_INVALID = 0,
				BUILD_TYPE_MAIN_CASTLE = 1,  // 主城
				BUILD_TYPE_HERO_HOUSE = 2,   // 英雄殿堂
				BUILD_TYPE_LEAGUE = 3,      // 联盟大厅
				BUILD_TYPE_TRADE_CENTER = 4, // 贸易中心
				BUILD_TYPE_WORKER_HOUSE = 5, // 矮人工匠屋
				BUILD_TYPE_FARMLANG = 6,	 // 农田
				BUILD_TYPE_SHOP = 7,         // 商铺
				BUILD_TYPE_BARN = 8,         // 粮仓
				BUILD_TYPE_EXCHEUQER = 9,    // 金库
				BUILD_TYPE_RESIDENT = 10,     // 居民
				BUILD_TYPE_SUMMON_TOWER = 11,// 招募塔
				BUILD_TYPE_TOWER = 12,       // 炮塔
				BUILD_TYPE_WALL = 13,        // 城墙
				BUILD_TYPE_MAX_COUNT
			};

			// 建筑类型
			public enum SubType : byte
			{
				SUB_TYPE_INVALID = 0,
				//////////////////////////////////
				// 塔
				/////////////////////////////////
				TOWER_TYPE_IRON = 1,   // 寒铁塔
				TOWER_TYPE_WOOD = 2,   // 韧木塔
				TOWER_TYPE_ICE = 3,    // 蓝冰塔
				TOWER_TYPE_LAVE = 4,   // 熔浆塔
				TOWER_TYPE_ROCK = 5,   // 坚岩塔
				TOWER_TYPE_MAX_COUNT
			};

			public enum BuildOperationEvent : byte
			{
				ADD_BUILD,      //添加建筑
				DEL_BUILD,  //删除建筑
				MOVE_BUILD,  //移动建筑
				LEVEL_BUILD,    //升级建筑
			};

		}
	}
}

