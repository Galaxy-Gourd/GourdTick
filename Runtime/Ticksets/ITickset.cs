using GG.Data.Base;

namespace GGTickBase
{
    public interface ITickset : IComponentUpdatable
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
        void StageForAddition(IComponentUpdatable client);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        void StageForRemoval(IComponentUpdatable client);

        #endregion METHODS
    }
}