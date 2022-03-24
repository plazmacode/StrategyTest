using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class Province
    {
        private Texture2D sprite;
        private Vector2 position;
        private float layer;
        private int size;
        private Color edgeColor;
        private Color provinceColor;
        private int borderThickness;
        private string name;
        private Player owner;

        private Rectangle topLine;
        private Rectangle bottomLine;
        private Rectangle rightLine;
        private Rectangle leftLine;
        private Rectangle background;

        public Vector2 Position { get => position; set => position = value; }
        public Rectangle TopLine { get => topLine; set => topLine = value; }
        public Rectangle BottomLine { get => bottomLine; set => bottomLine = value; }
        public Rectangle RightLine { get => rightLine; set => rightLine = value; }
        public Rectangle LeftLine { get => leftLine; set => leftLine = value; }
        public string Name { get => name; set => name = value; }
        public Player Owner { get => owner; set => owner = value; }

        public Province(Vector2 position, int provinceSize)
        {
            this.Position = position;
            borderThickness = 3;
            layer = 0.3f;
            sprite = GameWorld.Pixel;
            size = provinceSize;
            edgeColor = Color.Black;
            provinceColor = Color.White;
            SetRectangles();
        }

        public void UpdateOwner(Player owner)
        {
            this.owner = owner;
            this.provinceColor = owner.Color;
        }

        private void GenerateName()
        {
            name = position.ToString();
        }

        /// <summary>
        /// Used for initialization and resize
        /// </summary>
        public void SetRectangles()
        {
            float zoomSize = size * GameWorld.ZoomScale;
            Vector2 screenPosition = new Vector2(position.X * zoomSize + MapManager.Offset.X, position.Y * zoomSize + MapManager.Offset.Y);
            TopLine = new Rectangle((int)screenPosition.X + borderThickness, (int)screenPosition.Y, (int)zoomSize - borderThickness, borderThickness);

            BottomLine = new Rectangle((int)screenPosition.X, (int)screenPosition.Y + (int)zoomSize - borderThickness, (int)zoomSize - borderThickness, borderThickness);

            RightLine = new Rectangle((int)screenPosition.X + (int)zoomSize - borderThickness, (int)screenPosition.Y, borderThickness, (int)zoomSize);

            LeftLine = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, borderThickness, (int)zoomSize);

            background = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, (int)zoomSize, (int)zoomSize);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {
            //Hover
            if (background.Contains(Mouse.GetState().Position))
            {
                edgeColor = Color.Red;
            } else
            {
                edgeColor = Color.Black;
            }

            //Clicked
            if (background.Contains(GameWorld.MouseStateProp.Position) && GameWorld.MouseStateProp.LeftButton == ButtonState.Pressed &&
                GameWorld.OldMouseState.LeftButton == ButtonState.Released)
            {
                OnClick();
            }
        }

        private void OnClick()
        {
            MapManager.SelectedProvince = this;
            if (GameWorld.CurrentGameState == GameState.Choose)
            {
                UIElement current;
                UIManager.UIDictionaryProp.TryGetValue("chooseNationMenu", out current);
                UIManager.ShowMenu(current);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, background, null, provinceColor, default, default, SpriteEffects.None, layer);
            spriteBatch.Draw(sprite, TopLine, null, edgeColor, default, default, SpriteEffects.None, layer+0.1f);
            spriteBatch.Draw(sprite, BottomLine, null, edgeColor, default, default, SpriteEffects.None, layer + 0.1f);
            spriteBatch.Draw(sprite, LeftLine, null, edgeColor, default, default, SpriteEffects.None, layer + 0.1f);
            spriteBatch.Draw(sprite, RightLine, null, edgeColor, default, default, SpriteEffects.None, layer + 0.1f);

        }
    }
}
