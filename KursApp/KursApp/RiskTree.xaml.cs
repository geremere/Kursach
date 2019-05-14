using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Shapes;

namespace KursApp
{
    /// <summary>
    /// Логика взаимодействия для RiskTree.xaml
    /// </summary>
    public partial class RiskTree : Window
    {
        Risk drisk = null;
        Project project;
        double Widht;
        new double Height;
        Vertexcs FirstVer;
        List<Vertexcs> vert = new List<Vertexcs>();
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "back.jpg");

        public RiskTree(Risk drisk, Project project)
        {
            this.drisk = drisk;
            this.project = project;
            InitializeComponent();
        }

        private async void AddNewVertex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).DataContext == null) throw new Exception("выбрете вершину");
                if (DESC.Text == "") throw new Exception("Заполните поле Description");
                if (!double.TryParse(COST.Text, out double d)) throw new Exception("значение Cost должно быть вещественным числом больше нуля");
                if (double.Parse(COST.Text) <= 0) throw new Exception("значение Cost должно быть вещественным числом больше нуля");
                if (!double.TryParse(Prob.Text, out double d1)) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");
                if (double.Parse(Prob.Text) > 1) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");
                if (double.Parse(Prob.Text) < 0) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");
                Vertexcs parent = ((Vertexcs)((Button)sender).DataContext);

                int capacity = CountOfChildren(parent);
                int row = 0;
                CurrenRow(parent,ref row);
                if (capacity >= 3) MessageBox.Show("Количество детей не может превышать 3");
                else
                {
                    Vertexcs newver;
                    if (capacity==0)
                    {
                        newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text),double.Parse(Prob.Text),parent.X, parent.Y + 50);

                    }
                    else
                    {
                        if(capacity==1)
                        {
                            newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X/2, parent.Y + 50);

                        }
                        else
                        {
                            newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X * 3 / 2, parent.Y + 50);

                        }
                    }
                    TreeCommands tc = new TreeCommands();
                    await tc.IsertNewVertex(newver, parent.Id);
                    newver = await tc.GiveVertex(newver);
                    DrawNewVertex(newver);
                    DrawNewLine(newver, parent);
                    vert.Add(newver);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Enpty Exception");
            }

        }

        private void CurrenRow(Vertexcs parent,ref  int k)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if(parent.ParentId==vert[i].Id && vert[i].Probability==default)
                {
                    k++;
                    return;
                }
                if (parent.ParentId == vert[i].Id && vert[i].Probability != default)
                {
                    k++;
                    CurrenRow(vert[i],ref k);
                }
            }
        }

        /// <summary>
        /// рисует линии соединяющие точки графа
        /// </summary>
        /// <param name="newver"></param>
        /// <param name="parent"></param>
        private void DrawNewLine(Vertexcs newver, Vertexcs parent)
        {
            Line l = new Line();
            l.X1 = parent.X;
            l.Y1 = parent.Y+20;
            l.X2 = newver.X;
            l.Y2 = newver.Y;
            l.Stroke = Brushes.Black;
            grid.Children.Add(l);
        }

        /// <summary>
        /// рисует вершины
        /// </summary>
        /// <param name="newver"></param>
        private void DrawNewVertex(Vertexcs newver)
        {
            Button but = new Button();
            but.DataContext = newver;
            but.HorizontalAlignment = HorizontalAlignment.Left;
            but.VerticalAlignment = VerticalAlignment.Top;
            but.Margin = new Thickness(newver.X - 20, newver.Y, Widht - newver.X - 20,Height-newver.Y-20);
            but.Height = 20;
            but.Width = 30;
            but.Content = "Add";
            but.Click += But_Click;
            grid.Children.Add(but);


        }

        /// <summary>
        /// узнает количесво детей
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        private int CountOfChildren(Vertexcs vs)
        {
            int count = 0;
            for (int i = 0; i < vert.Count; i++)
            {
                if (vert[i].ParentId == vs.Id && vert[i].Probability != default(Double))
                {
                    count++;
                }
            }
            return count;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            Widht = grid.ActualWidth;
            Height = grid.ActualHeight;
            Label lab = new Label();
            lab.HorizontalAlignment = HorizontalAlignment.Center;
            lab.VerticalAlignment = VerticalAlignment.Top;
            lab.Content = drisk.RiskName;
            grid.Children.Add(lab);
            TreeCommands tc = new TreeCommands();
            Button but = new Button();
            but.HorizontalAlignment = HorizontalAlignment.Left;
            but.VerticalAlignment = VerticalAlignment.Top;
            but.Margin = new Thickness(Widht/2-20,50, Widht / 2 - 20,Height-70);
            but.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            if (!await tc.Exist(drisk.Id))
            {
                FirstVer = new Vertexcs(drisk.Id, drisk.RiskName, Widht / 2, 50);
                await tc.IsertNewVertex(FirstVer, drisk.Id);
                FirstVer = await tc.GiveFristVertex(drisk.Id);
                but.DataContext = FirstVer;

            }
            else
            {
                FirstVer = await tc.GiveFristVertex(drisk.Id);
                but.DataContext = FirstVer;
            }
            but.Height = 20;
            but.Width = 20;
            but.Content = "Add";
            but.Click += But_Click;
            grid.Children.Add(but);
            vert = await tc.GiveALlVertex();
            DrawRootVertexes(FirstVer);
        }

        /// <summary>
        /// рисует дерево при начальной загрузке
        /// </summary>
        /// <param name="curver"></param>
        private void DrawRootVertexes(Vertexcs curver)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if(vert[i].ParentId == curver.Id && vert[i].Probability != default(double))
                {
                    DrawNewVertex(vert[i]);
                    DrawNewLine(vert[i], curver);
                    DrawRootVertexes(vert[i]);
                }
            }
        }

        /// <summary>
        /// нажатие на вершину графа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Click(object sender, RoutedEventArgs e)
        {
            Add.DataContext = (Vertexcs)((Button)sender).DataContext;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Graphic p = new Graphic(project);
            Close();
            p.Show();
        }
    }
}
