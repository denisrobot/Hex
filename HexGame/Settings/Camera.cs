using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame.Settings {
    public class Camera {
        //Constants
        private const float ZOOM_SPEED = 0.02f;
        private const float CAMERA_SPEED = 15f;

        //Variables
        private Matrix transform;
        public Vector2 Position;
        public Vector2 Offset;
        private Point Resolution;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        private float leftBorder, rightBorder, topBorder, bottomBorder;
        private float rotation;
        public float zoom, maxZoom, minZoom;

        public Camera(int width, int height) {
            Offset = Vector2.Zero;
            Resolution = new Point(width, height);
            Position = new Vector2(Resolution.X * 0.5f, Resolution.Y * 0.5f);
            rotation = 0;
            zoom = 1f;
            maxZoom = 2f;
            minZoom = 0.1f;
        }

        public bool isOnScreen(Vector2 position) {
            int extraSpace = 32;

            leftBorder = Position.X - Resolution.X * 0.5f / zoom;
            rightBorder = Position.X + Resolution.X * 0.5f / zoom;
            topBorder = Position.Y - Resolution.Y * 0.5f / zoom;
            bottomBorder = Position.Y + Resolution.Y * 0.5f / zoom;

            if (position.X + extraSpace >= leftBorder &&
                position.X - extraSpace < rightBorder &&
                position.Y + extraSpace >= topBorder &&
                position.Y - extraSpace < bottomBorder) {
                return true;
            } else {
                return false;
            }
        }

        public void applyOffset() {
            Position.X += Offset.X;
            Position.Y += Offset.Y;
        }

        public void enableControls() {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Q)) {
                zoom += ZOOM_SPEED;
            } else if (currentKeyboardState.IsKeyDown(Keys.E)) {
                zoom -= ZOOM_SPEED;
            }
            if (currentKeyboardState.IsKeyDown(Keys.A)) {
                Position.X -= CAMERA_SPEED;
            } else if (currentKeyboardState.IsKeyDown(Keys.D)) {
                Position.X += CAMERA_SPEED;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W)) {
                Position.Y -= CAMERA_SPEED;
            } else if (currentKeyboardState.IsKeyDown(Keys.S)) {
                Position.Y += CAMERA_SPEED;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Z)) {
                zoom = 1;
            }

            if (zoom < minZoom) {
                zoom = minZoom;
            } else if (zoom > maxZoom) {
                zoom = maxZoom;
            }

            previousKeyboardState = currentKeyboardState;
        }

        public Matrix getTransform() {
            transform =
               Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(Resolution.X * 0.5f,
                   Resolution.Y * 0.5f, 0));

            return transform;
        }
    }
}