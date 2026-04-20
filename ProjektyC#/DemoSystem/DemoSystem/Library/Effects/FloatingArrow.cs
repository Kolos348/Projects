using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class FloatingArrowEffect : IEffect
    {
        private readonly Color _arrowColor;
        private readonly int _arrowWidth;
        private readonly int _arrowHeight;
        private readonly float _floatSpeed;
        private readonly int _amplitude;

        public FloatingArrowEffect(Color arrowColor, int arrowWidth = 80, int arrowHeight = 120, float floatSpeed = 0.05f, int amplitude = 30)
        {
            _arrowColor = arrowColor;
            _arrowWidth = arrowWidth;
            _arrowHeight = arrowHeight;
            _floatSpeed = floatSpeed;
            _amplitude = amplitude;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            float offsetY = (float)(Math.Sin(globalFrame * _floatSpeed) * _amplitude);
            int centerX = width / 2;
            int centerY = height / 2 + (int)offsetY;

            Point[] arrowPoints = new Point[]
            {
            new Point(centerX, centerY - _arrowHeight / 2), // tip
            new Point(centerX - _arrowWidth / 2, centerY + _arrowHeight / 2),
            new Point(centerX + _arrowWidth / 2, centerY + _arrowHeight / 2)
            };

            using Brush brush = new SolidBrush(_arrowColor);
            g.FillPolygon(brush, arrowPoints);
        }
    }

}
