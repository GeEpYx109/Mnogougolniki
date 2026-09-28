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
        protected bool touched;// можно ли сделать паблик или лучше свойствами, как я реализовал
        protected static Brush br;

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            touched = false;
        }

        static Shape()
        {
            R = 100;
            c = Color.Red;
            p = new Pen(c);
            br = new SolidBrush(Color.LightCoral);
        }

         public bool Touched
        {
            get
            {
                return touched;
            }
            set
            {
                touched = value;
            }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
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
            double am = Math.Sqrt((mousex - (x - (float)Math.Sqrt(3) / 2 * R)) * (mousex - (x - (float)Math.Sqrt(3) / 2 * R)) + (mousey - (y + R / 2)) * (mousey - (y + R / 2)));
            double mb = Math.Sqrt((x - mousex) * (x - mousex) + (mousey - (y - R)) * (mousey - (y - R)));
            double mc = Math.Sqrt((x + (float)Math.Sqrt(3) / 2 * R - mousex) * (x + (float)Math.Sqrt(3) / 2 * R - mousex) + (mousey - (y + R / 2)) * (mousey - (y + R / 2)));
            double ac = x + (float)Math.Sqrt(3) / 2 * R - (x - (float)Math.Sqrt(3) / 2 * R);
            double cb = Math.Sqrt((x + (float)Math.Sqrt(3) / 2 * R - x) * (x + (float)Math.Sqrt(3) / 2 * R - x) + (y + R / 2 - (y - R)) * (y + R / 2 - (y - R)));
            double ab = Math.Sqrt((x - (x - (float)Math.Sqrt(3) / 2 * R)) * (x - (x - (float)Math.Sqrt(3) / 2 * R)) + (y + R / 2 - (y - R)) * (y + R / 2 - (y - R)));
            double p1 = (ab + cb + ac) / 2;
            double p2 = (am + mb + ab) / 2;
            double p3 = (mb + mc + cb) / 2;
            double p4 = (mc + am + ac) / 2;
            double Sabc = Math.Sqrt(p1 * (p1 - ac) * (p1 - cb) * (p1 - ab));
            double Samb = Math.Sqrt(p2 * (p2 - am) * (p2 - mb) * (p2 - ab));
            double Smbc = Math.Sqrt(p3 * (p3 - mb) * (p3 - mc) * (p3 - cb));
            double Smca = Math.Sqrt(p4 * (p4 - mc) * (p4 - am) * (p4 - ac));

            if ((Math.Abs(Sabc - (Samb + Smbc + Smca)) <= 1))
            {
                return true;
            }
            else
            {
                return false;
            }
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
