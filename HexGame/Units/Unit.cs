using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HexGame.Core;
using HexGame.Settings;

namespace HexGame.Units {
    public class Unit : DrawableObject {
        public UnitStats stats;
        private Player owner;
        private Texture2D sprite;
        private int currentMorale;
        private int currentMove;
        private bool placed;
        private Vector2 coordinates;
        private Hex currentHex;
        private Guid id;
        public States State { get; set; }
        public enum States {
            Waiting,
            Moving,
            Engaged,
            Attacking,
            Defending,
            Dead
        }
        
        static public UnitStats infantry = new UnitStats(10, 4, 1, "Infantry", "Sprites/InfantryPlaceholder.png");
        static public UnitStats cavalry = new UnitStats(30, 7, 5, "Cavalry", "Sprites/InfantryPlaceholder.png");
        static public UnitStats dragoon = new UnitStats(20, 6, 3, "Dragoon", "Sprites/InfantryPlaceholder.png");
        static public UnitStats artillery = new UnitStats(20, 2, 0, "Artillery", "Sprites/InfantryPlaceholder.png");

        public Unit(Game game, UnitStats stats, Player owner) : base(game) {
            this.stats = stats;
            this.owner = owner;
            coordinates = new Vector2();
            currentMorale = stats.BaseMorale;
            currentMove = stats.BaseMove;
            placed = false;
            id = Guid.NewGuid();
            State = States.Waiting;

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
        public int CurrentMove() {
            return currentMove;
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
            coordinates = Layout.HexToPixel(GameManager.gridLayout, currentHex);
            placed = true;
        }

        public void UpdateLocation(Hex hex) {
            coordinates = Layout.HexToPixel(GameManager.gridLayout, hex);
            currentHex = hex;
        }

        public Hex GetCurrentHex() {
            return currentHex;
        }

        public void RemoveUnit() {
            placed = false;
            this.UnloadContent();
        }

        public void InitiateMove() {
            State = States.Moving;
        }

        public void  Wait() {
            State = States.Waiting;
        }

        public override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            sprite = Game.Content.Load<Texture2D>(stats.SpriteName);

            base.LoadContent();
        }

        public override void drawObject(SpriteBatch spriteBatch) {
            if (placed) {
                spriteBatch.Draw(
                    sprite, 
                    coordinates, 
                    null, 
                    Color.White, 
                    0f, 
                    new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                    GameManager.gridLayout.size / sprite.Width, 
                    SpriteEffects.None, 
                    0f);
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public string GetInformationString() {
            string pattern = 
@"Type: {0}
Morale: {1}/{2}
Move: {3}/{4}
Owner: {5}
State: {6}";
            string info = string.Format(pattern,
                stats.Type,
                currentMorale,
                stats.BaseMorale,
                currentMove,
                stats.BaseMove,
                owner.name,
                State);
            return info;
        }
    }
}
