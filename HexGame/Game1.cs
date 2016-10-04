using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;
using HexGame.Units;
using HexGame.Core;
using HexGame.UI;

namespace HexGame
{
    public class Game1 : Game
    {
        private Controls controls;
        public static List<Player> players;
        private SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphics;

        public Game1() {
            controls = new Controls();
            players = new List<Player>();
            graphics = new GraphicsDeviceManager(this);
            GameManager.game = this;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameManager.InitializeGame();
            base.Initialize();
        }

        protected override void LoadContent() {
            // TODO
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            GameManager.camera.applyOffset();
            GameManager.camera.enableControls();
            Controls.Update();
            GameManager.hexGrid.Visible = true;
            UIManager.turnDisplay.Visible = true;
            UIManager.unitInfo.Visible = true;

            if (TurnManager.currentTurn != null) {
                UIManager.turnDisplayText.Text = "Current turn: " + TurnManager.currentTurn.name;
            }
            UIManager.unitInfoText.Text = UnitManager.GetCurrentUnit().GetInformationString();
            MoveManager.MoveUpdate(UnitManager.GetCurrentUnit());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.LightGray);
            spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.AlphaBlend, 
                null, null, null, null,
                GameManager.camera.getTransform());
            foreach (DrawableObject drawableObject in GameManager.drawableGameObjects) {
                if (drawableObject.Visible) {
                    drawableObject.Draw(gameTime);
                }
            }
            foreach(DrawableUIObject drawableUIObject in GameManager.drawableUIObjects) {
                if (drawableUIObject.Visible) {
                    drawableUIObject.Draw(gameTime);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        /* Sets mousePosition equal to mouse position (with adjusting for world/camera coords.) */
        public Vector2 getMousePosition() {
            Matrix inverse = Matrix.Invert(GameManager.camera.getTransform());
            Vector2 vectorMousePosition = Vector2.Transform(
               new Vector2(Mouse.GetState().X, Mouse.GetState().Y), inverse);
            return new Vector2((int)vectorMousePosition.X, (int)vectorMousePosition.Y);
        }
    }
}
