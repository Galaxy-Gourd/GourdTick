using System;

namespace GGTickBase
{
    /// <summary>
    /// Helps validate tick system data.
    /// </summary>
    internal static class CoreTickValidationUtility
    {
        /// <summary>
        /// Ensures that valid data is passed into the core tick system.
        /// </summary>
        /// <param name="data">The data to validate.</param>
        /// <returns>True if validation passes.</returns>
        /// <exception cref="NullReferenceException">Throws when any part of tick system data is null.</exception>
        internal static bool ValidateCoreTickSystemConfigData(DataConfigModuleTick data)
        {
            if (data == null)
            {
                throw new NullReferenceException(
                    "Core tick system data cannot be null!");
            }
            
            if (data.Tick == null)
            {
                throw new NullReferenceException(
                    "Core tick system variable tick data cannot be null!");
            }

            if (data.FixedTicks == null)
            {
                throw new NullReferenceException(
                    "Core tick system fixed ticks data cannot be null!");
            }

            return true;
        }

        /// <summary>
        /// Validates delta time value.
        /// </summary>
        /// <param name="delta">The given delta interval.</param>
        /// <returns>True if validation passes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when validation fails.</exception>
        internal static bool ValidateDeltaInterval(double delta)
        {
            if (delta <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(delta), 
                    "Tick delta intervals must be greater than zero! Interval was:\n" 
                    + delta);
            }
            return true;
        }
    }
}