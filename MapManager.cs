using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    static class MapManager
    {

        private static Dictionary<Vector2, Province> map;
        private static Vector2 mapSize;

        static MapManager()
        {
            mapSize = new Vector2(10, 10);
        }

        public static void CreateMap()
        {
            GameWorld.CurrentGameState = GameState.BuildingMap;

            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    Vector2 pos = new Vector2(x, y);
                    map.Add(pos, new Province(pos));
                }
            }

            GameWorld.CurrentGameState = GameState.Play;
        }

        public static void Update()
        {
            while (GameWorld.CurrentGameState == GameState.Play)
            {
                foreach (Province province in map.Values)
                {
                    province.Update(GameWorld.GameTimeProp);
                }
            }
        }
    }
}
