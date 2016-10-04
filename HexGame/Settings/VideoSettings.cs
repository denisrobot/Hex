using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using HexGame.Core;

namespace HexGame.Settings {
    public class VideoSettings {
        private static int desktopWidth { get; set; }
        private static int desktopHeight { get; set; }

        public static void InitVideoSettings() {
            desktopWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            desktopHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            GameManager.game.graphics.PreferredBackBufferWidth = desktopWidth;
            GameManager.game.graphics.PreferredBackBufferHeight = desktopHeight;
        }

        public static void SetResolution(int width, int height) {
            GameManager.game.graphics.PreferredBackBufferWidth = width;
            GameManager.game.graphics.PreferredBackBufferHeight = height;
            GameManager.game.graphics.ApplyChanges();
        }
        public static Point GetResolution() {
            return new Point(GameManager.game.graphics.PreferredBackBufferWidth, GameManager.game.graphics.PreferredBackBufferHeight);
        }
        public static void SetFullscreen() {
            GameManager.game.graphics.IsFullScreen = true;
            GameManager.game.graphics.ApplyChanges();
        }
        public static void SetWindowed() {
            GameManager.game.graphics.IsFullScreen = false;
            GameManager.game.graphics.ApplyChanges();
        }
    }
}