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
        Point start = new Point(190,180);
        List<Vertexcs> vert = new List<Vertexcs>();

        public RiskTree(Risk drisk, Project project)
        {
            this.drisk = drisk;
            this.project = project;
            InitializeComponent();
        }

        private void AddNewVertex_Click(object sender, RoutedEventArgs e)
        {
            double height = grid.ActualHeight;
            double Widht = grid.ActualWidth;
            DawLines(start.X,start.Y,Height);
            vert.Add(new Vertexcs(((Vertexcs)((Button)sender).DataContext).Id, DESC.Text, double.Parse(COST.Text), double.Parse(Prob.Text), 290, height/3, 1));
            AddNewElement(vert[1]);

        }

        private void AddNewElement(Vertexcs vertexcs)
        {
           
        }

        private void DawLines(double x, double y, double h)
        {
            try
            {
                if (Prob.Text != "" && double.Parse(COST.Text) < 1 && double.Parse(COST.Text) > 0)
                {
                    Line l = new Line();
                    l.X1 = x;
                    l.Y1 = y;
                    l.X2 = x + 100;
                    l.Y2 = h / 3;
                    l.Stroke = Brushes.Black;
                    grid.Children.Add(l);

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Wrong in Enpty");
            }
          
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            RiskName.Text = drisk.RiskName;
            //BUgdett();
        }

        private void BUgdett()
        {
            Budget bg = new Budget();
            if (bg.DialogResult == true)
            {
                Cost.Text = bg.budget.ToString();
            }
            else
            {
                MessageBox.Show("Spmethin went wrong");
                BUgdett();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Cost.Text, out double result))
            {
                RiskName.Background = Brushes.Blue;
                Vertexcs startv = new Vertexcs(drisk.Id, drisk.RiskName, double.Parse(Cost.Text), 1, 190, 180, 0);
                startv.Id = 1;
                vert.Add(startv);
                add.DataContext = startv;
                Add.DataContext = add.DataContext;
            }
            else
            {
                MessageBox.Show("В поле должно быть введено вещественное значение");
            }
        }
    }
}
