using System.Collections.Generic;

namespace GGSharpTick
{
    public sealed class TickVariable : Tick
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        internal bool FixedStep { get; }

        #endregion Variables
        
        
        #region Constructor

        public TickVariable(DataConfigTickVariable data)
        {
            FixedStep = data.StepFixed;
            TickName = data.TickName;
            SetTicksets(data.Ticksets);
        }

        #endregion Constructor


        #region Ticksets

        /// <summary>
        /// Creates and sets the ticksets to be used in this tick.
        /// </summary>
        /// <param name="ticksetsData">The data from which to create the ticksets.</param>
        private void SetTicksets(IEnumerable<DataConfigTickset> ticksetsData)
        {
            Ticksets = new List<ITickset>();
            foreach (DataConfigTickset tick in ticksetsData)
            {
                TicksetVariable t = new TicksetVariable(tick, this);
                Ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}