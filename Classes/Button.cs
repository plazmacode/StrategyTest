using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class Button
    {
        private string action;
        private Vector2 position;
        private Vector2 size;
        private Color background;
        private Rectangle rect;
        private float layer;
        private string buttonText;

        public string ButtonText { get => buttonText; set => buttonText = value; }

        public Button(Vector2 position, Vector2 size, Color background, string action)
        {
            this.action = action;
            this.position = position;
            this.size = size;
            this.background = background;
            layer = 0.9f;
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public void Update()
        {
            if (rect.Contains(GameWorld.MouseStateProp.Position) && GameWorld.MouseStateProp.LeftButton == ButtonState.Pressed && GameWorld.OldMouseState.LeftButton == ButtonState.Released)
            {
                OnClick();
            }
        }

        private void OnClick()
        {
            MenuManager.ButtonAction(action);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Pixel, rect, null, background, default, default, SpriteEffects.None, layer);
            if (ButtonText != null)
            {
                //Do not divide measurestring by 2, because text scale is set to 2 already.
                Vector2 buttonTextPosition = new Vector2(position.X + size.X / 2 - GameWorld.Arial.MeasureString(buttonText).X, position.Y);
                spriteBatch.DrawString(GameWorld.Arial, ButtonText, buttonTextPosition, Color.White, 0, default, 2f, SpriteEffects.None, 0.95f);
            }
        }
    }
}
