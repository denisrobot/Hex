using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.UI {
    class BoxUIElement : BasicUIElement {
        private static float TRANSPARENCY = 0.7f;
        public Texture2D backgroundTexture;
        public Color BackgroundColor { get; set; }
        

        public BoxUIElement(Game game, BasicUIElement parent = null) :
            base(game) {
            this.parent = parent;
        }

        protected override void LoadContent() {
            backgroundTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            backgroundTexture.SetData<Color>(new Color[] { Color.White });
            base.LoadContent();
        }

        public override void DrawObject(SpriteBatch spriteBatch) {
            if (BackgroundColor != null) {
                spriteBatch.Draw(backgroundTexture, PositionRectangle, BackgroundColor * TRANSPARENCY);
            }
            base.DrawObject(spriteBatch);
        }
    }
}