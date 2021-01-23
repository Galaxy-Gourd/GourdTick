using GGSharpTick;

namespace GGTests.Tick.Demo
{
    public class DemoSimulationTickClientIntervalsTest : ITickClientFixed
    {
        #region Properties

        public int ticksSinceLastCheck { get; set; }

        #endregion Properties
        

        #region Tick

        void ITickClientFixed.Tick(float delta)
        {
            ticksSinceLastCheck++;
        }

        #endregion Tick
    }
}