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
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        bool flagApearing = true;
        public Projects()
        {
            InitializeComponent();            
        }
        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flagApearing)
            {
                ProjectCommands pc = new ProjectCommands();
                List<Project> list = await pc.GiveAllProjects();
                for (int i = 0; i < list.Count; i++)
                {
                    listBox.Items.Add(list[i]);
                    listBox.Items.ToString();
                }
                flagApearing = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewProject newpr = new NewProject();
            Close();
            newpr.Show();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                Project project = (Project)listBox.SelectedItem;
                Graphic pr = new Graphic(project);
                Close();
                pr.Show();
            }
            else
            {
                MessageBox.Show("Select Project");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Close();
            mw.Show();
        }
    }
}
