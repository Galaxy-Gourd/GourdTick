using System.Collections.Generic;

namespace GGSharpTick
{
    public abstract class Tickset : ITickset
    {
        #region Variables

        public Tick tick { get; protected set; }
        public int SubscriberCount { get; private set; }
        public string TicksetName => _ticksetData.TicksetName;
        
        protected DataConfigTickset _ticksetData { get; set; }

        /// <summary>
        /// The list of current clients subscribed to this tickset.
        /// </summary>
        protected readonly List<ITickClient> _current = new List<ITickClient>();
        
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

        void ITickset.StageForAddition(ITickClient client)
        {
            _stagedForAddition.Add(client);
        }

        void ITickset.StageForRemoval(ITickClient client)
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

            SubscriberCount = _current.Count;
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

            SubscriberCount = _current.Count;
            _stagedForRemoval.Clear();
        }

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public virtual void DoTick(float delta)
        {
            // Add/remove staged ticks from group
            AddStagedTickables();
            FlushStagedTickables();
        }

        #endregion Tick
    }
}