// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.getAdminedPublicChannels
	/// </summary>
	public partial class TLChannelsGetAdminedPublicChannels : TLObject
	{
		public TLChannelsGetAdminedPublicChannels() { }
		public TLChannelsGetAdminedPublicChannels(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChannelsGetAdminedPublicChannels; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x8D8D82D7);
			if (cache) WriteToCache(to);
		}
	}
}