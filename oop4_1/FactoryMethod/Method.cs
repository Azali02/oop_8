using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop4_1.Figures;

namespace oop4_1.FactoryMethod
{
    internal class Method
    {
        int countC = 1;
        int countS = 1;
        int countT = 1;
        int countG = 1;

        public virtual Figure CreateShape(string code)
        {
            Figure shape = null;
            string[] words = code.Split(' ');
            char f = char.Parse(words[0]);
            if ( f == 'G')
            {
                shape = new GGroup(countG);
                countG++;
                return shape;
            }
            int x = int.Parse(words[1]);
            int y = int.Parse(words[2]);
            int a = int.Parse(words[3]);
            char c = char.Parse(words[4]);
            Color color = Color.White;
            switch (c)
            {
                case 'B':
                    color = Color.Black;
                    break;
                case 'S':
                    color = Color.Blue;
                    break;
                case 'G':
                    color = Color.Green;
                    break;
                case 'Y':
                    color = Color.Yellow;
                    break;
                case 'R':
                    color = Color.Red;
                    break;
                default:
                    break;
            }
            switch (f)
            {
                case 'C':
                    shape = new CCircle(x, y, countC);
                    countC++;
                    break;
                case 'S':
                    shape = new Square(x, y, countS);
                    countS++;
                    break;
                case 'T':
                    shape = new Triangle(x, y, countT);
                    countT++;
                    break;
                default:
                    break;
            }
            shape.a = a;
            shape.pen.Color = color;
            return shape;
        }

        public GGroup LoadGroup(string code, StreamReader reader, Method factory, int i)
        {
            GGroup group = new GGroup(countG);
            countG++;
            string[] words = code.Split(' ');
            int n = int.Parse(words[1]);
            i += n;
            for (int j = 0; j < n; j++)
            {
                string _code = reader.ReadLine();
                if (_code[0] == 'G')
                {
                    group.Add(LoadGroup(_code, reader, factory, j));
                }
                else
                {
                    Figure shapeGroupe = factory.CreateShape(_code);
                    if (shapeGroupe != null) { group.Add(shapeGroupe); }
                }
            }
            return group;
        }
    }
}
