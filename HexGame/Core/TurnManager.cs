using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace HexGame.Core {
    public class TurnManager {
        public static Player currentTurn;

        public static void StartTurn(Player player) {
            player.startTurn();
        }

        public static void ChangeTurn() {
            int playerCount = Game1.players.Count;
            for (int i = 0; i < playerCount; i++) {
                if (Game1.players[i].isInTurn()) {
                    Game1.players[i].endTurn();
                    if (i+1 >= playerCount) {
                        Game1.players[0].startTurn();
                        break;
                    } else {
                        Game1.players[i + 1].startTurn();
                        break;
                    }
                } 
            }
        }
    }
}
