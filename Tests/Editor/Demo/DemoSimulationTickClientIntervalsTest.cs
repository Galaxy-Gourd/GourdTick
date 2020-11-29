using GGSharpTick;

namespace GGTests.Tick.Demo
{
    public class DemoSimulationTickClientIntervalsTest : ITickFixedClient
    {
        #region Properties

        public int ticksSinceLastCheck { get; set; }

        #endregion Properties
        

        #region Tick

        void ITickFixedClient.Tick(float delta)
        {
            ticksSinceLastCheck++;
        }

        #endregion Tick
    }
}