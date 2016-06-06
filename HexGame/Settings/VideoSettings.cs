using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexGame.Settings {
    public class VideoSettings {
        private GraphicsDeviceManager graphics;
        private int desktopWidth;
        private int desktopHeight;

        public VideoSettings(Game game, GraphicsDeviceManager graphics) {
            this.graphics = graphics;
            graphics.IsFullScreen = false;
            desktopWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            desktopHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = desktopWidth;
            graphics.PreferredBackBufferHeight = desktopHeight;
        }

        public void setResolution(int width, int height) {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }
        public Point getResolution() {
            return new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
        public void setFullscreen() {
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
        public void setWindowed() {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }
    }
}