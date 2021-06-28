using System.Collections.Generic;
using GGDataBase;

namespace GGTickBase
{
    public abstract class Tick : System<ITickset>, ITick
    {
        #region VARIABLES

        public uint TickCount { get; private set; }
        public float TicksPerSecond { get; private set; }
        public string TickName { get; protected set; }
        public float InterpolationValue { get; internal set; }

        private const float CONST_tpsUpdateRate = 4.0f;
        private int _tpsFrameCount;
        private float _tpsAccumDelta;

        #endregion VARIABLES


        #region TICK
        
        void IComponentUpdatable.DoUpdate(float delta)
        {
            foreach (ITickset ticksetInstance in _components)
            {
                ticksetInstance.DoUpdate(delta);
            }
            
            CalculateTPS(delta);
            TickCount++;
        }

        /// <summary>
        /// Calculates the approximate realtime frames per second (tps) for this tick.
        /// </summary>
        /// <param name="delta">Time elapsed (seconds) since previous tick.</param>
        private void CalculateTPS(float delta)
        {
            _tpsFrameCount++;
            _tpsAccumDelta += delta;
            if (_tpsAccumDelta > 1.0f / CONST_tpsUpdateRate)
            {
                TicksPerSecond = _tpsFrameCount / _tpsAccumDelta;
                _tpsFrameCount = 0;
                _tpsAccumDelta -= 1.0f / CONST_tpsUpdateRate;
            }
        }
        
        #endregion TICK
        
        
        #region TICKSETS

        /// <summary>
        /// Creates and sets the ticksets to be used in this tick.
        /// </summary>
        /// <param name="ticksetsData">The data from which to create the ticksets.</param>
        protected void SetTicksets(IEnumerable<DataConfigTickset> ticksetsData)
        {
            // Add explicit ticksets
            foreach (DataConfigTickset tick in ticksetsData)
            {
                Tickset t = new Tickset(tick, this);
                AddComponent(t);
            }
        }

        #endregion TICKSETS
    }
}