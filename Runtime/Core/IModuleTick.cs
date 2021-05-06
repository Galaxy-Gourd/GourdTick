using System;
using GGSharpData;

namespace GGSharpTick
{
    /// <summary>
    /// Describes client-facing API for core tick module
    /// </summary>
    public interface IModuleTick : IModule
    {
        #region Properties

        /// <summary>
        /// The variable ticks.
        /// </summary>
        TickVariable[] VariableTicks { get; }
        
        /// <summary>
        /// List of fixed (simulation) ticks
        /// </summary>
        TickFixed[] FixedTicks { get; }
        
        /// <summary>
        /// The amount of time elapsed since the simulation began.
        /// </summary>
        TimeSpan TimeElapsed { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// Registers a client to a tickset.
        /// </summary>
        /// <param name="obj">The client being registered.</param>
        /// <param name="tickset">The tickset to which to register the client.</param>
        void Register(ITickClient obj, ITickset tickset);

        /// <summary>
        /// Unregisters a client from a tickset.
        /// </summary>
        /// <param name="obj">The client being unregistered.</param>
        /// <param name="tickset">The tickset from which to unregister the client.</param>
        void Unregister(ITickClient obj, ITickset tickset);

        /// <summary>
        /// Ticks the core tick system with the given delta time.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick.</param>
        /// <param name="tick"></param>
        void DoTick(float delta, TickVariable tick = null);

        #endregion Methods
    }
}