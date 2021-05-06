namespace GGSharpTick
{
    public interface ITickset
    {
        #region PROPERTIES

        Tick tick { get; }
        
        /// <summary>
        /// How many clients are current subscribed to this tickset.
        /// </summary>
        int SubscriberCount { get; }
        
        /// <summary>
        /// The display/debug name of this tickset
        /// </summary>
        string TicksetName { get; }

        #endregion PROPERTIES


        #region METHODS

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
        void DoTick(float delta);

        #endregion METHODS
    }
}