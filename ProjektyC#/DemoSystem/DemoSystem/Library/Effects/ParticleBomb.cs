using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class ParticleBombEffect : IEffect
    {
        private class Particle
        {
            public float X, Y;
            public float VX, VY;
            public Color Color;
            public int Life;
        }

        private readonly List<Particle> _particles = new();
        private readonly Random _rand = new();
        private readonly int _particleCount;
        private readonly int _maxLife;
        private readonly float _maxSpeed;

        public ParticleBombEffect(int particleCount = 300, int maxLife = 80, float maxSpeed = 10f)
        {
            _particleCount = particleCount;
            _maxLife = maxLife;
            _maxSpeed = maxSpeed;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            if (localFrame == 0)
            {
                _particles.Clear();
                int centerX = width / 2;
                int centerY = height / 2;

                for (int i = 0; i < _particleCount; i++)
                {
                    double angle = _rand.NextDouble() * Math.PI * 2;
                    float speed = (float)(_rand.NextDouble() * _maxSpeed);
                    _particles.Add(new Particle
                    {
                        X = centerX,
                        Y = centerY,
                        VX = (float)Math.Cos(angle) * speed,
                        VY = (float)Math.Sin(angle) * speed,
                        Color = RandomColor(),
                        Life = _maxLife
                    });
                }
            }

            foreach (var p in _particles)
            {
                if (p.Life > 0)
                {
                    p.X += p.VX;
                    p.Y += p.VY;
                    p.Life--;

                    int alpha = (int)(255 * (float)p.Life / _maxLife);
                    using var brush = new SolidBrush(Color.FromArgb(alpha, p.Color));
                    g.FillRectangle(brush, p.X, p.Y, 2, 2);
                }
            }
        }

        private Color RandomColor()
        {
            return Color.FromArgb(
                _rand.Next(100, 256), // R
                _rand.Next(100, 256), // G
                _rand.Next(100, 256)  // B
            );
        }
    }

}
