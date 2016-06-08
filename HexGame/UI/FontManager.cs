using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.UI {
    static class FontManager {
        public static SpriteFont R12(Game game) {
            return game.Content.Load<SpriteFont>("UIFont_s12");
        }
        public static SpriteFont R15(Game game) {
            return game.Content.Load<SpriteFont>("UIFont_s15");
        }
        public static SpriteFont B15(Game game) {
            return game.Content.Load<SpriteFont>("UIFont_s15_Bold");
        }
        public static SpriteFont R20(Game game) {
            return game.Content.Load<SpriteFont>("UIFont_s20");
        }
    }
}
