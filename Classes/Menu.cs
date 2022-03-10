using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class Menu
    {
        private Vector2 position;
        private Color background;
        private Vector2 size;
        private float layer;
        private Rectangle rect;
        private Dictionary<string, Button> buttonDictionary = new Dictionary<string, Button>();
        private Dictionary<string, Menu> menuDictionary = new Dictionary<string, Menu>();
        private Dictionary<string, MenuText> textDictionary = new Dictionary<string, MenuText>();

        public Vector2 Position { get => position; set => position = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        internal Dictionary<string, Menu> MenuDictionary { get => menuDictionary; set => menuDictionary = value; }
        internal Dictionary<string, Button> ButtonDictionary { get => buttonDictionary; set => buttonDictionary = value; }
        internal Dictionary<string, MenuText> TextDictionary { get => textDictionary; set => textDictionary = value; }

        public Menu(Vector2 position, Color background, Vector2 size)
        {
            this.Position = position;
            this.background = background;
            this.size = size;
            layer = 0.8f;
            Rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        /// <summary>
        /// Sets Text layer to be above the menu
        /// </summary>
        public void SetTextValues()
        {
            foreach (MenuText menuText in TextDictionary.Values)
            {
                menuText.Layer = this.layer + 0.05f;
            }
        }

        public void Update()
        {
            foreach (Menu menu in MenuDictionary.Values)
            {
                menu.Update();
            }
            foreach (Button button in ButtonDictionary.Values)
            {
                button.Update();
            }
            foreach (MenuText menuText in textDictionary.Values)
            {
                menuText.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Pixel, Rect, null, background, default, default, SpriteEffects.None, layer);
            foreach (Menu menu in MenuDictionary.Values)
            {
                menu.Draw(spriteBatch);
            }
            foreach (Button button in ButtonDictionary.Values)
            {
                button.Draw(spriteBatch);
            }
            foreach (MenuText menuText in TextDictionary.Values)
            {
                menuText.Draw(spriteBatch);
            }
        }
    }
}
