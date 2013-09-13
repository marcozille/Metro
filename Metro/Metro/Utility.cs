using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Utility
{
    public class ControllaPunto
    {
        // guardare qui http://www.blackpawn.com/texts/pointinpoly/default.html

        private static float ProdottoScalare(PointF p1, PointF p2)
        {
            return (p1.X * p2.X) + (p1.Y * p2.Y);
        }

        public static bool ContenutoInTriangolo(Point point, Point[] vertici)
        {
            if(vertici.Length != 3)
                return false;

            PointF a = vertici[0];
            PointF b = vertici[1];
            PointF c = vertici[2];
            PointF p = point;

            PointF v0 = new PointF(c.X - a.X, c.Y - a.Y);
            PointF v1 = new PointF(b.X - a.X, b.Y - a.Y);
            PointF v2 = new PointF(p.X - a.X, p.Y - a.Y);

            float dot00 = ProdottoScalare(v0, v0);
            float dot01 = ProdottoScalare(v0, v1);
            float dot02 = ProdottoScalare(v0, v2);
            float dot11 = ProdottoScalare(v1, v1);
            float dot12 = ProdottoScalare(v1, v2);

            float invDenom = 1.0f / ((dot00 * dot11) - (dot01 * dot01));
            float u = ((dot11 * dot02) - (dot01 * dot12)) * invDenom;
            float v = ((dot00 * dot12) - (dot01 * dot02)) * invDenom;

            if ((u >= 0.0f) && (v >= 0.0f) && ((u + v) < 1.0f))
                return true;
            return false;
        }
    }
}
