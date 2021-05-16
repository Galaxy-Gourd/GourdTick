using System;
using GGSharpData;

namespace GGSharpTick
{
    //Inspo from: 
    //https://forum.unity.com/threads/writing-update-manager-what-should-i-know.402571/
    
    /// <summary>
    /// Implementation of ICoreTick interface
    /// </summary>
    public class ModuleTick : Module<DataConfigModuleTick>, IModuleTick
    {
        #region VARIABLES
        
        // Properties
        public ITelemetry<DataTelemetryTick> Telemetry { get; }
        internal TickVariable[] VariableTicks { get; }
        internal TickFixed[] FixedTicks { get; }
        internal TimeSpan TimeElapsed { get; private set; }

        #endregion VARIABLES
        
        
        #region CONSTRUCTION

        public ModuleTick(DataConfigModuleTick data, IModuleClientTick client = null) : base(data)
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
            
            // Create telemetry module
            Telemetry = new TelemetryTick();
            
            // Tell the client we're finished
            client?.OnModuleInitialized(new DataModuleInitializationTick
            {
                Module = this,
                FixedTicks = this.FixedTicks,
                VariableTicks = this.VariableTicks
            });
        }

        #endregion CONSTRUCTION


        #region REGISTRATION

        void IModuleTick.Register(IClientTickable obj, ITickset tickset)
        {
            tickset?.StageForAddition(obj);
        }

        void IModuleTick.Unregister(IClientTickable obj, ITickset tickset)
        {
            tickset?.StageForRemoval(obj);
        }

        #endregion REGISTRATION
        

        #region SOURCE

        void IModuleTick.DoTick(float delta, TickVariable tick)
        {
            // Validate delta interval data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;

            if (tick == null)
            {
                tick = VariableTicks[0];
            }
            
            // We can optionally drive the execution of fixed ticks using this tick as the driving clock
            if (tick.FixedStep)
            {
                TimeElapsed += TimeSpan.FromSeconds(delta);
                TickExecutorUtility.ExecuteFixedTicks(delta, FixedTicks);
            }
            
            TickExecutorUtility.ExecuteVariableTick(delta, tick);
            
            // Update telemetry
            (Telemetry as TelemetryTick).Broadcast(this);
        }

        #endregion SOURCE
    }
}