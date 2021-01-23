using GGSharpTick;

namespace GGTests.Tick
{
    public static class TickSystemConstructionUtility
    {
        public const float CONST_blankSimTickRate = 0.034f;
        
        #region System Config Data

        /// <summary>
        /// Creates a blank set of tick system config data for testing purposes (NOT NULL).
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData BlankCoreTickSystemConfigData()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                VariableTicks = BlankVariableTickDataGroup(), 
                FixedTicks = BlankFixedTickDataGroup()
            };
            return data;
        }

        /// <summary>
        /// Returns tick system data with null render tickset values
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData TickSystemDataWithNullRenderTicks()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                VariableTicks = null, 
                FixedTicks = BlankFixedTickDataGroup()
            };
            return data;
        }
        
        /// <summary>
        /// Returns tick system config data with null simulation tick data
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData TickSystemDataWithNullSimulationTicks()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                VariableTicks = BlankVariableTickDataGroup(), 
                FixedTicks = null
            };
            return data;
        }

        #endregion System Config Data
        

        #region Variable Ticks

        private static TickVariableConfigData[] BlankVariableTickDataGroup()
        {
            TickVariableConfigData[] testVariableTicks =
            {
                new TickVariableConfigData
                {
                    ticksets = new []
                    {
                        new TicksetConfigData
                        {
                            ticksetName = "testVarTickset"
                        }
                    },
                    tickName = "testVariableTick",
                    StepFixed = true
                }
            };
            return testVariableTicks;
        }
        
        public static TickVariableConfigData[] TickVariableDataGroup(int tickCount, int ticksetsPerTick)
        {
            TickVariableConfigData[] data = new TickVariableConfigData[tickCount];
            for (int i = 0; i < tickCount; i++)
            {
                TickVariableConfigData thisTick = new TickVariableConfigData
                {
                    tickName = "tick_" + i,
                    ticksets = new TicksetConfigData[ticksetsPerTick]
                };
                
                for (int e = 0; e < thisTick.ticksets.Length; e++)
                {
                    thisTick.ticksets[e] = new TicksetConfigData
                    {
                        ticksetName = "tick_" + i + "_tickset_" + e
                    };
                }
                data[i] = thisTick;
            }
            return data;
        }

        #endregion Variable Ticks

        
        #region Simulation Ticks

        private static TickFixedConfigData[] BlankFixedTickDataGroup()
        {
            TickFixedConfigData[] testFixedTicks =
            {
                new TickFixedConfigData
                {
                    ticksets = new []
                    {
                        new TicksetConfigData
                        {
                            ticksetName = "testFixedTickset"
                        }
                    },
                    MaxDelta = 0.5f,
                    tickName = "testFixedTick",
                    Tickrate = CONST_blankSimTickRate
                }
            };
            return testFixedTicks;
        }

        public static TickFixedConfigData[] TickFixedDataGroup(
            int tickCount, 
            int ticksetsPerTick,
            float tRate = 0.0334f,
            float tMax = 0.5f)
        {
            TickFixedConfigData[] data = new TickFixedConfigData[tickCount];
            for (int i = 0; i < tickCount; i++)
            {
                TickFixedConfigData thisTick = new TickFixedConfigData
                {
                    tickName = "tick_" + i,
                    ticksets = new TicksetConfigData[ticksetsPerTick],
                    Tickrate = tRate,
                    MaxDelta = tMax
                };
                
                for (int e = 0; e < thisTick.ticksets.Length; e++)
                {
                    thisTick.ticksets[e] = new TicksetConfigData
                    {
                        ticksetName = "tick_" + i + "_tickset_" + e
                    };
                }
                data[i] = thisTick;
            }
            return data;
        }

        #endregion Simulation Ticks
    }
}