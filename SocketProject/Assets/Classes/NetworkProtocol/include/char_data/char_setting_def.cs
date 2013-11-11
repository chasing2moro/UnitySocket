using UnityEngine;
using System;
using System.Collections;




namespace twp {
 namespace app {
  namespace unit {

   // 用于保存玩家客户端自身的相关设置，如：发言设置 客户端自定义数据等
   //
   // raw data
   //
   public class ClientSetting
   {
    // 当前任务相关标记
    public byte quest_flags;
    // 当前战斗相关标记
    public byte combat_flags;
    // 酒馆
    public byte refresh_pub;
    // 军器监标志
    public byte refresh_smith_shop;
    // 主城指引
    public UInt32 guide_flags;
    // 玩家操作事件点
    public UInt32 role_event_point;
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_ (out quest_flags);
					bin.Get_ (out combat_flags);
					bin.Get_ (out refresh_pub);
					bin.Get_ (out refresh_smith_shop);
					bin.Get_ (out guide_flags);
					bin.Get_ (out role_event_point);
				}
   };
			
   public class CharacterClientSettings
   {
    // todo 此处添加会影响到服务器的设置数据(如果有)
    
    // 客户端自定义数据设置 raw data
    public ClientSetting client_settings = new ClientSetting();
    // 保留
    byte[] reserved = new byte[64- sizeof(byte) * 4 - sizeof(UInt32) * 2/*sizeof(ClientSetting)*/] ;//
   // CharacterClientSettings()
   // {
    // assert(sizeof(CharacterClientSettings) == 64);
    // memset(this, 0, sizeof(CharacterClientSettings));
					
   // }
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					client_settings.FromBin(bin);
					for(int i = 0; i < 64- sizeof(byte) * 4 - sizeof(UInt32) * 2; ++i)
						bin.Get_ ( out reserved[i] );
				}
   };

  } // // namespace unit
 } // namespace app
} // namespace twp

