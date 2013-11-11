/*
* Copyright (c) 2013,广州要玩娱乐网络技术有限公司
* All rights reserved.
*
* 文件名称: base_item_def.h
* 文件标识:

* 摘要: 物品基础数据define
*
* 当前版本: 1.1


* 作者: LinJun
* 完成日期: 2013年8月27日
*
* 取代版本:1.0


* 原作者 : LiuJun
* 完成日期: 2013年8月27日
*/
using System;

namespace twp {
	namespace app {
		namespace item {

			// 一些常数定义
			public enum BaseItemDefine
			{
				// 最大同时支持的出售类型
				MAX_SELL_TYPE_COUNT = 3,
				// 每品种固定的物品需求数量(需求的条件数量)
				MAX_BASEITEM_TYPE_REQUIREMENT_AMOUNT = 3,
			};

			//
			// 物品主类型
			//
			public enum ItemMainType : byte
			{
				ITEM_MAIN_TYPE_INVALID = 0,
				// 资源类
				ITEM_MAIN_TYPE_RESOURCE,
				// 建筑类
				ITEM_MAIN_TYPE_BUILD,
				// 辅助类
				ITEM_MAIN_TYPE_EXPENDABLE,
			};
		
			
			// 物品子类类型
			public enum ItemSubType : byte
			{
				item_sub_type_invalid = 0,

				// 资源类
				item_sub_type_copper = 1, // 铜币
				item_sub_type_silver,     // 银币
				item_sub_type_food,       // 粮食
				item_sub_type_population, // 人口

				// 建筑类
				item_sub_type_alliance , // 工匠屋
				item_sub_type_barn,         // 粮仓
				item_sub_type_craftsman,    // 工匠屋
				item_sub_type_trap,         // 塔
				item_sub_type_excheuqer,    // 金库
				item_sub_type_farmlage,     // 农田
				item_sub_type_heropalace,   // 英雄殿堂
				item_sub_type_maincity,     // 主城
				item_sub_type_resident,     // 居民
				item_sub_type_shop,         // 商铺
				item_sub_type_trade_centralit, // 贸易中心
				item_sub_type_wall,         // 城墙

				// 辅助类
			};

			//
			// 物品需求类型定义
			//
			public enum ItemRequirementType : byte
			{
				// 等级 0
				ITEM_REQ_TYPE_UNIT_LEVEL = 0,
				// 最大需求类型数量
				MAX_ITEM_REQ_TYPE,
				// 非法类型
				ITEM_REQ_TYPE_INVALID = 0xff,
			};

			public enum CompareType : byte
			{
				// > 
				COMPARE_TYPE_GREATER = 0,
				// >=
				COMPARE_TYPE_GREATER_EQUAL,
				// ==
				COMPARE_TYPE_EQUAL,
				// <
				COMPARE_TYPE_LESS,
				// <=
				COMPARE_TYPE_LESS_EQUAL,
				// !=
				COMPARE_TYPE_NOT_EQUAL,
			};
			
			//
			// 物品标记
			//
			public enum ItemFlags
			{
				// 无
				ITEM_FLAG_NONE				= 0x00000000,
				// 不能出售
				ITEM_FLAG_CANTSELL			= 1 << 1,
			};

			public class SellPrice
			{
				byte sell_price_type;
				uint sell_price_value;
			};

			//
			// 物品需求结构定义
			//
			public class ItemRequirement
			{
				// 需求项id
				public ItemRequirementType idx;
				// 比较方式
				public CompareType compare_mode;
				// 需求参数
				public ushort v;

				/*
				public bool CompareRequirement(int org_value)
				{
					switch(compare_mode)
					{
					case COMPARE_TYPE_GREATER:
						return org_value > value;
					case COMPARE_TYPE_EQUAL:
						return org_value == value;
					case COMPARE_TYPE_LESS:
						return (int32)org_value < value;
					case COMPARE_TYPE_LESS_EQUAL:
						return (int32)org_value <= value;
					case COMPARE_TYPE_NOT_EQUAL:
						return (int32)org_value != value;
					default: // default is COMPARE_TYPE_GREATER_EQUAL
						return (int32)org_value >= value;
					}
				}
				*/
			};

			//
			// 每品种物品的基本属性
			//
			public class BaseItemTypeInfo
			{
				// 类型id（每品种时的主键）
				public uint type_idx; 
				// 物品种类(主类型)
				public ItemMainType main_type;
				// 子类型
				public ItemSubType sub_type;
				// 标准卖价
				public byte base_sell_price_count;
				
				public SellPrice[] price_data = new SellPrice[(int)BaseItemDefine.MAX_SELL_TYPE_COUNT];
				//	物品标志(每品种)
				public uint item_flags_pertype;

				// 物品使用需求
				public ItemRequirement[] requirement = new ItemRequirement[(int)BaseItemDefine.MAX_BASEITEM_TYPE_REQUIREMENT_AMOUNT];
				
				/*
				inline uint32 get_id()const { return type_idx; }

				// 获取需求（按索引）
				inline const ItemRequirement* get_requirement(uint32 index)const
				{
					if(index >= MAX_BASEITEM_TYPE_REQUIREMENT_AMOUNT)
						return NULL;
					const ItemRequirement* ret = &requirement[index];
					if(ret->idx == ITEM_REQ_TYPE_INVALID)
						return NULL;

					return ret;
				}

				// 查询需求（按id）
				inline const ItemRequirement* find_requirement(uint32 idx)const
				{
					for(uint32 n=0; n<MAX_BASEITEM_TYPE_REQUIREMENT_AMOUNT; ++n)
					{
						const ItemRequirement* req = get_requirement(n);
						if(req != NULL && req->idx == idx)
							return req;
					}

					return NULL;
				}
				*/
			};
			


		}
	}
}