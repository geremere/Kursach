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
    /// Логика взаимодействия для ProbabilityInfluenceOwner.xaml
    /// </summary>
    public partial class ProbabilityInfluenceOwner : Window
    {
        public ProbabilityInfluenceOwner()
        {
            InitializeComponent();
        }
        private async Task WriteOwners()
        {
            List<User> userslst = await new UsersCommand().GiveAllUsers();
            for (int i = 0; i < userslst.Count; i++)
            {
                Owners.Items.Add(userslst[i]);
            }
        }

        private void SetUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Double.Parse(Parsing(TBINf.Text)) >= 1 || Double.Parse(Parsing(TBINf.Text)) <= 0)
                    throw new ArgumentException("Значение Influence лежит в области (0,1)");
                if (Double.Parse(Parsing(TBProb.Text)) >= 1 || Double.Parse(Parsing(TBProb.Text)) <= 0)
                    throw new ArgumentException("Значение Influence лежит в области (0,1)");
                if (Owners.SelectedItem == null) throw new NullReferenceException("Выберете ответсвенного за риск");
                    this.DialogResult = true;
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");

            }
        }
        public User Owner
        {
            get => (User)Owners.SelectedItem;
        }
        public double Probability
        {
            get => Double.Parse(Parsing(TBProb.Text));
        }
        public double Influence
        {
            get => Double.Parse(Parsing(TBINf.Text));
        }
        bool flag = true;
        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Owners.Text = "Choise Owner";
                await WriteOwners();
                flag = false;
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

        private void CrateCleanRisk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            TBINf.Text = default(double).ToString();
            TBProb.Text = default(double).ToString();
        }
    }
}
