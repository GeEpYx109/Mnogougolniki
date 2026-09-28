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
        public Form1()
        {
            InitializeComponent();
            a = new Circle(100,100);
            b = new Triangle(100,100);
            c = new Square(100,100);

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

        }
    }
}