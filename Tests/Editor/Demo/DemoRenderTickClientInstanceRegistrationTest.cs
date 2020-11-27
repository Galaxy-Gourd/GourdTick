using GGRoot;
using GGTick;

namespace GGTests.Tick.Demo
{
    public class DemoRenderTickClientInstanceRegistrationTest : ITickVariableClient
    {
        #region Registration

        /// <summary>
        /// Registers this client with the fallback render tickset
        /// </summary>
        public void RegisterTickClient()
        {
            //Core.Tick.Register(this);
        }

        /// <summary>
        /// Unregisters this client from the fallback render tickset
        /// </summary>
        public void UnregisterTickClient()
        {
            //Core.Tick.Unregister(this);
        }

        #endregion Registration
        
        
        #region Tick

        void ITickVariableClient.Tick(float delta)
        {
            
        }

        #endregion Tick
    }
}