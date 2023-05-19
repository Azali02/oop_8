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
