using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Settings;

namespace HexGame {
    public class Grid : DrawableObject {
        int rows, cols;
        List<Vector2> gridTable;
        Texture2D sprite;

        public Grid(Game game, int rows, int cols) : base(game) {
            this.rows = rows;
            this.cols = cols;
            this.gridTable = new List<Vector2>();
            fillTable();

        }

        public int RowsCount {
            get { return rows; }
        }
        public int ColsCount {
            get { return cols; }
        }

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

        protected override void LoadContent() {
            sprite = Game.Content.Load<Texture2D>("hex_forest.png");
        }

        public override void drawObject(SpriteBatch spriteBatch) {
            Hex currentHex = FractionalHex.HexRound(
                Layout.PixelToHex(Game1.testLayout, new Vector2(
                    Controls.getMousePosition().X, Controls.getMousePosition().Y)));
            for (int i = 0; i < getHexes().Count; i++) {
                Color color = new Color();

                if (getHexes()[i].q == currentHex.q && getHexes()[i].r == currentHex.r) {
                    color = Color.Red;
                } else {
                    color = Color.White;
                }
                spriteBatch.Draw(
                    sprite,
                    Layout.HexToPixel(Game1.testLayout, getHexes()[i]),
                    null,
                    color,
                    0f,
                    new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                     Game1.testLayout.size / sprite.Width * 2,
                    SpriteEffects.None,
                    0f);
            }
        }
    }
}
