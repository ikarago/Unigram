// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLKeyboardButtonRow : TLObject 
	{
		public TLVector<TLKeyboardButtonBase> Buttons { get; set; }

		public TLKeyboardButtonRow() { }
		public TLKeyboardButtonRow(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.KeyboardButtonRow; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Buttons = TLFactory.Read<TLVector<TLKeyboardButtonBase>>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x77608B83);
			to.WriteObject(Buttons, cache);
			if (cache) WriteToCache(to);
		}
	}
}