// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLInputMediaUploadedPhoto : TLInputMediaBase, ITLMediaCaption 
	{

		public TLInputMediaUploadedPhoto() { }
		public TLInputMediaUploadedPhoto(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.InputMediaUploadedPhoto; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			File = TLFactory.Read<TLInputFileBase>(from, cache);
			Caption = from.ReadString();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xF7AFF1C0);
			to.WriteObject(File, cache);
			to.Write(Caption);
			if (cache) WriteToCache(to);
		}
	}
}