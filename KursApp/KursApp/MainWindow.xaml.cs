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
            if (await uc.InLogin(textBox.Text.Trim(),passwordBox.Password.Trim())!=2)
            {
                
                NewProject np = new NewProject();
                Close();
                np.Show();
            }
            else
            {
                if (await uc.InLogin(textBox.Text.Trim(), passwordBox.Password.Trim()) != 1)
                {

                    ProjectChoise pc = new ProjectChoise();
                    Close();
                    pc.Show();
                }
                else
                {
                    MessageBox.Show("Ошибка при вводе логина или пороля");
                }
            }
        }
    }
}
