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
        private static KeyboardState previousState;

        public static void Update() {
            KeyboardState currentState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            /* End current player's turn after Space is pressed. */
            if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space)) {
                TurnManager.ChangeTurn();
            }

            /* Move current unit */
            if (currentState.IsKeyDown(Keys.M) && previousState.IsKeyUp(Keys.M)) {
                Units.Unit currentUnit = UnitManager.GetCurrentUnit();
                if (currentUnit.State != Units.Unit.States.Moving) {
                    MoveManager.StartMove(currentUnit);
                } else if (currentUnit.State == Units.Unit.States.Moving) {
                    MoveManager.EndMove(currentUnit);
                }
            }

            previousState = currentState;
        }

        public static Vector2 getMousePosition() {
            Matrix inverse = Matrix.Invert(GameManager.camera.getTransform());
            Vector2 vectorMousePosition = Vector2.Transform(
               new Vector2(Mouse.GetState().X, Mouse.GetState().Y), inverse);
            return new Vector2((int)vectorMousePosition.X, (int)vectorMousePosition.Y);
        }
    }
}
