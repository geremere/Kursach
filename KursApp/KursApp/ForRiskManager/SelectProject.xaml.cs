﻿using System;
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
    /// Логика взаимодействия для SelectProject.xaml
    /// </summary>
    public partial class SelectProject : Window
    {
        User user = null;
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "back.jpg");

        public SelectProject(User user)
        {
            this.user = user;
            InitializeComponent();
        }
        bool flag = true;
        private async  void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Back.Background = new ImageBrush(new BitmapImage(new Uri(path)));
                Back.Foreground = new ImageBrush(new BitmapImage(new Uri(path)));

                ProjectCommands pc = new ProjectCommands();
                DataCommands dc = new DataCommands();
                List<string> lst = await dc.GiveOwnersProjects(user);
                List<Project> prlst = await pc.GiveAllProjects();
                try
                {
                    for (int i = 0; i < prlst.Count; i++)
                    {
                        for (int j = 0; j < lst.Count; j++)
                        {
                            if (lst[j] == prlst[i].Name)
                            {
                                listBox.Items.Add(prlst[i]);
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("You havent projects and risks");
                }
                flag = false;
            }
        }
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                Project project = (Project)listBox.SelectedItem;
                Gra_hicForRiskManager pfpm = new Gra_hicForRiskManager(project, user);
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
