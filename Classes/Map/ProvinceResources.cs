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
        /// Used to add population to provinces that are not owned by a player
        /// </summary>
        public void AddWildPopulation()
        {
            population += random.Next(-5, 30);
            if (population < 0)
            {
                population = 0;
            }
            //Plague
            if (population > 200 && random.Next(0, 100) <= 10)
            {
                population = (int)(population * 0.8f);
            }
            int x = TimeManager.TotalDays;
            int y = population;
            populationPlot.AddData(new Vector2(x, y));
        }

        /// <summary>
        /// Updates the provinces resources
        /// <para>Change population etc.</para>
        /// </summary>
        public void Update()
        {
            //80% chance to get 1 more population for early growth
            if (random.Next(0, 100) <= 80)
            {
                population++;
            }

            //5% chance to get 2/100th population extra
            if (random.Next(0,1000) <= 50)
            {
                population += population / 50;
            }

        }
    }
}
