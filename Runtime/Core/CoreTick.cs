using System;
using System.Collections.Generic;
using SharpCore;

namespace GGTick
{
    //Inspo from: 
    //https://forum.unity.com/threads/writing-update-manager-what-should-i-know.402571/
    
    /// <summary>
    /// Implementation of ICoreTick interface
    /// </summary>
    public class CoreTick : CoreSystemBase<CoreTickSystemConfigData>, ICoreTick
    {
        #region Properties

        public ITickInstance<ITickRenderClient> renderTick { get; }
        public ITickInstance<ITickRenderClient> lateRenderTick { get; }
        public TickSimulation[] simulationTicks { get; }
        public TimeSpan elapsedSinceSimStartup { get; private set; }
        
        #endregion Properties
        
        
        #region Construction

        public CoreTick(CoreTickSystemConfigData data) : base(data)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create default render tick
            renderTick = new TickRender(data.renderTicksets);
            
            // Create late render tick
            lateRenderTick = new TickRender(data.lateRenderTicksets);
            
            // Create simulation ticks and add to lists
            List<TickSimulation> fixedList = new List<TickSimulation>();
            foreach (TickSimulationConfigData sim in data.simulationTicks)
            {
                fixedList.Add(new TickSimulation(sim));
            }
            simulationTicks = fixedList.ToArray();
        }
 
        #endregion Construction


        #region Registration

        void ICoreTick.Register(ITickSimulationClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickSimulationClient> s = 
                TicksetMatchUtility.GetSimulationTickset(tickset, simulationTicks);
            s.stagedForAddition.Add(obj);
        }
        
        void ICoreTick.Register(ITickRenderClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, renderTick);
            s.stagedForAddition.Add(obj);
        }

        void ICoreTick.Register(ITickLateRenderClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, lateRenderTick);
            s.stagedForAddition.Add(obj);
        }

        void ICoreTick.Unregister(ITickSimulationClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickSimulationClient> s = 
                TicksetMatchUtility.GetSimulationTickset(tickset, simulationTicks);
            s.stagedForRemoval.Add(obj);
        }

        void ICoreTick.Unregister(ITickRenderClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, renderTick);
            s.stagedForRemoval.Add(obj);
        }

        void ICoreTick.Unregister(ITickLateRenderClient obj, TicksetConfigData tickset)
        {
            TicksetBase<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, lateRenderTick);
            s.stagedForRemoval.Add(obj);
        }

        #endregion Registration
        

        #region Source

        void ICoreTick.OnUpdate(float delta)
        {
            // Validate delta data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;
            
            // Update total elapsed time
            elapsedSinceSimStartup += TimeSpan.FromSeconds(delta);
            
            // Tick simulations
            TickExecutorUtility.ExecuteSimulationTicks(delta, simulationTicks);
            
            // Tick renders
            TickExecutorUtility.ExecuteRenderTicks(delta, renderTick);
        }

        void ICoreTick.OnLateUpdate(float delta)
        {
            // Validate delta data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;
            
            // Tick late renders
            TickExecutorUtility.ExecuteRenderTicks(delta, lateRenderTick);
        }

        #endregion Source
    }
}