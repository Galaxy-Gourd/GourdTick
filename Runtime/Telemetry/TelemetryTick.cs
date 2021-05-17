using System;
using System.Collections.Generic;
using GG.Data.Base;

namespace GG.Tick.Base
{
    internal class TelemetryTick : Telemetry<ModuleTick, DataTelemetryTick>
    {
        #region FORMAT

        protected override void FormatData(ModuleTick module)
        {
            _data.TimeElapsed = new KeyValuePair<string, TimeSpan>("timeElapsed", module.TimeElapsed);
        }

        #endregion FORMAT
    }
}