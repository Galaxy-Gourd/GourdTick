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
    public class ModuleTick : Module<DataConfigModuleTick, DataModuleInitializationTick>, IModuleTick
    {
        #region VARIABLES
        
        // Properties
        public ITelemetry<DataTelemetryTick> Telemetry { get; }        
        internal TimeSpan TimeElapsed { get; private set; }

        private readonly TickVariable _tick;
        private readonly TickFixed[] _fixedTicks;

        #endregion VARIABLES
        
        
        #region CONSTRUCTION

        public ModuleTick(
            DataConfigModuleTick data,
            Action<DataModuleInitializationTick>[] callbacks = null)
            : base(data, callbacks)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create variable tick for this module
            _tick = new TickVariable(data.Tick);

            // Create fixed ticks
            _fixedTicks = new TickFixed[data.FixedTicks.Length];
            for (int i = 0; i < data.FixedTicks.Length; i++)
            {
                _fixedTicks[i] = new TickFixed(data.FixedTicks[i]);
            }

            // Create telemetry module
            Telemetry = new TelemetryTick();

            // We need to tell Unity-side that we're finished over here
            DispatchModuleInitializationCallbacks(new DataModuleInitializationTick
            {
                Module = this,
                Tick = _tick,
                FixedTicks = _fixedTicks
            });
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
            TickExecutorUtility.ExecuteFixedTicks(delta, _fixedTicks);
            TickExecutorUtility.ExecuteVariableTick(delta, _tick);
            
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