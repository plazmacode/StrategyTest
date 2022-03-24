using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    static class MapManager
    {
        private static Rectangle mapRect;
        private static Dictionary<Vector2, Province> map;
        private static Vector2 mapSize;
        private static Vector2 offset;
        private static Vector2 oldOffset;
        private static int provinceSize;
        private static Province selectedProvince;
        private static Player selectedNation;

        static MapManager()
        {
            mapSize = new Vector2(15, 15);
            provinceSize = 60;
        }

        public static Vector2 Offset { get => offset; set => offset = value; }
        internal static Dictionary<Vector2, Province> Map { get => map; set => map = value; }
        internal static Province SelectedProvince { get => selectedProvince; set => selectedProvince = value; }
        public static Rectangle MapRect { get => mapRect; set => mapRect = value; }
        internal static Player SelectedNation { get => selectedNation; set => selectedNation = value; }

        public static void OnResize()
        {
            float trueProvinceSize = provinceSize * GameWorld.ZoomScale;
            oldOffset = offset;
            Offset = new Vector2(GameWorld.ScreenSize.X / 2 - mapSize.X * provinceSize / 2, (GameWorld.ScreenSize.Y / 2 - mapSize.Y * provinceSize / 2)) + GameWorld.CameraPosition;
            MapRect = new Rectangle((int)Offset.X, (int)Offset.Y, (int)mapSize.X * (int)trueProvinceSize, (int)mapSize.Y * (int)trueProvinceSize);
            foreach (Province province in map.Values)
            {
                province.SetRectangles();
            }
        }

        public static void CreateMap()
        {
            Map = new Dictionary<Vector2, Province>();
            GameWorld.CurrentGameState = GameState.BuildingMap;
            Offset = new Vector2(GameWorld.ScreenSize.X / 2 - mapSize.X * provinceSize / 2, (GameWorld.ScreenSize.Y / 2 - mapSize.Y * provinceSize / 2));
            MapRect = new Rectangle((int)Offset.X, (int)Offset.Y, (int)mapSize.X * provinceSize, (int)mapSize.Y * provinceSize);

            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    Vector2 pos = new Vector2(x, y);
                    Province province = new Province(pos, provinceSize);
                    Map.Add(pos, province);
                }
            }

            GameWorld.CurrentGameState = GameState.Choose;
        }

        public static void Update()
        {
            if (GameWorld.MouseStateProp.LeftButton == ButtonState.Released && GameWorld.OldMouseState.LeftButton == ButtonState.Pressed)
            {
                if (!UIManager.PlayAbleAreaClicked())
                {
                    UIManager.OnResize();
                    //MenuManager.MenuList.Clear(); //Faster but some menus will be hidden until called again
                }
            }
            if (GameWorld.CurrentGameState == GameState.Play || GameWorld.CurrentGameState == GameState.Choose)
            {
                foreach (Province province in Map.Values)
                {
                    province.Update(GameWorld.GameTimeProp);
                }
            }
        }
    }
}
