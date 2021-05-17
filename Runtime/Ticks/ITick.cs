using System.Collections.Generic;

namespace GG.Tick.Base
{
    /// <summary>
    /// Defines contract for an instance of a TickBase (TickRender or TickSimulation) 
    /// </summary>
    public interface ITick
    {
        #region PROPERTIES

        /// <summary>
        /// The ticksets that belong to this tick
        /// </summary>
        List<ITickset> Ticksets { get; }

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


        #region METHODS

        /// <summary>
        /// Executes a tick of the tick instance (ticks every tickset).
        /// </summary>
        /// <param name="delta">Time elapsed (seconds) since previous tick.</param>
        void DoTick(float delta);
        
        #endregion METHODS
    }
}