using System.Collections.Generic;

namespace GGTick
{
    public sealed class TickSimulation : TickBase<ITickSimulationClient>
    {
        #region Variables
        
        /// <summary>
        /// The configuration data for this tick
        /// </summary>
        internal readonly TickSimulationConfigData configData;

        /// <summary>
        /// Accumulates delta time and processes correct number of sim ticks in AppTick.
        /// </summary>
        internal float accumulator { get; set; }
        
        #endregion Variables


        #region Constructor

        public TickSimulation(TickSimulationConfigData data)
        {
            configData = data;
            tickLabel = data.tickName;
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
            ticksets = new List<TicksetBase<ITickSimulationClient>>();
            
            // Add explicit ticksets
            foreach (TicksetConfigData tick in ticksetsData)
            {
                TicksetSimulation t = new TicksetSimulation(tick, this);
                ticksets.Add(t);
            }
            
            // Add interpolation tickset last
            TicksetConfigData interpData = new TicksetConfigData
            {
                ticksetName = tickLabel + "_interp"
            };
            TicksetSimulation interp = new TicksetSimulation(interpData, this);
            ticksets.Add(interp);
        }

        #endregion Ticksets
    }
}