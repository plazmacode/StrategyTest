using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Handles the user input
    /// </summary>
    static class InputManager
    {
        private static Vector2 draggingPosition;
        private static Vector2 cameraVelocity;
        private static float cameraSpeed;

        static InputManager()
        {
            cameraSpeed = 400;
        }

        /// <summary>
        /// Update method for checking every input
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update()
        {
            GameInput();
            CameraInput();
            CameraMovement();
        }

        private static void GameInput()
        {
            //Keys[] keyArray = new Keys[Keyboard.GetState().GetPressedKeyCount()];
            //Keyboard.GetState().GetPressedKeys(keyArray);
            //for (int i = 0; i < keyArray.Length; i++)
            //{
            //    GameWorld.DebugTexts.Add("Keypressed: " + keyArray[i]);
            //}
            //Pause ingame time
            if (ButtonPressed(Keys.Space))
            {
                if (GameWorld.CurrentGameState == GameState.Play)
                {
                    GameWorld.CurrentGameState = GameState.Pause;
                } else if (GameWorld.CurrentGameState == GameState.Pause)
                {
                    GameWorld.CurrentGameState = GameState.Play;
                }
            }

            //Increase ingame time speed
            if (ButtonPressed(Keys.Add) || ButtonPressed(Keys.OemPlus))
            {
                TimeManager.ChangeGameSpeed("up");
            }

            //Decrease ingame time speed
            if (ButtonPressed(Keys.Subtract) || ButtonPressed(Keys.OemMinus))
            {
                TimeManager.ChangeGameSpeed("down");
            }
        }

        /// <summary>
        /// Check if key is down, while it was up last frame
        /// <para>Runs code once when key is down</para>
        /// <para>Does not run code on button release, but on the button press</para>
        /// </summary>
        /// <param name="key">Key Input</param>
        /// <returns></returns>
        private static bool ButtonPressed(Keys key)
        {
            if (GameWorld.KeyStateProp.IsKeyDown(key) && GameWorld.OldKeyState.IsKeyUp(key))
            {
                return true;

            } else
            {
                return false;
            }
        }

        private static void CameraInput()
        {
            GameWorld.DebugTexts.Add(GameWorld.ZoomScale.ToString());
            void UpdateOffset()
            {
                MapManager.OnResize();
                UIManager.OnResize();
            }
            if (GameWorld.MouseStateProp.ScrollWheelValue > GameWorld.OldMouseState.ScrollWheelValue)
            {
                GameWorld.ZoomScale = MathF.Round(GameWorld.ZoomScale * 1.1f * 10) /10;
                UpdateOffset();
            }
            else if (GameWorld.MouseStateProp.ScrollWheelValue < GameWorld.OldMouseState.ScrollWheelValue)
            {
                GameWorld.ZoomScale = MathF.Round(GameWorld.ZoomScale / 1.1f * 10) / 10;
                UpdateOffset();
            }
            //Max zoom level
            if (GameWorld.ZoomScale > 3)
            {
                GameWorld.ZoomScale = 3;
            }

            //Dragging
            if (GameWorld.MouseStateProp.MiddleButton == ButtonState.Pressed)
            {
                draggingPosition = new Vector2(GameWorld.OldMouseState.X - GameWorld.MouseStateProp.Position.X,
                    GameWorld.OldMouseState.Y - GameWorld.MouseStateProp.Position.Y);
                GameWorld.CameraPosition -= draggingPosition / GameWorld.ZoomScale;
                UpdateOffset();
            }

            //Arrow Keys & WASD
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.Left))
            {
                cameraVelocity += new Vector2(1, 0);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.Right))
            {
                cameraVelocity += new Vector2(-1, 0);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.Up))
            {
                cameraVelocity += new Vector2(0, 1);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.Down))
            {
                cameraVelocity += new Vector2(0, -1);
            }

            if (GameWorld.KeyStateProp.IsKeyDown(Keys.A))
            {
                cameraVelocity += new Vector2(1, 0);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.D))
            {
                cameraVelocity += new Vector2(-1, 0);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.W))
            {
                cameraVelocity += new Vector2(0, 1);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.S))
            {
                cameraVelocity += new Vector2(0, -1);
            }
        }

        private static void CameraMovement()
        {
            float deltaTime = (float)GameWorld.GameTimeProp.ElapsedGameTime.TotalSeconds;

            //Update camera position
            GameWorld.CameraPosition += cameraVelocity * cameraSpeed * deltaTime;

            //Update OnResize positions
            if (cameraVelocity != Vector2.Zero)
            {
                MapManager.OnResize();
                foreach (Province province in MapManager.Map.Values)
                {
                    province.SetRectangles();
                }
            }
            cameraVelocity = Vector2.Zero;
        }
    }
}
