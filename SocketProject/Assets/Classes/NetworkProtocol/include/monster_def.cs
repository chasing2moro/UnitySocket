/*
* Copyright (c) 2013,广州要玩娱乐网络技术有限公司
* All rights reserved.
*
* 文件名称: monster_def.h
* 文件标识:

* 摘要: 怪物数据define
*
* 当前版本: 1.1


* 作者: LinJun
* 完成日期: 2013年8月19日
*
* 取代版本:1.0


* 原作者 : LiuJun
* 完成日期: 2013年8月19日
*/

using System;


namespace twp {
    namespace app {
        namespace unit {

			public enum MonsterLimit
			{
				MAX_MONSTER_FIVE_ELEMENTS_PARAM_COUNT = 5,

				HERO_SUB_TYPE_BEGIN = 1,
				HERO_SUB_TYPE_END = 20,
			};
			public enum MonsterStatus
			{
				MONSTERSTATUS_STOP_ATTACK		= 0x00000001,
				MONSTERSTATUS_STOP_MOVE			= 0x00000002
			};

            public class MonsterInfoDB
            {
                // 类型id
                public UInt32 type_idx;
				// 种类
				public byte sort;
				// 不擅长种类
				public byte scare_sort;
                // 生命值
                public UInt32 hp;
                // 生命值上限
                public UInt32 hp_max;
				// 攻击力
				public UInt32 attack;
				// 攻击力
				public UInt32 armor;
                // 五行
                public byte five_elements;
				// 命中率
				public byte accuracy;
				// 技能
				public UInt32 spell_idx;
                // ai idx
                public UInt32 ai_idx;
                // 移动速度
                public UInt32 move_speed;
				// 获得军资
				public UInt16 loot_military_supplies;
				// 状态
				public byte status;
				// 不同5行伤害参数
				public byte[] five_elements_attacked_param = new byte[(int)MonsterLimit.MAX_MONSTER_FIVE_ELEMENTS_PARAM_COUNT];
				// 免伤值
				public byte dismiss_damage;

            };

			// 怪物数据
			public class MonsterInfo
			{
				// 类型
				public UInt32 type_idx;
				// 生命值
				public UInt32 hp;
				public UInt32 max_hp;
				// 攻击力
				public UInt32 attack;
				// 防御力
				public UInt32 defense;
				// 攻击频率(多少毫秒攻击一次)
				public UInt32 attack_rate;
				// 攻击半径
				public UInt32 attack_radius;
				// 视野半径
				public UInt32 view_radius;
				// 追击半径
				public UInt32 pursue_radius;
				// 周身半径
				public UInt32 collise_radius;
				// 移动标示
				twp.app.scene.MoveFlag move_flag;

			};

			//
			// 从数据库中读取的怪物AI
			//
			public class AIInfoDB
			{
				// AI等级
				public UInt32 ai_level;
				// uint32 ai_flags[AI_STATUS_MAX];	//AI规则

			};

			// 普通怪物AI
			public enum MonsterAI
			{
				// 初始化
				STATE_MONSTER_NORMAL_INIT = 0,
				// 待机
				STATE_MONSTER_IDIE = 1,
				// 移动
				STATE_MONSTER_MOVE = 2,
				// 攻击
				STATE_MONSTER_ATTACK = 3,
				// 追击
				STATE_MONSTER_PURSUE = 4,
				// 消失
				STATE_COMBAT_LEAVE = 5,
			};

			public enum LockAttackUnitState : byte
			{
				LOCK_ATTACK_UNIT_DEAD = 0, // 最终锁定攻击目标死亡
				LOCK_ATTACK_UNIT_LOSE = 1, // 最终锁定攻击目标丢失
				LOCK_ATTACK_UNIT_VIEW = 2, // 最终锁定攻击目标进入范围
			};



        } // // namespace character
    } // namespace app
} // namespace twp

