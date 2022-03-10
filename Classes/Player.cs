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

        public Player(bool AI)
        {
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

        private void AIBehaviour()
        {
            while (IsAlive)
            {
                if (GameWorld.CurrentGameState == GameState.Play)
                {

                }
            }
        }

        private void Update()
        {
            //Some method that makes AI thread run at same speed as Main Thread
            //Set AI Move calculations on AI thread
            //Run AI class update on main thread
            //Update all AI "moves" on main thread
        }
    }
}
