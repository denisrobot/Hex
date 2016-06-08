using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;
using HexGame.Units;
using HexGame.Core;
using HexGame.UI;

namespace HexGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private VideoSettings videoSettings;
        public static List<DrawableObject> drawableGameObjects;
        public static List<DrawableUIObject> drawableUIObjects;

        public static Camera camera;
        private Controls controls;
        public static List<Player> players;

        public static Layout testLayout;
        public Grid hexGrid;
        private Pathfinding pf;
        private Player player1;
        private Player player2;
        private Unit testUnit;

        private TextElement testText1;
        private TextElement testText2;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            videoSettings = new VideoSettings(this, graphics);
            controls = new Controls();
            players = new List<Player>();
            Content.RootDirectory = "Content";

        }

        protected override void Initialize() {
            videoSettings.setResolution(1280, 800);
            //videoSettings.setFullscreen();
            camera = new Camera(videoSettings.getResolution().X, videoSettings.getResolution().Y);
            drawableGameObjects = new List<DrawableObject>();
            drawableUIObjects = new List<DrawableUIObject>();
            this.IsMouseVisible = true;

            testLayout = new Layout(Layout.flat, new Vector2(30, 30), new Vector2(100, 100));
            hexGrid = new Grid(this, 10, 10);

            pf = new Pathfinding(hexGrid);

            player1 = new Player(1, "Denis");
            player2 = new Player(2, "Turner");
            player1.startTurn();

            testUnit = new Unit(this, Unit.infantry, player1, "Sprites/InfantryPlaceholder.png");
            testUnit.PlaceUnit(0, 0, 0);

            foreach(KeyValuePair<Guid, Unit> entry in UnitManager.GetUnits) {
                Console.WriteLine(entry.Key);
                Console.WriteLine(entry.Value.GetOwner.name);
            }

            testText1 = new TextElement(this, 200, 30, Vector2.Zero, FontManager.R15(this), null);
            testText1.BackgroundColor = Color.LightGray;
            testText1.Text = "LOL 1";

            testText2 = new TextElement(this, 300, 300, new Vector2(500, 500), FontManager.R12(this), testText1);
            testText2.BackgroundColor = Color.Black;
            testText2.Text = "LOL 2";
            testText2.AlignWithParent(Vector2.Zero);
            testText2.AddPadding(0, 0, 0, 0);
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.applyOffset();
            camera.enableControls();
            controls.Update();
            hexGrid.Visible = true;
            testText1.Visible = true;
            testText2.Visible = true;
            //testUnit.Visible = true;
            //testUnit.Enabled = true;
            if (TurnManager.currentTurn != null) {
                testText1.Text = "Current turn: " + TurnManager.currentTurn.name;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.AlphaBlend, 
                null, null, null, null,
                camera.getTransform());
            foreach (DrawableObject drawableObject in drawableGameObjects) {
                if (drawableObject.Visible) {
                    drawableObject.Draw(gameTime);
                }
            }
            foreach(DrawableUIObject drawableUIObject in drawableUIObjects) {
                if (drawableUIObject.Visible) {
                    drawableUIObject.Draw(gameTime);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        /* Sets mousePosition equal to mouse position (with adjusting for world/camera coords.) */
        public Vector2 getMousePosition() {
            Matrix inverse = Matrix.Invert(camera.getTransform());
            Vector2 vectorMousePosition = Vector2.Transform(
               new Vector2(Mouse.GetState().X, Mouse.GetState().Y), inverse);
            return new Vector2((int)vectorMousePosition.X, (int)vectorMousePosition.Y);
        }
    }
}
