// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.deleteChannel
	/// </summary>
	public partial class TLChannelsDeleteChannel : TLObject
	{
		public TLInputChannelBase Channel { get; set; }

		public TLChannelsDeleteChannel() { }
		public TLChannelsDeleteChannel(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChannelsDeleteChannel; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Channel = TLFactory.Read<TLInputChannelBase>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xC0111FE3);
			to.WriteObject(Channel, cache);
			if (cache) WriteToCache(to);
		}
	}
}