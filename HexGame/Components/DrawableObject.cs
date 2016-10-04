using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;
using HexGame.Core;

namespace HexGame {
    public abstract class DrawableObject : DrawableGameComponent {
        //public GameState gameState;
        public SpriteBatch spriteBatch;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState currentMouseState;
        public MouseState previousMouseState;
        public RenderTarget2D renderTarget;

        public DrawableObject(Game game) : base(game) {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            GameManager.drawableGameObjects.Add(this);
            Game.Components.Add(this);
        }

        public override void Initialize() {
            this.Enabled = true;
            this.Visible = true;
            base.Initialize();
        }

        protected override void LoadContent() {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            //camera.enableControls();
            //camera.applyOffset();
        }


        public abstract void drawObject(SpriteBatch spriteBatch);

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                    null, null, null, null, null,
                    GameManager.camera.getTransform());
            drawObject(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
