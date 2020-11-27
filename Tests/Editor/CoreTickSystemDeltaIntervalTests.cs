using System;
using GGRoot;
using NUnit.Framework;
using GGTick;

namespace GGTests.Tick
{
    [TestFixture]
    public class TickSourceTests
    {
        #region Tests

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void InvalidDeltaTickIntervalsDoFailValidation(float interval)
        {
            try
            {
                var _ = TickSystemTestsInstaller.InstallTickSystem
                    (TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
                Core.Tick.DoTick(interval);
                Assert.Fail("Invalid tick delta intervals are passing validation.");
            }
            catch (ArgumentOutOfRangeException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        [TestCase(0.01f)]
        [TestCase(5)]
        public void ElapsedTimeIsAccurateGivenValidConstantDeltaIntervals(float interval)
        {
            var _ = TickSystemTestsInstaller.InstallTickSystem
                (TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            const int tickCount = 50;
            for (int i = 0; i < tickCount; i++)
            {
                Core.Tick.DoTick(interval);
            }
            
            TimeSpan elapsed = TimeSpan.FromSeconds(tickCount * interval);
            float round = Math.Abs((float)Core.Tick.elapsedSinceSimStartup.TotalSeconds - (float)elapsed.TotalSeconds);
            Assert.LessOrEqual(round, 0.0001f,
                "Elapsed time does not add up based on constant delta intervals.");
        }

        #endregion Tests
    }
}