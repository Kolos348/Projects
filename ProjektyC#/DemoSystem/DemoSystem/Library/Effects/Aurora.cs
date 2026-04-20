using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace DemoSystem.Library.Effects
{
    internal class AuroraPulseEffect : IEffect
    {
        private readonly Color _baseColor = RandomColor();

        private static Color RandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(
                rand.Next(100, 256), // R
                rand.Next(100, 256), // G
                rand.Next(100, 256)  // B
            );
        }

        private readonly int _bandWidth;
        private readonly int _bandHeight;
        private readonly float _pulseSpeed;
        private readonly bool _fromTop;

        public AuroraPulseEffect(int bandWidth = 1366, int bandHeight = 768, float pulseSpeed = 0.01f, bool fromTop = false)
        {
            _bandWidth = bandWidth;
            _bandHeight = bandHeight;
            _pulseSpeed = pulseSpeed;
            _fromTop = fromTop;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            float pulse = (float)(Math.Sin(globalFrame * _pulseSpeed) * 0.5 + 0.5); // Range: 0 to 1
            int centerX = width / 2;
            int centerY = height / 2;

            Color baseColor = RandomColor();
            Color transparent = Color.FromArgb(0, baseColor);
            Color glow = Color.FromArgb((int)(180 * pulse), baseColor);


            Rectangle gradientRect = new Rectangle(
                centerX - _bandWidth / 2,
                centerY - _bandHeight / 2,
                _bandWidth,
                _bandHeight
            );

            using var brush = new LinearGradientBrush(
                gradientRect,
                _fromTop ? glow : transparent,
                _fromTop ? transparent : glow,
                LinearGradientMode.Vertical
            );

            g.FillRectangle(brush, gradientRect);
        }
    }

}
