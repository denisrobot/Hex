using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame;
using HexGame.UI;
using HexGame.Settings;
using HexGame.Units;
using HexGame.Core;

namespace HexGame.Core {
    class MoveManager {

        public static void StartMove(Unit unit) {
            unit.State = Unit.States.Moving;
        }
        public static void MoveUpdate(Unit unit) { 
            if (unit.State == Unit.States.Moving) {
                Grid grid = GameManager.hexGrid;
                Pathfinding pathfinding = new Pathfinding(grid);
                List<Hex> path;
                path = pathfinding.findPath(
                    new Vector2(grid.GetCurrentMouseHoverHex().q, grid.GetCurrentMouseHoverHex().r),
                    new Vector2(unit.GetCurrentHex().q, unit.GetCurrentHex().r));
                grid.SetUnitPath(path);
            }
        }
        public static void EndMove(Unit unit) {
            unit.State = Unit.States.Waiting;
            GameManager.hexGrid.ClearUnitPath();
        }
        public static void MoveUnit(Unit unit, Hex destination) {
            int moveLength = Hex.Distance(unit.GetCurrentHex(), destination);
            if (moveLength <= unit.CurrentMove()) {
                unit.UpdateLocation(destination);
                unit.DecreaseMove(moveLength);
                EndMove(unit);
                UnitManager.SetCurrentUnit(unit);
            }
        }
    }   
}