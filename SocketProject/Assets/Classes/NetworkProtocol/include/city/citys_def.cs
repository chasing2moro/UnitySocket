using System;

namespace twp
{
	namespace app
	{
		namespace city
		{
			public enum ECitysDefLimit
			{
				INVALID_CITY_IDX     = 0, // 无效的城市ID
				INVALID_BUILD_IDX    = 0, // 无效的建筑id
				CITYS_MAX_ROW        = 35, // 最大行
				CITYS_MAX_COL        = 35, // 最大列
				LIMIT_CITY_MAX_LEVEL   = 20,// 城市最大等级
			};

			// 城市类型
			public enum CitysType : byte
			{
				CITY_TYPE_MAIN = 0,   // 主要城市
				CITY_TYPE_NORMAL = 1, // 普通城市
				CITY_TYPE_MAX,        // 最大城市类型
			};

			// 反馈城市建筑结果
			public enum CityBuildResultType : byte
			{
				CITY_BUILD_RESULT_FAILD_NONE = 0, // 无错误
				CITY_BUILD_RESULT_FAILD_ERROR = 1, // 错误
			};

			// 城市英雄操作
			public enum CityHeroOperate : byte
			{
			    CITY_CALL_BROWSER_HERO  = 0, //主城召唤浏览英雄
			    CITY_GIVE_UP_BROWSER_HERO = 1, //放弃浏览英雄
			    CITY_CALL_HERO    = 2, //召唤英雄
			    CITY_TRAIN_HERO   = 3,  //培养英雄
			    CITY_LEVEL_UP_HERO   = 4, //升级英雄
			    CITY_LEVEL_UP_HERO_SKILL = 5, //英雄技能升级
			    CITY_FIRE_HERO    = 6, //解雇英雄
			    CITY_BUY_TRAIN_TIMES  = 7, //购买次数
			    CITY_BUILD_ADD_HERO   = 8, // 建筑入驻英雄
			    CITY_BUILD_DEL_HERO   = 9, //英雄离开建筑
			    CITY_BUILD_MODIFY_HERO  = 10, //英雄改变建筑内的排位顺序
			    CITY_REQ_HERO_LIST   = 11, //获得可选用的英雄列表
			    CITY_HERO_LEARN_SKILL  = 12, //英雄学习技能
			    CITY_HERO_CLEAN_TRAIN_CD = 13, //清除培养cd
			    CITY_HERO_CLEAN_SKILL_CD = 14, //清除技能cd
			};

		}
	} 
} 
