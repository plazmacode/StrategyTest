using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class MenuText
    {
        private string name;
        private string text;
        private Vector2 position;
        private Color color;
        private float layer;
        private float scale;


        public string Text { get => text; set => text = value; }
        public float Layer { get => layer; set => layer = value; }
        public string Name { get => name; set => name = value; }
        public float Scale { get => scale; set => scale = value; }

        public MenuText(string text, Vector2 position, Color color)
        {
            this.text = text;
            this.position = position;
            this.color = color;
            Scale = 1;
        }

        public void Update()
        {
            if (Name == "timeText")
            {
                text = TimeManager.CurrentTime("normal");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Arial, text, position, color, default, default, Scale, SpriteEffects.None, Layer);
        }
    }
}
