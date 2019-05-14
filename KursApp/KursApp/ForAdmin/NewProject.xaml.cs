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
    /// Логика взаимодействия для NewProject.xaml
    /// </summary>
    public partial class NewProject : Window
    {
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "back.jpg");
        bool flag =true;
        public NewProject()
        {
            InitializeComponent();
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProjectCommands pc = new ProjectCommands();
                await pc.IsertNewProject(Name.Text, ((User)(Owners.SelectedItem)).Login, TypeOfProject.Text);
                Graphic cr = new Graphic(new Project(Name.Text, ((User)(Owners.SelectedItem)).Login, TypeOfProject.Text));
                Close();
                cr.Show();
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

        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Back.Background = new ImageBrush(new BitmapImage(new Uri(path)));
                TypeOfProject.Text = "Select Type Of Project";
                List<User> userslst = await new UsersCommand().GiveAllUsers();
                for (int i = 0; i < userslst.Count; i++)
                {
                    Owners.Items.Add(userslst[i]);
                }
                flag = false;
            }

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Projects p = new Projects();
            Close();
            p.Show();
        }

    }
}
