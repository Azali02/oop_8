using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace oop4_1.Figures
{
    internal class Square : Figure
    {
        public int k = 1;
        public Square(int x, int y, int count)
        {
            this.x = x;
            this.y = y;
            pen = new Pen(Color.Red, 3);
            this.k = count;
            this._name = "Square " + k.ToString();
        }
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(pen, x - a / 2, y - a / 2, a, a);
        }
        public override bool isClickedOnFigure(int X, int Y) //определение попадания в фигуру
        {
            return X >= x - a / 2 && X <= x + a / 2 && Y >= y - a / 2 && Y <= y + a / 2;
        }
        public override void Save(string filename)
        {
            string inf = "S " + x.ToString()
                           + " " + y.ToString() + " " + a.ToString()
                           + " " + ColorCode();
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine(inf);
            }
        }
    }
}
