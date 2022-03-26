using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Blueprint class for creating other UIElements
    /// </summary>
    abstract class UIElement
    {
        protected UIElement parent;
        protected Vector2 position;
        protected Vector2 size;
        protected Color background;
        protected Rectangle rect;
        protected float layer;
        protected Texture2D sprite;
        protected float scale;
        protected string name;

        /// <summary>
        /// Property for easier access of position
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }

        /// <summary>
        /// Property for easier access of scale
        /// </summary>
        public float Scale { get => scale; set => scale = value; }

        /// <summary>
        /// Property to change color of UIelements after creation. Used in UpdateUI() method
        /// </summary>
        public Color Background { get => background; set => background = value; }

        /// <summary>
        /// The parent of the UIElement, used when an element is a part of a UIArea
        /// </summary>
        public UIElement Parent { get => parent; set => parent = value; }
        public string Name { get => name; set => name = value; }
        public Vector2 Size { get => size; set => size = value; }

        /// <summary>
        /// Constructor for creating UI with texture. This is on the abstract class.
        /// </summary>
        /// <param name="position">Vector2</param>
        /// <param name="sprite">Texture2D</param>
        /// <param name="layer">float between 0.0 and 1.0</param>
        public UIElement(Vector2 position, Texture2D sprite, float layer)
        {
            this.Position = position;
            this.sprite = sprite;
            this.layer = layer;
            this.Scale = 1;

            rect = new Rectangle((int)position.X, (int)position.Y, (int)sprite.Width, (int)sprite.Height);
        }
        /// <summary>
        /// Constructor for creating UI with Rectangle. This is on the abstract class.
        /// </summary>
        /// <param name="position">Vector2</param>
        /// <param name="background">Color for color of Rectangle</param>
        /// <param name="size">Vector2 for size of rectangle</param>
        /// <param name="layer">float between 0.0 and 1.0</param>
        public UIElement(Vector2 position, Vector2 size, Color background, float layer)
        {
            this.Position = position;
            this.Background = background;
            this.Size = size;
            this.layer = layer;
            this.Scale = 1;

            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
        /// <summary>
        /// Abstract Update() Method. So that all UIElmenents can be updated with one call
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Sets UIElements position relative to the parents position
        /// <para>0,0 would then place the element at the parents position</para>
        /// </summary>
        public virtual void SetToParentPosition()
        {
            if (parent != null)
            {
                this.position = parent.position + this.position;
                rect = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        /// <summary>
        /// Abstract Draw() Method. So that all UIElmenents can be drawn with one call
        /// </summary>
        /// <param name="spriteBatch">spriteBatch reference for drawing sprites</param>
        public abstract void Draw(SpriteBatch spriteBatch);


    }
}