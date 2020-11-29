using System;
using System.Collections.Generic;
using GGTests.Tick.Demo;
using NUnit.Framework;
using GGSharpTick;

namespace GGTests.Tick
{
    [TestFixture]
    public class TickExecutionTests
    {
        #region Execution Order
        
        [Test]
        [TestCase(0)]
        [TestCase(5)]
        public void VariableTicksetsAreExecutedInOrderCreated(int ticksets)
        {
            // Create tick system with variable ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.variableTicks =
                TickSystemConstructionUtility.TickVariableDataGroup(1, ticksets);
            TickSystemTestsInstaller.InstallTickSystem(coreTickConfigData);
            
            // Create clients in order
            DemoOrderedVariableTickClient.tickOrderCounter = 0;
            List<DemoOrderedVariableTickClient> clients = new List<DemoOrderedVariableTickClient>();
            int count = 0;
            for (int i = 0; i < coreTickConfigData.variableTicks.Length; i++)
            {
                for (int e = 0; e < coreTickConfigData.variableTicks[i].ticksets.Length; e++)
                {
                    clients.Add(new DemoOrderedVariableTickClient(
                        TickSystemTestsInstaller.TestTick.variableTicks[i].ticksets[e],
                        count));
                    count++;
                }
            }
            
            // Tick
            TickSystemTestsInstaller.TestTick.DoTick(0.0334f);
            
            // Client tick indices should match their tick keys
            foreach (var c in clients)
            {
                Assert.AreEqual(c.targetOrder, c.thisOrderedEntryResult,
                    "Variable ticksets are not executing in the order they were created.");
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(5, 0)]
        [TestCase(5, 5)]
        [TestCase(3, 7)]
        public void FixedTicksetsAreExecutedInOrderCreated(int ticks, int ticksetsPerTick)
        {
            // Tick data
            const float tRate = 0.035f;
            const float tMax = 0.05f;
            
            // Construct data with additional fixed ticks/ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.fixedTicks =
                TickSystemConstructionUtility.TickFixedDataGroup(
                    ticks, 
                    ticksetsPerTick, 
                    tRate, 
                    tMax);
            TickSystemTestsInstaller.InstallTickSystem(coreTickConfigData);
            
            // Create clients in order
            DemoOrderedFixedTickClient.tickOrderCounter = 0;
            List<DemoOrderedFixedTickClient> clients = new List<DemoOrderedFixedTickClient>();
            int count = 0;
            for (int i = 0; i < coreTickConfigData.fixedTicks.Length; i++)
            {
                for (int e = 0; e < coreTickConfigData.fixedTicks[i].ticksets.Length; e++)
                {
                    clients.Add(new DemoOrderedFixedTickClient(
                        TickSystemTestsInstaller.TestTick.fixedTicks[i].ticksets[e],
                        count));
                    count++;
                }
            }
            
            // Tick; make sure the delta is long enough to tick simulation
            float diff = tMax - tRate;
            TickSystemTestsInstaller.TestTick.DoTick(tMax - (diff / 2));
            
            // Client tick indices should match their tick keys
            foreach (var c in clients)
            {
                Assert.AreEqual(c.targetOrder, c.thisOrderedEntryResult,
                    "Fixed ticksets are not executing in the order they were created.");
            }
        }
        
        #endregion Execution Order


        #region Fixed Ticks

        [Test]
        [TestCase(0.0334f)]
        [TestCase(0.05f)]
        [TestCase(1)]
        public void FixedTicksDoFireAtCorrectIntervalsRelativeToVariableTick(float mockTickrate)
        {
            // Create new tick core system, override with custom fixed tick
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.fixedTicks =
                TickSystemConstructionUtility.TickFixedDataGroup(
                    1, 
                    1, 
                    mockTickrate,
                    float.MaxValue);
            TickSystemTestsInstaller.InstallTickSystem(coreTickConfigData);

            // Create fixed tick instance
            DemoSimulationTickClientIntervalsTest simTick = new DemoSimulationTickClientIntervalsTest();
            TickSystemTestsInstaller.TestTick.Register(simTick, TickSystemTestsInstaller.TestTick.fixedTicks[0].ticksets[0]);

            Random r = new Random();
            float elapsedSinceLastTick = 0;
            for (int i = 0; i < 100; i++)
            {
                // Tick
                float mockDelta = (float)r.NextDouble();
                TickSystemTestsInstaller.TestTick.DoTick(mockDelta);
                elapsedSinceLastTick += mockDelta;
                
                // Collect target ticks
                int targetTicks = 0;
                float mockSimAccumulator = elapsedSinceLastTick;
                while (mockSimAccumulator >= mockTickrate)
                {
                    mockSimAccumulator -= mockTickrate;
                    targetTicks++;
                }
                elapsedSinceLastTick = mockSimAccumulator;
                
                // Detect ticks
                if (simTick.ticksSinceLastCheck < targetTicks)
                {
                    Assert.Fail(
                        "Fixed tick client is not ticking enough based on variable tickrate: "
                        + "TARGET: " + targetTicks
                        + " RESULT: " + simTick.ticksSinceLastCheck);
                }
                else if (simTick.ticksSinceLastCheck > targetTicks)
                {
                    Assert.Fail(
                        "Fixed tick client is ticking too frequently based on variable tickrate: "
                        + "TARGET: " + targetTicks
                        + " RESULT: " + simTick.ticksSinceLastCheck);
                }
                else
                {
                    simTick.ticksSinceLastCheck = 0;
                }
            }
        }

        #endregion Fixed Ticks
    }
}