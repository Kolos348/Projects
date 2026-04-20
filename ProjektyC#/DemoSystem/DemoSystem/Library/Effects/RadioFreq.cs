using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    using System;
    using System.Drawing;

    internal class FrequencyLineEffect : IEffect
    {
        private readonly Color _lineColor;
        private readonly int _lineY;
        private readonly int _amplitude;
        private readonly float _baseFrequency;
        private readonly float _frequencyModSpeed;
        private readonly float _frequencyModRange;
        private readonly float _scrollSpeed;
        private readonly int _thickness;

        public FrequencyLineEffect(Color lineColor, int lineY = 384, int amplitude = 40, float baseFrequency = 0.02f,
                                   float frequencyModSpeed = 0.01f, float frequencyModRange = 0.015f,
                                   float scrollSpeed = 0.1f, int thickness = 2)
        {
            _lineColor = lineColor;
            _lineY = lineY;
            _amplitude = amplitude;
            _baseFrequency = baseFrequency;
            _frequencyModSpeed = frequencyModSpeed;
            _frequencyModRange = frequencyModRange;
            _scrollSpeed = scrollSpeed;
            _thickness = thickness;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            using var pen = new Pen(_lineColor, _thickness);
            float offset = globalFrame * _scrollSpeed;

            float animatedFrequency = _baseFrequency + (float)Math.Sin(globalFrame * _frequencyModSpeed) * _frequencyModRange;

            PointF[] points = new PointF[width];
            for (int x = 0; x < width; x++)
            {
                float y = _lineY + (float)Math.Sin(x * animatedFrequency + offset) * _amplitude;
                points[x] = new PointF(x, y);
            }

            g.DrawLines(pen, points);
        }
    }

}
