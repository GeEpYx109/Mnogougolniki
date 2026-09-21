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

            Circle a = new Circle(100, 100);
            a.Draw(G);
            Triangle b = new Triangle(100, 100);
            b.Draw(G);
            Square c = new Square(100, 100);
            c.Draw(G);
        }
    }
}