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
    /// Логика взаимодействия для Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        Point center = new Point(500, 50);
        List<Risk> Risklst = null;
        Point mousePos;// точка, в которой щапонимается знчени мыши
        Point nowcenter;
        bool flag = true;
        public Graphic(List<Risk> lst)
        {
            Risklst = lst;
            InitializeComponent();
        }        

        /// <summary>
        /// когда открывается окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Drawing();
                flag = false;
            }
        }
        /// <summary>
        /// отрисовываем точки и геперболу
        /// </summary>
        private void Drawing()
        {
            cnv.Children.Clear();
            double radius = 250;
            const int K = 100;
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

            for (int i = 0; i < Risklst.Count; i++)
            {
                Risklst[i].point.X = 425 * Risklst[i].Probability + 75;
                Risklst[i].point.Y = -350 * Risklst[i].Influence + 400;
                Ellipse elipse = new Ellipse();
                elipse.Height = 5;
                elipse.Width = 5;
                elipse.StrokeThickness = 2;
                if (Math.Sqrt((Risklst[i].point.X - center.X) * (Risklst[i].point.X - center.X) +
                    (Risklst[i].point.Y - center.Y) * (Risklst[i].point.Y - center.Y)) < radius)
                {
                    DAngerous.Items.Remove(Risklst[i]);
                    DAngerous.Items.Add(Risklst[i]);
                    elipse.Stroke = Brushes.Red;
                    elipse.Fill = Brushes.Red;
                }
                else
                {
                    if(DAngerous.HasItems)
                    {
                        DAngerous.Items.Remove(Risklst[i]);
                    }
                    elipse.Stroke = Brushes.Green;
                    elipse.Fill = Brushes.Green;
                }
                elipse.VerticalAlignment = VerticalAlignment.Top;
                elipse.HorizontalAlignment = HorizontalAlignment.Left;
                elipse.Margin = new Thickness(Risklst[i].point.X, Risklst[i].point.Y, 0, 0);
                cnv.Children.Add(elipse);
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

        /// <summary>
        /// нажатие на кнопку мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cnv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mousePos = e.GetPosition(null);
            nowcenter = center;
        }

        /// <summary>
        /// mouseup двигаем гиперболу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cnv_MouseUp(object sender, MouseButtonEventArgs e)
        {
            center.X = nowcenter.X - mousePos.X + e.GetPosition(null).X;
            center.Y = nowcenter.Y - mousePos.Y + e.GetPosition(null).Y;
            if(center.X>750 || center.Y<-200)
            {
                center.Y = -150;
                center.X = 750;
            }
            if(center.Y>230||center.X<250)
            {
                center.Y = 230;
                center.X = 250;
            }
            Drawing();
        }

        /// <summary>
        /// переход с дереву рисков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
