using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame.Units {
    public struct UnitStats {
        private int baseMorale;
        private int baseMove;
        private int baseCost;
        private string type;

        public UnitStats(int baseMorale, int baseMove, int baseCost, string type) {
            this.baseMorale = baseMorale;
            this.baseMove = baseMove;
            this.baseCost = baseCost;
            this.type = type;
        }

        public int BaseMorale {
            get { return baseMorale; }
        }

        public int BaseMove {
            get { return baseMove; }
        }

        public int BaseCost {
            get { return baseCost; }
        }

        public string Type {
            get { return type; }
        }
    }
}
