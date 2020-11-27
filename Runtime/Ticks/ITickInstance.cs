using System.Collections.Generic;

namespace GGTick
{
    /// <summary>
    /// Defines contract for an instance of a TickBase (TickRender or TickSimulation) 
    /// </summary>
    public interface ITickInstance
    {
        #region Properties

        /// <summary>
        /// The ticksets that belong to this tick
        /// </summary>
        List<ITicksetInstance> ticksets { get; }

        /// <summary>
        /// How many ticks have been executed thus far.
        /// </summary>
        uint tickCount { get; }
        
        /// <summary>
        /// FPS of this tick
        /// </summary>
        float ticksPerSecond { get; }
        
        /// <summary>
        /// The name of the tick
        /// </summary>
        string TickName { get; }
        
        /// <summary>
        /// 
        /// </summary>
        float InterpolationValue { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// Executes a tick of the tick instance (ticks every tickset).
        /// </summary>
        /// <param name="delta">Time elapsed (seconds) since previous tick.</param>
        void Tick(float delta);
        
        #endregion Methods
    }
}