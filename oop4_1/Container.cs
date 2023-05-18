using oop4_1.FactoryMethod;
using oop4_1.Figures;
using oop4_1.Observers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oop4_1
{
    internal class Container : Figure, Observer
    {
        const string filename = "C:/Users/Азалия/source/repos/oop4_1/oop4_1/FactoryMethod/save_figures.txt";
        private List<Figure> shapes;
        public bool ctrlPressed = false;
        public bool selectMany = false;
        //public Decorator decorator;
        //private TreeViewObserver treeViewObserver;

        //public Container(TreeViewObserver treeViewObserver)
        //{
        //    shapes = new List<Figure>();
        //    this.treeViewObserver = treeViewObserver;
        //    treeViewObserver.AddObserver(this);
        //    this.AddObserver(treeViewObserver);
        //}

        public Container()
        {
            shapes = new List<Figure>();
        }

        public void Add(int x, int y, string fType, Color color)
        {
            Figure newFigure = null;
            switch (fType)
            {
                case "Circle":
                    newFigure = new CCircle(x, y);
                    break;
                case "Square":
                    newFigure = new Square(x, y);
                    break;
                case "Triangle":
                    newFigure = new Triangle(x, y);
                    break;
                default:
                    return;
            }
            newFigure.SetColor(color);
            Figure dec = new Decorator(newFigure);
            shapes.Add(dec);
            //this.NotifyEveryone();
        }

        public bool isSelect(int x, int y)
        {
            bool q = false;
            for (int i = 0; i < shapes.Count; i++)
            {
                Figure f = shapes[i];
                if (f.isClickedOnFigure(x, y))
                {
                    q = true;
                    if (!f.DecoratorCheck())
                    {
                        Figure decoratedFigure = new Decorator(f);
                        shapes.RemoveAt(i);
                        shapes.Insert(i, decoratedFigure);
                        //this.NotifyEveryone();
                        if (selectMany == false)
                            break;
                        continue;
                    }
                }
            }
            return q;
        }

        public bool AddLine()
        {
            List<Figure> shapeLine = new List<Figure>();
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is Decorator dec)
                {
                    shapeLine.Add(dec.GetOriginalFigure());
                }
            }
            if (shapeLine.Count == 2)
            {
                Line line = new Line();
                line.addLine(shapeLine[0], shapeLine[1]);
                shapes.Add(line);
                //line.NotifyEveryone();
                return true;
            }
            return false;
        }

        public void unSelectAll()
        {
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i] is Decorator decorator)
                {
                    shapes[i] = decorator.GetOriginalFigure();
                }
                //else if (shapes[i].DecoratorCheck()) //выделенная группа
                //{
                //    shapes[i].UndecoratedGroup();
                //}
            }
        }

        public void delSelected()
        {
            int count = 0;
            for (int i = 0; i < shapes.Count;)
            {
                if (shapes[i].DecoratorCheck())
                {
                    shapes.RemoveAt(i);
                    count++;
                    continue;
                }
                ++i;
            }
            //this.NotifyEveryone();
        }

        public void delAll()
        {
            for (int i = 0; i < shapes.Count;)
            {
                shapes.Remove(shapes[i]);
            }
            //this.NotifyEveryone();
        }

        public override void Draw(Graphics g)
        {
            foreach (Figure f in shapes)
            {
                f.Draw(g);
            }
        }

        public void move(int x, int y, int width, int height)
        {
            foreach (Figure f in shapes)
            {
                if (f.DecoratorCheck())
                {
                    f.move(x, y, width, height);
                }
            }
        }

        public void SizeUp(int a, int width, int height)
        {

            foreach (Figure f in shapes)
            {
                if (f.DecoratorCheck())
                {
                    f.SizeUp(a, width, height);
                }
            }
        }

        override public void SetColor(Color color)
        {
            foreach (Figure shape in shapes)
            {
                if (shape.DecoratorCheck())
                {
                    shape.SetColor(color);

                }
            }
        }

        public void Compose()
        {
            int k = 0;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].DecoratorCheck())
                {
                    k++;
                }
            }
            if (k > 1)
            {
                var gGroup = new GGroup();
                for (int i = 0; i < shapes.Count;)
                {
                    if (shapes[i] is Decorator dec)
                    {
                        gGroup.Add(dec.GetOriginalFigure());
                        shapes.Remove(shapes[i]);
                    }
                    else i++;
                }
                Decorator dGroup = new Decorator(gGroup);
                shapes.Add(dGroup);
                //this.NotifyEveryone();
                //decorator.NotifyEveryoneSelect();
            }
        }

        public void unCompose()
        {
            int i = 0;
            int j = shapes.Count;
            for (; i < j;)
            {
                if (shapes[i] is Decorator decorator)
                {
                    if (decorator._shape is GGroup group)
                    {
                        foreach (Figure innerShape in group.GetFigures())
                        {
                            Decorator dec = new Decorator(innerShape);
                            shapes.Add(dec);
                        }
                        j--;
                        shapes.RemoveAt(i);
                        continue;
                    }
                }
                i++;
            }
            //this.NotifyEveryone();
        }

        public void ctrlChange()
        {
            ctrlPressed = !ctrlPressed;
            //treeViewObserver.ctrlPressed = !ctrlPressed;
        }

        public override bool isClickedOnFigure(int x, int y)
        {
            if (ctrlPressed == false)
                unSelectAll();
            if (isSelect(x, y))
            {
                return true;
            }
            return false;
        }

        public void Save()
        {
            SaveArray array = new SaveArray();
            array.save(shapes, filename);
        }

        public void Load()
        {
            Method factory = new Method(); // create a new factory method object
            ShapeArray array = new ShapeArray(); // create a new shape array object
            array.LoadShapes(filename, factory, shapes); // call the LoadShapes method with the initialized objects and figure list
        }
    }
}
