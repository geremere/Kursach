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
    /// Логика взаимодействия для Budget.xaml
    /// </summary>
    public partial class Budget : Window
    {
        public Budget()
        {
            InitializeComponent();
        }

        private void SetUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double bud;
                if(double.TryParse(Budgett.Text, out bud))
                {
                    this.DialogResult = true;
                }

            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public double budget
        {
            get => double.Parse(Budgett.Text);
        }
    }
}
