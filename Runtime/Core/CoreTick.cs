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
        #region Properties
        
        public TickVariable[] variableTicks { get; }
        public TickFixed[] fixedTicks { get; }
        public TimeSpan elapsedSinceSimStartup { get; private set; }
        
        #endregion Properties
        
        
        #region Construction

        public CoreTick(CoreTickSystemConfigData data, ICoreSystemClientTick systemClient = null)
            : base(data, systemClient)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create variable ticks
            variableTicks = new TickVariable[data.variableTicks.Length];
            for(int i = 0; i < data.variableTicks.Length; i++)
            {
                variableTicks[i] = new TickVariable(data.variableTicks[i]);
            }
            
            // Create fixed ticks
            fixedTicks = new TickFixed[data.fixedTicks.Length];
            for(int i = 0; i < data.fixedTicks.Length; i++)
            {
                fixedTicks[i] = new TickFixed(data.fixedTicks[i]);
            }
        }

        public override void OnPostCoreSystemsInitialization()
        {
            base.OnPostCoreSystemsInitialization();
            
            _systemClient?.OnSystemTickInitialized(variableTicks, fixedTicks);
        }

        #endregion Construction


        #region Registration

        void ICoreTick.Register(ITickClient obj, ITicksetInstance tickset)
        {
            tickset?.StageForAddition(obj);
        }

        void ICoreTick.Unregister(ITickClient obj, ITicksetInstance tickset)
        {
            tickset?.StageForRemoval(obj);
        }

        #endregion Registration
        

        #region Source

        void ICoreTick.DoTick(float delta, TickVariable tick)
        {
            // Validate delta data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;

            // Null check
            if (tick == null)
            {
                tick = variableTicks[0];
            }
            
            // Are we also ticking fixed step?
            if (tick.fixedStep)
            {
                elapsedSinceSimStartup += TimeSpan.FromSeconds(delta);
                TickExecutorUtility.ExecuteFixedTicks(delta, fixedTicks);
            }
            
            TickExecutorUtility.ExecuteVariableTick(delta, tick);
        }

        #endregion Source
    }
}