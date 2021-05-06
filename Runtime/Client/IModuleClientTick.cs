using GGSharpData;

namespace GGSharpTick
{
    public interface IModuleClientTick : IModuleClient
    {
        /// <summary>
        /// Passes tick system data on to client after initialization
        /// </summary>
        /// <param name="variableTicks"></param>
        /// <param name="fixedTicks"></param>
        void OnModuleInitialized(TickVariable[] variableTicks, TickFixed[] fixedTicks);
    }
}