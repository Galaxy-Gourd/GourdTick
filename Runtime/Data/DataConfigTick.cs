using GG.Data.Base;

namespace GG.Tick.Base
{
    public abstract class DataConfigTick : DataConfig
    {
        public DataConfigTickset [] Ticksets;
        public string TickName;
    }
}