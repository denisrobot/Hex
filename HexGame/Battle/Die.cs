using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame {
    public class Die {
        public static Random rnd = new Random();

        public static int Roll(int sides) {
            return rnd.Next(1, sides + 1);
        }

        public static bool isSuccess(int roll) {
            if (roll == 8 || roll == 9 || roll == 10) {
                return true;
            } else {
                return false;
            }
        }

        public static int successCount(int diceCount) {
            int successCount = 0;
            for (int i = 0; i < diceCount; i++) {
                if (isSuccess(Roll(10))) {
                    successCount++;
                }
            }
            return successCount;
        }
    }
}
