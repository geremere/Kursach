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
        string Type = null;
        public ChoiseRisks(string type)
        {
            Type = type;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                List<Risk> defaultlst = (List<Risk>)await new RisksCommand().GiveAllProjects("default");
                List<Risk> speciallst = (List<Risk>)await new RisksCommand().GiveAllProjects(Type);

                for (int i = 0; i < defaultlst.Count; i++)
                {
                    DefaultLST.Items.Add(defaultlst[i]);
                }
                for (int i = 0; i < speciallst.Count; i++)
                {
                    SpecialLST.Items.Add(speciallst[i]);
                }
                flag = false;
            }

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            List <Risk> defailtlist= new List<Risk>();
            for (int i = 0; i < DefaultLST.SelectedItems.Count; i++)
            {
                    defailtlist.Add((Risk)DefaultLST.SelectedItems[i]);               
            }
            for (int i = 0; i < SpecialLST.SelectedItems.Count; i++)
            {
                defailtlist.Add((Risk)SpecialLST.SelectedItems[i]);
            }
            ProbabilityAndEffects pandef = new ProbabilityAndEffects(defailtlist);
            Close();
            pandef.Show();

        }
    }
}
