using System;
using System.Collections.Generic;
using GGRoot;
using GGTests.Tick.Demo;
using NUnit.Framework;
using GGTick;

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
        public void RenderTickClientsDoRegisterWithRenderTicksets(int clientCount)
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());

            // Create and regster clients
            for (int i = 0; i < clientCount; i++)
            {
                DemoRenderTickClientInstanceRegistrationTest thisClient = 
                    new DemoRenderTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient();
            }
            
            // Tick once to ready client additions
            Core.Tick.DoTick(0.01f);

            Assert.AreEqual(GetTotalNumberOfRenderTickSubscribers(), clientCount,
                "Subscriber count for render ticksets do not match number of registrants.");
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(20)]
        public void SimulationTickClientsDoRegisterWithSimTicks(int clientCount)
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());

            // Create and regster clients
            for (int i = 0; i < clientCount; i++)
            {
                DemoSimulationTickClientInstanceRegistrationTest thisClient = 
                    new DemoSimulationTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient();
            }
            
            // Tick once to ready client additions
            Core.Tick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);

            Assert.AreEqual(GetTotalNumberOfSimulationTickSubscribers(), clientCount,
                "Subscriber count for simulation ticksets do not match number of registrants.");
        }

        [Test]
        public void RenderTickClientsDoUnregisterCorrectly()
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            
            // Register clients
            Random r = new Random();
            int subCount = r.Next(10, 20);
            List<DemoRenderTickClientInstanceRegistrationTest> clients = 
                new List<DemoRenderTickClientInstanceRegistrationTest>();
            for (int i = 0; i < subCount; i++)
            {
                DemoRenderTickClientInstanceRegistrationTest thisClient = 
                    new DemoRenderTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient();
                clients.Add(thisClient);
            }
            
            // Tick once to ready client additions
            Core.Tick.DoTick(0.01f);
            
            // Unrgister clients
            int remCount = r.Next(3, 8);
            for (int i = 0; i < remCount; i++)
            {
                clients[i].UnregisterTickClient();
            }
            
            // Tick once to ready client subtractions
            Core.Tick.DoTick(0.01f);
            
            Assert.AreEqual(GetTotalNumberOfRenderTickSubscribers(), subCount - remCount,
                "Subscriber count for render ticksets do not match number of registrants.");
        }

        [Test]
        public void SimulationTickClientsDoUnregisterCorrectly()
        {
            // Create default tick core
            TickSystemTestsInstaller.InstallTickSystem(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            
            // Register clients
            Random r = new Random();
            int subCount = r.Next(10, 20);
            List<DemoSimulationTickClientInstanceRegistrationTest> clients = 
                new List<DemoSimulationTickClientInstanceRegistrationTest>();
            for (int i = 0; i < subCount; i++)
            {
                DemoSimulationTickClientInstanceRegistrationTest thisClient = 
                    new DemoSimulationTickClientInstanceRegistrationTest();
                thisClient.RegisterTickClient();
                clients.Add(thisClient);
            }
            
            // Tick once to ready client additions
            Core.Tick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);
            
            // Unrgister clients
            int remCount = r.Next(3, 8);
            for (int i = 0; i < remCount; i++)
            {
                clients[i].UnregisterTickClient();
            }
            
            // Tick once to ready client subtractions
            Core.Tick.DoTick(TickSystemConstructionUtility.CONST_blankSimTickRate);
            
            Assert.AreEqual(GetTotalNumberOfSimulationTickSubscribers(), subCount - remCount,
                "Subscriber count for render ticksets do not match number of registrants.");
        }

        #region Utility

        private static int GetTotalNumberOfRenderTickSubscribers()
        {
            int registeredClients = 0;
            foreach (var t in Core.Tick.variableTicks[0].ticksets)
            {
                registeredClients += t.subscriberCount;
            }
            return registeredClients;
        }
        
        private static int GetTotalNumberOfSimulationTickSubscribers()
        {
            int registeredClients = 0;
            foreach (var t in Core.Tick.fixedTicks)
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