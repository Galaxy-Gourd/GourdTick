using System;

namespace GGTick
{
    /// <summary>
    /// Executes ticks across existing tick instances + ticksets.
    /// </summary>
    public static class TickExecutorUtility
    {
        /// <summary>
        /// Ticks simulation.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="simulationTicks">The simulation ticks on which to execute.</param>
        public static void ExecuteSimulationTicks(float delta, TickSimulation[] simulationTicks)
        {
            foreach (TickSimulation simTick in simulationTicks)
            {
                float tickrate = simTick.configData.tickrate;
                simTick.accumulator += delta;
                simTick.accumulator = Math.Min(simTick.accumulator, simTick.configData.maxDelta);

                while (simTick.accumulator >= tickrate)
                {
                    // Tick
                    simTick.accumulator -= tickrate;
                    ((ITickInstance<ITickSimulationClient>) simTick).Tick(tickrate);
                }
            }
        }
        
        /// <summary>
        /// Ticks rendering.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="renderTick">The render tick on which to operate.</param>
        public static void ExecuteRenderTicks(
            float delta, 
            ITickInstance<ITickRenderClient> renderTick)
        {
            renderTick.Tick(delta);
        }
    }
}