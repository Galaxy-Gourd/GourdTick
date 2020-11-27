using GGRoot;
using GGTick;

namespace GGTests.Tick.Demo
{
    public class DemoOrderedSimulationTickClient : ITickFixedClient
    {
        #region Data

        /// <summary>
        /// We increment this value every time a render tickset is ticked, giving us a view of the tick order
        /// </summary>
        public static int tickOrderCounter;

        public readonly int targetOrder;
        public int thisOrderedEntryResult { get; private set; }

        #endregion Data
        
        
        #region Constructor

        public DemoOrderedSimulationTickClient(
            TicksetConfigData data, 
            int targetOrder)
        {
            this.targetOrder = targetOrder;
            //Core.Tick.Register(this, data);
        }

        #endregion Constructor
        
        
        #region Tick

        public void Tick(float delta)
        {
            thisOrderedEntryResult = tickOrderCounter;
            tickOrderCounter++;
        }

        #endregion Tick
    }
}