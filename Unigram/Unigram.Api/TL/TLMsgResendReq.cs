// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLMsgResendReq : TLObject 
	{
		public TLVector<Int64> MsgIds { get; set; }

		public TLMsgResendReq() { }
		public TLMsgResendReq(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MsgResendReq; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			MsgIds = TLFactory.Read<TLVector<Int64>>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x7D861A08);
			to.WriteObject(MsgIds, cache);
			if (cache) WriteToCache(to);
		}
	}
}