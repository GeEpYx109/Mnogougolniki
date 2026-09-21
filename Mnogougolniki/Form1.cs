using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mnogougolniki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Pen p = new Pen(Color.Red);
            G.DrawLine(p, 100, 100, 200, 200);
            Pen p1 = new Pen(Color.Black);
            G.DrawEllipse(p1, 300, 300, 100, 100);

            Circle a = new Circle(100, 100);
            a.Draw(G);
            Triangle b = new Triangle(400, 100);
            b.Draw(G);
        }
    }

    abstract class Shape
    {
        int x;
        int y;
        static int R;
        static Color c;

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static Shape()
        {
            R = 10;
            c = Color.Red;
        }
    }

    class Circle : Shape
    {

        public Circle(int x, int y) : base(x, y)
        {
        }
    }

    class Triangle : Shape
    {

        public Triangle(int x, int y) : base(x, y)
        {
        }
    }

    class Square : Shape
    {

        public Square(int x, int y) : base(x, y)
        {
        }
    }
}