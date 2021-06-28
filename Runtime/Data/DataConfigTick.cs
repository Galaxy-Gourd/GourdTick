using GG.Data.Base;

namespace GGTickBase
{
    public abstract class DataConfigTick : DataConfig
    {
        public DataConfigTickset [] Ticksets;
        public string TickName;
    }
}