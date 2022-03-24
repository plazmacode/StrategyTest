﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class UIText : UIElement
    {
        private string name;
        private string text;
        private Color color;

        public string Text { get => text; set => text = value; }
        public float Layer { get => layer; set => layer = value; }
        public string Name { get => name; set => name = value; }

        public UIText(string text, Vector2 position, Color color, float layer) : base(position, GameWorld.Arial.MeasureString(text), color, layer)
        {
            this.text = text;
            Scale = 1;
        }

        public override void Update()
        {
            if (Name == "timeText")
            {
                text = TimeManager.CurrentTime("normal");
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