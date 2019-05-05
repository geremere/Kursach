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
        List<Risk> Risklst = null;
        public Graphic(List<Risk> lst)
        {
            Risklst = lst;
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Point[] array = new Point[Risklst.Count];
            for (int i = 0; i < Risklst.Count; i++)
            {
                array[i].Y = 425 * Risklst[i].Probability + 75;
                array[i].X = -350 * Risklst[i].Effect + 400;
                Ellipse elipse = new Ellipse();
                elipse.Height = 5;
                elipse.Width = 5;
                elipse.StrokeThickness = 2;
                elipse.Stroke = Brushes.Black;
                elipse.Margin = new Thickness(array[i].X, array[i].Y, 0, 0);
            }
        }
    }
}
