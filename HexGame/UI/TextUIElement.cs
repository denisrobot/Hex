using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.UI {
    class TextUIElement : BasicUIElement {
        private SpriteFont font;
        public string Text { get; set; }

        public TextUIElement(Game game, SpriteFont font, BasicUIElement parent = null) :
            base(game) {
            this.Text = "Text element";
            this.font = font;
            this.parent = parent;
        }

        public Vector2 GetTextSize() {
            return font.MeasureString(Text);
        }

        public override void DrawObject(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, Text, position, Color.Black);
            base.DrawObject(spriteBatch);
        }
    }
}