using System.Collections.Generic;

namespace GGSharpTick
{
    public sealed class TickFixed : TickBase
    {
        #region Variables

        /// <summary>
        /// The configuration data for this tick
        /// </summary>
        internal readonly TickFixedConfigData configData;

        /// <summary>
        /// Accumulates delta time and processes correct number of fixed ticks
        /// </summary>
        internal float accumulator { get; set; }
        
        #endregion Variables


        #region Constructor

        public TickFixed(TickFixedConfigData data)
        {
            configData = data;
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
            
            // Add explicit ticksets
            foreach (TicksetConfigData tick in ticksetsData)
            {
                TicksetFixed t = new TicksetFixed(tick, this);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}