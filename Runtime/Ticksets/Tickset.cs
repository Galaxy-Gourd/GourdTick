using System.Collections.Generic;
using GG.Data.Base;

namespace GGTickBase
{
    internal class Tickset : ITickset
    {
        #region VARIABLES

        public Tick tick { get; }
        public int SubscriberCount { get; private set; }
        public string TicksetName => _ticksetData.TicksetName;
        
        private readonly DataConfigTickset _ticksetData;
        private readonly List<IComponentUpdatable> _current = new List<IComponentUpdatable>();
        private readonly List<IComponentUpdatable> _stagedForAddition = new List<IComponentUpdatable>();
        private readonly List<IComponentUpdatable> _stagedForRemoval = new List<IComponentUpdatable>();

        #endregion VARIABLES


        #region CONSTRUCTION

        public Tickset(DataConfigTickset ticksetData, Tick tick)
        {
            this.tick = tick;
            _ticksetData = ticksetData;
        }

        #endregion CONSTRUCTION
        

        #region STAGING

        void ITickset.StageForAddition(IComponentUpdatable client)
        {
            _stagedForAddition.Add(client);
        }

        void ITickset.StageForRemoval(IComponentUpdatable client)
        {
            _stagedForRemoval.Add(client);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void AddStagedTickables()
        {
            foreach (IComponentUpdatable t in _stagedForAddition)
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
            foreach (IComponentUpdatable t in _stagedForRemoval)
            {
                _current.Remove(t);
            }

            SubscriberCount = _current.Count;
            _stagedForRemoval.Clear();
        }
        
        #endregion STAGING


        #region UPDATE

        void IComponentUpdatable.DoUpdate(float delta)
        {
            // Add/remove staged ticks from group
            AddStagedTickables();
            FlushStagedTickables();
            
            // Update!
            foreach (IComponentUpdatable component in _current)
            {
                component.DoUpdate(delta);
            }
        }

        #endregion UPDATE
    }
}