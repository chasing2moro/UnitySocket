//
// 关系
// 

namespace twp {
	namespace app {
		namespace relation {
			
			public enum RelationDefine
			{
				// 好友上限
				RELATION_COUNT_TOTAL_MAX = 60,
			};


			public enum RelationType : byte // 关系标志
			{
				RELATION_TYPE_NONE		,	//未知(陌生人)
				RELATION_TYPE_FRIEND	,	//好友
				RELATION_TYPE_BLACK		,	//黑名单
				RELATION_TYPE_COUPLE	,	//夫妻
				RELATION_TYPE_MAX		,	//最大值
			};

			public enum OperationRelation : byte//操作好友方式
			{
				OPERATION_RELATION_NULL,   //错误操作
				OPERATION_RELATION_ADD_ID, //加入好友ID方式
				OPERATION_RELATION_ADD_NAME,//加入好友名字方式
				OPERATION_RELATION_DELETE, //删除好友
				OPERATION_RELATION_BLACK , //拉入黑名单
				OPERATION_RELATION_MEMBER, //查看细节
			};

			public enum RelationTime   //好友相关的时间
			{
			};

			public enum MemberUpdateType //更新操作类型
			{
				MEM_UPDATE_INSERT,	//新增一个成员
				MEM_UPDATE_UPDATE,	//更新成员类型
				MEM_UPDATE_DELETE,	//删除一个成员
			};

			public class RelationMemberInfo //好友显示数据
			{				
				public uint	char_idx; //好友ID 
				public string char_name; //twp::app::unit::LIMIT_NAME_STR_LENGTH+1//好友名称
				public RelationType	relation_type; //关系
				public uint battle_integral; // 战斗积分
				public bool	is_online; //是否在线
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_(out char_idx);
					char_name = bin.GetStringData((int)twp.app.unit.EUnitLimit.LIMIT_NAME_STR_LENGTH+1);
					byte v;
					bin.Get_(out v);
					relation_type = (RelationType)v;
					bin.Get_(out battle_integral);
					sbyte isonline;
					bin.Get_(out isonline);
					is_online = (isonline == 0)? false : true;
				}
			};


			// 好友信息
			public class RelationMember
			{
				// 玩家id
				public uint char_id;
				// 关系标志
				public byte relation_type;
			};

			public class RelationBaseInfo
			{
				public ushort	friend_max_number;		//好友列表最大数
				public ushort	black_max_number;		//黑名单列表最大数
				//RelationBaseInfo():friend_max_number(RELATION_COUNT_TOTAL_MAX),black_max_number(50){}
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_(out friend_max_number);
					bin.Get_(out black_max_number);
				}
			};

		} // namespace relation
	} // namespace app
} // namespace twp
