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
    /// Логика взаимодействия для ProbabilityAndEffects.xaml
    /// </summary>
    public partial class ProbabilityAndEffects : Window
    {
        bool flag = true;
        List<Risk> newlst=null;
        Project project = null;
        public ProbabilityAndEffects(List<Risk> risks,Project pr)
        {
            project = pr;
            newlst =risks;
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                for (int i = 0; i < newlst.Count; i++)
                {
                    ChoisedRisks.Items.Add(newlst[i]);
                }
                flag = false;
            }
        }

        private void ChoisedRisks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((Risk)ChoisedRisks.SelectedItem).Probability == default(Double))           
                Prob.Text = "";           
            else
                Prob.Text = ((Risk)ChoisedRisks.SelectedItem).Probability.ToString();
            if (((Risk)ChoisedRisks.SelectedItem).Influence == default(Double))
                Efect.Text = "";
            else
                Efect.Text = ((Risk)ChoisedRisks.SelectedItem).Influence.ToString();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Risk)ChoisedRisks.SelectedItem).Influence = Double.Parse(Parsing(Efect.Text));
                ((Risk)ChoisedRisks.SelectedItem).Probability = Double.Parse(Parsing(Prob.Text));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string Parsing(string l)
        {
            string ret = "";
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] == '.') ret += ',';
                else
                {
                    ret += l[i];
                }
            }
            return ret;
        }

        private async void Conrinue_Click(object sender, RoutedEventArgs e)
        {
            List<Risk> lst = new List<Risk>();
            //ChoisedRisks.SelectionMode = SelectionMode.Multiple;
            //ChoisedRisks.SelectAll();
            for (int i = 0; i < ChoisedRisks.Items.Count; i++)
            {
                lst.Add((Risk)ChoisedRisks.Items[i]);
            }
            if (CHechChech(lst))
            {
                DataCommands dc = new DataCommands();
                await dc.IsertNewRisks(lst, project.Name);
                Graphic gr = new Graphic(lst);
                Close();
                gr.Show();
            }
        }

        private bool CHechChech(List<Risk> lst)
        {
            for (int i = 0; i < newlst.Count; i++)
            {
                if (lst[i].Probability == default(Double) || lst[i].Influence == default(Double))
                    return false;
            }
            return true;
        }

        private void Delite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
