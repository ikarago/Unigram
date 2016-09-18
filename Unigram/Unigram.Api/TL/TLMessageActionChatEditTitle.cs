// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLMessageActionChatEditTitle : TLMessageActionBase 
	{

		public TLMessageActionChatEditTitle() { }
		public TLMessageActionChatEditTitle(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessageActionChatEditTitle; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Title = from.ReadString();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xB5A1CE5A);
			to.Write(Title);
			if (cache) WriteToCache(to);
		}
	}
}