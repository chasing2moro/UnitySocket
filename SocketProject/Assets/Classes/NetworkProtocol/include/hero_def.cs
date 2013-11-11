using System.Collections;
using twp.app.unit;
using System;

namespace twp
{
	namespace app
	{
		namespace hero
		{
			public class Hero_Def_Const
			{
				public const UInt32	MAX_HERO_SKILL_SIZE = 20;
				public const UInt32	MAX_HERO_LEVEL = 20;
				public const byte	HERO_ATTACK_STATUS = 0;
				public const byte	HERO_REVIVE_STATUS = 1;
				public const byte	HERO_NOT_ATTACK_STATUS = 2;
				public const UInt16	HERO_TRAIN_DEFAULT_TIMES = 10;
				public const UInt32	HERO_BUY_TRAIN_TIMES_COST = 100000;
				public const UInt32	INVALID_SKILL_ID = 0;
			}
			
			public enum HERO_LIST_TYPE
			{
				HERO_LIST_IN_CITY = 0, //已经在城市中的英雄
				HERO_LIST_AVAILABLE, //可以选用的
			};
			
			public enum COST_TYPE
			{
				COST_COPPER = 19,	//铜币
				COST_FOOD	= 20,	//粮食
				COST_SILVER	= 21,	//银币
				COST_POPULATION = 22, //人口
				COST_GOLD = 23,	//钻石
			};

			public enum HERO_STAR
			{
				HERO_STAR_INVALID = 0,	
				HERO_STAR_ONE = 1,
				HERO_STAR_TWO,
				HERO_STAR_THREE,
				HERO_STAR_FOUR,
				HERO_STAR_FIVE
			};

			public enum E_HERO_TYPE
			{
				    E_HERO_NONE = -1,    //nothing
				    E_HERO_SUMMON_SUCCESS = 0,  //召唤英雄成功
				    E_HERO_TRAIN_SUCCESS = 1,  //培训英雄成功
				    E_HERO_LEVEL_SUCCESS = 2,  //升级英雄成功
				    E_HERO_FIRE_SUCCESS = 3,  //解雇英雄成功
				    E_HERO_GIVE_UP_SUCCESS = 4,  //放弃雇佣英雄成功
				    E_HERO_BUY_TRAIN_TIMES_SUCCESS, //购买培养次数成功
				    E_HERO_CREATE_BROWSER_HERO_SUCCESS, //创建雇佣浏览英雄成功
				    E_HERO_CREATE_BROWSER_HERO_ERROR, //创建雇佣浏览英雄失败
				    E_HERO_GIVE_UP_ERROR_NOT_BROWSER, //没用雇佣英雄，放弃失败
				    E_HERO_COST_RESOURCE_SUCCESS,   //消耗资源成功
				    E_HERO_LEVEL_UP_SKILL_SUCCESS, //升级技能成功
				    E_HERO_LEARN_SKILL_SUCCESS,  //英雄学习技能成功
				    E_HERO_NOT_ENOUGH_FOOD, //粮食不足
				    E_HERO_NOT_ENOUGH_COPPER, //铜币不足
				    E_HERO_NOT_ENOUGH_SILVER, //银币不足
				    E_HERO_NOT_ENOUGH_GOLD, //钻石不足
				    E_HERO_COST_TYPE_ERROR,  //消耗类型错误
				    E_HERO_SUMMON_NOT_ENOUGH_SPACE, //英雄殿堂容量不足
				    E_HERO_SUMMON_CREATE_HERO_ERROR,//创建英雄失败，服务器内部错误,缺少配置,
				    E_HERO_TRAIN_IN_CD,    //英雄培养cd，不能进行培养
				    E_HERO_TRAIN_NOT_ENOUGH_TIMES, //培养次数不足
				    E_HERO_LEVEL_UP_FAIL,   //升级英雄失败
				    E_HERO_REACH_MAX_LEVEL,   //英雄等级到达最高级，不能升级
				    E_HERO_CONFIG_ERROR,   //配置错误
				    E_COST_TYPE_ERROR,    //消耗类型错误，参数有误
				    E_HERO_NULL_ERROR,    //没找到相应的英雄,
				    E_SKILL_LEVEL_NOT_LARGER_HERO_LEVEL, //技能等级不能大于英雄等级
				    E_HERO_PLAYER_NOT_IN_CITY,  //玩家不在城市，不能对其进行操作
				    E_NO_HERO_TRAIN,    // 没有英雄进行培养
				    E_HERO_ENTER_BUILD_SUCCESS,  //英雄进驻建筑成功
				    E_HERO_ENTER_BUILD_FAIL,  //英雄进驻建筑失败
				    E_HERO_BUILD_NOT_HERO_HOUSE, //建筑物并非英雄殿堂
				    E_HERO_LEVELUP_SKILL_IN_CD,  //技能升级CD中，不能进行升级
				    E_HERO_LEVELUP_SKILL_FAIL,  //升级技能失败
				    E_HERO_LEVELUP_SKILL_NO_NEXT, //技能已达到最高级，不能升级
				    E_HERO_CLEAN_TRAIN_CD_SUCCESS, //清除培养cd成功
				    E_HERO_CLEAN_SKILL_CD_FAIL,  //清除技能cd失败
				    E_HERO_CLEAN_SKILL_CD_SUCCESS, //清除技能cd成功
				    E_HERO_LEAVE_BUILD_SUCCESS,  //离开建筑成功
    				E_HERO_LEAVE_BUILD_FAIL,  //离开建筑失败
    				E_HERO_DISPACTH_HERO_SUCCESS, //派遣英雄成功
    				E_HERO_DISPACTH_HERO_FAIL,  //派遣英雄失败
    				E_HERO_RECEIVE_HERO_SUCCESS, //获得派遣英雄
			};

			public class SkillListBin
			{
				public UInt32 skill_count;   //技能数目
				
				public class Skill_List
				{
					public UInt32 skill_id;  //技能id
					public byte level;   //技能等级
					public UInt32 levelup_cd;  //技能累计cd
					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out 	skill_id);
						bin.Get_ (out level);
						bin.Get_ (out levelup_cd);
					}
				};
				public Skill_List[] skill_List = new  Skill_List[Hero_Def_Const.MAX_HERO_SKILL_SIZE];
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out skill_count);
					for (UInt32 i = 0; i < Hero_Def_Const.MAX_HERO_SKILL_SIZE/*skill_count*/; ++i) {
						skill_List [i] = new Skill_List ();
						skill_List [i].FromBin (bin);
					}
				}
			};

			public struct HeroBase
			{
				public UInt32	hero_id;		//英雄的索引id,全局唯一
				public UInt32	char_id;		//英雄所属玩家id
				public UInt32	type_id;		//英雄类型id
				public UInt32	attack;			//攻击力
				public UInt32	defense;		//防御力
				public UInt32	max_hp;			//血量
				public UInt32	hp;				//当前血量
				public UInt32	level;			//等级
				public UInt16	train_add;		//培养累加值
				public UInt32	train_times;	//培训剩余次数
				public UInt32	buy_train_times;//购买的培养次数
				public UInt32	hero_map_id;	//英雄所在的地图
				public UInt64	hero_city_id;	//英雄所在城市
				public UInt32	hero_build_id;	//英雄所在的建筑物id,0表示不再任何建筑物
				public HERO_STAR	hero_star;	//英雄评星
				public byte	 hero_status;	//英雄状态
				public UInt64	revive_end_time;//恢复结束时间
				public SkillListBin skill_list_bin;//技能

				//以下是英雄的基础资质，出生后不再改变
				public UInt32	base_attack;	//攻击基础
				public UInt32	base_defense;	//防御基础
				public UInt32	base_max_hp;	//血量基础
				/*
				public HeroBase()
				{
					
					//assert(sizeof(*this) == (48 + sizeof(skill_list)));
					//memset(skill_list_bin.skill_list, 0, sizeof(skill_list_bin.skill_list));
					revive_end_time = 0;
					level = 1;
					hero_star = HERO_STAR_ONE;
					hero_status = HERO_ATTACK_STATUS;
					train_times = HERO_TRAIN_DEFAULT_TIMES;
					buy_train_times = 0;
					train_add = 0;
					hero_city_id = 0;
					hero_build_id = 0;
					hero_map_id = 0;
					attack = defense = max_hp = hp = base_attack = base_defense = base_max_hp = 0;
					
				}*/
			};

			public struct HeroBaseForCache
			{
				public HeroBase	hero_base;
				public bool	 is_need_save;
				public bool	 is_new;
				
				/*
				HeroBaseForCache(const HeroBase* hero, bool is_new_base = false)
				{
					memcpy(&hero_base, hero, sizeof(HeroBase));
					is_need_save = false;
					is_new = is_new_base;
				}
				void update_hero_base(const HeroBase* hero)
				{
					memcpy(&hero_base, hero, sizeof(HeroBase));
					is_need_save = true;
				}
				HeroBase* get_hero_base()
				{
					return &hero_base;
				}
				*/
			};

			public struct HeroInfoForUpdate
			{
				HeroBase	hero_base;
				bool			is_new;
				//HeroBase*	get_hero_base(){return &hero_base;}
			};
			
			public class BrowserHero
			{
				public UInt64	city_id;
				public UInt32	char_id;
				public UInt32	attack;
				public UInt32	defense;
				public UInt32	max_hp;
				public UInt32	type_id;
				public HERO_STAR hero_star;
				public UInt32	skill_id;

				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out	city_id);
					bin.Get_ (out	char_id);
					bin.Get_ (out	attack);
					bin.Get_ (out	defense);
					bin.Get_ (out	max_hp);
					bin.Get_ (out	type_id);
					int temp;
					bin.Get_ (out temp);
					hero_star = (HERO_STAR)temp;
					bin.Get_ (out	skill_id);
				}
			};

			public class HeroForClient
			{
			    public UInt64 hero_id;   //英雄id
			    public UInt32 type_id;   //英雄类型
			
			    public UInt64 hero_city_id;  //城市id
			    public byte hero_map_id;  //地图id
			    public UInt16 hero_build_id;  //英雄所在的建筑id
			    public UInt16 build_order;  //英雄所在建筑物的顺序
			    public byte level;    //英雄等级
			    public UInt32 train_times;  //培养次数
				public UInt32 buy_train_times;//购买的培养次数
				public UInt32 train_cd_times;//培养累计cd
				
				//本地，用来记录收到train_cd_times时的时间, 非服务器字段
				public UInt32 train_cd_times_ReceiveTime;
				
			    public UInt32 attack;    //攻击力
			    public UInt32 defense;   //防御力
			    public UInt32 max_hp;    //最大血量
			    public UInt32 hp;     //当前血量
			    public SkillListBin skill_list_bin = new SkillListBin();//技能
				
				public void FromBin (NetSocket.ByteArray bin)
				{
					bin.Get_ (out hero_id);	
					bin.Get_ (out type_id);
					
					bin.Get_ (out hero_city_id);
					bin.Get_ (out hero_map_id);
					bin.Get_ (out hero_build_id);
					bin.Get_ (out build_order);
					bin.Get_ (out level);
					
					bin.Get_ (out train_times);
					train_cd_times_ReceiveTime =1;// AppMaster.TimeSince1970;
					
					bin.Get_ (out buy_train_times);
					bin.Get_ (out train_cd_times);
					
					bin.Get_ (out attack);
					bin.Get_ (out defense);
					bin.Get_ (out max_hp);
					bin.Get_ (out hp);
					
					skill_list_bin.FromBin (bin);
				}
			};

		}//namespace hero
	} //namespace app
}//namespace twp

