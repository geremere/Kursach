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
    /// Логика взаимодействия для ProjectChoise.xaml
    /// </summary>
    public partial class ProjectChoise : Window
    {
        User user = null;
        bool flag = true;
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "back.jpg");

        public ProjectChoise(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Back.Background = new ImageBrush(new BitmapImage(new Uri(path)));
                Back.Foreground = new ImageBrush(new BitmapImage(new Uri(path)));

                ProjectCommands pc = new ProjectCommands();
                List<Project> prlst = await pc.GiveProjectsForOwner(user.Login);
                for (int i = 0; i < prlst.Count; i++)
                {
                    listBox.Items.Add(prlst[i]);
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                Project project = (Project)listBox.SelectedItem;
                GraphicForProjectManager pfpm = new GraphicForProjectManager(project,user);
                Close();
                pfpm.Show();
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
