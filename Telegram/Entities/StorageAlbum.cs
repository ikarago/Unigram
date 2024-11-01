using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Common;
using Windows.Foundation;

namespace Telegram.Entities
{
    public partial class StorageAlbum : StorageMedia
    {
        public IList<StorageMedia> Media { get; }

        public StorageAlbum(IList<StorageMedia> media)
            : base(null, 0)
        {
            Media = media.ToList();
        }

        public const double ITEM_MARGIN = 2;
        public const double MAX_WIDTH = 320 + ITEM_MARGIN;
        public const double MAX_HEIGHT = 420 + ITEM_MARGIN;

        private ((Rect, MosaicItemPosition)[], Size)? _positions;

        public void Invalidate()
        {
            _positions = null;
        }

        public (Rect[], Size) GetPositionsForWidth(double w)
        {
            var positions = _positions ??= MosaicAlbumLayout.chatMessageBubbleMosaicLayout(new Size(MAX_WIDTH, MAX_HEIGHT), GetSizes());

            var ratio = w / positions.Item2.Width;
            var rects = new Rect[positions.Item1.Length];

            for (int i = 0; i < rects.Length; i++)
            {
                var rect = positions.Item1[i].Item1;
                var x = Sanitize(rect.X * ratio);
                var y = Sanitize(rect.Y * ratio);
                var width = Sanitize(rect.Width * ratio);
                var height = Sanitize(rect.Height * ratio);

                rects[i] = new Rect(x, y, width, height);
            }

            var finalWidth = Sanitize(positions.Item2.Width * ratio);
            var finalHeight = Sanitize(positions.Item2.Height * ratio);

            return (rects, new Size(finalWidth, finalHeight));
        }

        private double Sanitize(double value)
        {
            value = Math.Max(0, value);
            value = double.IsNaN(value) ? 0 : value;
            value = double.IsInfinity(value) ? 0 : value;

            return value;
        }

        private IEnumerable<Size> GetSizes()
        {
            foreach (var message in Media)
            {
                yield return new Size(message.ActualWidth, message.ActualHeight);
            }
        }
    }
}
