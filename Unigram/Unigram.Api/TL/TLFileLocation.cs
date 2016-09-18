// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLFileLocation : TLFileLocationBase 
	{
		public Int32 DCId { get; set; }

		public TLFileLocation() { }
		public TLFileLocation(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.FileLocation; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			DCId = from.ReadInt32();
			VolumeId = from.ReadInt64();
			LocalId = from.ReadInt32();
			Secret = from.ReadInt64();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x53D69076);
			to.Write(DCId);
			to.Write(VolumeId);
			to.Write(LocalId);
			to.Write(Secret);
			if (cache) WriteToCache(to);
		}
	}
}