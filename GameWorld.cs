using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace StrategyTest
{
    public enum GameState { Play, Choose, Pause, BuildingMap }

    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static SpriteFont arial;
        private static GameTime gameTime;

        private static GameState currentGameState;

        private static List<string> debugTexts = new List<string>();

        private static Vector2 screenSize;
        private static Vector2 oldScreenSize;
        private static Vector2 cameraPosition;
        private static float zoomScale;

        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static Vector2 OldScreenSize { get => oldScreenSize; set => oldScreenSize = value; }
        public static Vector2 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
        public static List<string> DebugTexts { get => debugTexts; set => debugTexts = value; }
        public static SpriteFont Arial { get => arial; set => arial = value; }
        public static GameTime GameTimeProp { get => gameTime; set => gameTime = value; }
        public static GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }
        public void OnResize(Object sender, EventArgs e)
        {
            OldScreenSize = screenSize;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }


        protected override void Initialize()
        {
            CurrentGameState = GameState.Play;
            Arial = Content.Load<SpriteFont>("arial");
            cameraPosition = Vector2.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            GameTimeProp = gameTime;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            //Draw extra debug texts
            for (int i = 0; i < DebugTexts.Count; i++)
            {
                _spriteBatch.DrawString(Arial, DebugTexts[i], new Vector2(0, 240 + i * 24), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
            DebugTexts.Clear();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
