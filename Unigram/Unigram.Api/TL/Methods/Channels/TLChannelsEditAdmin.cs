// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.editAdmin
	/// </summary>
	public partial class TLChannelsEditAdmin : TLObject
	{
		public TLInputChannelBase Channel { get; set; }
		public TLInputUserBase UserId { get; set; }
		public TLChannelParticipantRoleBase Role { get; set; }

		public TLChannelsEditAdmin() { }
		public TLChannelsEditAdmin(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChannelsEditAdmin; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Channel = TLFactory.Read<TLInputChannelBase>(from, cache);
			UserId = TLFactory.Read<TLInputUserBase>(from, cache);
			Role = TLFactory.Read<TLChannelParticipantRoleBase>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xEB7611D0);
			to.WriteObject(Channel, cache);
			to.WriteObject(UserId, cache);
			to.WriteObject(Role, cache);
			if (cache) WriteToCache(to);
		}
	}
}