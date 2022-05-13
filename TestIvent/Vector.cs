using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIvent
{
    public class Vector
    {
        public Vector(float X, float Y)
        {
            x = X;
            y = Y;
        }
        public Vector(PointF p1, PointF p2) : this(p2.X - p1.X, p2.Y - p1.Y) { }
        public Vector(PointF p) : this(-p.X, -p.Y) { }
        public float x, y;
        public float getLength() => (float)Math.Sqrt(x * x + y * y);
        public static Vector operator +(Vector V1, Vector V2) => new Vector(V1.x + V2.x, V1.y + V2.y);
        public static Vector operator -(Vector V1, Vector V2) => new Vector(V1.x - V2.x, V1.y - V2.y);
        public static PointF operator +(PointF p, Vector v) => new PointF(v.x + p.X, v.y + p.Y);
        public static PointF operator -(PointF p, Vector v) => new PointF(v.x - p.X, v.y - p.Y);
        public static Vector operator *(Vector v, float f) => new Vector(v.x * f, v.y * f);
        public static Vector operator /(Vector v, float f) => new Vector(v.x / f, v.y / f);
        public static Vector operator -(Vector V) => new Vector(-V.x, -V.y);
        public PointF ToPointF() => new PointF(x, y);
        public Vector ToUnitVector() => this / getLength();
        public Vector ToLength(float length) => ToUnitVector() * length;
        //public void SetLength(float length) => this = ToLength(length);

        public override string ToString()
        {
            return $"({x};{y})";
        }
    }
}
