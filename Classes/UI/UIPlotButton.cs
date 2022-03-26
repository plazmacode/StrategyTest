using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// A button that shows a plot once it has been clicked
    /// </summary>
    class UIPlotButton : UIElement
    {
        private string buttonText;
        private Vector2 buttonTextOffset;
        private Color textColor;
        private float textScale = 2;
        private UIPlot uiPlot;

        public string ButtonText { get => buttonText; set => buttonText = value; }
        public Vector2 ButtonTextOffset { get => buttonTextOffset; set => buttonTextOffset = value; }
        public Color TextColor { get => textColor; set => textColor = value; }
        public float TextScale { get => textScale; set => textScale = value; }
        public UIPlot UIPlotProp { get => uiPlot; set => uiPlot = value; }

        /// <summary>
        /// Creates a button with a texture
        /// </summary>
        /// <param name="position">Vector 2 for position of button</param>
        /// <param name="sprite">Texture2D</param>
        /// <param name="layer">float between 0.0 and 1.0</param>
        /// <param name="action">string used in UIManager to determine code execution on button press</param>
        public UIPlotButton(Vector2 position, Texture2D sprite, float layer, UIPlot plot) : base(position, sprite, layer)
        {
            this.UIPlotProp = plot;
            textColor = Color.White;
            Size = new Vector2(sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Creates a button with a rectangle and color
        /// </summary>
        /// <param name="position">Vector 2 for position of button</param>
        /// <param name="size">Size of rectangle</param>
        /// <param name="background">Color of rectangle</param>
        /// <param name="layer">float between 0.0 and 1.0</param>
        /// <param name="action">string used in UIManager to determine code execution on button press</param>
        public UIPlotButton(Vector2 position, Vector2 size, Color background, float layer, UIPlot plot) : base(position, size, background, layer)
        {
            this.UIPlotProp = plot;
            textColor = Color.White;
        }

        /// <summary>
        /// The rectangle for mouse click detection has to be updated. 
        /// Because the Scale of a button with a sprite is set after its creation.
        /// </summary>
        public void UpdateRect()
        {
            if (sprite != null)
            {
                Rect = new Rectangle((int)position.X, (int)position.Y, (int)sprite.Width * (int)scale, (int)sprite.Height * (int)scale);
            }
        }

        public override void Update()
        {
            if (Rect.Contains(GameWorld.MouseStateProp.Position) && GameWorld.MouseStateProp.LeftButton == ButtonState.Pressed && GameWorld.OldMouseState.LeftButton == ButtonState.Released)
            {
                OnClick();
            }
            uiPlot.Update();
        }

        private void OnClick()
        {
            if (UIPlotProp.IsShown)
            {
                UIPlotProp.IsShown = false;
            } else
            {
                UIPlotProp.IsShown = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sprite == null)
            {
                spriteBatch.Draw(GameWorld.Sprites["pixel"], Rect, null, Background, default, default, SpriteEffects.None, layer);
            }
            else
            {
                spriteBatch.Draw(sprite, position, null, Color.White, default, default, scale, SpriteEffects.None, layer);
            }
            if (ButtonText != null)
            {
                Vector2 buttonTextPosition = new Vector2(position.X + (Size.X * scale) / 2 - GameWorld.Arial.MeasureString(ButtonText).X * TextScale / 2, position.Y + GameWorld.Arial.MeasureString(ButtonText).Y / 2);
                spriteBatch.DrawString(GameWorld.Arial, ButtonText, buttonTextPosition + ButtonTextOffset, TextColor, 0, default, TextScale, SpriteEffects.None, 0.95f);
            }
            if (UIPlotProp.IsShown)
            {
                UIPlotProp.Draw(spriteBatch);
            }
        }
    }
}
