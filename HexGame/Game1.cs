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


namespace HexGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private VideoSettings videoSettings;
        public static Layout testLayout;
        public Grid hexGrid;
        private Pathfinding pf;
        private Texture2D terrain;
        public static Camera camera;
        private Controls controls;
        private Player player1;
        private Player player2;
        private Unit testUnit;

        public static List<DrawableObject> drawableGameObjects;
        public static List<Player> players;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            videoSettings = new VideoSettings(this, graphics);
            controls = new Controls();
            players = new List<Player>();
            Content.RootDirectory = "Content";

        }

        protected override void Initialize() {
            videoSettings.setResolution(1280, 800);
            camera = new Camera(videoSettings.getResolution().X, videoSettings.getResolution().Y);
            drawableGameObjects = new List<DrawableObject>();
            this.IsMouseVisible = true;

            testLayout = new Layout(Layout.flat, new Vector2(30, 30), new Vector2(100, 100));
            hexGrid = new Grid(this, 10, 10);

            pf = new Pathfinding(hexGrid);

            player1 = new Player(1, "Denis");
            player2 = new Player(2, "Turner");
            player1.startTurn();

            Damage testDmg = new Damage(5, 2, 1);

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
            //testUnit.Visible = true;
            //testUnit.Enabled = true;
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
