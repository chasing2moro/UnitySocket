using System;
using UnityEngine;

namespace twp
{
	namespace app
	{
		namespace map
		{
			public class TDCONST
			{
				public const  UInt32 MAP_WEIGHT = 201;
				public const UInt32 MAP_HEIGHT = 201;
				public const int LEFT_FLAG = -2;
				public const UInt32 RIGHT_FLAG = 2;
				public const int UP_FLAG = -2;
				public const UInt32 DOWN_FLAG = 1;
				public const UInt32 LEFT_TO_RIGHT_SCREEN = 5;
				public const UInt32 UP_TO_DOWN_SCREEN = 4;
			}
			
			public enum MoveFlag
			{
				UP = 0,
				DOWN,
				RIGHT,
				LEFT
			};
			
			public enum MapType
			{
				GRAP_TYPE = 0, //抢占
				ROB_TYPE  //掠夺
			};
			
			public class MapObjectBase
			{
				//地图配置
				public UInt32 map_id;            //地图id
				public MapType map_type;           //地图类型
				public UInt32 next_map_id;          //下一个开放的地图
				public UInt32 need_defense;          //需要抵抗的波数
				public UInt32 populaion_intell;         //人口资质
				public UInt32 food_intell;          //粮食资质
				public UInt32 copper_intell;          //铜币资质
				public UInt32 plunder_times;          //掠夺次数
				public UInt32 last_open;           //持续开放的时间
				public UInt32 open_interval;          //开放时间间隔
				public byte[] map_name = new byte[twp.app.unit.TDCONST.MAX_MAP_NAME + 1];       //地图名称
				public byte[] map_describe = new byte[twp.app.unit.TDCONST.MAX_MAP_DESCRIBE + 1];     //地图的描述
				//服务器使用数据
				public UInt32 weight;            //地图的宽度
				public UInt32 height;            //地图的高度
				public UInt64 open_time;           //地图开放时间
				public byte is_open;           //地图是否为开放状态
				public UInt32 assign_id;           //已分配的城市id
					
				public bool IsOpen ()
				{
					return is_open == 1;
				}
			};
			
			public class MapObject : MapObjectBase
			{
				//public twp.app.city.CityObject[,] city = new twp.app.city.CityObject[TDCONST.MAP_WEIGHT, TDCONST.MAP_HEIGHT]; //保存的城市信息
			};
					
			public class MapInfoDB : MapObjectBase
			{
						
			};
		
			public class MapGridDB
			{
				public UInt64 city_id;           //城市id
				public UInt32 flags;            //地图id,x,y
				public UInt32 char_idx;           //角色id
				public UInt32 nation_type;          //国家id
				public UInt64 protected_end_time;         //保护结束时间
				
				//城市名称
				public byte[] _city_name = new byte[twp.app.unit.TDCONST.MAX_CITY_NAME + 1];       
				public string city_name
				{
					get{return System.Text.Encoding.Default.GetString(_city_name);}	
				}
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out city_id);
					bin.Get_ (out flags);
					bin.Get_ (out char_idx);
					bin.Get_ (out nation_type);
					bin.Get_ (out protected_end_time);
					for(uint i = 0; i < twp.app.unit.TDCONST.MAX_CITY_NAME + 1; ++i)
						bin.Get_(out _city_name[i]);
				}
			};
			
			public class MapGridSaveDB : MapGridDB
			{
				public UInt32 map_id;
				//public twp.app.city.BuildsInfo builds;
				//public twp.app.city.WallsInfo walls;
				public bool is_new;
			};
			
			public class CoordinateMap
			{
				public int[,] grid = new int[TDCONST.MAP_WEIGHT, TDCONST.MAP_WEIGHT];
				public UInt32 weight;
				public UInt32 height;

				public CoordinateMap ()
				{
					height = TDCONST.MAP_WEIGHT;
					weight = TDCONST.MAP_WEIGHT;
				}
			};

		} 
	} 
} 
