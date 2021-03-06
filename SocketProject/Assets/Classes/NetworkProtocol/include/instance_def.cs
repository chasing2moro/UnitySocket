using System;

namespace twp {
    namespace app {
        namespace scene {


			public enum ELimitInstance
			{
				INVALID_INSTANCE_STATIC_POSITION = 0xff, // 无效的副本静态数据块位置
				INSTANCE_MAX_ALLOW_PLAYER_COUNT_PVE = 1, // PVE最大人数
				INSTANCE_MAX_ALLOW_PLAYER_COUNT_PVP = 2, // PVP最大人数
				INSTANCE_MAX_ALLOW_PLAYER_COUNT = 2,     // 副本最大人数
				INSTANCE_OVER_HERO_DIALOG_COUNT = 20,    // 副本结算显示的英雄类型数量
				INSTANCE_SCENE_BEGIN_ROW = 4,            // 副本起始行
				INSTANCE_SCENE_BEGIN_COL = 4,            // 副本起始列
				INSTANCE_ROW_SIZE = 4,                   // 副本行列大小
				INSTANCE_COL_SIZE = 4,
			};

			// 数据恢复类型
			public enum CombatResumeType
			{
				INSTANCE_RESUME_TYPE_INIT = 0,   // 游戏初始化恢复
				INSTANCE_RESUME_TYPE_COMBAT = 1, // 游戏战斗中恢复
			};
			
			// 数据恢复元素类型
			public enum CombatResumeElemType
			{
				RESUME_ELEM_TYPE_BUILD_DATA = 0,   // 建筑
				RESUME_ELEM_TYPE_HERO_DATA = 1,    // 英雄
				RESUME_ELEM_TYPE_MONSTER_DATA = 2, // 怪物
			};

			// 副本操作类型
			public enum CombatOperationType : byte
			{
				COMBAT_BASIC_OPERATION_TYPE_UNIT_MOVE = 0, // 单位移动
				COMBAT_BASIC_OPERATION_TYPE_APPLY_COMBAT = 1,// 请求战斗
				COMBAT_BASIC_OPERATION_TYPE_UNIT_BORN = 2,   // 单位出生
				COMBAT_BASIC_OPERATION_TYPE_UNIT_CHAGNE_DEST = 3, // 攻击目标改变
				COMBAT_BASIC_OPERATION_TYPE_OVER = 4,        // 玩家请求结束游戏
			};

			// 副本事件类型
			public enum InstanceEventType : byte
			{
				ET_INTO_COMBAT_TIME		= 0,  // 游戏倒计时
				ET_INTO_COMBAT_SUCCED,		  // 开始战斗(成功)
				ET_INTO_COMBAT_FAIL,          // 开始战斗(失败)
				ET_ING_COMBAT_OVER,           // 游戏结束
				ET_ING_COMBAT_UNIT_BORN,      // 单位出生
			};
			
			// 怪物更新时间类型
			public enum InstanceMonEventType : byte
			{
				ET_INFO_MON_BORN = 0,        // 单位出生
				ET_INFO_MON_CHANGE_DEST = 1, // 改变攻击目标
			};
			
			// 阵营类型
			public enum CombatCampType
			{
				COMBAT_CAMP_TYPE_INVALID = 0, // 无效阵营
				COMBAT_CAMP_TYPE_ATTACK = 1,  // 攻击方阵营
				COMBAT_CAMP_TYPE_DEFENSE = 2, // 防御方阵营
			};
			
			// 恢复建筑和英雄的标示
			public enum ResumeFlag
			{
				RESUME_UNIT_FLAG_BUILD = 1 << 1, // 建筑 
				RESUME_UNIT_FLAG_HERO = 1 << 2,  // 英雄 
			};
			
			// 怪物出场标示
			public enum MonBornFlag
			{
				MON_BORN_FLAG_INIT = 0,  // 怪物初始化
				MON_BORN_FLAG_STAGE = 1, // 怪物出场
			};
			
			// 碰撞检测方式
			public enum ColliseType
			{
				COLLISE_TYPE_INVALID = 0,  // 无效
				COLLISE_TYPE_ATTACK = 1,   // 检测是否进入攻击半径
				COLLISE_TYPE_VIEW = 2,     // 检测是否进入视野半径
				COLLISE_TYPE_PURSUE = 3,   // 检测是否进入追击半径
			};
			
			// AI 段 (注 :1000 一个段)
			public enum InstanceAI
			{
				AI_MONSTER = 1000,	// 怪物(1000 ~ 1999)
			};
			
			// 建筑数据
			public struct BuildData
			{
				UInt32 build_idx;  // 建筑ID
				byte build_type;  // 建筑类型
				byte sub_type;    // 建筑子类
				UInt16 level;      // 等级
				byte row;         // 位置
				byte col;
			};
			
			// 怪物数据
			public struct MonsterData
			{
				UInt32 mon_idx;        // 怪物ID
				UInt32 mon_type;       // 怪物类型(用于头像)
				UInt32 build_idx;      // 附属建筑id
				UInt32 hp;             // 血量
				UInt32 attack;         // 攻击力
				UInt32 defense;        // 防御力
				MonBornFlag born_flag; // 出场标示
			};
			
			// 副本结果类型
			public enum CombatResultType : byte
			{
				COMBAT_RESULT_TYPE_NONE = 0, // 进行中...
				COMBAT_RESULT_TYPE_WIN = 1,  // 胜利
				COMBAT_RESULT_TYPE_FAIL = 2, // 失败
				COMBAT_RESUTT_TYPE_DRAW = 3, // 平局
			};



        } // namespace instance
    } // namespace app
} // namespace twp