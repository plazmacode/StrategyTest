using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public Province(Vector2 position, Texture2D sprite)
        {
            this.sprite = sprite;
            this.position = position;
            layer = 0.3f;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, position, null, default, default, default, 1f, SpriteEffects.None, layer);
        }
    }
}
