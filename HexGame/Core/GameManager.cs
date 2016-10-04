using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;
using HexGame.Core;
using HexGame.UI;
using HexGame.Units;

namespace HexGame.Core {
    class GameManager {
        public static List<DrawableObject> drawableGameObjects;
        public static List<DrawableUIObject> drawableUIObjects;

        private static UIManager UI;
        public static Camera camera;
        private static List<Player> players;

        public static Layout gridLayout;
        public static Grid hexGrid { get; set; }
        private static Player player1;
        private static Player player2;
        private static Unit testUnit, testUnit2, testUnit3;

        public static Game1 game { get; set; }

        public static void InitializeGame() {
            VideoSettings.InitVideoSettings();
            //VideoSettings.SetFullscreen();
            VideoSettings.SetWindowed();
            VideoSettings.SetResolution(1280, 800);
            camera = new Camera(VideoSettings.GetResolution().X, VideoSettings.GetResolution().Y);
            game.IsMouseVisible = true;

            drawableGameObjects = new List<DrawableObject>();
            drawableUIObjects = new List<DrawableUIObject>();

            gridLayout = new Layout(Layout.flat, new Vector2(30, 30), new Vector2(128, 128));
            hexGrid = new Grid(game, 10, 10);

            player1 = new Player(1, "Denis");
            player2 = new Player(2, "Turner");
            player1.startTurn();

            testUnit = new Unit(game, Unit.infantry, player1);
            testUnit.PlaceUnit(0, 0, 0);
            testUnit2 = new Unit(game, Unit.cavalry, player2);
            testUnit2.PlaceUnit(5, 5, -10);
            testUnit3 = new Unit(game, Unit.artillery, player2);
            testUnit3.PlaceUnit(7, 7, -14);
            UnitManager.SetCurrentUnit(testUnit);

            UIManager.DrawUI();
        }
    }
}
