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
        bool flag = true;
        Risk drisk = null;
        Project project;
        double Widht;
        new double Height;
        Vertexcs FirstVer;
        List<Vertexcs> vert = new List<Vertexcs>();
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "back.jpg");
        string pathplus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plus.png");


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

                //int capacity = CountOfChildren(parent);
                int row = 1;
                CurrenRow(parent,ref row);
                if (parent.Probability != 0) row++;
                //if (capacity > 4) throw new ArgumentException("Количество детей не может превышать 4");
                if (row >= 4) throw new ArgumentException("Dетвь дерева не может превышать 4");
                Vertexcs newver;
                string line = $"{(parent.X - Widht / (2 * Math.Pow(4, row))):f3}";
                if (Cheker(double.Parse(line)))
                {
                    newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X - Widht / (2 * Math.Pow(4, row)), parent.Y + 50);

                }
                else
                {
                    line = $"{(parent.X + Widht / (2 * Math.Pow(4, row))):f3}";
                    if (Cheker(double.Parse(line)))
                    {
                        newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X + Widht / (2 * Math.Pow(4, row)), parent.Y + 50);

                    }
                    else
                    {
                        line = $"{(parent.X - 3* Widht / (2 * Math.Pow(4, row))):f3}";
                        if (Cheker(double.Parse(line)))
                        {
                            newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X - 3 * Widht / (2 * Math.Pow(4, row)), parent.Y + 50);
                        }
                        else
                        {
                            line = $"{(parent.X + 3 * Widht / (2 * Math.Pow(4, row))):f3}";
                            if (Cheker(double.Parse(line)))
                            {
                                newver = new Vertexcs(parent.Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), parent.X + 3 * Widht / (2 * Math.Pow(4, row)), parent.Y + 50);
                            }
                            else
                            {
                                throw new Exception("Количество детей не может превышать 4");
                            }
                        }
                    }
                }
                TreeCommands tc = new TreeCommands();
                await tc.IsertNewVertex(newver, parent.Id);
                newver = await tc.GiveVertex(newver);
                DrawNewVertex(newver);
                DrawNewLine(newver, parent);
                vert.Add(newver);
                
                
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Enpty Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Enpty Exception");
            }

        }

        private bool Cheker(double x)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (x == vert[i].X)
                    return false;
            }
            return true;
        }
        private void CostCurrentBranch(Vertexcs current, ref double k)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (current.ParentId == vert[i].Id && vert[i].Probability == default)
                {
                    return;
                }
                if (current.ParentId == vert[i].Id && vert[i].Probability != default)
                {
                    k += vert[i].Probability * vert[i].Cost;
                    CostCurrentBranch(vert[i], ref k);
                }
            }
        }
        private void CurrenRow(Vertexcs parent,ref  int k)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if(parent.ParentId==vert[i].Id && vert[i].Probability==default)
                {
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
            cnv.Children.Add(l);
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
            but.Margin = new Thickness(newver.X - 10, newver.Y, Widht - newver.X - 10,Height-newver.Y-20);
            but.Height = 20;
            but.Width = 20;
            but.Background = new ImageBrush(new BitmapImage(new Uri(pathplus)));
            but.Click += But_Click;
            cnv.Children.Add(but);


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
            if (flag)
            {
                try
                {
                    Widht = cnv.ActualWidth;
                    Height = cnv.ActualHeight;
                    Label lab = new Label();
                    lab.HorizontalAlignment = HorizontalAlignment.Center;
                    lab.VerticalAlignment = VerticalAlignment.Top;
                    lab.Content = drisk.RiskName;
                    grid.Children.Add(lab);
                    TreeCommands tc = new TreeCommands();
                    Button but = new Button();
                    but.HorizontalAlignment = HorizontalAlignment.Left;
                    but.VerticalAlignment = VerticalAlignment.Top;
                    but.Margin = new Thickness(Widht / 2 - 10, 50, Widht / 2 - 10, Height - 70);
                    but.Background = new ImageBrush(new BitmapImage(new Uri(pathplus)));
                    Back.Background = new ImageBrush(new BitmapImage(new Uri(path)));
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
                    but.Click += But_Click;
                    cnv.Children.Add(but);
                    vert = await tc.GiveALlVertex();
                    DrawRootVertexes(FirstVer);
                    flag = false;
                }
                catch(NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
            Vertexcs v = (Vertexcs)Add.DataContext;
            if(v.Probability!=0)
            {
                ProbabilityTB.Text = v.Probability.ToString();
                CostTB.Text = v.Cost.ToString();
                DescriptionTB.Text = v.Description;
            }
            else
            {
                ProbabilityTB.Text = "";
                CostTB.Text = "";
                DescriptionTB.Text = "";
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Graphic p = new Graphic(project);
            Close();
            p.Show();
        }

        private async void Delite_Click(object sender, RoutedEventArgs e)
        {
            cnv.Children.Clear();
            TreeCommands tc = new TreeCommands();
            if (((Vertexcs)Add.DataContext).Probability != 0)
            {
                DeliteVertexes((Vertexcs)Add.DataContext);
                await tc.DeliteVerTex((Vertexcs)Add.DataContext);
                Button but = new Button();
                but.HorizontalAlignment = HorizontalAlignment.Left;
                but.VerticalAlignment = VerticalAlignment.Top;
                but.Margin = new Thickness(Widht / 2 - 10, 50, Widht / 2 - 10, Height - 70);
                but.Background = new ImageBrush(new BitmapImage(new Uri(pathplus)));
                Back.Background = new ImageBrush(new BitmapImage(new Uri(path)));
                but.DataContext = FirstVer;
                but.Height = 20;
                but.Width = 20;
                but.Click += But_Click;
                cnv.Children.Add(but);
                TreeCommands tc1 = new TreeCommands();
                vert = await tc1.GiveALlVertex();
            }
            DrawRootVertexes(FirstVer);

        }

        private async void DeliteVertexes(Vertexcs currentvertex)
        {
            TreeCommands tc = new TreeCommands();
            for (int i = 0; i < vert.Count; i++)
            {
                if (vert[i].Probability != 0 && vert[i].ParentId==currentvertex.Id)
                {
                    DeliteVertexes(vert[i]);
                    await tc.DeliteVerTex(vert[i]);
                }
            }
        }
    }
}
