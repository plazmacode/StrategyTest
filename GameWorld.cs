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
        private static Texture2D pixel;
        private static SpriteFont arial;
        private static GameTime gameTime;
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

        private static MouseState mouseState;
        private static MouseState oldMouseState;
        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

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
        public static Texture2D Pixel { get => pixel; set => pixel = value; }
        public static KeyboardState KeyStateProp { get => keyState; set => keyState = value; }
        public static KeyboardState OldKeyState { get => oldKeyState; set => oldKeyState = value; }
        public static MouseState MouseStateProp { get => mouseState; set => mouseState = value; }
        public static MouseState OldMouseState { get => oldMouseState; set => oldMouseState = value; }
        public static float ZoomScale { get => zoomScale; set => zoomScale = value; }
        public static Dictionary<string, Texture2D> Sprites { get => sprites; set => sprites = value; }

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
            MapManager.OnResize();
            UIManager.OnResize();
        }


        protected override void Initialize()
        {
            CurrentGameState = GameState.Play;
            Arial = Content.Load<SpriteFont>("arial");

            Sprites.Add("pixel", Content.Load<Texture2D>("pixel"));
            Sprites.Add("plot", Content.Load<Texture2D>("plot"));

            cameraPosition = Vector2.Zero;
            zoomScale = 1f;

            UIManager.LoadMenus();
            MapManager.CreateMap();

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
            MouseStateProp = Mouse.GetState();
            KeyStateProp = Keyboard.GetState();

            InputManager.Update();
            MapManager.Update();
            UIManager.Update();

            if (currentGameState == GameState.Play)
            {
                TimeManager.AdvanceTime();
            }

            OldMouseState = MouseStateProp;
            OldKeyState = KeyStateProp;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (Province province in MapManager.Map.Values)
            {
                province.Draw(_spriteBatch);
            }
            Vector2 gameStatePosition = new Vector2(screenSize.X -500 - Arial.MeasureString(currentGameState.ToString()).X, 0);
            _spriteBatch.DrawString(Arial, currentGameState.ToString(), gameStatePosition, Color.White, 0, default, 2f, SpriteEffects.None, 0.9f);

            foreach (UIArea menu in UIManager.UIListProp)
            {
                menu.Draw(_spriteBatch);
            }

            DrawMapBoundary(MapManager.MapRect);

            //Draw extra debug texts
            for (int i = 0; i < DebugTexts.Count; i++)
            {
                _spriteBatch.DrawString(Arial, DebugTexts[i], new Vector2(0, 240 + i * 24), Color.Red, 0, default, 2f, SpriteEffects.None, 0.9f);
            }
            DebugTexts.Clear();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMapBoundary(Rectangle rect)
        {
            int lineWidth = 5;
            Color color = Color.DarkGray;

            //rect.X = rect.X + (int)CameraPosition.X;
            //rect.Y = rect.Y + (int)CameraPosition.Y;

            Rectangle topLine = new Rectangle(rect.X - lineWidth, rect.Y - lineWidth, rect.Width + lineWidth * 2, lineWidth);
            Rectangle bottomLine = new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, lineWidth);
            Rectangle rightLine = new Rectangle(rect.X + rect.Width, rect.Y, lineWidth, rect.Height + lineWidth);
            Rectangle leftLine = new Rectangle(rect.X - lineWidth, rect.Y, lineWidth, rect.Height + lineWidth);

            _spriteBatch.Draw(GameWorld.Sprites["pixel"], topLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            _spriteBatch.Draw(GameWorld.Sprites["pixel"], bottomLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            _spriteBatch.Draw(GameWorld.Sprites["pixel"], rightLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            _spriteBatch.Draw(GameWorld.Sprites["pixel"], leftLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }
}
