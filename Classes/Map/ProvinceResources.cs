using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Represents the resources that are inside a province
    /// </summary>
    class ProvinceResources
    {
        private int population;
        private Random random;
        private Plot populationPlot;

        /// <summary>
        /// The amount of people that live in a province
        /// </summary>
        public int Population { get => population; set => population = value; }
        public Plot PopulationPlot { get => populationPlot; set => populationPlot = value; }

        public ProvinceResources()
        {
            random = new Random();
            population = random.Next(50,100);
            populationPlot = new Plot();
        }

        /// <summary>
        /// Updates the provinces resources
        /// <para>Change population etc.</para>
        /// </summary>
        public void Update()
        {
            if (random.Next(0, 100) <= 5)
            {
                population += 1 * population / 1000;
            }
        }
    }
}
