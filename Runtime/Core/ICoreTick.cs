using System;
using GGData;

namespace GGTick
{
    /// <summary>
    /// Describes client-facing API for core tick system
    /// </summary>
    public interface ICoreTick : ICoreSystem
    {
        #region Properties

        /// <summary>
        /// The variable ticks.
        /// </summary>
        TickVariable[] variableTicks { get; }
        
        /// <summary>
        /// List of fixed (simulation) ticks
        /// </summary>
        TickFixed[] fixedTicks { get; }
        
        /// <summary>
        /// The amount of time elapsed since the simulation began.
        /// </summary>
        TimeSpan elapsedSinceSimStartup { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// Registers a client to a tickset.
        /// </summary>
        /// <param name="obj">The client being registered.</param>
        /// <param name="tickset">The tickset to which to register the client.</param>
        void Register(ITickClient obj, ITicksetInstance tickset);

        /// <summary>
        /// Unregisters a client from a tickset.
        /// </summary>
        /// <param name="obj">The client being unregistered.</param>
        /// <param name="tickset">The tickset from which to unregister the client.</param>
        void Unregister(ITickClient obj, ITicksetInstance tickset);

        /// <summary>
        /// Ticks the core tick system with the given delta time.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick.</param>
        /// <param name="tick"></param>
        void DoTick(float delta, TickVariable tick = null);

        #endregion Methods
    }
}