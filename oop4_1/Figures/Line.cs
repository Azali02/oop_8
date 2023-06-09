﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop4_1.Figures
{
    internal class Line : Figure
    {
        public Figure shape1;
        public Figure shape2;
        public bool dec = false;

        public Line(int count)
        {
            pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.Dash;
            this.k = count;
            this._name = "Line " + k.ToString();
        }

        public void addLine(Figure s1, Figure s2)
        {
            x = s1.x;
            y = s1.y;
            shape1 = s1;
            shape2 = s2;
            s2.AddObserver(s1);
        }

        public override void Save(string filename)
        {
            //--------------
        }

        //public override void Load(StreamReader stream)
        //{
        //    string[] values = stream.ReadLine().Split(' ');
        //    p.X = int.Parse(values[0]);
        //    p.Y = int.Parse(values[1]);
        //    R = int.Parse(values[2]);
        //    Colored = char.Parse(values[3]);
        //}

        public override bool isClickedOnFigure(int x, int y)
        {
            return false;
        }

        public override bool DecoratorCheck()
        {
            return dec;
        }

        override public void Draw(Graphics g)
        {
            Point startPoint = new Point(shape1.x, shape1.y);
            Point endPoint = new Point(shape2.x, shape2.y);

            // Рисуем линию между начальной и конечной точками
            g.DrawLine(pen, startPoint, endPoint);
            g.DrawEllipse(pen, shape1.x - shape1.a / 12, shape1.y - shape1.a / 12, shape1.a / 6, shape1.a / 6);
            return;
        }

        public void Del()
        {
            shape1.RemoveObserver();
            shape2.RemoveObserver();
            this.RemoveObserver();
        }
    }
}
