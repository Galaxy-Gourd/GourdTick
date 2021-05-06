using System.Collections.Generic;

namespace GGSharpTick
{
    public abstract class Tick : ITick
    {
        #region Properties

        public List<ITickset> Ticksets { get; protected set; }
        public uint TickCount { get; private set; }
        public float TicksPerSecond { get; private set; }
        public string TickName { get; protected set; }
        public float InterpolationValue { get; internal set; }

        #endregion Properties


        #region Variables

        private const float CONST_tpsUpdateRate = 4.0f;
        private int _tpsFrameCount;
        private float _tpsAccumDelta;

        #endregion Variables


        #region Tick
        
        void ITick.DoTick(float delta)
        {
            foreach (ITickset ticksetInstance in Ticksets)
            {
                ticksetInstance.DoTick(delta);
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
        
        #endregion Tick
    }
}