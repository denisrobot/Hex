using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Units;


namespace HexGame.Core {
    public class Player {
        public int id, turnCount;
        public bool turn;
        public string name;
        public List<Unit> units;

        public Player(int id, string name) {
            this.id = id;
            this.turnCount = 0;
            this.turn = false;
            this.name = name;
            units = new List<Unit>();
            Game1.players.Add(this);
        }

        public void addUnit(Unit unit) {
            units.Add(unit);
        }

        public void startTurn() {
            turn = true;
            Console.WriteLine(name + "'s turn");
        }

        public void endTurn() {
            turn = false;
            Console.WriteLine(name + " ended their turn");
        }

        public bool isInTurn() {
            return turn;
        }
    }
}
