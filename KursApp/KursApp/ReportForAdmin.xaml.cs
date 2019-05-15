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
        Point center;//точка центра
        const double radius = 250;
        const int K = 100;

        
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
                DataCommands dc = new DataCommands();
                SelectedRisks = await dc.GiveAllRisks(project);
                Drawing();
                flag = false;
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
    }
}
