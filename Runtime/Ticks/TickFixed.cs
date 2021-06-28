namespace GGTickBase
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
    }
}