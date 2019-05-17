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
    /// Логика взаимодействия для ReportForAdmin.xaml
    /// </summary>
    public partial class ReportForAdmin : Window
    {
        bool flag = true;
        Risk drisk = null;
        Project project = null;
        List<Risk> SelectedRisks = null;
        List<Vertexcs> vert = null;
        Vertexcs FirstVerTex;
        Point center;//точка центра
        const double radius = 250;
        const int K = 100;
        double Widht;
        new double Height;



        public ReportForAdmin(Risk drisk,Project project,Point center)
        {
            this.center = center;
            this.drisk = drisk;
            this.project = project;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            if(flag)
            {
                Widht = cnv.ActualWidth;
                Height = cnv.ActualHeight;

                DataCommands dc = new DataCommands();
                SelectedRisks = await dc.GiveAllRisks(project);
                Drawing();
                TreeCommands tc = new TreeCommands();
                vert = await tc.GiveALlVertex();
                FirstVerTex = await tc.GiveFristVertex(drisk.Id);
                CreateFirstVertex();
                CostCurrentBranch(FirstVerTex, 0);
                DrawMaxDangerous();
                WriteInListView();
                flag = false;
            }
        }

        private void WriteInListView()
        {
            for (int i = 0; i < SelectedRisks.Count; i++)
            {
                if (Math.Sqrt((SelectedRisks[i].point.X - center.X) * (SelectedRisks[i].point.X - center.X) +
                        (SelectedRisks[i].point.Y - center.Y) * (SelectedRisks[i].point.Y - center.Y)) < radius)
                    Dangerous.Items.Add(SelectedRisks[i]);
            }
        }

        private void CreateFirstVertex()
        {
            

            Point point = new Point(FirstVerTex.X*3/2, FirstVerTex.Y);
            Label lab = new Label();
            lab.HorizontalAlignment = HorizontalAlignment.Center;
            lab.VerticalAlignment = VerticalAlignment.Top;
            lab.Margin = new Thickness(point.X - 2, point.Y + 200, 0, 0);

            lab.Content = drisk.RiskName;
            cnv.Children.Add(lab);

            Ellipse elipse = new Ellipse();

            elipse.Width = 4;
            elipse.Height = 4;
            elipse.HorizontalAlignment = HorizontalAlignment.Left;
            elipse.VerticalAlignment = VerticalAlignment.Top;
            elipse.StrokeThickness = 2;
            elipse.Stroke = Brushes.Black;
            elipse.Margin = new Thickness(point.X - 2, point.Y - 2, 0, 0);
            cnv.Children.Add(elipse);
            DrawRootVertexes(FirstVerTex);
        }
        private void DrawRootVertexes(Vertexcs curver)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (vert[i].ParentId == curver.Id && vert[i].Probability != default(double))
                {
                    DrawNewVertex(vert[i]);
                    DrawNewLine(vert[i], curver);
                    DrawRootVertexes(vert[i]);
                }
            }
        }

        private void DrawNewVertex(Vertexcs newver)
        {
            Point point = new Point(newver.X, newver.Y);
            Ellipse elipse = new Ellipse();

            elipse.Width = 6;
            elipse.Height = 6;
            elipse.HorizontalAlignment = HorizontalAlignment.Left;
            elipse.VerticalAlignment = VerticalAlignment.Top;
            elipse.StrokeThickness = 2;
            elipse.Stroke = Brushes.Black;
            elipse.Margin = new Thickness(newver.X / 2 + Widht / 2 - 3, newver.Y -3, 0, 0);
            cnv.Children.Add(elipse);


        }

        private void DrawNewLine(Vertexcs newver, Vertexcs parent)
        {
            Line l = new Line();
            l.X1 = parent.X / 2 + Widht / 2;
            l.Y1 = parent.Y;
            l.X2 = newver.X / 2 + Widht / 2;
            l.Y2 = newver.Y ;
            l.Stroke = Brushes.Black;
            cnv.Children.Add(l);
        }
        private void CostCurrentBranch(Vertexcs curver, double k)
        {
            double cost = k;
            for (int i = 0; i < vert.Count; i++)
            {
                if (curver.Id == vert[i].ParentId && vert[i].Probability != default)
                {
                    cost += vert[i].Probability * vert[i].Cost;
                    CostCurrentBranch(vert[i], cost);
                    cost -= vert[i].Probability * vert[i].Cost;
                }
            }
            if (ExistChild(curver))
                curver.Value = cost;
        }

        private bool ExistChild(Vertexcs curver)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (curver.Id == vert[i].ParentId && vert[i].Probability != default)
                {
                    return false;
                }
            }
            return true;
        }
        private void DrawMaxDangerous()
        {
            Vertexcs Max = vert[0];
            Vertexcs Min = null;
            for (int i = 0; i < vert.Count; i++)
            {
                if (vert[i].Value != 0)
                {
                    Min = vert[i];
                    for (int j = 0; j < vert.Count; j++)
                    {
                        if (vert[j].Value < Min.Value && vert[j].Value != 0) Min = vert[j];
                    }
                    break;
                }

            }
            for (int i = 1; i < vert.Count; i++)
            {
                if (vert[i].Value > Max.Value) Max = vert[i];

            }
            if (Max != null)
            {
                Label l = new Label();
                l.Content = Max.Value;
                l.Margin = new Thickness(Max.X/2 + Widht / 2, Max.Y + 20, 0, 0);
                l.VerticalAlignment = VerticalAlignment.Top;
                l.HorizontalAlignment = HorizontalAlignment.Left;
                l.Height = 40;
                l.Foreground = Brushes.Red;
                cnv.Children.Add(l);
                DrawREDLine(Max);
            }

            if (Min != null)
            {
                Label l1 = new Label();
                l1.Content = Min.Value;
                l1.Margin = new Thickness(Min.X/2 + Widht / 2, Min.Y + 20, 0, 0);
                l1.VerticalAlignment = VerticalAlignment.Top;
                l1.HorizontalAlignment = HorizontalAlignment.Left;
                l1.Height = 40;
                l1.Foreground = Brushes.Green;
                cnv.Children.Add(l1);
                DrawGRINLine(Min);
            }
        }

        private void DrawGRINLine(Vertexcs min)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (min.ParentId == vert[i].Id)
                {
                    Line l = new Line();
                    l.X1 = vert[i].X/ 2 + Widht / 2;
                    l.Y1 = vert[i].Y;
                    l.X2 = min.X / 2 + Widht / 2;
                    l.Y2 = min.Y;
                    l.Stroke = Brushes.Green;
                    l.Width = 10;
                    cnv.Children.Add(l);

                    if (vert[i].Probability == default(double)) break;
                    else DrawGRINLine(vert[i]);

                }
            }
        }

        private void DrawREDLine(Vertexcs max)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (max.ParentId == vert[i].Id)
                {
                    Line l = new Line();
                    l.X1 = vert[i].X / 2 + Widht / 2;
                    l.Y1 = vert[i].Y;
                    l.X2 = max.X / 2 + Widht / 2;
                    l.Y2 = max.Y;

                    l.Stroke = Brushes.Red;
                    cnv.Children.Add(l);

                    if (vert[i].Probability == default(double)) break;
                    else DrawREDLine(vert[i]);

                }
            }
        }



        static public double FindY(double x, double radius, Point center)
        {
            double c = -radius * radius + (x - center.X) * (x - center.X) + center.Y * center.Y;
            double b = -2 * center.Y;
            double a = 1;
            double desc = (b * b - 4 * a * c);
            if (desc < 0)
            {
                throw new Exception("No point");
            }
            return (-b + Math.Sqrt(desc)) / 2 * a;
        }

        private void Drawing()
        {
            cnv.Children.Clear();
            for (int i = 0; i < K - 1; i++)
            {
                double old = center.X - radius + radius / K * i;
                double nw = center.X - radius + radius / K * (i + 1);

                Line l = new Line();
                l.X1 = old;
                l.X2 = nw;
                l.Y1 = FindY(old, radius, center);
                l.Y2 = FindY(nw, radius, center);
                l.Stroke = Brushes.Black;
                cnv.Children.Add(l);
            }

            for (int i = 0; i < SelectedRisks.Count; i++)
            {
                if (SelectedRisks[i].Probability != default(Double) && SelectedRisks[i].Influence != default(Double))
                {
                    SelectedRisks[i].point.X = 425 * SelectedRisks[i].Probability + 75;
                    SelectedRisks[i].point.Y = -350 * SelectedRisks[i].Influence + 400;
                    Ellipse elipse = new Ellipse();
                    elipse.Height = 10;
                    elipse.Width = 10;
                    elipse.StrokeThickness = 2;
                    if (Math.Sqrt((SelectedRisks[i].point.X - center.X) * (SelectedRisks[i].point.X - center.X) +
                        (SelectedRisks[i].point.Y - center.Y) * (SelectedRisks[i].point.Y - center.Y)) < radius)
                    {

                        elipse.Stroke = Brushes.Red;
                        elipse.Fill = Brushes.Red;
                    }
                    else
                    {
                        elipse.Stroke = Brushes.Green;
                        elipse.Fill = Brushes.Green;
                    }
                    elipse.VerticalAlignment = VerticalAlignment.Top;
                    elipse.HorizontalAlignment = HorizontalAlignment.Left;
                    elipse.Margin = new Thickness(SelectedRisks[i].point.X, SelectedRisks[i].point.Y, 0, 0);
                    cnv.Children.Add(elipse);
                }
            }
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
