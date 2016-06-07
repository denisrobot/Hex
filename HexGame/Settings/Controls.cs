using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using HexGame.Core;

namespace HexGame.Settings {
    class Controls {
        KeyboardState previousState;

        public void Update() {
            KeyboardState currentState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            /* End current player's turn after Space is pressed. */
            if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space)) {
                TurnManager.changeTurn();
            }
            previousState = currentState;
        }

        public static Vector2 getMousePosition() {
            Matrix inverse = Matrix.Invert(Game1.camera.getTransform());
            Vector2 vectorMousePosition = Vector2.Transform(
               new Vector2(Mouse.GetState().X, Mouse.GetState().Y), inverse);
            return new Vector2((int)vectorMousePosition.X, (int)vectorMousePosition.Y);
        }
    }
}
