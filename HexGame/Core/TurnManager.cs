using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

using HexGame.Units;

namespace HexGame.Core {
    public class TurnManager {
        public static Player currentTurn;

        public static void StartTurn(Player player) {
            player.startTurn();
        }

        public static void ChangeTurn() {
            int playerCount = Game1.players.Count;
            int playerIndex = 0;
            for (int i = 0; i < playerCount; i++) {
                if (Game1.players[i].isInTurn()) {
                    Game1.players[i].endTurn();
                    if (i+1 >= playerCount) {
                        playerIndex = 0;
                    } else {
                        playerIndex = i + 1;
                    }
                    Game1.players[playerIndex].startTurn();
                    MoveManager.EndMove(UnitManager.GetCurrentUnit());
                    UnitManager.SetCurrentUnit(Game1.players[playerIndex].units[0]);
                    break;
                } 
            }
        }
    }
}
