/*
* Copyright (c) 2013,广州要玩娱乐网络技术有限公司
* All rights reserved.
*
* 文件名称: item_shop_def.h
* 文件标识:

* 摘要: 商城数据define
*
* 当前版本: 1.1


* 作者: LinJun
* 完成日期: 2013年8月27日
*
* 取代版本:1.0


* 原作者 : LiuJun
* 完成日期: 2013年8月27日
*/

namespace twp {
	namespace app {
		namespace item {
			//
			// 常量定义
			//
			public enum ItemShopDefine
			{
				SHOPTYPE_ID_COMMERCIALCITY = 3000, // 商城id
				MAX_PAY_TYPE_COUNT = 3, // 最多同时支持的支付方式
			};

			// 物品买入支付货币类型定义
			public enum PayType : byte
			{
				PAY_TYPE_NONE       = 0, // 无效
				PAY_TYPE_COPPER     = 1, // 铜矿
				PAY_TYPE_SILVER     = 2, // 银矿
				PAY_TYPE_FOOD       = 3, // 粮食
				PAY_TYPE_POPULATION = 4, // 人口
				PAY_TYPE_GOLD       = 5, // 点卷
			};

			// 商店特殊标签类型
			public enum ShopFlag : ushort
			{	
				SHOP_FLAG_NONE = 0,
				SHOP_FLAG_HOT = 1 << 1,	      // 热门	
				SHOP_FLAG_SPACIAL = 1 << 2,	  // 特价	
				SHOP_FTAG_LIMITATION = 1 << 3,// 限量	
			};

			// 商店类型
			public enum ShopType : byte
			{
				SHOP_TYPE_COMMON = 0, // 公共商店
				SHOP_TYPE_NPC    = 1, // NPC商店
				SHOP_TYPE_MALL   = 2, // 商城
			};

			// 商城操作事件
			public enum ShopEventType : byte
			{
				SHOPEVENT_NONE     = 0,     // 成功
				SHOPEVENT_ERROR    = 1,		// 错误
				SHOPEVENT_BUYITEM  = 2,     // 购买操作
				SHOPEVENT_BUY_BUILD_ITEM  = 3,     // 购买建筑物品
				SHOPEVENT_BUY_RES_ITEM  = 4,     // 购买资源物品
				SHOPEVENT_SELLITEM = 8,		// 出售操作
			};

			public enum ShopError
			{
				SHOPERROR_NONE,
				SHOPERROR_UNKOWN,
				SHOPERROR_BAD_ARGUMENT,		//参数出错，操作失败
				SHOPERROR_NOT_ENOUGH_MONEY,	//金额不足
				SHOPERROR_TARGET_UNFRIEND,	//对方不是您的好友
				SHOPERROR_ITEM_SOLD_DOWN,	//商品已经下架了
				SHOPERROR_LIMIT_COUNT,		//超出限量的数量了
				SHOPERROR_LIMIT_CANT_DEMAND,//限量商品无法索取
				SHOPERROR_SOLD_UNDO,		//无法出售该物品

				SHOPERROR_NOT_ENOUGH_GOLD,	//点卷不足,无法刷新神秘商店
				
			};

			// 商城从dp加载
			public class ShopInfoDB
			{
				// 商店唯一id
				public ushort shop_idx;
				// 商店类型
				public ShopType shop_type;
			};

			// 支付方式
			public class PayTypeData
			{
				public PayType type;
				public uint uValue;
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					byte v;
					bin.Get_(out v);
					type = (PayType)v;
					bin.Get_(out uValue);
				}
			};

			// 商店物品
			public class ShopItem
			{
				// 物品类型
				public uint item_type;
				// 标志(热门、限量、特价)
				public ShopFlag flags;
				// 拥有的商品不同价格不同
				public ushort price_num;
				// 支付方式总数
				public byte pay_count;
				public PayTypeData []pay_data = new PayTypeData[(int)ItemShopDefine.MAX_PAY_TYPE_COUNT];
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_(out item_type);
					ushort v;
					bin.Get_(out v);
					flags = (ShopFlag)v;
					bin.Get_(out price_num);
					bin.Get_(out pay_count);
					
					for( int i = 0; i < (int)ItemShopDefine.MAX_PAY_TYPE_COUNT; ++i )
					{
						if( null == pay_data[i] ) pay_data[i] = new twp.app.item.PayTypeData();
						
						pay_data[i].FromBin(bin);
					}
				}
			};

			//
			// 商店所属物品信息
			//
			struct ShopItemInfoDB
			{
				public ushort shop_idx; // 所属商店id
				public ShopItem item_info; // 商店物品
			};

		}
	}
}