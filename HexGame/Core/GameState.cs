using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame.Core {
    public class GameState {
        public GameStates currentState { get; set; }

        public enum GameStates {
            Menu,
            Running,
            End
        }

        public GameState() {
            currentState = GameStates.Menu;
        }
    }
}