using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HexGame.Core;

namespace HexGame {
    public abstract class DrawableUIObject : DrawableGameComponent {
        public GameState gameState;
        public SpriteBatch spriteBatch;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        public DrawableUIObject(Game game) : base(game) {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            Game1.drawableUIObjects.Add(this);
            Game.Components.Add(this);
        }

        public abstract void DrawObject(SpriteBatch spriteBatch);

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            DrawObject(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}