using System;
using NUnit.Framework;
using GGSharpTick;

namespace GGTests.Tick
{
    [TestFixture]
    public class CoreTickSystemConstructionTests
    {
        #region Null Checks

        [Test]
        public void CoreTickSystemNullConfigDataDoesFailValidation()
        {
            try
            {
                var _ = TickSystemTestsInstaller.InstallTickSystem(null);
                Assert.Fail("Tick system with null config data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        public void CoreTickSystemConfigDataWithNullVariableTicksetsDoesFailValidation()
        {
            try
            {
                CoreTick _ = TickSystemTestsInstaller.InstallTickSystem
                    (TickSystemConstructionUtility.TickSystemDataWithNullRenderTicks());
                Assert.Fail("Tick system with null variable tickset data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        public void CoreTickSystemConfigDataWithNullFixedTicksDoesFailValidation()
        {
            try
            {
                CoreTick _ = TickSystemTestsInstaller.InstallTickSystem
                    (TickSystemConstructionUtility.TickSystemDataWithNullSimulationTicks());
                Assert.Fail("Tick system with null fixed ticks data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }

        #endregion Null Checks


        #region Data Integrity

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 5)]
        [TestCase(5, 0)]
        [TestCase(5, 5)]
        public void VariableTicksDoInitializeWithCorrectNumberOfTicksets(int ticks, int ticksetsPerTick)
        {
            // Construct data with additoinal render ticksets
            CoreTickSystemConfigData coreTickConfigData = 
                TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.VariableTicks =
                TickSystemConstructionUtility.TickVariableDataGroup(ticks, ticksetsPerTick);
            TickSystemTestsInstaller.InstallTickSystem(coreTickConfigData);
            
            // Total variable ticksets should be equal to ticks + ticksets per (simulation ticks have no defaults)
            int count = 0;
            foreach (var tick in TickSystemTestsInstaller.TestTick.VariableTicks)
            {
                count += tick.ticksets.Count;
            }
            
            // Total render ticksets should be equal to addtional ticksets plus 1 (the default tickset)
            Assert.AreEqual(ticks * ticksetsPerTick, count,
                "Variable tickset count is not correct!");
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 5)]
        [TestCase(5, 0)]
        [TestCase(5, 5)]
        public void FixedTicksDoInitializeWithCorrectNumberOfTicksets(int ticks, int ticksetsPerTick)
        {
            // Construct data with additional simulation ticks/ticksets
            CoreTickSystemConfigData coreTickConfigData = 
                TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.FixedTicks =
                TickSystemConstructionUtility.TickFixedDataGroup(ticks, ticksetsPerTick);
            TickSystemTestsInstaller.InstallTickSystem(coreTickConfigData);

            // Total fixed ticksets should be equal to ticks + ticksets per (simulation ticks have no defaults)
            int count = 0;
            foreach (var tick in TickSystemTestsInstaller.TestTick.FixedTicks)
            {
                count += tick.ticksets.Count;
            }
            Assert.AreEqual(ticks * ticksetsPerTick, count,
                "Fixed tickset count is not correct!");
        }

        #endregion Data Integrity
    }
}