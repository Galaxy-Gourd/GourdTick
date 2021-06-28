using System.Collections.Generic;
using GG.Data.Base;

namespace GGTickBase
{
    /// <summary>
    /// Defines contract for an instance of a TickBase (TickRender or TickSimulation) 
    /// </summary>
    public interface ITick : IComponentUpdatable
    {
        #region PROPERTIES

        /// <summary>
        /// How many ticks have been executed thus far.
        /// </summary>
        uint TickCount { get; }
        
        /// <summary>
        /// FPS of this tick
        /// </summary>
        float TicksPerSecond { get; }
        
        /// <summary>
        /// The name of the tick
        /// </summary>
        string TickName { get; }
        
        /// <summary>
        /// 
        /// </summary>
        float InterpolationValue { get; }

        #endregion PROPERTIES
    }
}