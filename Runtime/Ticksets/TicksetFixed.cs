namespace GGTick
{
    public class TicksetFixed: TicksetBase
    {
        #region Constructor

        public TicksetFixed(TicksetConfigData data, TickFixed t)
        {
            TicksetData = data;
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
            foreach (var tickClient in _current)
            {
                var obj = (ITickFixedClient) tickClient;
                obj.Tick(delta);
            }
        }

        #endregion
    }
}