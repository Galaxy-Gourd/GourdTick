using GGSharpTick;

namespace GGTests.Tick
{
    public static class TickSystemTestsInstaller
    {
        public static ICoreTick TestTick;
        public static CoreTick InstallTickSystem(CoreTickSystemConfigData data)
        {
            TestTick = new CoreTick(data);
            return TestTick as CoreTick;
        }
    }
}