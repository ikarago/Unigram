// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Messages
{
	/// <summary>
	/// RCP method messages.getUnusedStickers
	/// </summary>
	public partial class TLMessagesGetUnusedStickers : TLObject
	{
		public Int32 Limit { get; set; }

		public TLMessagesGetUnusedStickers() { }
		public TLMessagesGetUnusedStickers(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessagesGetUnusedStickers; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Limit = from.ReadInt32();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x4309D65B);
			to.Write(Limit);
			if (cache) WriteToCache(to);
		}
	}
}