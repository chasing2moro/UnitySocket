using System;

namespace twp
{
	namespace app
	{
		namespace city
		{
			//
			// 常量定义
			//
			enum ECityCacheLimit
			{
				LIMIT_BUILD_MAX_COUNT = 67,   // 最多存储的建筑数量
				LIMIT_WALL_MAX_COUNT = 670,   // 最多存储的城墙数量
				LIMIT_SAVE_DP_RATE_TIME = 10, // 存储dp的时间频率(单位秒)
			};
			//
			// 城市标记
			//
			enum CitysFlag : byte
			{
				CITY_FLAG_TYPE_NONE = 0,
				CITY_FLAG_TYE_BATTLE = 1, // 正在战斗
			};
			// 操作类型
			enum OperationType : byte
			{
				OPERATION_TYPE_INVALID = 0, // 无效
				OPERATION_TYPE_MOVE    = 1, // 移动
				OPERATION_TYPE_CREATE  = 2, // 创建
				OPERATION_TYPE_DELETE  = 3, // 删除建筑
				OPERATION_TYPE_LEVELUP = 4, // 升级建筑
			};
			// 创建城市类型
			enum CreateCityEventType : byte
			{
				CREATE_CITY_EVENT_TYPE_INIT = 0,     // 初始化分配主城
				CREATE_CITY_EVENT_TYPE_BUY_CITY = 1, // 购买城市
				CREATE_CITY_EVENT_TYPE_DP = 2,       // dp数据加载
			};

			// ss2ws 更新建筑事件
			enum CachesUpdateBuildEvent : byte
			{
				CACHES_UPDATE_EVENT_CREATE_BUILD  = 0, // 创建建筑
				CACHES_UPDATE_EVENT_DELETE_BUILD  = 1, // 删除建筑
				CACHES_UPDATE_EVENT_LEVELUP_BUILD = 2, // 升级建筑
				CACHES_UPDATE_EVENT_MOVE_BUILD    = 3, // 移动建筑
			};
			// ss2ws 更新城市事件
			enum CachesUpdateCityEvent : byte
			{
				CACHE_UPDATE_EVENT_CREATE_CITY = 0, // 创建城市
			};
			// ws2ss 更新单位类型
			enum CachesWs2SsUpdateEvent : byte
			{
				CACHES_UPDATE_TYPE_INVALID = 0, // 无效
				CACHES_UPDATE_TYPE_BASE = 1,    // 基础数据
				CACHES_UPDATE_TYPE_WALL = 2,    // 城墙
				CACHES_UPDATE_TYPE_BUILD = 3,   // 建筑
			};
			//
			// 更新标示
			//
			enum UpdateFlagType : uint
			{
				UPDATE_FLAG_INVALID = 0,
				UPATE_FLAG_BASE_DATA = 1 << 1,//twp.app.typedef.BIT(1), // 更新基础数据
				UPDATE_FLAG_BUILD = 1 << 2,// twp.app.typedef.BIT(2),    // 更新建筑数据
				UPDATE_FLAG_WALL = 1 << 3//twp.app.typedef.BIT(3),     // 更新城墙数据
			};
			//
			// 查看城市缓存数据
			//
			public enum ShowCityCacheFlag : uint
			{
				SHOW_CITY_FLAG_BUILD = 0,      // 查看建筑
				SHOW_CITY_FLAG_HERO = 1 << 1,  // 查看英雄
			};
			// 数据恢复元素类型
			public enum CityResumeElemType : byte
			{
				// 棋盘
				RESUME_ELEM_TYPE_BUILD = 0,   // 建筑
				RESUME_ELEM_TYPE_WALL = 1,    // 城墙
				RESUME_ELEM_TYPE_HERO = 2,    // 英雄
			};
 

			// 建筑缓存数据
			public class BuildsInfoDP
			{
				public twp.app.build.BuildsType  main_type; // 主类型
				public twp.app.build.SubType sub_type;     // 子类型
				public UInt16 build_idx;            // 建筑索引
				public byte row;                   // 行
				public byte col;                   // 列
				public byte level;                 // 等级
				public UInt32 gather_time;   //收获时间
			    public UInt32 level_up_end_time; //升级时间
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					byte temp;
					bin.Get_ (out temp);
					main_type = (twp.app.build.BuildsType)temp;
					bin.Get_ (out temp);
					sub_type = (twp.app.build.SubType)temp;
					bin.Get_ (out build_idx);
					bin.Get_ (out row);
					bin.Get_ (out col);
					bin.Get_ (out level);
					bin.Get_ (out gather_time);
					bin.Get_ (out level_up_end_time);
				}
			};

			// 城墙缓存数据
			public class WallsInfoDP
			{
				public UInt16 build_idx; // 建筑索引
				public byte row;        // 行
				public byte col;        // 列
				public byte level;      // 等级
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out build_idx);
					bin.Get_ (out row);
					bin.Get_ (out col);
					bin.Get_ (out level);
				}
			};
#if false
   struct bin_builds
   {
    uint16 count;
    BuildsInfoDP data[LIMIT_BUILD_MAX_COUNT]; // 建筑列表
   };
   struct bin_walls
   {
    uint16 count;
    WallsInfoDP data[LIMIT_WALL_MAX_COUNT]; 
   };
   // 城市缓存数据
   struct CitysCacheInfoDP
   {
    uint64 city_idx; // 城市id
    uint32 char_idx; // 玩家id
    uint8 country;   // 国家类型
    city::CitysType city_type; // 城市类型
    uint8 area_row;            // 大地图坐标
    uint8 area_col;
    uint32 protected_time;     // 保护时间
    bin_builds builds; // 建筑列表
    bin_walls walls;   // 城墙列表
    
    inline uint64 get_city_id() { return city_idx; }
    inline uint32 get_char_id() { return char_idx; }
    inline uint8 get_country() { return country; }
    inline uint8 get_area_row() { return area_row; }
    inline uint8 get_area_col() { return area_col; }
    inline bin_builds& get_builds() { return builds;}
    inline uint16 get_builds_count() { return builds.count;}
    inline BuildsInfoDP* get_build_list() { return builds.data;}
    inline bin_walls& get_walls() { return walls;}
    inline uint16 get_walls_count() { return walls.count;}
    inline WallsInfoDP* get_wall_list() { return walls.data;}
   };
   // 建筑移动类型
   struct BuildMoveData
   {
    uint32 char_idx;
    uint8 area;
    uint64 city_idx;
    build::BuildsType build_type;
    uint16 build_idx;
    uint8 row;
    uint8 col;
   };
#endif
		}
	}
}
