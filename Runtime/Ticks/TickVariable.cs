using System.Collections.Generic;

namespace GGSharpTick
{
    public class TickVariable : TickBase
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        internal bool fixedStep { get; }

        #endregion Variables
        
        
        #region Constructor

        public TickVariable(TickVariableConfigData data)
        {
            fixedStep = data.StepFixed;
            TickName = data.tickName;
            SetTicksets(data.ticksets);
        }

        #endregion Constructor


        #region Ticksets

        /// <summary>
        /// Creates and sets the ticksets to be used in this tick.
        /// </summary>
        /// <param name="ticksetsData">The data from which to create the ticksets.</param>
        private void SetTicksets(IEnumerable<TicksetConfigData> ticksetsData)
        {
            ticksets = new List<ITicksetInstance>();
            foreach (TicksetConfigData tick in ticksetsData)
            {
                TicksetVariable t = new TicksetVariable(tick, this);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}