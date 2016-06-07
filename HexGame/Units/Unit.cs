using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Core;

namespace HexGame.Units {
    public class Unit : DrawableObject {
        private UnitStats stats;
        private Player owner;
        private string spriteName;
        private Texture2D sprite;
        private int currentMorale;
        private int currentMove;
        private bool placed;
        private Vector2 coordinates;
        private Hex currentHex;
        private Guid id;

        enum States {
            Waiting,
            Moving,
            Engaged,
            Attacking,
            Defending,
            Dead
        }

        static public UnitStats infantry = new UnitStats(10, 4, 1, "Infantry");
        static public UnitStats cavalry = new UnitStats(30, 7, 5, "Cavalry");
        static public UnitStats dragoon = new UnitStats(20, 6, 3, "Dragoon");
        static public UnitStats artillery = new UnitStats(20, 2, 0, "Artillery");

        public Unit(Game game, UnitStats stats, Player owner, string spriteName) : base(game) {
            this.stats = stats;
            this.owner = owner;
            this.spriteName = spriteName;
            currentMorale = stats.BaseMorale;
            currentMove = stats.BaseMove;
            placed = false;
            id = Guid.NewGuid();

            owner.addUnit(this);
            UnitManager.AddUnit(this);
        }

        public void DecreaseMorale(int amount) {
            currentMorale = currentMorale - amount;
        }
        public void IncreaseMorale(int amount) {
            currentMorale = currentMorale + amount;
        }
        public void ModifyMorale(int h) {
            currentMorale = h;
        }
        public void ResetMorale() {
            currentMorale = stats.BaseMorale;
        }
        public void DecreaseMove(int amount) {
            currentMove = currentMove - amount;
        }
        public void IncreaseMove(int amount) {
            currentMove = currentMove + amount;
        }
        public void ModifyMove(int m) {
            currentMove = m;
        }
        public void ResetMove() {
            currentMove = stats.BaseMove;
        }
        public Guid GetID {
            get { return id; }
        }
        public Player GetOwner {
            get { return owner; }
        }

        public void PlaceUnit(int q, int r, int s) {
            currentHex = new Hex(q, r, s);
            coordinates = Layout.HexToPixel(Game1.testLayout, currentHex);
            placed = true;
        }

        public override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            sprite = Game.Content.Load<Texture2D>(spriteName);

            base.LoadContent();
        }

        public override void drawObject(SpriteBatch spriteBatch) {
            if (placed) {
                spriteBatch.Draw(sprite, coordinates, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
