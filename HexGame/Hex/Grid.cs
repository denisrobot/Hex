using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;
using HexGame.Units;
using HexGame.Core;

namespace HexGame {
    public class Grid : DrawableObject {
        int rows, cols;
        List<Vector2> gridTable;
        Texture2D sprite, square;
        List<Hex> path;
        List<Texture2D> spriteList;
        Random rnd;

        public Grid(Game game, int rows, int cols) : base(game) {
            this.rows = rows;
            this.cols = cols;
            gridTable = new List<Vector2>();
            spriteList = new List<Texture2D>();
            rnd = new Random();
            path = null;
            fillTable();

        }

        public int RowsCount {
            get { return rows; }
        }
        public int ColsCount {
            get { return cols; }
        }
        // TODO: missing s coordinate is causing some problems:
        // for example adding a unit to 7,7,7 actually puts it to the hex 7,7,-14
        private void fillTable() {
            for (int r = 0; r < rows; r++) {
                for (int q = 0; q < cols; q++) {
                    gridTable.Add(new Vector2(r, q));
                }
            }
        }

        public List<Hex> getHexes() {
            List<Hex> hexes = new List<Hex>();
            for (int i = 0; i < gridTable.Count; i++) {
                hexes.Add(new Hex((int)gridTable[i].X, (int)gridTable[i].Y, 0));
            }
            return hexes;
        }

        public Hex GetCurrentMouseHoverHex() {
            Hex currentHex = FractionalHex.HexRound(
                Layout.PixelToHex(GameManager.gridLayout, new Vector2(
                    Controls.getMousePosition().X, Controls.getMousePosition().Y)));
            return currentHex;
        }

        public void SetUnitPath(List<Hex> path) {
            this.path = path;
        }

        public void ClearUnitPath() {
            path = null;
        }
        
        protected override void LoadContent() {
            spriteList.Add(Game.Content.Load<Texture2D>("Sprites/hex_grass_1.png"));
            spriteList.Add(Game.Content.Load<Texture2D>("Sprites/hex_grass_2.png"));
            spriteList.Add(Game.Content.Load<Texture2D>("Sprites/hex_grass_3.png"));
            spriteList.Add(Game.Content.Load<Texture2D>("Sprites/hex_grass_4.png"));
            sprite = Game.Content.Load<Texture2D>("Sprites/hex_grass_1.png");
            square = Game.Content.Load<Texture2D>("Sprites/square.png");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void drawObject(SpriteBatch spriteBatch) {
            /* Drawing hex hover */
            Hex currentHex = GetCurrentMouseHoverHex();
            for (int i = 0; i < getHexes().Count; i++) {
                Color color = new Color();

                if (getHexes()[i].q == currentHex.q && getHexes()[i].r == currentHex.r) {
                    color = Color.Blue;
                } else {
                    color = Color.White;
                }
                spriteBatch.Draw(
                    sprite,
                    Layout.HexToPixel(GameManager.gridLayout, getHexes()[i]),
                    null,
                    color,
                    0f,
                    new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                    GameManager.gridLayout.size / sprite.Width * 2f,
                    SpriteEffects.None,
                    0f);
            }

            /* Registering grid mouse clicks. To be moved to Controls */
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {

                Hex clickedHex = FractionalHex.HexRound(
                    Layout.PixelToHex(
                        GameManager.gridLayout,
                        new Vector2(Controls.getMousePosition().X, Controls.getMousePosition().Y)
                    )
                );
                Unit currentUnit = UnitManager.GetCurrentUnit();
                if (currentUnit.State == Unit.States.Moving) {
                    MoveManager.MoveUnit(currentUnit, clickedHex);                
                } else {
                    UnitManager.SelectUnitByHex(clickedHex);
                }
            }

            /* Drawing unit move path */
            // TODO: this should be moved somewhere else.
            if (path != null) {
                Color color = Color.Green;
                for (int i = 0; i < path.Count; i++) {
                    if (i > UnitManager.GetCurrentUnit().CurrentMove()) {
                        color = Color.Red;
                    } 
                    spriteBatch.Draw(
                        sprite,
                        Layout.HexToPixel(GameManager.gridLayout, path[i]),
                        null,
                        color,
                        0f,
                        new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                         GameManager.gridLayout.size / sprite.Width * 2,
                        SpriteEffects.None,
                        0f);
                }
            }
        }

    }        
}

public struct HexagonalGrid {
    public int radius;

    public HexagonalGrid(int radius) {
        this.radius = radius;
    }

    public static List<Vector2> getTable(int radius) {
        List<Vector2> gridTable = new List<Vector2>();
        for (int r = -radius; r <= radius; r++) {
            for (int q = -radius; q <= radius; q++) {
                gridTable.Add(new Vector2(r, q));
            }
        }
        return gridTable;
    }
}
