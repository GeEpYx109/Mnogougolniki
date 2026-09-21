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
    }

    class Shape
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

        static Shape() {
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