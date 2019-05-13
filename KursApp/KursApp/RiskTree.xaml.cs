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
        double Height;
        Vertexcs FirstVer;
        List<Vertexcs> vert = new List<Vertexcs>();

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
                if (DESC.Text != "") throw new Exception("Заполните поле Description");
                if (double.TryParse(COST.Text, out double d)) throw new Exception("значение Cost должно быть вещественным числом больше нуля");
                if (double.Parse(COST.Text) <= 0) throw new Exception("значение Cost должно быть вещественным числом больше нуля");
                if (double.TryParse(Prob.Text, out double d1)) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");
                if (double.Parse(Prob.Text) < 1) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");
                if (double.Parse(Prob.Text) > 0) throw new Exception("Значение Probability должно быть вещественным числом в предалам (0,1)");

                int capacity = CountOfChildren(((Vertexcs)((Button)sender).DataContext));
                if (capacity >= 3) MessageBox.Show("Количество детей не может превышать 3");
                else
                {
                    Vertexcs parent = ((Vertexcs)((Button)sender).DataContext);
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

        private void DrawNewVertex(Vertexcs newver)
        {
            Button but = new Button();
            but.DataContext = newver;
            but.HorizontalAlignment = HorizontalAlignment.Left;
            but.VerticalAlignment = VerticalAlignment.Top;
            but.Margin = new Thickness(newver.X - 20, newver.Y, Widht - newver.X - 20,Height-newver.Y-20);
            but.Height = 20;
            but.Width = 40;
            but.Content = "Add Vertex";
            but.Click += But_Click;
            grid.Children.Add(but);


        }

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
            but.Width = 40;
            but.Content = "Add Vertex";
            but.Click += But_Click;
            grid.Children.Add(but);
            vert = await tc.GiveALlVertex();
            DrawRootVertexes(FirstVer);
        }

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

        private void DrawAllVertex(Vertexcs curver)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (vert[i].ParentId == curver.Id && vert[i].Probability != default(Double))
                {
                    DrawNewVertex(vert[i]);
                    DrawNewLine(vert[i], curver);
                    DrawAllVertex(vert[i]);
                }

            }
        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            Add.DataContext = (Vertexcs)((Button)sender).DataContext;
        }
    }
}
