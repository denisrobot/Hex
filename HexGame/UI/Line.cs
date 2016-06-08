using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame.UI {
    class Line {

        private static Texture2D createPixel(GraphicsDevice graphicsDevice) {
            Texture2D pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.Black });

            return pixel;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice gd, Vector2 start, Vector2 end) {
            Vector2 edge = end - start;

            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(createPixel(gd),
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
