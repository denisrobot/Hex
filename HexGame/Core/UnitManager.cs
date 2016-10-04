using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HexGame.Units;

namespace HexGame.Core {
    class UnitManager {
        private static Dictionary<Guid, Unit> units;
        private static Unit selectedUnit;

        static UnitManager() {
            units = new Dictionary<Guid, Unit>();
            selectedUnit = null;
        }

        public static void AddUnit(Unit unit) {
            units.Add(unit.GetID, unit);
        }

        public static Dictionary<Guid, Unit> GetUnits {
            get { return units; }
        }

        public static Unit GetUnitByID(Guid id) {
            if (units.ContainsKey(id)) {
                return units[id];
            } else {
                Console.WriteLine("Could not find unit with ID " + id);
                return null;
            }
        }

        public static void SelectUnitByHex(Hex hex, bool preventNonTurnSelection=true) {
            foreach (KeyValuePair<Guid, Unit> unit in GetUnits) {
                if (Hex.Equal(unit.Value.GetCurrentHex(), hex)) {
                    if (preventNonTurnSelection && unit.Value.GetOwner == TurnManager.currentTurn) {
                        SetCurrentUnit(unit.Value);
                    } else if (!preventNonTurnSelection) {
                        SetCurrentUnit(unit.Value);
                    }
                }
            }
        }

        public static void SetCurrentUnit(Unit unit) {
            selectedUnit = unit;
        }

        public static Unit GetCurrentUnit() {
            return selectedUnit;
        }
    }
}
