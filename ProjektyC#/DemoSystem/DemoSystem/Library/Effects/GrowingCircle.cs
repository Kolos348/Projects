
namespace DemoSystem.Library.Effects
{
    internal class GrowingCircle : IEffect
    {
        private Brush _brush;
        private int _xOffset;
        private int _yOffset;

        public GrowingCircle(Brush brush, int xOffset = 0, int yOffset = 0)
        {
            _brush = brush;
            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        public void Render(Graphics g, int width, int height, int localFrame, int globalFrame)
        {
            g.FillEllipse(
                _brush,
                _xOffset + width / 2 - localFrame,
                _yOffset + height / 2 - localFrame,
                localFrame * 2,
                localFrame * 2
            );
        }
    }
}
