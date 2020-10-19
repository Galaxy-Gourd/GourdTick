namespace GGTick
{
    public class TicksetSimulation: TicksetBase<ITickSimulationClient>
    {
        #region Constructor

        public TicksetSimulation(TicksetConfigData data, TickSimulation t)
        {
            ticksetData = data;
            tick = t;
        }

        #endregion Constructor
        
        
        #region Tick

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public override void Tick(float delta)
        {
            base.Tick(delta);
            foreach (ITickSimulationClient obj in _current)
            {
                obj.Tick(delta);
            }
        }

        #endregion
    }
}