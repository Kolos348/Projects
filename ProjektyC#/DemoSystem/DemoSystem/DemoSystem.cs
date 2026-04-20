using DemoSystem.Factories;
using DemoSystem.Library;

namespace DemoSystem
{
    public partial class DemoSystem : Form
    {

        private Demo _demo;

        public DemoSystem()
        {
            InitializeComponent();

            var factory = new MyDemoFactory();

            _demo = factory.CreateDemo();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void DemoSystem_Paint(object sender, PaintEventArgs e)
        {
            _demo.Render(e.Graphics, this.Width, this.Height);
        }
    }
}
