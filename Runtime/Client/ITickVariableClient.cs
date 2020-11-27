namespace GGTick
{
    public interface ITickVariableClient : ITickClient
    {
        /// <summary>
        /// Ticks the variable client.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick for this variable tickset.</param>
        void Tick(float delta);
    }
}