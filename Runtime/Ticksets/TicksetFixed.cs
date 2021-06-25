namespace GG.Tick.Base
{
    internal class TicksetFixed: Tickset
    {
        #region CONSTRUCTION

        internal TicksetFixed(DataConfigTickset data, TickFixed t)
        {
            _ticksetData = data;
            tick = t;
        }

        #endregion CONSTRUCTION
        
        
        #region TICK

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public override void DoTick(float delta)
        {
            base.DoTick(delta);
            foreach (IClientTickable tickClient in _current)
            {
                IClientTickableFixed obj = (IClientTickableFixed) tickClient;
                obj.Tick(delta);
            }
        }

        #endregion TICK
    }
}