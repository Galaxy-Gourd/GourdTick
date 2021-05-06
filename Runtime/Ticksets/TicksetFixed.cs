namespace GGSharpTick
{
    public class TicksetFixed: Tickset
    {
        #region Constructor

        public TicksetFixed(DataConfigTickset data, TickFixed t)
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
                var obj = (ITickClientFixed) tickClient;
                obj.Tick(delta);
            }
        }

        #endregion
    }
}