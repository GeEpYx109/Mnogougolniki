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
        protected static Pen p;

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static Shape()
        {
            R = 100;
            c = Color.Red;
            p = new Pen(c);
        }

        public abstract void Draw(Graphics G);
        public abstract bool IsInside(int mousex, int mousey);
    }

    class Circle : Shape
    {

        public Circle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            G.DrawEllipse(p, x - R, y - R, R * 2, R * 2);
        }

        public override bool IsInside(int mousex, int mousey)
        {
            if ((mousex - x) * (mousex - x) + (mousey - y) * (mousey - y) <= R * R)
            {
                return true;
            }
            else return false;
        }
    }

    class Triangle : Shape
    {

        public Triangle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            G.DrawLine(p, x - (float)Math.Sqrt(3) / 2 * R, y + R / 2, x + (float)Math.Sqrt(3) / 2 * R, y + R / 2);
            G.DrawLine(p, x + (float)Math.Sqrt(3) / 2 * R, y + R / 2, x, y - R);
            G.DrawLine(p, x, y - R, x - (float)Math.Sqrt(3) / 2 * R, y + R / 2);
        }

        public override bool IsInside(int mousex, int mousey)
        {
            int am = Math.Sqrt()
        }
    }

    class Square : Shape
    {

        public Square(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics G)
        {
            G.DrawLine(p, x - R/(float)Math.Sqrt(2), y - R/(float)Math.Sqrt(2), x + R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2));
            G.DrawLine(p, x + R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2), x + R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2));
            G.DrawLine(p, x + R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2), x - R / (float)Math.Sqrt(2), y + R/(float)Math.Sqrt(2));
            G.DrawLine(p, x - R / (float)Math.Sqrt(2), y + R / (float)Math.Sqrt(2), x - R / (float)Math.Sqrt(2), y - R / (float)Math.Sqrt(2));
        }

        public override bool IsInside(int mousex, int mousey)
        {
            if (Math.Abs(x - mousex) <= R / (float)Math.Sqrt(2) && Math.Abs(y - mousey) <= R / (float)Math.Sqrt(2))
            {
                return true;
            }
            else return false;
        }
    }
}
