using GGSharpTick;

namespace GGTests.Tick.Demo
{
    public class DemoOrderedFixedTickClient : ITickClientFixed
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

        public DemoOrderedFixedTickClient(ITicksetInstance tickset, int targetOrder)
        {
            this.targetOrder = targetOrder;
            TickSystemTestsInstaller.TestTick.Register(this, tickset);
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