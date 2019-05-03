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
        List<Risk> newlst=null;
        public ProbabilityAndEffects(List<Risk> risks)
        {
            newlst =risks;
            InitializeComponent();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            for (int i = 0; i < newlst.Count; i++)
            {
                ChoisedRisks.Items.Add(newlst[i]);
            }
        }
    }
}
