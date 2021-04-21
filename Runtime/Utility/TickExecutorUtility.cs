using System;

namespace GGSharpTick
{
    /// <summary>
    /// Executes ticks across existing tick instances + ticksets.
    /// </summary>
    public static class TickExecutorUtility
    {
        /// <summary>
        /// Ticks fixed.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="ticks">The fixed ticks on which to execute.</param>
        public static void ExecuteFixedTicks(float delta, TickFixed[] ticks)
        {
            foreach (TickFixed tick in ticks)
            {
                float tickrate = tick.configData.Tickrate;
                tick.accumulator += delta;
                tick.accumulator = Math.Min(tick.accumulator, tick.configData.MaxDelta);
                
                // Set interpolation value
                tick.InterpolationValue = Math.Min(tick.accumulator / tick.configData.Tickrate, 1);

                // If we've accumulated enough time, tick
                while (tick.accumulator >= tickrate)
                {
                    // Tick
                    ((ITickInstance) tick).Tick(tickrate);
                    tick.accumulator -= tickrate;
                    tick.InterpolationValue = Math.Min(tick.accumulator / tick.configData.Tickrate, 1);
                }
            }
        }
        
        /// <summary>
        /// Ticks variable.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="tick">The variable tick on which to operate.</param>
        public static void ExecuteVariableTick(float delta, ITickInstance tick)
        {
            tick.Tick(delta);
        }
    }
}