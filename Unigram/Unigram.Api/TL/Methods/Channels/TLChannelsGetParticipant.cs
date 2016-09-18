// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.getParticipant
	/// </summary>
	public partial class TLChannelsGetParticipant : TLObject
	{
		public TLInputChannelBase Channel { get; set; }
		public TLInputUserBase UserId { get; set; }

		public TLChannelsGetParticipant() { }
		public TLChannelsGetParticipant(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChannelsGetParticipant; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Channel = TLFactory.Read<TLInputChannelBase>(from, cache);
			UserId = TLFactory.Read<TLInputUserBase>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x546DD7A6);
			to.WriteObject(Channel, cache);
			to.WriteObject(UserId, cache);
			if (cache) WriteToCache(to);
		}
	}
}