using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    internal class PulsingRainbowCircle : IEffect
    {
        private int _xOffset;
        private int _yOffset;

        public PulsingRainbowCircle(int xOffset = 0, int yOffset = 0)
        {
            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            int radius = (int)(Math.Abs(Math.Sin(globalFrame * 0.05)) * 50 + 10);
            Color color = ColorFromHue(globalFrame % 360);
            using (Brush brush = new SolidBrush(color))
            {
                g.FillEllipse(
                    brush,
                    _xOffset + width / 2 - radius,
                    _yOffset + height / 2 - radius,
                    radius * 2,
                    radius * 2
                );
            }
        }

        private Color ColorFromHue(int hue)
        {
            return ColorFromHSV(hue, 1.0, 1.0);
        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            return hi switch
            {
                0 => Color.FromArgb(v, t, p),
                1 => Color.FromArgb(q, v, p),
                2 => Color.FromArgb(p, v, t),
                3 => Color.FromArgb(p, q, v),
                4 => Color.FromArgb(t, p, v),
                _ => Color.FromArgb(v, p, q),
            };
        }
    }
}
