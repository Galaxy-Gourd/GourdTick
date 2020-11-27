using GGData.Data;

namespace GGTick
{
    public class CoreTickSystemConfigData : CoreSystemData
    {
        public TickVariableConfigData[] variableTicks;
        public TickFixedConfigData[] fixedTicks;
    }
}