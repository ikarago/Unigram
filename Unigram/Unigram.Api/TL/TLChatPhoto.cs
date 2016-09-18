// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLChatPhoto : TLChatPhotoBase 
	{
		public TLFileLocationBase PhotoSmall { get; set; }
		public TLFileLocationBase PhotoBig { get; set; }

		public TLChatPhoto() { }
		public TLChatPhoto(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChatPhoto; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			PhotoSmall = TLFactory.Read<TLFileLocationBase>(from, cache);
			PhotoBig = TLFactory.Read<TLFileLocationBase>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x6153276A);
			to.WriteObject(PhotoSmall, cache);
			to.WriteObject(PhotoBig, cache);
			if (cache) WriteToCache(to);
		}
	}
}