﻿using System.IO;
using Telegram.Api.Extensions;

namespace Telegram.Api.TL
{
    public abstract class TLInputPhotoBase : TLObject { }

    public class TLInputPhotoEmpty : TLInputPhotoBase
    {
        public const uint Signature = TLConstructors.TLInputPhotoEmpty;

        public override byte[] ToBytes()
        {
            return TLUtils.SignatureToBytes(Signature);
        }

        public override TLObject FromStream(Stream input)
        {
            return this;
        }

        public override void ToStream(Stream output)
        {
            output.Write(TLUtils.SignatureToBytes(Signature));
        }
    }

    public class TLInputPhoto : TLInputPhotoBase
    {
        public const uint Signature = TLConstructors.TLInputPhoto;

        public TLLong Id { get; set; }
        public TLLong AccessHash { get; set; }

        public override byte[] ToBytes()
        {
            return TLUtils.Combine(
                TLUtils.SignatureToBytes(Signature),
                Id.ToBytes(),
                AccessHash.ToBytes());
        }

        public override TLObject FromStream(Stream input)
        {
            Id = GetObject<TLLong>(input);
            AccessHash = GetObject<TLLong>(input);

            return this;
        }

        public override void ToStream(Stream output)
        {
            output.Write(TLUtils.SignatureToBytes(Signature));
            Id.ToStream(output);
            AccessHash.ToStream(output);
        }
    }
}
