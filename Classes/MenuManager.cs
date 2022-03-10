using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    static class MenuManager
    {
        private static List<Menu> menuList = new List<Menu>();
        private static List<Menu> removeMenuList = new List<Menu>();
        private static List<Menu> addMenuList = new List<Menu>();
        private static Dictionary<string, Menu> menuDictionary = new Dictionary<string, Menu>();

        public static Dictionary<string, Menu> MenuDictionary { get => menuDictionary; set => menuDictionary = value; }
        public static List<Menu> MenuList { get => menuList; set => menuList = value; }

        public static void OnResize()
        {

            MenuDictionary.Clear();
            MenuList.Clear();
            LoadMenus();
        }

        public static void LoadMenus()
        {
            Menu chooseNationMenu = new Menu(new Vector2(0, 0), new Color(0, 10, 60), new Vector2(400, GameWorld.ScreenSize.Y));
            Button redNationButton = new Button(new Vector2(150,50), new Vector2(50,50), new Color(255,0,0), "chooseRedNation");
            Button chooseNationButton = new Button(new Vector2(20, GameWorld.ScreenSize.Y-150), new Vector2(360, 50), new Color(0, 30, 90), "chooseNation");
            chooseNationButton.ButtonText = "Choose Nation";
            chooseNationMenu.ButtonDictionary.Add("chooseNationButton", chooseNationButton);
            chooseNationMenu.ButtonDictionary.Add("redNationButton", redNationButton);

            if (GameWorld.CurrentGameState != GameState.BuildingMap || GameWorld.CurrentGameState != GameState.Choose)
            {
                //Load menus that are always shown(time etc.)
                Menu timeMenu = new Menu(new Vector2(GameWorld.ScreenSize.X - 300, 0), new Color(0, 10, 60), new Vector2(300, 100));
                MenuText timeText = new MenuText(TimeManager.CurrentTime("normal"), timeMenu.Position, Color.White) {Scale=2f };
                timeText.Name = "timeText";
                timeMenu.TextDictionary.Add("timeText", timeText);
                timeMenu.SetTextValues();

                Button speedUp = new Button(new Vector2(), new Vector2(), new Color(), "speedUp");
                Button speedDown = new Button(new Vector2(), new Vector2(), new Color(), "speedDown");

                menuList.Add(timeMenu);
            }

            MenuDictionary.Add("chooseNationMenu", chooseNationMenu);
        }

        /// <summary>
        /// Adds Menu to list of menus, that will be updated.
        /// </summary>
        /// <param name="menu"></param>
        public static void ShowMenu(Menu menu)
        {
            if (!MenuList.Contains(menu))
            {
                addMenuList.Add(menu);
            }
        }

        public static void ButtonAction(string action)
        {
            if (action == "chooseRedNation")
            {
                Player selectedNation = new Player(false);
                selectedNation.Color = Color.Red;
                MapManager.SelectedNation = selectedNation;
            }

            if (action == "chooseNation")
            {
                if (MapManager.SelectedNation != null)
                {
                    MapManager.SelectedProvince.UpdateOwner(MapManager.SelectedNation);
                    GameWorld.CurrentGameState = GameState.Pause;
                    removeMenuList.AddRange(menuList);
                }
            }
        }

        /// <summary>
        /// Return true if mouse clicks on mapRect or inside a menu.
        /// </summary>
        /// <returns></returns>
        public static bool PlayAbleAreaClicked()
        {
            foreach (Menu menu in menuList)
            {
                if (menu.Rect.Contains(GameWorld.MouseStateProp.Position))
                {
                    return true;
                }
            }
            if (MapManager.MapRect.Contains(GameWorld.MouseStateProp.Position))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public static void Update()
        {
            menuList.AddRange(addMenuList);
            foreach (Menu menu in removeMenuList)
            {
                menuList.Remove(menu);
            }
            addMenuList.Clear();
            removeMenuList.Clear();
            foreach (Menu menu in MenuList)
            {
                menu.Update();
            }
        }
    }
}
