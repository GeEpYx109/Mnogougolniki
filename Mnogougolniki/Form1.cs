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
        Pen hull1 = new Pen(Color.Green);
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
            drawhull(G);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool isInsideAny = false;
            if (e.Button == MouseButtons.Left)
            {
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
            else if (e.Button == MouseButtons.Right)
            {
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (shapes[i].IsInside(e.X, e.Y))
                    {
                        shapes.RemoveAt(i);
                        Refresh();
                        break;
                    } 
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int movex = e.X - lastX;
            int movey = e.Y - lastY;
            lastX = e.X;
            lastY = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape s in shapes)
                {
                    if (s.Touched)
                    {
                        s.X += movex;
                        s.Y += movey;
                        isMoving = true;
                    }
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

        private void drawhull(Graphics G)
        {
            if (shapes.Count >= 3)
            {
                for(int i = 0; i <= shapes.Count - 1; i++)
                {
                    for (int j = i + 1; j <= shapes.Count - 1; j++)
                    {
                        int ups = 0;
                        int downs = 0;
                        bool k0 = shapes[i].X == shapes[j].X;
                        double k = 0;
                        double b = 0;
                        if(!k0)
                        {
                            k = (double)(shapes[i].Y - shapes[j].Y) / (shapes[i].X - shapes[j].X);
                            b = shapes[i].Y - k * shapes[i].X;
                        }
                        for (int p = 0; p <=shapes.Count - 1; p++)
                        {
                            if (p == i || p == j) continue;
                            if (k0)
                            {
                                if (shapes[p].X > shapes[i].X) ups++;
                                else if (shapes[p].X < shapes[i].X) downs++;
                            }
                            else
                            {
                                if (shapes[p].Y > (k * shapes[p].X + b)) ups += 1;
                                else if (shapes[p].Y < (k * shapes[p].X + b)) downs += 1;
                            }
                        }
                        if (downs == 0 || ups == 0)
                        {
                            G.DrawLine(hull1, shapes[i].X, shapes[i].Y, shapes[j].X, shapes[j].Y);
                        }
                    }
                }
            }
        }
    }
}