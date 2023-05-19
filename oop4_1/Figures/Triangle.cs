﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace oop4_1.Figures
{
    internal class Triangle : Figure
    {
        private double h;
        Point[] vec = new Point[3];

        public Triangle(int x, int y, int count)
        {
            this.x = x;
            this.y = y;
            pen = new Pen(Color.Red, 3);
            this.k = count;
            this._name = "Triangle " + k.ToString();
        }
        public override void Draw(Graphics g)
        {
            h = a * Math.Sqrt(3) / 2;
            vec[0] = new Point(x + a / 2, (int)(y + h / 2));
            vec[1] = new Point(x - a / 2, (int)(y + h / 2));
            vec[2] = new Point(x, (int)(y - h / 2));

            g.DrawPolygon(pen, vec);
        }
        public override bool isClickedOnFigure(int X, int Y)
        {
            float alpha = ((float)(vec[1].Y - vec[2].Y) * (X - vec[2].X) + (float)(vec[2].X - vec[1].X) * (Y - vec[2].Y)) /
                  ((float)(vec[1].Y - vec[2].Y) * (vec[0].X - vec[2].X) + (float)(vec[2].X - vec[1].X) * (vec[0].Y - vec[2].Y));
            float beta = ((float)(vec[2].Y - vec[0].Y) * (X - vec[2].X) + (float)(vec[0].X - vec[2].X) * (Y - vec[2].Y)) /
                         ((float)(vec[1].Y - vec[2].Y) * (vec[0].X - vec[2].X) + (float)(vec[2].X - vec[1].X) * (vec[0].Y - vec[2].Y));
            float gamma = 1.0f - alpha - beta;

            return alpha >= 0 && beta >= 0 && gamma >= 0;
        }
        public override void Save(string filename)
        {
            string inf = "T " + x.ToString()
                           + " " + y.ToString() + " " + a.ToString()
                           + " " + ColorCode();
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine(inf);
            }
        }

    }
}
