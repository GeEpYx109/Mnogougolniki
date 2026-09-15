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
        int x ;
        int y;
        static int R;
        static string Color;

        public Shape(int x, int y, int R, string Color)
        {
            this.x = x;
            this.y = y;
            Shape.R = R;
            Shape.Color = Color;
        }
    }

    class Circle : Shape
    {
        public Circle(int x, int y, int R, string Color) : base(x, y, R, Color)
        {
        }
    }
    
    class Triangle : Shape
    {
        public Triangle(int x, int y, int R, string Color) : base(x, y, R, Color)
        {
        }
    }
    
    class Square : Shape
    {
        public Square(int x, int y, int R, string Color) : base(x, y, R, Color)
        {
        }
    }
}
