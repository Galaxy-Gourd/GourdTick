namespace GG.Tick.Base
{
    public interface IClientTickableFixed : IClientTickable
    {
        #region METHODS

        /// <summary>
        /// Ticks the fixed client.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick for this fixed tickset.</param>
        void Tick(float delta);

        #endregion METHODS
    }
}                    