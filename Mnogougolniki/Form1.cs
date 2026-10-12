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
        enum vibor {Circle, Square, Triangle};
        vibor figura = vibor.Circle;

        List <Shape> shapes;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            shapes = new List <Shape> ();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            foreach(Shape s in shapes)
            {
                s.Draw(G);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool isInsideAny = false;
            foreach (Shape s in shapes)
            {
                if (s.IsInside(e.X, e.Y))
                {
                    s.Touched = true;
                    isInsideAny = true;
                }
            }
            lastX = e.X;
            lastY = e.Y;
            if (!isInsideAny)
            {
                switch (figura)
                {
                    case (vibor.Circle):
                        shapes.Add(new Circle(e.X, e.Y));
                        break;
                    case (vibor.Square):
                        shapes.Add(new Square(e.X, e.Y));
                        break;
                    case (vibor.Triangle):
                        shapes.Add(new Triangle(e.X, e.Y));
                        break;

                }
                Refresh();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int movex = e.X - lastX;
            int movey = e.Y - lastY;
            lastX = e.X;
            lastY = e.Y;
            foreach(Shape s in shapes)
            {
                if (s.Touched)
                {
                    s.X += movex;
                    s.Y += movey;
                    isMoving = true;
                }
            }
            if (isMoving) Refresh();
            isMoving = false;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Shape s in shapes)
            {
                s.Touched = false;
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figura = vibor.Circle;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figura = vibor.Square;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figura = vibor.Triangle;
        }
    }
}