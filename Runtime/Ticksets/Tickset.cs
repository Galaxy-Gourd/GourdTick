using System.Collections.Generic;

namespace GG.Tick.Base
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
        protected readonly List<IClientTickable> _current = new List<IClientTickable>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly List<IClientTickable> _stagedForAddition = new List<IClientTickable>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly List<IClientTickable> _stagedForRemoval = new List<IClientTickable>();

        #endregion Variables
        

        #region Tick

        void ITickset.StageForAddition(IClientTickable client)
        {
            _stagedForAddition.Add(client);
        }

        void ITickset.StageForRemoval(IClientTickable client)
        {
            _stagedForRemoval.Add(client);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void AddStagedTickables()
        {
            foreach (IClientTickable t in _stagedForAddition)
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
            foreach (IClientTickable t in _stagedForRemoval)
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