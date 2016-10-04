using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HexGame {
    public struct Layout {
        private Vector2 origin;
        public Vector2 size;
        private Orientation orientation;

        public Layout(Orientation orientation, Vector2 origin,  Vector2 size) {
            this.orientation = orientation;
            this.origin = origin;
            this.size = size;
        }

        static public Orientation pointy = new Orientation(
            (float)Math.Sqrt(3.0), (float)Math.Sqrt(3.0) / 2.0f, 0.0f, 3.0f / 2.0f, 
            (float)Math.Sqrt(3.0) / 3.0f, -1.0f / 3.0f, 0.0f, 2.0f / 3.0f, 0.5f);

        static public Orientation flat = new Orientation(
            3.0f / 2.0f, 0.0f, (float)Math.Sqrt(3.0) / 2.0f, (float)Math.Sqrt(3.0), 
            2.0f / 3.0f, 0.0f, -1.0f / 3.0f, (float)Math.Sqrt(3.0) / 3.0f, 0.0f);

        static public Vector2 HexToPixel(Layout layout, Hex c) {
            Orientation M = layout.orientation;
            float x = (M.f0 * c.q + M.f1 * c.r) * layout.size.X;
            float y = (M.f2 * c.q + M.f3 * c.r) * layout.size.Y;
            return new Vector2(x + layout.origin.X, y + layout.origin.Y);
        }

        static public FractionalHex PixelToHex(Layout layout, Vector2 p) {
            Orientation M = layout.orientation;
            Vector2 size = layout.size;
            Vector2 origin = layout.origin;
            Vector2 pt = new Vector2((p.X - origin.X) / size.X, (p.Y - origin.Y) / size.Y);
            double q = M.b0 * pt.X + M.b1 * pt.Y;
            double r = M.b2 * pt.X + M.b3 * pt.Y;
            return new FractionalHex(q, r, -q - r);
        }

        static public Vector2 HexCornerOffset(Layout layout, int corner) {
            Vector2 size = layout.size;
            float angle = 2f * (float)Math.PI * (corner + layout.orientation.startAngle) / 6;
            return new Vector2(size.X * (float)Math.Cos(angle), size.Y * (float)Math.Sin(angle));
        }

        static public List<Vector2> PolygonCorners(Layout layout, Hex c) {
            List<Vector2> corners = new List<Vector2>();
            Vector2 center = HexToPixel(layout, c);
            for (int i = 0; i < 6; i++) {
                Vector2 offset = HexCornerOffset(layout, i);
                corners.Add(new Vector2(center.X + offset.X, center.Y + offset.Y));
            }
            return corners;
        }

        public float GetHeight() {
            return this.size.X * 2f;
        }

        public float GetWidth() { 
            return (float)Math.Sqrt(3) / 2f * GetHeight();
        }
    }

    public struct Orientation {
        public float f0, f1, f2, f3;
        public float b0, b1, b2, b3;
        public float startAngle;

        public Orientation(float f0, float f1, float f2, float f3,
                           float b0, float b1, float b2, float b3,
                           float startAngle) {
            this.f0 = f0; this.f1 = f1; this.f2 = f2; this.f3 = f3;
            this.b0 = b0; this.b1 = b1; this.b2 = b2; this.b3 = b3;
            this.startAngle = startAngle;
        }
    }
}
