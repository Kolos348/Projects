using DemoSystem.Library;
using DemoSystem.Library.Effects;
using System.Runtime.InteropServices;

namespace DemoSystem.Factories
{
    internal class MyDemoFactory : IDemoFactory
    {
        public Demo CreateDemo()
        {
            var demo = new Demo();

            var moment1 = new Moment(frameCount: 100);
            var moment2 = new Moment(frameCount: 50);
            var moment3 = new Moment(frameCount: 100);
            var moment4 = new Moment(frameCount: 100);
            var moment5 = new Moment(frameCount: 100);
            moment1.AddEffect(new BallSimulation(
                count: 200,          // number of balls
                width: 1366,         // canvas width
                height: 718         // canvas height
            ));
            moment2.AddEffect(new FloatingArrowEffect(Color.Green, amplitude: 80));
            moment2.AddEffect(new FloatingArrowEffect(Color.Blue, amplitude: 160));
            moment2.AddEffect(new FloatingArrowEffect(Color.Red,amplitude:0));
            moment3.AddEffect(new ParticleBombEffect(50000,160,20));
            moment4.AddEffect(new WaveEffect(Color.Blue));
            moment5.AddEffect(new FrequencyLineEffect(Color.Black));
            demo.AddMoment(moment5);
            demo.AddMoment(moment1);
            demo.AddMoment(moment2);
            demo.AddMoment(moment3);
            demo.AddMoment(moment4);

            return demo;
        }
    }
}
