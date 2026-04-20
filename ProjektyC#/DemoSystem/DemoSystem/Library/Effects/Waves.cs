using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    using System;
    using System.Drawing;

    using System;
    using System.Drawing;

    internal class WaveEffect : IEffect
    {
        private readonly Color _waveColor;
        private readonly int _amplitude;
        private readonly float _frequency;
        private readonly float _speed;
        private readonly int _thickness;
        private readonly float _driftSpeed;
        private readonly int _driftAmplitude;

        public WaveEffect(Color waveColor, int amplitude = 30, float frequency = 0.02f, float speed = 0.1f, int thickness = 4, float driftSpeed = 0.2f, int driftAmplitude = 45)
        {
            _waveColor = waveColor;
            _amplitude = amplitude;
            _frequency = frequency;
            _speed = speed;
            _thickness = thickness;
            _driftSpeed = driftSpeed;
            _driftAmplitude = driftAmplitude;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            using var pen = new Pen(_waveColor, _thickness);
            float offset = globalFrame * _speed;
            float driftOffset = (float)Math.Sin(globalFrame * _driftSpeed) * _driftAmplitude;

            for (int y = 0; y < height; y += 40)
            {
                float baseY = y + driftOffset;
                PointF[] points = new PointF[width];
                for (int x = 0; x < width; x++)
                {
                    float waveY = baseY + (float)Math.Sin(x * _frequency + offset) * _amplitude;
                    points[x] = new PointF(x, waveY);
                }
                g.DrawLines(pen, points);
            }
        }
    }


}
