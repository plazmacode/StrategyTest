using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class UIText : UIElement
    {
        private string text;
        private Color color;

        public string Text { get => text; set => text = value; }
        public float Layer { get => layer; set => layer = value; }

        public UIText(string text, Vector2 position, Color color, float layer) : base(position, GameWorld.Arial.MeasureString(text), color, layer)
        {
            this.text = text;
            Scale = 1;
        }

        public override void Update()
        {
            Player currentPlayer;
            if (Name == "timeText")
            {
                text = TimeManager.CurrentTime("normal");
            }
            if (Name == "provinceName")
            {
                text = MapManager.SelectedProvince.Name;
            }
            if (Name == "provincePopulation")
            {
                text = "Population: " + MapManager.SelectedProvince.Resources.Population.ToString();
            }

            if (GameWorld.CurrentGameState != GameState.Choose)
            {
                MapManager.PlayerDictionary.TryGetValue("player", out currentPlayer);
                if (Name == "totalPopulation")
                {
                    text = NumberFormatter.KNumber(currentPlayer.Resources.Population);
                }
                if (Name == "cashText")
                {
                    text = NumberFormatter.KNumber(currentPlayer.Resources.Cash) + "$";
                }
            }
        }
        /// <summary>
        /// Draws the text
        /// </summary>
        /// <param name="spriteBatch">spriteBatch reference for drawing sprites</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Arial, text, Position, Background, default, default, Scale, SpriteEffects.None, layer);
        }
    }
}
