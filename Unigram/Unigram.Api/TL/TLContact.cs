// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLContact : TLObject 
	{
		public Int32 UserId { get; set; }
		public Boolean Mutual { get; set; }

		public TLContact() { }
		public TLContact(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.Contact; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			UserId = from.ReadInt32();
			Mutual = from.ReadBoolean();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xF911C994);
			to.Write(UserId);
			to.Write(Mutual);
			if (cache) WriteToCache(to);
		}
	}
}