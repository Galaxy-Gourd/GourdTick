using System;
using System.Collections.Generic;
using GGDataBase;

namespace GGTickBase
{
    public class DataTelemetryTick : DataTelemetry
    {
        #region DATA

        /// <summary>
        /// Time elapsed in the tick system
        /// </summary>
        public KeyValuePair<string, TimeSpan> TimeElapsed;

        #endregion DATA
    }
}