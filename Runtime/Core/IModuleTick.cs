using GGSharpData;

namespace GGSharpTick
{
    /// <summary>
    /// Describes client-facing API for core tick module
    /// </summary>
    public interface IModuleTick : IModule
    {
        #region PROPERTIES

        /// <summary>
        /// Telemetry data for this ticking module
        /// </summary>
        ITelemetry<DataTelemetryTick> Telemetry { get; }

        #endregion PROPERTIES


        #region METHODS

        /// <summary>
        /// Registers a client to a tickset.
        /// </summary>
        /// <param name="obj">The client being registered.</param>
        /// <param name="tickset">The tickset to which to register the client.</param>
        void Register(IClientTickable obj, ITickset tickset);

        /// <summary>
        /// Unregisters a client from a tickset.
        /// </summary>
        /// <param name="obj">The client being unregistered.</param>
        /// <param name="tickset">The tickset from which to unregister the client.</param>
        void Unregister(IClientTickable obj, ITickset tickset);

        /// <summary>
        /// Ticks the core tick system with the given delta time.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick.</param>
        /// <param name="tick"></param>
        void DoTick(float delta, TickVariable tick = null);

        #endregion METHODS
    }
}