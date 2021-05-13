using System;
using System.Collections.Generic;
using GGSharpData;

namespace GGSharpTick
{
    public class DataTelemetryTick : DataTelemetry
    {
        public KeyValuePair<string, TimeSpan> TimeElapsed;
    }
}