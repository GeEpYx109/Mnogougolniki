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
        Circle a;
        Triangle b;
        Square c;
        int lastX;
        int lastY;
        bool isMoving = false;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;// спроси можно ли это использовать
            a = new Circle(100,100);
            b = new Triangle(200,200);
            c = new Square(500,500);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            a.Draw(G);
            b.Draw(G);
            c.Draw(G);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (a.IsInside(e.X, e.Y))
            {
                a.Touched = true;
            }
            if (b.IsInside(e.X, e.Y))
            {
                b.Touched = true;
            }
            if (c.IsInside(e.X, e.Y))
            {
                c.Touched = true;
            }
            lastX = e.X;
            lastY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int movex = e.X - lastX;
            int movey = e.Y - lastY;
            lastX = e.X;
            lastY = e.Y;
            if (a.Touched)
            {
                a.X += movex;
                a.Y += movey;
                isMoving = true;
            }
            if (b.Touched)
            {
                b.X += movex;
                b.Y += movey;
                isMoving = true;
            }
            if (c.Touched)
            {
                c.X += movex;
                c.Y += movey;
                isMoving = true;
            }
            if (isMoving) Refresh();
            isMoving = false;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            a.Touched = false;
            b.Touched = false;
            c.Touched = false;
        }
    }
}