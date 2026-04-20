using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    internal class BallSimulation : IEffect
    {
        private List<Ball> _balls;
        private Random _rand = new Random();

        public BallSimulation(int count, int width, int height)
        {
            _balls = new List<Ball>();
            for (int i = 0; i < count; i++)
            {
                float radius = _rand.Next(10, 30);
                _balls.Add(new Ball(
                    _rand.Next((int)radius, width - (int)radius),
                    _rand.Next((int)radius, height - (int)radius),
                    (float)(_rand.NextDouble() * 4 - 2),
                    (float)(_rand.NextDouble() * 4 - 2),
                    radius,
                    Color.FromArgb(_rand.Next(256), _rand.Next(256), _rand.Next(256))
                ));
            }
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            foreach (var ball in _balls)
                ball.Update(width, height);

            for (int i = 0; i < _balls.Count; i++)
            {
                for (int j = i + 1; j < _balls.Count; j++)
                    _balls[i].CollideWith(_balls[j]);
            }

            foreach (var ball in _balls)
                ball.Draw(g);
        }
    }

}
