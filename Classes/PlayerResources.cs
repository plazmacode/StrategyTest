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
        private int cash;

        public int Cash { get => cash; set => cash = value; }

        /// <summary>
        /// Adds resources to the player, based on the amount of resources in the province.
        /// </summary>
        /// <param name="province"></param>
        public void AddResources(Province province)
        {
            Cash += province.Resources.Population;
        }
    }
}
