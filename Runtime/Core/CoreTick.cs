using System;
using GGSharpData;

namespace GGSharpTick
{
    //Inspo from: 
    //https://forum.unity.com/threads/writing-update-manager-what-should-i-know.402571/
    
    /// <summary>
    /// Implementation of ICoreTick interface
    /// </summary>
    public class CoreTick : CoreSystemBase<CoreTickSystemConfigData, ICoreSystemClientTick>, ICoreTick
    {
        #region VARIABLES
        
        // Properties
        public TickVariable[] VariableTicks { get; }
        public TickFixed[] FixedTicks { get; }
        public TimeSpan ElapsedSinceSimStartup { get; private set; }

        #endregion VARIABLES
        
        
        #region CONSTRUCTION

        public CoreTick(CoreTickSystemConfigData data, ICoreSystemClientTick systemClient = null)
            : base(data, systemClient)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create variable ticks
            VariableTicks = new TickVariable[data.VariableTicks.Length];
            for(int i = 0; i < data.VariableTicks.Length; i++)
            {
                VariableTicks[i] = new TickVariable(data.VariableTicks[i]);
            }
            
            // Create fixed ticks
            FixedTicks = new TickFixed[data.FixedTicks.Length];
            for(int i = 0; i < data.FixedTicks.Length; i++)
            {
                FixedTicks[i] = new TickFixed(data.FixedTicks[i]);
            }
        }

        public override void OnPostAllSystemsInitialized()
        {
            base.OnPostAllSystemsInitialized();
            
            _systemClient?.OnSystemTickInitialized(VariableTicks, FixedTicks);
        }

        #endregion CONSTRUCTION


        #region REGISTRATION

        void ICoreTick.Register(ITickClient obj, ITicksetInstance tickset)
        {
            tickset?.StageForAddition(obj);
        }

        void ICoreTick.Unregister(ITickClient obj, ITicksetInstance tickset)
        {
            tickset?.StageForRemoval(obj);
        }

        #endregion REGISTRATION
        

        #region SOURCE

        void ICoreTick.DoTick(float delta, TickVariable tick)
        {
            // Validate delta data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;

            // Null check
            if (tick == null)
            {
                tick = VariableTicks[0];
            }
            
            // Are we also ticking fixed step?
            if (tick.fixedStep)
            {
                ElapsedSinceSimStartup += TimeSpan.FromSeconds(delta);
                TickExecutorUtility.ExecuteFixedTicks(delta, FixedTicks);
            }
            
            TickExecutorUtility.ExecuteVariableTick(delta, tick);
        }

        #endregion SOURCE

    }
}