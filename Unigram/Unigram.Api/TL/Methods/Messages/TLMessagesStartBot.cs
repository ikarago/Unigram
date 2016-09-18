// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Messages
{
	/// <summary>
	/// RCP method messages.startBot
	/// </summary>
	public partial class TLMessagesStartBot : TLObject
	{
		public TLInputUserBase Bot { get; set; }
		public TLInputPeerBase Peer { get; set; }
		public Int64 RandomId { get; set; }
		public String StartParam { get; set; }

		public TLMessagesStartBot() { }
		public TLMessagesStartBot(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessagesStartBot; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Bot = TLFactory.Read<TLInputUserBase>(from, cache);
			Peer = TLFactory.Read<TLInputPeerBase>(from, cache);
			RandomId = from.ReadInt64();
			StartParam = from.ReadString();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xE6DF7378);
			to.WriteObject(Bot, cache);
			to.WriteObject(Peer, cache);
			to.Write(RandomId);
			to.Write(StartParam);
			if (cache) WriteToCache(to);
		}
	}
}