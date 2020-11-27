using GGRoot;
using GGTick;

namespace GGTests.Tick
{
    public static class TickSystemTestsInstaller
    {
        public static CoreTick InstallTickSystem(CoreTickSystemConfigData data)
        {
            Core.Tick = new CoreTick(data);
            return Core.Tick as CoreTick;
        }
    }
}