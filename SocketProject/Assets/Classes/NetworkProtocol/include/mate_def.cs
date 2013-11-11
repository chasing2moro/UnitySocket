using System;

//
// 索敌
// 
namespace twp {
 namespace app {
  namespace mate {
   public enum EMateLimit
   {
    	// 索敌浮动等级
     	SEARCH_ENEMY_LEVEL_MAX = 3,
   };
   
   public enum MatchState : byte// 匹配状态
   {
    MATCH_FALSE = 0,  // 匹配失败
    MATCH_TRUE,          // 匹配成功
   };
   
   public enum MatchOperateType : byte // 匹配操作状态
   {
    	PLAYER_OPERATE_MATCH_CITY, //匹配到城市
   };
  

   public class MateInfo
   {
     //玩家
     public UInt32 char_idx;          // 玩家id
    public byte[] name = new byte[(UInt16)twp.app.unit.EUnitLimit.LIMIT_NAME_STR_LENGTH + 1];  // 名字
    //城市
    public UInt64 city_id;          //城市id
    public UInt32  city_level;          //城市等级
    public UInt32 flags;           //地图id,x,y
    public UInt32 nation_type;         //国家id
    public byte[] city_name = new byte[twp.app.unit.TDCONST.MAX_CITY_NAME + 1];      //城市名称
    public UInt64 protected_end_time;        //保护结束时间(保留字段)
   };

  } 
 } 
} 


