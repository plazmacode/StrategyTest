using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    class UIArea : UIElement
    {
        private Dictionary<string, UIElement> subUIElements = new Dictionary<string, UIElement>();


        public Rectangle Rect { get => rect; set => rect = value; }
        public Dictionary<string, UIElement> SubUIElements { get => subUIElements; set => subUIElements = value; }

        public UIArea(Vector2 position, Color background, Vector2 size, float layer) : base(position, size, background, layer)
        {
            Rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        /// <summary>
        /// Sets Text layer to be above the menu
        /// </summary>
        public void SetTextValues()
        {
            foreach (UIText uiText in subUIElements.Values)
            {
                uiText.Layer = this.layer + 0.05f;
            }
        }

        public override void Update()
        {
            foreach (UIElement uiElement in SubUIElements.Values)
            {
                uiElement.Update();
            }
        }

        public override void SetToParentPosition()
        {
            if (SubUIElements.Count > 0)
            {
                foreach (UIElement uiElement in SubUIElements.Values)
                {
                    uiElement.Parent = this;
                    uiElement.SetToParentPosition();
                }
            }
            if (parent != null)
            {
                this.position = parent.Position + this.position;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Pixel, Rect, null, background, default, default, SpriteEffects.None, layer);
            foreach (UIElement uiElement in SubUIElements.Values)
            {
                uiElement.Draw(spriteBatch);
            }
        }
    }
}
