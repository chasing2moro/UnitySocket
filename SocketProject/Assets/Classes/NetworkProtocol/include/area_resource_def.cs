using System;

namespace twp
{
	namespace app
	{
		namespace resource
		{
			public enum EArea_Resource_Def
			{
				LIMIT_CHAT_TEXT_LENGTH = 512,     // 聊天内容最大值
				ENGINEER_USE_MAX = 2,             //初始化 工程师数量
			};

			public enum AreaResourceType : byte //区域资源类型
			{
				AREA_RESOURCE_TYPE_RESOURCE,     //资源
				AREA_RESOURCE_TYPE_RESOURCE_MAX, //资源上限
				AREA_RESOURCE_TYPE_ENGINEER,     //工程师
				AREA_RESOURCE_TYPE_ENGINEER_MAX, //工程师上限
			};

			//
			// 资源类型
			//
			public enum ResourceType : byte
			{
				RESOURCE_SILVER,     //银
				RESOURCE_COPPER,     //铜
				RESOURCE_FOOD,       //粮食
				RESOURCE_POPULATION,  //人口
				RESOURCE_ENGINEER,//工程师

				RESOURCE_MAX,     //资源最大值
			};

			public enum OperateType : byte
			{
				OPERATE_TYPE_ADD, //增加
				OPERATE_TYPE_SUD, //减少
			};

			//
			//工程师状态
			//
			public enum EngineerType : byte
			{
				ENGINEER_USE, //使用
				ENGINEER_IDLE,//闲置

				ENGINEER_MAX, //状态最大
			};

			public class Resource  //资源
			{
#if false
				public UInt32 silver;          // 银
				public UInt32 copper;          // 铜
				public UInt32 copper_max;  // 铜上限

				public UInt32 food;            // 粮食
				public UInt32 food_max;  // 粮食上限
    
				public UInt32 population;  // 人口
				public UInt32 population_max; // 最大人口
#else
				public byte worker_house;//矮人工匠屋数量			
#endif
				
				public void FromBin (NetSocket.ByteArray bin)
				{		
#if false
					bin.Get_ (out silver);          // 银
					bin.Get_ (out copper);          // 铜
					bin.Get_ (out copper_max);  // 铜上限
					bin.Get_ (out food);            // 粮食
					bin.Get_ (out food_max);  // 粮食上限
					bin.Get_ (out population);  // 人口
					bin.Get_ (out population_max); // 最大人口
#else
					bin.Get_ (out worker_house);
#endif
				}
			};

			public class Engineer // 工程师
			{
				public byte engineer_use;     // 使用中
				public byte engineer_idle;    // 闲置
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out engineer_use);     // 使用中
					bin.Get_ (out engineer_idle);    // 闲置
				}
			};



		} // namespace resource
	} // namespace app
} // namespace twp

