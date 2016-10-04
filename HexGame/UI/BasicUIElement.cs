using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.UI {
    class BasicUIElement : DrawableUIObject {
        private const int DEFAULT_WIDTH = 100;
        private const int DEFAULT_HEIGHT = 100;
        private const int DEFAULT_X_POS = 0;
        private const int DEFAULT_Y_POS = 0;

        public int width, height;
        public Vector2 position;
        public BasicUIElement parent;

        public BasicUIElement(Game game, BasicUIElement parent = null) : base(game) {
            width = DEFAULT_WIDTH;
            height = DEFAULT_HEIGHT;
            position = new Vector2(DEFAULT_X_POS, DEFAULT_Y_POS);
            this.parent = parent;
        }

        public Rectangle PositionRectangle {
            get { return new Rectangle((int)position.X, (int)position.Y, width, height); }
        }

        public void AddPadding(int paddingLeft = 0, int paddingRight = 0, int paddingTop = 0, int paddingBottom = 0) {
            width = width - paddingRight - paddingLeft;
            height = height - paddingTop - paddingBottom;
            position.X = position.X + paddingLeft;
            position.Y = position.Y + paddingTop;
        }

        public void AlignWithParent() {
            if (parent != null) {
                SetPosition(new Vector2(parent.position.X, parent.position.Y));
            } else {
                Console.WriteLine("Could not align with parent. Make sure parent object is provided.");
            }
        }

        public void ResizeToParent() {
            if (parent != null) {
                width = parent.width;
                height = parent.height;
            } else {
                Console.WriteLine("Could resize to parent. Make sure parent object is provided.");
            }
        }

        public void Resize(int width, int height) {
            this.width = width;
            this.height = height;
        }

        public void SetPosition(Vector2 position) {
            this.position = position;
        }

        public void ApplyOffset(Vector2 offset) {
            position.X += offset.X;
            position.Y += offset.Y;
        }

        public void DrawTopBorder() {
            Line.Draw(spriteBatch, Game.GraphicsDevice,
                new Vector2(position.X, position.Y),
                new Vector2(position.X + width, position.Y));
        }
        public void DrawRightBorder() {
            Line.Draw(spriteBatch, Game.GraphicsDevice,
                new Vector2(position.X + width, position.Y),
                new Vector2(position.X + width, position.Y + height));
        }
        public void DrawBottomBorder() {
            Line.Draw(spriteBatch, Game.GraphicsDevice,
                new Vector2(position.X, position.Y + height),
                new Vector2(position.X + width, position.Y + height));
        }
        public void DrawLeftBorder() {
            Line.Draw(spriteBatch, Game.GraphicsDevice,
                new Vector2(position.X, position.Y),
                new Vector2(position.X, position.Y + height));
        }

        public override void DrawObject(SpriteBatch spriteBatch) { }
    }
}