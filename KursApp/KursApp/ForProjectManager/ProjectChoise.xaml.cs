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
    /// Логика взаимодействия для ProjectChoise.xaml
    /// </summary>
    public partial class ProjectChoise : Window
    {
        User user = null;
        public ProjectChoise(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            ProjectCommands pc = new ProjectCommands();
            List<Project> prlst = await pc.GiveProjectsForOwner(user.Login);
            for (int i = 0; i < prlst.Count; i++)
            {
                listBox.Items.Add(prlst[i]);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                Project project = (Project)listBox.SelectedItem;
                GraphicForProjectManager pfpm = new GraphicForProjectManager(project);
                Close();
                pfpm.Show();
        }
    }
}
