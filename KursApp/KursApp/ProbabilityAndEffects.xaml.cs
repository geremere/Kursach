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
    /// Логика взаимодействия для ProbabilityAndEffects.xaml
    /// </summary>
    public partial class ProbabilityAndEffects : Window
    {
        bool flag = true;
        List<Risk> newlst=null;
        public ProbabilityAndEffects(List<Risk> risks)
        {
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
            if (((Risk)ChoisedRisks.SelectedItem).Effect == default(Double))
                Efect.Text = "";
            else
                Efect.Text = ((Risk)ChoisedRisks.SelectedItem).Effect.ToString();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Risk)ChoisedRisks.SelectedItem).Effect = Double.Parse(Efect.Text);
                ((Risk)ChoisedRisks.SelectedItem).Probability = Double.Parse(Prob.Text);
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

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
