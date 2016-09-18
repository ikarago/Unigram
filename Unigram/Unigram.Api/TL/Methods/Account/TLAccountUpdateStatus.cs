// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Account
{
	/// <summary>
	/// RCP method account.updateStatus
	/// </summary>
	public partial class TLAccountUpdateStatus : TLObject
	{
		public Boolean Offline { get; set; }

		public TLAccountUpdateStatus() { }
		public TLAccountUpdateStatus(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.AccountUpdateStatus; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Offline = from.ReadBoolean();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x6628562C);
			to.Write(Offline);
			if (cache) WriteToCache(to);
		}
	}
}