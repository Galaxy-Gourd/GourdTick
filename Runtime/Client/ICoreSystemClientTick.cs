using GGData;

namespace GGTick
{
    public interface ICoreSystemClientTick : ICoreSystemClient
    {
        /// <summary>
        /// Passes tick system data on to client after initialization
        /// </summary>
        /// <param name="variableTicks"></param>
        /// <param name="fixedTicks"></param>
        void OnSystemTickInitialized(
            TickVariable[] variableTicks,
            TickFixed[] fixedTicks);
    }
}