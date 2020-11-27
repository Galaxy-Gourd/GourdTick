using System.Collections.Generic;

namespace GGTick
{
    public abstract class TicksetBase : ITicksetInstance
    {
        #region Variables

        public TickBase tick { get; protected set; }
        
        // Tickset data
        public TicksetConfigData TicksetData { get; protected set; }

        /// <summary>
        /// The list of current clients subscribed to this tickset.
        /// </summary>
        protected readonly List<ITickClient> _current = new List<ITickClient>();

        public int subscriberCount { get; private set; }

        public string ticksetName => TicksetData.ticksetName;
        
        /// <summary>
        /// 
        /// </summary>
        private readonly List<ITickClient> _stagedForAddition = new List<ITickClient>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly List<ITickClient> _stagedForRemoval = new List<ITickClient>();

        #endregion Variables
        

        #region Tick

        void ITicksetInstance.StageForAddition(ITickClient client)
        {
            _stagedForAddition.Add(client);
        }

        void ITicksetInstance.StageForRemoval(ITickClient client)
        {
            _stagedForRemoval.Add(client);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void AddStagedTickables()
        {
            foreach (ITickClient t in _stagedForAddition)
            {
                _current.Add(t);
            }

            subscriberCount = _current.Count;
            _stagedForAddition.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FlushStagedTickables()
        {
            foreach (ITickClient t in _stagedForRemoval)
            {
                _current.Remove(t);
            }

            subscriberCount = _current.Count;
            _stagedForRemoval.Clear();
        }

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public virtual void Tick(float delta)
        {
            // Add/remove staged ticks from group
            AddStagedTickables();
            FlushStagedTickables();
        }

        #endregion Tick
    }
}