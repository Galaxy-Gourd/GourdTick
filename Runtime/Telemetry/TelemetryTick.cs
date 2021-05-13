using System;
using System.Collections.Generic;
using GGSharpData;

namespace GGSharpTick
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