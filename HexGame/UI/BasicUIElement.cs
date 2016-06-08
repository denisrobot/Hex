using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.UI {
    class BasicUIElement : DrawableUIObject {
        public int width, height;
        public Vector2 position;
        public BasicUIElement parent;

        public BasicUIElement(Game game, int width, int height, Vector2 position, BasicUIElement parent) : base(game) {
            this.width = width;
            this.height = height;
            this.position = position;
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

        public void AlignWithParent(Vector2 offset) {
            if (parent != null) {
                position.X = parent.position.X + offset.X;
                position.Y = parent.position.Y + offset.Y;
            } else {
                Console.WriteLine("Could not align with parent. Make sure parent object is provided.");
            }
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

    class TextElement : BasicUIElement {
        private SpriteFont font;
        public string Text { get; set; }
        public Texture2D backgroundTexture;
        public Color BackgroundColor { get; set; }

        public TextElement(Game game, int width, int height, Vector2 position, SpriteFont font, BasicUIElement parent) :
            base(game, width, height, position, parent) {
            this.font = font;
        }

        protected override void LoadContent() {
            backgroundTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            backgroundTexture.SetData<Color>(new Color[] { Color.White });
            base.LoadContent();
        }

        public override void DrawObject(SpriteBatch spriteBatch) {
            Vector2 stringSize = font.MeasureString(Text);
            if (BackgroundColor != null) {
                spriteBatch.Draw(backgroundTexture, PositionRectangle, BackgroundColor);
            }
            spriteBatch.DrawString(font, Text,
                new Vector2(
                    position.X,
                    position.Y),
                Color.Black);
            //Line.Draw(spriteBatch, Game.GraphicsDevice,
            //    new Vector2(position.X, position.Y + stringSize.Y),
            //    new Vector2(position.X + width, position.Y + stringSize.Y));
            base.DrawObject(spriteBatch);
        }
    }
}