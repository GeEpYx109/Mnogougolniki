using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mnogougolniki
{
    abstract class Shape
    {
        protected int x;
        protected int y;
        protected static int R;
        protected static Color c;

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static Shape()
        {
            R = 100;
            c = Color.Red;
        }

        public abstract void Draw(Graphics G);
    }

    class Circle : Shape
    {

        public Circle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            Pen p = new Pen(Color.Red);
            G.DrawEllipse(p, x - R, y - R, R * 2, R * 2);
        }
    }

    class Triangle : Shape
    {

        public Triangle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            Pen p = new Pen(c);
            G.DrawLine(p, x - (float)Math.Sqrt(3) / 2 * R, y + R / 2, x + (float)Math.Sqrt(3) / 2 * R, y + R / 2);
            G.DrawLine(p, x + (float)Math.Sqrt(3) / 2 * R, y + R / 2, x, y - R);
            G.DrawLine(p, x, y - R, x - (float)Math.Sqrt(3) / 2 * R, y + R / 2);
        }
    }

    class Square : Shape
    {

        public Square(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            Pen p = new Pen(c);
            G.DrawLine(p, x - R/(float)Math.Sqrt(2), y - R/(float)Math.Sqrt(2), x + R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2));
            G.DrawLine(p, x + R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2), x + R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2));
            G.DrawLine(p, x + R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2), x - R / (float)Math.Sqrt(2), y + R/(float)Math.Sqrt(2));
            G.DrawLine(p, x - R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2), x - R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2));
        }
    }
}
