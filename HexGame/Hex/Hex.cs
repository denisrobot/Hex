using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame {
    public struct Hex {
        public int q, r, s;

        public Hex(int q, int r, int s) {
            this.q = q;
            this.r = r;
            this.s = s;
        }

        static public bool Equal(Hex a, Hex b) {
            return a.q == b.q && a.r == b.r && a.s == b.s;
        }

        static public Hex Add(Hex a, Hex b) {
            return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
        }

        static public Hex Subtract(Hex a, Hex b) {
            return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        static public Hex Multiply(Hex a, Hex b) {
            return new Hex(a.q * b.q, a.r * b.r, a.s * b.s);
        }

        static public int Length(Hex Hex) {
            return (int)((Math.Abs(Hex.q) + Math.Abs(Hex.r) + Math.Abs(Hex.s)) / 2);
        }

        static public int Distance(Hex a, Hex b) {
            return Length(Subtract(a, b));
        }

        static public List<Hex> directions = new List<Hex> {
            new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1),
            new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1)
        };

        static public Hex Direction(int direction) {
            return directions[direction];
        }

        static public Hex Neighbor(Hex Hex, int direction) {
            return Add(Hex, Direction(direction));
        }
    }
}