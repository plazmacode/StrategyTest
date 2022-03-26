using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    static class UIManager
    {
        private static List<UIElement> UIList = new List<UIElement>();
        private static List<UIElement> removeUIList = new List<UIElement>();
        private static List<UIElement> addUIList = new List<UIElement>();
        private static Dictionary<string, UIElement> UIDictionary = new Dictionary<string, UIElement>();

        public static Dictionary<string, UIElement> UIDictionaryProp { get => UIDictionary; set => UIDictionary = value; }
        public static List<UIElement> UIListProp { get => UIList; set => UIList = value; }

        public static void OnResize()
        {

            UIDictionaryProp.Clear();
            UIListProp.Clear();
            LoadMenus();
        }

        public static void LoadMenus()
        {
            Color menuBlue = new Color(0, 10, 60);

            //Choose nation
            UIArea chooseNationMenu = new UIArea(new Vector2(0, 200), new Vector2(400, GameWorld.ScreenSize.Y-200), menuBlue, 0.5f);
            UIButton redNationButton = new UIButton(new Vector2(150,50), new Vector2(50,50), new Color(255,0,0), 0.51f, "chooseRedNation");
            UIButton chooseNationButton = new UIButton(new Vector2(20, GameWorld.ScreenSize.Y-300), new Vector2(360, 50), new Color(0, 30, 90), 0.51f, "chooseNation");
            chooseNationButton.ButtonText = "Choose Nation";
            chooseNationMenu.SubUIElements.Add("chooseNationButton", chooseNationButton);
            chooseNationMenu.SubUIElements.Add("redNationButton", redNationButton);

            UIDictionaryProp.Add("chooseNationMenu", chooseNationMenu);
            //UI after game has started
            if (GameWorld.CurrentGameState != GameState.BuildingMap || GameWorld.CurrentGameState != GameState.Choose)
            {
                //Load menus that are always shown(time etc.)
                UIArea timeMenu = new UIArea(new Vector2(GameWorld.ScreenSize.X - 300, 0), new Vector2(300, 100), menuBlue, 0.6f);
                UIText timeText = new UIText(TimeManager.CurrentTime("normal"), Vector2.Zero, Color.White, 0.61f) { Name = "timeText", Scale = 2f };
                timeMenu.SubUIElements.Add("timeText", timeText);
                //timeMenu.SetTextValues();

                UIButton speedUp = new UIButton(new Vector2(250, 0), new Vector2(50, 50), new Color(0, 255, 0), 0.61f, "speedUp");
                UIButton speedDown = new UIButton(new Vector2(250, 50), new Vector2(50, 50), new Color(255, 0, 0), 0.61f, "speedDown");
                speedUp.ButtonText = "+";
                speedDown.ButtonText = "-";

                timeMenu.SubUIElements.Add("speedUp", speedUp);
                timeMenu.SubUIElements.Add("speedDown", speedDown);
                UIList.Add(timeMenu);

                UIArea topMenu = new UIArea(new Vector2(0,0), new Vector2(GameWorld.ScreenSize.X - 600, 100), menuBlue, 0.5f);
                UIText totalPopulation = new UIText("Pop: nullPop", new Vector2(10, 10), Color.White, 0.51f) { Name = "totalPopulation" };
                UIText cashText = new UIText("Cash: 0$", new Vector2(120, 10), Color.White, 0.51f) { Name = "cashText" };
                topMenu.SubUIElements.Add("totalPopulation", totalPopulation);
                topMenu.SubUIElements.Add("cashText", cashText);
                UIList.Add(topMenu);

                UIArea provinceMenu = new UIArea(new Vector2(0, 200), new Vector2(500, GameWorld.ScreenSize.Y - 200), menuBlue, 0.5f);
                UIText provinceName = new UIText("NullName", new Vector2(10, 10), Color.White, 0.51f) { Name = "provinceName", Scale = 1.5f };
                UIText provincePopulation = new UIText("Population: NullPopulation", new Vector2(5, 40), Color.White, 0.51f) { Name = "provincePopulation"};
                UIPlot populationPlot = new UIPlot(provinceMenu.Position + new Vector2(10, provinceMenu.Size.Y - 340), new Vector2(300, 300), Color.White, 0.51f) { Name = "populationPlot" };
                UIPlotButton provincePopulationPlotButton = new UIPlotButton(new Vector2(200, 20), GameWorld.Sprites["plot"], 0.51f, populationPlot);                
                provinceMenu.SubUIElements.Add("provinceName", provinceName);
                provinceMenu.SubUIElements.Add("provincePopulation", provincePopulation);
                provinceMenu.SubUIElements.Add("provincePopulationPlotButton", provincePopulationPlotButton);
                

                UIDictionaryProp.Add("provinceMenu", provinceMenu);
            }

            //Update parent values on UIElements that are under a UIArea
            //Also set position relative to parent
            foreach (UIArea uiArea in UIList)
            {
                if (uiArea.SubUIElements.Count > 0)
                {
                    foreach (UIElement uiElement in uiArea.SubUIElements.Values)
                    {
                        uiElement.Parent = uiArea;
                        uiElement.SetToParentPosition();
                    }
                }
            }
            foreach (UIArea uiArea in UIDictionary.Values)
            {
                if (uiArea.SubUIElements.Count > 0)
                {
                    foreach (UIElement uiElement in uiArea.SubUIElements.Values)
                    {
                        uiElement.Parent = uiArea;
                        uiElement.SetToParentPosition();
                    }
                }
            }
        }

        /// <summary>
        /// Adds Menu to list of menus, that will be updated.
        /// </summary>
        /// <param name="menu"></param>
        public static void ShowMenu(UIElement menu)
        {
            if (!UIListProp.Contains(menu))
            {
                addUIList.Add(menu);
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
                //Maybe move this to MapManager
                if (MapManager.SelectedNation != null)
                {
                    MapManager.SelectedProvince.UpdateOwner(MapManager.SelectedNation);
                    MapManager.SelectedProvince.Resources.Population += 1000; //Capitals get extra 1000 population, maybe move this to province class
                    MapManager.SelectedNation.OwnedProvinces.Add(MapManager.SelectedProvince.Position,MapManager.SelectedProvince);
                    MapManager.PlayerDictionary.Add("player", MapManager.SelectedNation);
                    GameWorld.CurrentGameState = GameState.Pause;
                    removeUIList.AddRange(UIList);
                }
            }

            if (action == "speedUp")
            {
                TimeManager.ChangeGameSpeed("up");
            }

            if (action == "speedDown")
            {
                TimeManager.ChangeGameSpeed("down");
            }
        }

        /// <summary>
        /// Return true if mouse clicks on mapRect or inside a menu.
        /// </summary>
        /// <returns></returns>
        public static bool PlayAbleAreaClicked()
        {
            foreach (UIArea menu in UIList)
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
            UIList.AddRange(addUIList);
            foreach (UIElement uiElement in removeUIList)
            {
                UIList.Remove(uiElement);
            }
            addUIList.Clear();
            removeUIList.Clear();
            foreach (UIElement uiElement in UIListProp)
            {
                uiElement.Update();
            }
        }
    }
}
