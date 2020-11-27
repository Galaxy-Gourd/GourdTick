using GGRoot;
using GGTick;

namespace GGTests.Tick.Demo
{
    public class DemoSimulationTickClientInstanceRegistrationTest : ITickFixedClient
    {
        #region Registration

        /// <summary>
        /// Registers this client with the fallback simulation tickset
        /// </summary>
        public void RegisterTickClient()
        {
            //Core.Tick.Register(this);
        }

        /// <summary>
        /// Unregisters this client from the fallback simulation tickset
        /// </summary>
        public void UnregisterTickClient()
        {
            //Core.Tick.Unregister(this);
        }

        #endregion Registration
        
        
        #region Tick

        void ITickFixedClient.Tick(float delta)
        {
            
        }

        #endregion Tick
    }
}