using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using oop4_1.FactoryMethod;
using oop4_1.Figures;
using oop4_1.Observers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace oop4_1
{

    public partial class Form1 : Form
    {

        //enum FigureType { Circle, Square, Triangle }
        //private FigureType currentFigure;

        //private List<Figure> figure = new List<Figure>();  //������� ������ ��������
        //private string filename = "C:/Users/������/source/repos/oop4_1/oop4_1/FactoryMethod/save_figures.txt";

        Container container;
        //TreeViewObserver observer;
        bool ctrlpress = false;// ������ ������� ��������
        bool ClickLine = false;
        string fType = "Circle";

        public Form1()
        {
            InitializeComponent();
            cbColor.SelectedIndex = 0;

            //observer = new TreeViewObserver(shapeTree);
            //container = new Container(observer);
            container = new Container();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)  //�������� ������� ������� ������� �� ����������
        {
            if (ModifierKeys == Keys.Control)  //��� Control
            {
                chb_Ctrl.Checked = true;
                ctrlpress = true;
                container.ctrlChange();
            }
            else if (e.KeyCode == Keys.Delete)  //��� Delete
            {
                container.delSelected();
                Refresh();
            }
            else if (e.KeyCode == Keys.Z) //���������� ���������� ��������
            {
                container.SizeUp(-2, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
            else if (e.KeyCode == Keys.X) //���������� ���������� ��������
            {
                container.SizeUp(2, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
            else if (e.KeyCode == Keys.A)
            {      
                container.move(-2, 0, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
            else if (e.KeyCode == Keys.D)
            {
                container.move(2, 0, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
            else if (e.KeyCode == Keys.W)
            {
                container.move(0, -2, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
            else if (e.KeyCode == Keys.S)
            {
                container.move(0, 2, pictureBox1.Width, pictureBox1.Height);
                Refresh();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)  //�������� ������� ���������� ������� 
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control && chb_Ctrl.Checked == true) //�������� ������� ���������� ������� Control
            {
                chb_Ctrl.Checked = false;
                container.ctrlPressed = !container.ctrlPressed;
                ctrlpress = !ctrlpress;
            }
        }

        private void button1_Click(object sender, EventArgs e)  //�������� ������� ������ ��������
        {
            container.delAll();
            Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)  //�������� ������� ������� ����
        {
            if (chb_Ctrl.Checked == false) 
            {
                if (!ClickLine)  //������� ����� ������
                {
                    container.unSelectAll();
                    container.Add(e.X, e.Y, fType, Color.FromName(cbColor.SelectedItem.ToString()));
                    Refresh();
                }
                else
                {
                    if (!container.isSelect(e.X, e.Y)) //��� ��������� � ������, ��� ���������� � ���������� true
                    {
                        container.Add(e.X, e.Y, fType, Color.FromName(cbColor.SelectedItem.ToString()));
                        Refresh();
                    }
                    if(container.AddLine()) //��� �������� �������� ����� �������� true
                    {
                        ClickLine = false;
                        Refresh();
                    }   
                }
            }
            else  //�������� ������ ��� ������� Control
            {
                container.isSelect(e.X, e.Y);
                Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)  //�������� ������� Paint
        {
            container.Draw(e.Graphics);
            Refresh();
        }

        private void rbCircle_CheckedChanged(object sender, EventArgs e)
        {
            fType = "Circle";
        }

        private void rbSquare_CheckedChanged(object sender, EventArgs e)
        {
            fType = "Square";
            //for (int i = 0; i < figure.Count; i++)
            //{
            //    if (figure[i] is Decorator decorator)
            //    {
            //        Figure originalFigure = decorator.GetOriginalFigure();
            //        Figure newFigure = new Square(originalFigure.x, originalFigure.y);
            //        newFigure.SetColor(originalFigure.GetColor());
            //        newFigure.a = originalFigure.a;
            //        Figure decC = new Decorator(newFigure);
            //        figure.Insert(i, decC);
            //        figure.RemoveAt(i + 1);
            //    }
            //}
            //Refresh();
        }

        private void rbTriangle_CheckedChanged(object sender, EventArgs e)
        {
            fType = "Triangle";
            //for (int i = 0; i < figure.Count; i++)
            //{
            //    if (figure[i] is Decorator decorator)
            //    {
            //        Figure originalFigure = decorator.GetOriginalFigure();
            //        Figure newFigure = new Triangle(originalFigure.x, originalFigure.y);
            //        newFigure.SetColor(originalFigure.GetColor());
            //        newFigure.a = originalFigure.a;
            //        Figure decC = new Decorator(newFigure);
            //        figure.Insert(i, decC);
            //        figure.RemoveAt(i + 1);
            //    }
            //}
            //Refresh();
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(container != null)
            {
                container.SetColor(Color.FromName(cbColor.SelectedItem.ToString()));
                Refresh();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width - 40;
            pictureBox1.Height = this.Height - 160;
        }

        private void cbColor_MouseEnter(object sender, EventArgs e)
        {
            cbColor.DroppedDown = true;
        }

        private void button_dlt_Click(object sender, EventArgs e)
        {
            container.delSelected();
            Refresh();
        }

        private void btn_notSelection_Click(object sender, EventArgs e)
        {
            container.unSelectAll();
            Refresh();
        }

        private void btnGroup_Click(object sender, EventArgs e) //�����������
        {
            container.Compose();
            Refresh();
        }

        private void btnUngroup_Click(object sender, EventArgs e) //��������������
        {
            //for (int i = 0; i < figure.Count;)
            //{
            //    if (figure[i] is Decorator decorator)
            //    {
            //        if (decorator.GetOriginalFigure() is GGroup gGroup)
            //        {
            //            while (gGroup.Count() > 1)
            //            {
            //                figure.Add(gGroup.GetOriginalFigure());
            //                gGroup.RemoveAt();
            //            }
            //            figure.Add(gGroup.GetOriginalFigure());
            //            figure.Remove(figure[i]);
            //        }
            //    }
            //    ++i;
            //}
            container.unCompose();
            Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e) //�������� �� �����
        {
            container.Load();
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e) //���������� � ����
        {
            container.Save();
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            container.unSelectAll();
            Refresh();
            ClickLine = true;
        }
    }
}