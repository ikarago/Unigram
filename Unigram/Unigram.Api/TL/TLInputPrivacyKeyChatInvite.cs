// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLInputPrivacyKeyChatInvite : TLInputPrivacyKeyBase 
	{
		public TLInputPrivacyKeyChatInvite() { }
		public TLInputPrivacyKeyChatInvite(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.InputPrivacyKeyChatInvite; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xBDFB0426);
			if (cache) WriteToCache(to);
		}
	}
}