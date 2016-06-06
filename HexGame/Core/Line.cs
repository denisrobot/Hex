using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame.Core {
    class Line {
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        public Line(SpriteBatch sb, GraphicsDevice gd) {
            this.spriteBatch = sb;
            this.graphicsDevice = gd;
        }

        private Texture2D createPixel() {
            Texture2D pixel = new Texture2D(this.graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.Black });

            return pixel;
        }

        public void Draw(Vector2 start, Vector2 end) {
            Vector2 edge = end - start;

            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(createPixel(),
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    1),
                null,
                Color.Black,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
    }
}
