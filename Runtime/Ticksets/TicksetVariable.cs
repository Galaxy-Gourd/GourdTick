namespace GG.Tick.Base
{
    internal class TicksetVariable : Tickset
    {
        #region Constructor

        public TicksetVariable(DataConfigTickset data, TickVariable t)
        {
            _ticksetData = data;
            tick = t;
        }

        #endregion Constructor
        
        
        #region Tick

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public override void DoTick(float delta)
        {
            base.DoTick(delta);
            foreach (var tickClient in _current)
            {
                var obj = (IClientTickableVariable) tickClient;
                obj.Tick(delta);
            }
        }

        #endregion
    }
}