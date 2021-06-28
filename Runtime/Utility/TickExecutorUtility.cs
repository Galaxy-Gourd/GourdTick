using System;
using UnityEngine;

namespace GGTickBase
{
    /// <summary>
    /// Executes ticks across existing tick instances + ticksets.
    /// </summary>
    internal static class TickExecutorUtility
    {
        /// <summary>
        /// Ticks fixed.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="ticks">The fixed ticks on which to execute.</param>
        internal static void ExecuteFixedTicks(float delta, TickFixed[] ticks)
        {
            foreach (TickFixed tick in ticks)
            {
                float tickrate = tick.ConfigData.Tickrate;
                tick.Accumulator += delta;
                tick.Accumulator = Math.Min(tick.Accumulator, tick.ConfigData.MaxDelta);
                
                // Set interpolation value
                tick.InterpolationValue = Math.Min(tick.Accumulator / tickrate, 1);

                // If we've accumulated enough time, tick
                while (tick.Accumulator >= tickrate)
                {
                    // Tick
                    ((ITick) tick).DoUpdate(tickrate);
                    tick.Accumulator -= tickrate;
                    tick.InterpolationValue = Math.Min(tick.Accumulator / tickrate, 1);
                }
            }
        }
        
        /// <summary>
        /// Ticks variable.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="tick">The variable tick on which to operate.</param>
        internal static void ExecuteVariableTick(float delta, ITick tick)
        {
            tick.DoUpdate(delta);
        }
    }
}