using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame {
    public class Damage {
        private int w, t, d;

        public Damage(int w, int t, int d) {
            this.w = w;
            this.t = t;
            this.d = d;
        }

        public int Calculate() {
            int wD10 = Die.successCount(w);
            int tD10 = Die.successCount(t);
            return (wD10 + tD10 - d) + w;
        }
    }
}
