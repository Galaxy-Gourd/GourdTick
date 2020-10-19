using SharpCore.Data;

namespace GGTick
{
    public class CoreTickSystemConfigData : CoreSystemData
    {
        public TicksetConfigData[] renderTicksets;
        public TicksetConfigData[] lateRenderTicksets;
        public TickSimulationConfigData[] simulationTicks;
    }
}