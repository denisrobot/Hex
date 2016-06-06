using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame.Units {
    public class Weapon {
        private WeaponStats stats;

        /* Ranged */
        static public WeaponStats pistol = new WeaponStats(5, 2, 1, 5, "", 2, "Pistol");
        static public WeaponStats carbine = new WeaponStats(5, 3, 2, 5, "", 3, "Carbine");

        public Weapon(WeaponStats stats) {
            this.stats = stats;
        }
    }
}
