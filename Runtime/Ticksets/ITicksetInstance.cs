namespace GGSharpTick
{
    public interface ITicksetInstance
    {
        #region Properties

        TickBase tick { get; }
        
        /// <summary>
        /// How many clients are current subscribed to this tickset.
        /// </summary>
        public int subscriberCount { get; }
        
        /// <summary>
        /// The display/debug name of this tickset
        /// </summary>
        public string ticksetName { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        void StageForAddition(ITickClient client);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        void StageForRemoval(ITickClient client);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        void Tick(float delta);

        #endregion Methods
    }
}