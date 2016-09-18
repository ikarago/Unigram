// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Upload
{
	/// <summary>
	/// RCP method upload.saveBigFilePart
	/// </summary>
	public partial class TLUploadSaveBigFilePart : TLObject
	{
		public Int64 FileId { get; set; }
		public Int32 FilePart { get; set; }
		public Int32 FileTotalParts { get; set; }
		public Byte[] Bytes { get; set; }

		public TLUploadSaveBigFilePart() { }
		public TLUploadSaveBigFilePart(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.UploadSaveBigFilePart; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			FileId = from.ReadInt64();
			FilePart = from.ReadInt32();
			FileTotalParts = from.ReadInt32();
			Bytes = from.ReadByteArray();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xDE7B673D);
			to.Write(FileId);
			to.Write(FilePart);
			to.Write(FileTotalParts);
			to.WriteByteArray(Bytes);
			if (cache) WriteToCache(to);
		}
	}
}