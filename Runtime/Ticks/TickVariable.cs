using System.Collections.Generic;

namespace GG.Tick.Base
{
    public sealed class TickVariable : Tick
    {
        #region CONSTRUCTION

        public TickVariable(DataConfigTickVariable data)
        {
            TickName = data.TickName;
            SetTicksets(data.Ticksets);
        }

        #endregion CONSTRUCTION


        #region TICKSETS

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

        #endregion TICKSETS
    }
}