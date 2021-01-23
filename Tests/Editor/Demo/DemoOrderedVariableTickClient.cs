using GGSharpTick;

namespace GGTests.Tick.Demo
{
    public class DemoOrderedVariableTickClient : ITickClientVariable
    {
        #region Data

        /// <summary>
        /// We increment this value every time a render tickset is ticked, giving us a view of the tick order
        /// </summary>
        public static int tickOrderCounter;
        
        public int targetOrder { get; }
        public int thisOrderedEntryResult { get; private set; }

        #endregion Data
        
        
        #region Constructor

        public DemoOrderedVariableTickClient(ITicksetInstance tickset, int orderedInstance)
        {
            targetOrder = orderedInstance;
            TickSystemTestsInstaller.TestTick.Register(this, tickset);
        }

        #endregion Constructor


        #region Tick

        void ITickClientVariable.Tick(float delta)
        {
            thisOrderedEntryResult = tickOrderCounter;
            tickOrderCounter++;
        }

        #endregion Tick
        
    }
}