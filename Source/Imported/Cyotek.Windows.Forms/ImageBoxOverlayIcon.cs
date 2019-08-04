using System;
using System.Drawing;

namespace Cyotek.Windows.Forms
{
    public sealed class ImageBoxOverlayIcon
    {
        public string IconKey { get; set; } = "";
        public Point Location { get; set; } = Point.Empty;

        public ImageBoxOverlayIcon(string key, int x, int y)
        {
            IconKey = key;
            Location = new Point(x, y);
        }
    }
}
