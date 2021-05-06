using System.Collections.Generic;

namespace GGSharpTick
{
    public sealed class TickFixed : Tick
    {
        #region Variables

        /// <summary>
        /// The configuration data for this tick
        /// </summary>
        internal readonly DataConfigTickFixed ConfigData;

        /// <summary>
        /// Accumulates delta time and processes correct number of fixed ticks
        /// </summary>
        internal float Accumulator { get; set; }
        
        #endregion Variables


        #region Constructor

        public TickFixed(DataConfigTickFixed data)
        {
            ConfigData = data;
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
            
            // Add explicit ticksets
            foreach (DataConfigTickset tick in ticksetsData)
            {
                TicksetFixed t = new TicksetFixed(tick, this);
                Ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}