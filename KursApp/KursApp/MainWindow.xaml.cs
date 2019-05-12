using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SqlConnection sqlConnect;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (sqlConnect != null && sqlConnect.State != ConnectionState.Closed)
            //    sqlConnect.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersCommand uc = new UsersCommand();
            if (await uc.InLogin(textBox.Text.Trim(),passwordBox.Password.Trim())==3)
            {
                Projects np = new Projects();
                Close();
                np.Show();
            }
            else
            {
                if (await uc.InLogin(textBox.Text.Trim(), passwordBox.Password.Trim()) == 2)
                {
                    UsersCommand uc2 = new UsersCommand();
                    User user2 = await uc2.GiveUser(textBox.Text.Trim(), passwordBox.Password.Trim());
                    ProjectChoise pc = new ProjectChoise(user2);
                    Close();
                    pc.Show();
                }
                else
                {
                    if(await uc.InLogin(textBox.Text.Trim(), passwordBox.Password.Trim()) == 1)
                    {
                        UsersCommand uc1 = new UsersCommand();
                        User user1 = await uc1.GiveUser(textBox.Text.Trim(), passwordBox.Password.Trim());
                        SelectProject sp = new SelectProject(user1);
                        sp.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Ошибка при вводе логина или пороля");
                }
            }
        }
    }
}
