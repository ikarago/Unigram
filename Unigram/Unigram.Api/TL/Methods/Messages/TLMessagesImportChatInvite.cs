// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Messages
{
	/// <summary>
	/// RCP method messages.importChatInvite
	/// </summary>
	public partial class TLMessagesImportChatInvite : TLObject
	{
		public String Hash { get; set; }

		public TLMessagesImportChatInvite() { }
		public TLMessagesImportChatInvite(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessagesImportChatInvite; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Hash = from.ReadString();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x6C50051C);
			to.Write(Hash);
			if (cache) WriteToCache(to);
		}
	}
}