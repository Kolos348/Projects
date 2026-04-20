using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystem.Library.Effects
{
    internal class Ball
    {
        public float X, Y;
        public float VX, VY;
        public float Radius;
        public Color Color;

        public Ball(float x, float y, float vx, float vy, float radius, Color color)
        {
            X = x;
            Y = y;
            VX = vx;
            VY = vy;
            Radius = radius;
            Color = color;
        }

        public void Update(int width, int height)
        {
            X += VX;
            Y += VY;

            // Wall collision
            if (X - Radius < 0 || X + Radius > width) VX *= -1;
            if (Y - Radius < 0 || Y + Radius > height) VY *= -1;
        }

        public void Draw(Graphics g)
        {
            using Brush brush = new SolidBrush(Color);
            g.FillEllipse(brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }

        public void CollideWith(Ball other)
        {
            float dx = other.X - X;
            float dy = other.Y - Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            float minDist = Radius + other.Radius;

            if (dist < minDist && dist > 0)
            {
                // Normalize
                float nx = dx / dist;
                float ny = dy / dist;

                // Relative velocity
                float dvx = other.VX - VX;
                float dvy = other.VY - VY;

                // Dot product
                float impactSpeed = dvx * nx + dvy * ny;

                if (impactSpeed > 0) return;

                // Simple elastic collision
                float impulse = 2 * impactSpeed / 2;
                VX += impulse * nx;
                VY += impulse * ny;
                other.VX -= impulse * nx;
                other.VY -= impulse * ny;
            }
        }
    }

}
