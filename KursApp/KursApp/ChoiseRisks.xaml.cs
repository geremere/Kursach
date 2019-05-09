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
    /// Логика взаимодействия для ChoiseRisks.xaml
    /// </summary>
    public partial class ChoiseRisks : Window
    {
        bool flag = true;
        Project project;
        List<string> sourse = null;
        public ChoiseRisks(Project pr)
        {
            project = pr;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                RisksCommand rc = new RisksCommand();
                ComboBox.Items.Add("Общие риски");
                ComboBox.Items.Add(project.Type);
                sourse = await rc.GiveRisksSourse();
                for (int i = 0; i < sourse.Count; i++)
                {
                    ComboBox.Items.Add(sourse[i]);
                }
                List<Risk> lst = await rc.GiveRiskInTypeProject("default");
                for (int i = 0; i < lst.Count; i++)
                {
                    RisksLST.Items.Add(lst[i]);
                }
                flag = false;
            }

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            List <Risk> defailtlist= new List<Risk>();
            for (int i = 0; i < SelectedLST.SelectedItems.Count; i++)
            {
                defailtlist.Add((Risk)SelectedLST.SelectedItems[i]);
            }
            ProbabilityAndEffects pandef = new ProbabilityAndEffects(defailtlist, project);
            Close();
            pandef.Show();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RisksLST_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLST.Items.Add(RisksLST.SelectedItem);
        }

        private async void Clasificated_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RisksLST.Items.Clear();
            if(ComboBox.SelectedItem.ToString() == "Общие риски")
            {
                RisksCommand rc = new RisksCommand();
                List<Risk> lst = await rc.GiveRiskInTypeProject("default");
                for (int i = 0; i < lst.Count; i++)
                {
                    RisksLST.Items.Add(lst[i]);
                }
            }
            else
            {
                if(ComboBox.SelectedItem.ToString() == project.Type)
                {
                    RisksCommand rc = new RisksCommand();
                    List<Risk> typelst = await rc.GiveRiskInTypeProject(project.Type);
                    for (int i = 0; i < typelst.Count; i++)
                    {
                        RisksLST.Items.Add(typelst[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < sourse.Count; i++)
                    {
                        if(ComboBox.SelectedItem.ToString() == sourse[i])
                        {
                            RisksCommand rc = new RisksCommand();
                            List<Risk> sourselst = await rc.GiveRisksInTypeSourse(sourse[i],project.Type);
                            for (int j = 0; j < sourselst.Count; j++)
                            {
                                RisksLST.Items.Add(sourselst[j]);
                            }

                        }
                    }
                }
            }
        }

        private void SelectedLST_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLST.Items.Remove(SelectedLST.SelectedItem);
        }
    }
}
