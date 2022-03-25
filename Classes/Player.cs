using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StrategyTest
{
    class Player
    {
        private Color color;
        private bool AI;
        private Thread playerThread;
        private bool isAlive;
        private PlayerResources resources;
        private Dictionary<Vector2, Province> ownedProvinces = new Dictionary<Vector2, Province>();

        public Player(bool AI)
        {
            Resources = new PlayerResources();
            this.AI = AI;
            IsAlive = true;
            if (AI)
            {
                playerThread = new Thread(AIBehaviour);
                playerThread.IsBackground = true;
                playerThread.Start();
            }
        }

        public Color Color { get => color; set => color = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        /// <summary>
        /// A Dictionary of the provinces that a player owns.
        /// </summary>
        public Dictionary<Vector2, Province> OwnedProvinces { get => ownedProvinces; set => ownedProvinces = value; }

        /// <summary>
        /// The amount of resources the player has
        /// </summary>
        public PlayerResources Resources { get => resources; set => resources = value; }

        private void AIBehaviour()
        {
            while (IsAlive)
            {
                if (GameWorld.CurrentGameState == GameState.Play)
                {

                }
            }
        }

        /// <summary>
        /// Updates the players owned provinces
        /// </summary>
        private void UpdateProvinces()
        {
            foreach (Province province in OwnedProvinces.Values)
            {
                Resources.SetStatistics();
                Resources.AddResources(province);
                province.Resources.Update();
            }
        }

        public void DailyUpdate()
        {
            UpdateProvinces();
            //Some method that makes AI thread run at same speed as Main Thread
            //Set AI Move calculations on AI thread
            //Run AI class update on main thread
            //Update all AI "moves" on main thread
        }

        public void MonthlyUpdate()
        {
            foreach (Province province in ownedProvinces.Values)
            {
                int x = TimeManager.TotalDays;
                int y = province.Resources.Population;
                province.Resources.PopulationPlot.AddData(new Vector2(x, y));
            }
        }
    }
}
