using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame.Units {
    public struct WeaponStats {
        private int damage;
        private int range;
        private int reload;
        private int ammo;
        private string restriction;
        private int cost;
        private string type;

        public WeaponStats(int damage, int range, int reload,
                           int ammo, string restriction, int cost, string type) {
            this.damage = damage;
            this.range = range;
            this.reload = reload;
            this.ammo = ammo;
            this.restriction = restriction;
            this.cost = cost;
            this.type = type;
        }

        public int Damage {
           get { return damage; }
        }
        public int Range {
            get { return range; }
        }
        public int Reload {
            get { return reload; }
        }
        public int Ammo {
            get { return ammo; }
        }
        public string Restriction {
            get { return restriction; }
        }
        public int Cost {
            get { return cost; }
        }
        public string Type {
            get { return type; }
        }
    }
}
