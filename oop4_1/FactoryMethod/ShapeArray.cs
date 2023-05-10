using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop4_1.Figures;

namespace oop4_1.FactoryMethod
{
    internal class ShapeArray : Method
    {
        public void LoadShapes(string filename, Method factory, List<Figure> figure)
        {
            for (int i = 0; i < figure.Count;)
            {
                figure.Remove(figure[i]);
            }
            FileStream stream = new FileStream(filename, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            int count = int.Parse(reader.ReadLine());
            Load(count, reader, factory, figure);
            
            reader.Close();
            stream.Close();
        }
        public GGroup LoadGroup(string code, StreamReader reader, Method factory, int i)
        {
            GGroup group = new GGroup();
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
        public void Load(int count, StreamReader reader, Method factory, List<Figure> figure)
        {
            for (int i = 0; i < count; i++)
            {
                string code = reader.ReadLine();
                if (code[0] == 'G')
                {
                    
                    figure.Add(LoadGroup(code, reader, factory, i));
                }
                else
                {
                    Figure shape = factory.CreateShape(code);
                    if (shape != null)
                    {
                        figure.Add(shape);
                    }
                }
            }
        }
    }
}
