using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace twp {
	namespace app {
		namespace aura {

			public enum E_Aura_Limit
			{
				INVALID_AURA_ID = 0,
				AURA_TYPE_GENERATEBATTLEPOINT_ID = 1,
				AURA_TYPE_GENERATEHOTBUFF = 2,			// 产生热量 buff
				AURA_TYPE_GENERATEHOT = 3,				// 产生热量
				AURA_REPLACE_LEVEL_INVALID = 0,
				MAX_AURA_VALUE_COUNT = 4,
			};

			public enum E_Aura_Enum
			{
				AURA_GENERATE_QUANTITY_VALUE = 25,  // 周期增加热量值
			};

			public enum EffectType // uint16
			{
				EFFECT_TYPE_INVALID						= 0,

				//保护主城
				EFFECT_TYPE_PROTECT			= 1,
				//不能攻击
				EFFECT_TYPE_NOT_ATTACK		= 2,
				//减速
				EFFECT_TYPE_SLOW_DOWN		= 3,
				//燃烧
				EFFECT_TYPE_BURN			= 4,
			};

			public enum AuraFlags
			{
				AF_NONE				= 0x00000000,
				// 需要保存
				AF_NEEDSAVE			= 0x00000001,
				// 副本中广播
				AF_NEEDINSTANCEBROCASE= 0x00000002,
			};

			// 类数据
			public class AuraInfoDB
			{
				public UInt16 type_idx;
				// 大类
				public UInt16 sort;
				// 效果类型
				public UInt16 effect_type;
				// aura替换级别
				public byte replace_level;
				// flags
				public UInt32 aura_flags;
				// 克制类型
				public UInt16 negative_type;
				// 延迟执行时间
				public UInt32 delay_begin_time;
				// 周期执行时间
				public UInt32 period_time;
				// 生命周期
				public UInt64 lifetime;
				// 同类型是否时间叠加 or 重置
				public byte time_replace;
				// 数值
				public int[] value = new int[(int)E_Aura_Limit.MAX_AURA_VALUE_COUNT];
			};

			
			// 对象id
			public class AuraObjectInfo
			{
				UInt16 type_idx;
				// 开始时间(秒)
				UInt32 create_time;
				// 生存时间(秒)
				UInt32 life_time;
			};

			// aura携带信息
			public class AuraRemember
			{
				public class Continueing_Injury
				{
					public UInt32 cut_hp;
				}
				
				public class Continueing_Resume
				{
					public UInt32 hp;
				}
				
				public class Attack_Range
				{
					public UInt32 range;
				}
				
				public class Add_Move_Speed
				{
					public UInt32 change_move_speed;
				}
				
				public class Dec_Move_Speed
				{
					public UInt32 change_move_speed;
				}
				
				public class Attack_Silence
				{
					
				}
				
				public class Add_Attack_Param
				{
					public UInt16 change_attack;
				}
				
				public class Dec_Attack_Param
				{
					public UInt16 change_attack;
				}
				
				public class Add_Attack_Cooldown
				{
					public UInt32 change_cooldown;
					public UInt32 current_spell_idx;
				}
				
				public class Dec_Attack_Cooldown
				{
					public UInt32 change_cooldown;
					public UInt32 current_spell_idx;
				}
				
				public class Add_Accracy_Param
				{
					public byte change_accuracy;
				}
				
				public class Dec_Accracy_Param
				{
					public byte change_accuracy;
				}
				
				public class Add_Five_Element_Attack_Param
				{
					public UInt32 five_elements_index;
					public byte change_attack_param;
				}
				
				public  class Dec_Five_Element_Attack_Param
				{
					public UInt32 five_elements_index;
					public byte change_attack_param;
				}
				
				public class Add_Target_Count
				{
					public UInt32 current_spell_idx;
					public UInt32 change_target_count;
				}
				
				public class Add_Armor_Param
				{
					public UInt16 change_armor;
				}
				
				public class Dec_Armor_Param
				{
					public UInt16 change_armor;
				}
				
				
				public class Inner_Union
				{
					// 持续伤害
					public Continueing_Injury continueing_injury;

					// 持续加血
					public Continueing_Resume continueing_resume;

					// 增加射程
					public Attack_Range attack_range;


					// 加快移动速度
					public Add_Move_Speed add_move_speed;
					// 减慢移动速度
					public Dec_Move_Speed dec_move_speed;
					// 攻击沉默
					public Attack_Silence attack_silence;
					// 增加攻击力
					public Add_Attack_Param add_attack_param;
					// 减少攻击力
					public Dec_Attack_Param dec_attack_param;
					// 增加攻击间隔（降低攻击频率）
					public Add_Attack_Cooldown add_attack_cooldown;
					// 减少攻击间隔（提高攻击频率）
					public Dec_Attack_Cooldown dec_attack_cooldown;
					// 增加准确率
					public Add_Accracy_Param add_accracy_param;
					// 减少准确率
					public Dec_Accracy_Param dec_accracy_param;
					// 提高五行受伤害参数
					public Add_Five_Element_Attack_Param  add_five_element_attack_param;
					// 降低五行受伤害参数
					public Dec_Five_Element_Attack_Param dec_five_element_attack_param;
					// 增加攻击目标
					public Add_Target_Count add_target_count;
					// 增加护甲
					public Add_Armor_Param add_armor_param;
					// 减少护甲
					public Dec_Armor_Param dec_armor_param;
				}
				
				public Inner_Union inner_Union = new Inner_Union();
				
			};

			// aura事件
			public class AuraEvent
			{
				public enum EventType
				{
					AURA_EVENT_INVALID = 0,				// 非法消息
					AURA_EVENT_ADD_AURA,				// 新添加
					AURA_EVENT_DEL_AURA,				// 删除
					AURA_EVENT_UPDATE_AURA,				// 更新
				}
				
				public class Continueing_Injury//8
				{
					public UInt32 current_hp;
					public UInt32 lost_hp;
					
					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out current_hp);
						bin.Get_ (out lost_hp);
						
					}
				}
				
				public class Continueing_Resume//4
				{
					public UInt32 hp;
					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out hp);
						bin.Move(8-4);
					}
				}
				
				public class Change_Attack_Range//4
				{
					public UInt32 range;
					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out range);
						bin.Move(8-4);
					}
				}
				
				//[StructLayout(LayoutKind.Explicit)]
				public class InnerUnion
				{
					// 持续伤害
					//[FieldOffset(0)]
					public Continueing_Injury continueing_injury;
					// 持续加血
					//[FieldOffset(0)]
					public Continueing_Resume continueing_resume;
					// 射程
					//[FieldOffset(0)]
					public Change_Attack_Range change_attack_range;
					
					public void FromBin(EffectType type ,NetSocket.ByteArray bin)
					{
						switch(type)
						{
							
						case EffectType.EFFECT_TYPE_BURN:
						{
							continueing_injury = new Continueing_Injury();
							continueing_injury.FromBin(bin);
							break;
						}
						default:
						{
							Debug.LogError("Not Handle Type....");
							break;
						}
							
						}
					}
				}
				
				
				// aura id
				public UInt16 aura_type_idx;
				// effect type
				public UInt16 effect_type;
				public twp.app.unit.UnitID unit_idx = new twp.app.unit.UnitID();
				// 事件类型
				public EventType event_type;

				// 时间
				public UInt64 lifetime;

				// 事件附带值
				public InnerUnion innerUnion = new InnerUnion();
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_(out aura_type_idx);
					bin.Get_(out effect_type);
					unit_idx.FromBin(bin);
					
					int temp;
					bin.Get_(out temp);
					event_type = (EventType)temp;
					
					bin.Get_(out lifetime);
					innerUnion.FromBin((EffectType)effect_type, bin);
				}
			};

		} // // namespace aura
	} // namespace app
} // namespace twp
