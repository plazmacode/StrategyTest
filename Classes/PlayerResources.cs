using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Represents the resources that a player has
    /// </summary>
    class PlayerResources
    {
        private float cash;
        private int population;
        private int manpower;

        public float Cash { get => cash; set => cash = value; }
        public int Population { get => population; set => population = value; }


        /// <summary>
        /// Adds resources to the player, based on the amount of resources in the province.
        /// </summary>
        /// <param name="province"></param>
        public void AddResources(Province province)
        {
            Population += province.Resources.Population;
            Cash += province.Resources.Population / 1000;
        }

        /// <summary>
        /// Resets some values so the statistics can be found
        /// </summary>
        public void SetStatistics()
        {
            Population = 0;
        }
    }
}
