using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mnogougolniki
{
    public partial class Form1 : Form
    {
        static Random rnd = new Random();
        Stopwatch sw;

        int[] chartN = { 50, 100, 150, 200, 250, 300, 350, 400, 450, 500 };
        double[] tDef = new double[10];
        double[] tJar = new double[10];
        bool chartReady = false;
        Pen hull1 = new Pen(Color.Green);
        int lastX;
        int lastY;
        bool isMoving = false;
        enum vibor {Circle, Square, Triangle};
        vibor figura = vibor.Circle;
        bool isInsideAny;

        enum algorythm {Def, Jar, Chart}
        algorythm algor = algorythm.Def;

        List <Shape> shapes;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
            shapes = new List <Shape> ();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            if (algor == algorythm.Def) drawhull(G);
            else if (algor == algorythm.Jar) drawhullJarvis (G);
            if (algor != algorythm.Chart)
            {
                for (int i = 0; i < shapes.Count - 1; i++)
                {
                    shapes[i].Draw(G);
                }
                int final = shapes.Count - 1;
                if (final >= 0)
                {
                    if (final <= 2)
                        shapes[final].Draw(G);
                    else
                        if (shapes[final].Connected == true || isInsideAny == true) shapes[final].Draw(G);

                }
            }
            else
            {
                int left = 60;
                int right = ClientSize.Width - 40;
                int top = 60;
                int bottom = ClientSize.Height - 40;

                Pen axis = new Pen(Color.Black);
                Pen penDef = new Pen(Color.Blue);
                Pen penJar = new Pen(Color.Red);

                G.DrawLine(axis, left, bottom, left, top);
                G.DrawLine(axis, left, bottom, right, bottom);

                double maxT = 0.001;
                for (int i = 0; i < chartN.Length; i++)
                    maxT = Math.Max(maxT, Math.Max(tDef[i], tJar[i]));
                int maxN = chartN[chartN.Length - 1];

                float prevX = left;
                float prevDefY = bottom;
                float prevJarY = bottom;
                for (int i = 0; i < chartN.Length; i++)
                {
                    float x = left + (float)(right - left) * chartN[i] / maxN;
                    float yDef = bottom - (float)((bottom - top) * tDef[i] / maxT);
                    float yJar = bottom - (float)((bottom - top) * tJar[i] / maxT);

                    G.DrawLine(penDef, prevX, prevDefY, x, yDef);
                    G.DrawLine(penJar, prevX, prevJarY, x, yJar);

                    prevX = x;
                    prevDefY = yDef;
                    prevJarY = yJar;
                }

                G.DrawString("Def", Font, Brushes.Blue, right - 80, top);
                G.DrawString("Jarvis", Font, Brushes.Red, right - 80, top + 20);
                G.DrawString(maxT.ToString("F1") + " мс", Font, Brushes.Black, 5, top);
                G.DrawString(maxN + " точек", Font, Brushes.Black, right - 50, bottom + 5);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isInsideAny = false;
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
                    if (shapes.Count >= 3 && IsInsideHull(e.X, e.Y))
                    {
                        foreach (Shape s in shapes)
                        {
                            s.Touched = true;
                        }
                    }
                    else
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
            if (shapes.Count >= 3)
            {
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (!shapes[i].Connected)
                    {
                        shapes.RemoveAt(i);
                    }
                }
            }
            Refresh();
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
                foreach (Shape s in shapes)
                {
                    s.Connected = false;
                }
                for (int i = 0; i <= shapes.Count - 1; i++)
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
                        if ((downs == 0 || ups == 0) && G != null)
                        {
                            G.DrawLine(hull1, shapes[i].X, shapes[i].Y, shapes[j].X, shapes[j].Y);
                            shapes[i].Connected = true;
                            shapes[j].Connected = true;
                        }
                    }
                }
            }
        }

        private void drawhullJarvis(Graphics G)
        {
            if (shapes.Count < 3) return;
            foreach (Shape s in shapes) s.Connected = false;
            Shape first = shapes[0];
            foreach (Shape s in shapes)
            {
                if (s.Y > first.Y || (s.Y == first.Y && s.X < first.X))
                    first = s;
            }

            Shape current = first;
            double dX = -1;
            double dY = 0;

            do
            {
                Shape next = null;
                double cos = 1;


                foreach (Shape s in shapes)
                {
                    if (s == current) continue;
                    double vx = s.X - current.X;
                    double vy = s.Y - current.Y;
                    double distance = Math.Sqrt(vx * vx + vy * vy);

                    double costemp = (dX * vx + dY * vy) / (Math.Sqrt(dX * dX + dY * dY) * distance);
                    if (costemp < cos)
                    {
                        cos = costemp;
                        next = s;
                    }
                }
                current.Connected = true;
                next.Connected = true;

                if (G != null)
                {
                    G.DrawLine(hull1, current.X, current.Y, next.X, next.Y);
                }
                dX = current.X - next.X;
                dY = current.Y - next.Y;
                current = next;
            } while (current != first);
        }
        private bool IsInsideHull(int mx, int my)
        {
            bool isinside = false;

            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    int ups = 0;
                    int downs = 0;

                    bool k0 = shapes[i].X == shapes[j].X;
                    double k = 0;
                    double b = 0;
                    if (!k0)
                    {
                        k = (double)(shapes[i].Y - shapes[j].Y) / (shapes[i].X - shapes[j].X);
                        b = shapes[i].Y - k * shapes[i].X;
                    }

                    for (int p = 0; p < shapes.Count; p++)
                    {
                        if (p == i || p == j) continue;

                        if (k0)
                        {
                            if (shapes[p].X > shapes[i].X) ups++;
                            else if (shapes[p].X < shapes[i].X) downs++;
                        }
                        else
                        {
                            if (shapes[p].Y > (k * shapes[p].X + b)) ups++;
                            else if (shapes[p].Y < (k * shapes[p].X + b)) downs++;
                        }
                    }

                    if (ups == 0 || downs == 0)
                    {
                        bool mouseUp;
                        bool mouseDown;
                        if (k0)
                        {
                            mouseUp = mx > shapes[i].X;
                            mouseDown = mx < shapes[i].X;
                        }
                        else
                        {
                            mouseUp = my > (k * mx + b);
                            mouseDown = my < (k * mx + b);
                        }

                        if (ups > 0 && mouseDown) return false;
                        if (downs > 0 && mouseUp) return false;
                        isinside = true;
                    }
                }
            }
            return isinside;
        }

        private void byDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algor = algorythm.Def;
            Refresh();
        }

        private void jarvisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algor = algorythm.Jar;
            Refresh();
        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!chartReady) CalcChart();
            algor = algorythm.Chart;
            Refresh();
        }

        private void CalcChart()
        {
            List<Shape> saved = shapes;

            for (int i = 0; i < chartN.Length; i++)
            {
                shapes = new List<Shape>();
                while (shapes.Count < chartN[i])
                {
                    int x = rnd.Next(0, 500);
                    int y = rnd.Next(0, 500);

                    bool exists = false;
                    foreach (Shape s in shapes)
                    {
                        if (s.X == x && s.Y == y) exists = true;
                    }
                    if (!exists) shapes.Add(new Circle(x, y));
                }

                sw = Stopwatch.StartNew();
                drawhull(null);
                sw.Stop();
                tDef[i] = sw.Elapsed.TotalMilliseconds;

                sw = Stopwatch.StartNew();
                drawhullJarvis(null);
                sw.Stop();
                tJar[i] = sw.Elapsed.TotalMilliseconds;
            }
            shapes = saved;
            chartReady = true;
        }
    }
}