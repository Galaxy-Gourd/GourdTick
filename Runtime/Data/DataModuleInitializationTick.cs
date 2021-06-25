using System;
using GG.Data.Base;

namespace GG.Tick.Base
{
    public class DataModuleInitializationTick : DataModuleInitialization
    {
        public TickVariable Tick;
        public TickFixed[] FixedTicks;
    }
}