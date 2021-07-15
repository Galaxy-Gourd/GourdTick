using System;
using GGDataBase;
using UnityEngine;

namespace GGTickBase
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
        public TickVariable Tick { get; }
        public TickFixed[] FixedTicks { get; }
        public ITelemetry<DataTelemetryTick> Telemetry { get; }        
        internal TimeSpan TimeElapsed { get; private set; }
        
        #endregion VARIABLES
        
        
        #region CONSTRUCTION

        public ModuleTick(DataConfigModuleTick data) : base(data)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create variable tick for this module
            Tick = new TickVariable(data.Tick);

            // Create fixed ticks
            FixedTicks = new TickFixed[data.FixedTicks.Length];
            for (int i = 0; i < data.FixedTicks.Length; i++)
            {
                FixedTicks[i] = new TickFixed(data.FixedTicks[i]);
            }

            // Create telemetry module
            Telemetry = new TelemetryTick();
        }

        #endregion CONSTRUCTION
        
        
        #region TICK

        public override void DoUpdate(float delta)
        {
            base.DoUpdate(delta);
            
            // Validate delta interval data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;

            // We drive the execution of fixed ticks using this tick as the driving clock
            TimeElapsed += TimeSpan.FromSeconds(delta);
            TickExecutorUtility.ExecuteFixedTicks(delta, FixedTicks);
            TickExecutorUtility.ExecuteVariableTick(delta, Tick);
            
            // Update telemetry
            (Telemetry as TelemetryTick).Broadcast(this);
        }

        #endregion TICK


        #region REGISTRATION

        void IModuleTick.Register(IComponentUpdatable obj, ITickset tickset)
        {
            tickset?.StageForAddition(obj);
        }

        void IModuleTick.Unregister(IComponentUpdatable obj, ITickset tickset)
        {
            tickset?.StageForRemoval(obj);
        }

        #endregion REGISTRATION
    }
}