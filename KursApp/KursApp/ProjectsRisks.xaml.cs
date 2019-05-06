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
    /// Логика взаимодействия для ProjectsRisks.xaml
    /// </summary>
    public partial class ProjectsRisks : Window
    {
        Project project = null;
        public ProjectsRisks(Project pr)
        {
            project = pr;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            DataCommands dc = new DataCommands();
            List<Risk> risklst = await dc.GiveAllRisks(project.Name);
            for (int i = 0; i < risklst.Count; i++)
            {
                ChoisedRisks.Items.Add(risklst[i]);
            }
        }

        private void ChoisedRisks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SetUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
