using System;
using System.Collections.Generic;
using GGTests.Tick.Demo;
using NUnit.Framework;

namespace GGTests.Tick
{
    /// <summary>
    /// Tests registration and unregistering of client ticks.
    /// </summary>
    [TestFixture]
    public class TickRegistrationTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(20)]
        public void VariableTickClientsDoRegisterWithVariableTicksets(int clientCount)
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());

            // Create and regster clients
            for (int i = 0; i < clientCount; i++)
            {
                DemoVariableTickClientInstanceRegistrationTest thisClient = 
                    new DemoVariableTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient(TickSystemTestsInstaller.TestTick.VariableTicks[0].ticksets[0]);
            }
            
            // Tick once to ready client additions
            TickSystemTestsInstaller.TestTick.DoTick(0.01f);

            Assert.AreEqual(GetTotalNumberOfVariableTickSubscribers(), clientCount,
                "Subscriber count for variable ticksets do not match number of registrants.");
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(20)]
        public void FixedTickClientsDoRegisterWithFixedTicks(int clientCount)
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());

            // Create and regster clients
            for (int i = 0; i < clientCount; i++)
            {
                DemoFixedTickClientInstanceRegistrationTest thisClient = 
                    new DemoFixedTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient(TickSystemTestsInstaller.TestTick.FixedTicks[0].ticksets[0]);
            }
            
            // Tick once to ready client additions
            TickSystemTestsInstaller.TestTick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);

            Assert.AreEqual(GetTotalNumberOfSimulationTickSubscribers(), clientCount,
                "Subscriber count for fixed ticksets do not match number of registrants.");
        }

        [Test]
        public void VariableTickClientsDoUnregisterCorrectly()
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            
            // Register clients
            Random r = new Random();
            int subCount = r.Next(10, 20);
            List<DemoVariableTickClientInstanceRegistrationTest> clients = 
                new List<DemoVariableTickClientInstanceRegistrationTest>();
            for (int i = 0; i < subCount; i++)
            {
                DemoVariableTickClientInstanceRegistrationTest thisClient = 
                    new DemoVariableTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient(TickSystemTestsInstaller.TestTick.VariableTicks[0].ticksets[0]);
                clients.Add(thisClient);
            }
            
            // Tick once to ready client additions
            TickSystemTestsInstaller.TestTick.DoTick(0.01f);
            
            // Unrgister clients
            int remCount = r.Next(3, 8);
            for (int i = 0; i < remCount; i++)
            {
                clients[i].UnregisterTickClient(TickSystemTestsInstaller.TestTick.VariableTicks[0].ticksets[0]);
            }
            
            // Tick once to ready client subtractions
            TickSystemTestsInstaller.TestTick.DoTick(0.01f);
            
            Assert.AreEqual(GetTotalNumberOfVariableTickSubscribers(), subCount - remCount,
                "Subscriber count for variable ticksets do not match number of registrants.");
        }

        [Test]
        public void FixedTickClientsDoUnregisterCorrectly()
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            
            // Register clients
            Random r = new Random();
            int subCount = r.Next(10, 20);
            List<DemoFixedTickClientInstanceRegistrationTest> clients = 
                new List<DemoFixedTickClientInstanceRegistrationTest>();
            for (int i = 0; i < subCount; i++)
            {
                DemoFixedTickClientInstanceRegistrationTest thisClient = 
                    new DemoFixedTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient(TickSystemTestsInstaller.TestTick.FixedTicks[0].ticksets[0]);
                clients.Add(thisClient);
            }
            
            // Tick once to ready client additions
            TickSystemTestsInstaller.TestTick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);
            
            // Unrgister clients
            int remCount = r.Next(3, 8);
            for (int i = 0; i < remCount; i++)
            {
                clients[i].UnregisterTickClient(TickSystemTestsInstaller.TestTick.FixedTicks[0].ticksets[0]);
            }
            
            // Tick once to ready client subtractions
            TickSystemTestsInstaller.TestTick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);
            
            Assert.AreEqual(GetTotalNumberOfSimulationTickSubscribers(), subCount - remCount,
                "Subscriber count for fixed ticksets do not match number of registrants.");
        }

        #region Utility

        private static int GetTotalNumberOfVariableTickSubscribers()
        {
            int registeredClients = 0;
            foreach (var t in TickSystemTestsInstaller.TestTick.VariableTicks[0].ticksets)
            {
                registeredClients += t.subscriberCount;
            }
            return registeredClients;
        }
        
        private static int GetTotalNumberOfSimulationTickSubscribers()
        {
            int registeredClients = 0;
            foreach (var t in TickSystemTestsInstaller.TestTick.FixedTicks)
            {
                foreach (var s in t.ticksets)
                {
                    registeredClients += s.subscriberCount;
                }
            }
            return registeredClients;
        }

        #endregion Utility
    }
}