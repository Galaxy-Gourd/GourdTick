using GGTick;

namespace GGTests.Tick.Demo
{
    public class DemoSimulationTickClientIntervalsTest : ITickSimulationClient
    {
        #region Properties

        public int ticksSinceLastCheck { get; set; }

        #endregion Properties
        

        #region Tick

        void ITickSimulationClient.Tick(float delta)
        {
            ticksSinceLastCheck++;
        }

        #endregion Tick
    }
}