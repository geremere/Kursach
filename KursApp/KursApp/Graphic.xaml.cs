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
    /// Логика взаимодействия для Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        Point center = new Point(500, 50);//точка центра
        List<Risk> AllRisklst = null;
        List<string> sourse = null;//список возможных сортировок
        Point mousePos;// точка, в которой щапонимается знчени мыши
        List<Risk> SelectedRisks = null;
        Point nowcenter;
        bool flag = true;
        Project project = null;//проект
        User user = null;
        public Graphic(Project pr,User us)
        {
            user = us;
            project = pr;
            InitializeComponent();
        }        

        /// <summary>
        /// когда открывается окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                Drawing();
                DataCommands dc = new DataCommands();
                SelectedRisks = await dc.GiveAllRisks(project.Name);
                RisksCommand rc = new RisksCommand();
                AllRisklst = await rc.GiveAllRisks();
                ComboBox.Items.Add("Общие риски");
                ComboBox.Text = "Общие риски";
                ComboBox.Items.Add(project.Type);
                sourse = await rc.GiveRisksSourse();
                Cheker();
                ADDToSelctes();
                for (int i = 0; i < sourse.Count; i++)
                {
                    ComboBox.Items.Add(sourse[i]);
                }
                flag = false;
            }
        }
        /// <summary>
        /// добавляет в вкладку выбранные риски
        /// </summary>
        private void ADDToSelctes()
        {
            for (int i = 0; i < SelectedRisks.Count; i++)
            {
                SelRisks.Items.Add(SelectedRisks[i]);
            }
        }

        /// <summary>
        /// проверяет не нахоядтся ли данные жлементы уже в проекте
        /// </summary>
        private void Cheker()
        {
            for (int i = 0; i < AllRisklst.Count; i++)
            {
                for (int j = 0; j < SelectedRisks.Count; j++)
                {
                    if (AllRisklst[i].RiskName == SelectedRisks[j].RiskName)
                    {
                        AllRisklst[i].Selected = true;
                    }

                }
            }
        }

        /// <summary>
        /// отрисовываем точки и геперболу
        /// </summary>
        private void Drawing()
        {
            cnv.Children.Clear();
            double radius = 250;
            const int K = 100;
            for (int i = 0; i < K - 1; i++)
            {
                double old = center.X - radius + radius / K * i;
                double nw = center.X - radius + radius / K * (i + 1);

                Line l = new Line();
                l.X1 = old;
                l.X2 = nw;
                l.Y1 = FindY(old, radius, center);
                l.Y2 = FindY(nw, radius, center);
                l.Stroke = Brushes.Black;
                cnv.Children.Add(l);
            }

            for (int i = 0; i < SelRisks.Items.Count; i++)
            {
                if(SelectedRisks[i].Probability!=default(Double)&& SelectedRisks[i].Influence!=default(Double))
                {
                    ((Risk)SelRisks.Items[i]).point.X = 425 * ((Risk)SelRisks.Items[i]).Probability + 75;
                    ((Risk)SelRisks.Items[i]).point.Y = -350 * ((Risk)SelRisks.Items[i]).Influence + 400;
                    Ellipse elipse = new Ellipse();
                    elipse.Height = 5;
                    elipse.Width = 5;
                    elipse.StrokeThickness = 2;
                    if (Math.Sqrt((((Risk)SelRisks.Items[i]).point.X - center.X) * (((Risk)SelRisks.Items[i]).point.X - center.X) +
                        (((Risk)SelRisks.Items[i]).point.Y - center.Y) * (((Risk)SelRisks.Items[i]).point.Y - center.Y)) < radius)
                    {

                        elipse.Stroke = Brushes.Red;
                        elipse.Fill = Brushes.Red;
                    }
                    else
                    {
                        elipse.Stroke = Brushes.Green;
                        elipse.Fill = Brushes.Green;
                    }
                    elipse.VerticalAlignment = VerticalAlignment.Top;
                    elipse.HorizontalAlignment = HorizontalAlignment.Left;
                    elipse.Margin = new Thickness(((Risk)SelRisks.Items[i]).point.X, ((Risk)SelRisks.Items[i]).point.Y, 0, 0);
                    cnv.Children.Add(elipse);
                }               
            }
        }

        static public double FindY(double x, double radius, Point center)
        {
            double c = -radius * radius + (x - center.X) * (x - center.X) + center.Y * center.Y;
            double b = -2 * center.Y;
            double a = 1;
            double desc = (b * b - 4 * a * c);
            if (desc < 0)
            {
                throw new Exception("No point");
            }
            return (-b + Math.Sqrt(desc)) / 2 * a;
        }

        /// <summary>
        /// нажатие на кнопку мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cnv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mousePos = e.GetPosition(null);
            nowcenter = center;
        }

        /// <summary>
        /// mouseup двигаем гиперболу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cnv_MouseUp(object sender, MouseButtonEventArgs e)
        {
            center.X = nowcenter.X - mousePos.X + e.GetPosition(null).X;
            center.Y = nowcenter.Y - mousePos.Y + e.GetPosition(null).Y;
            if(center.X>750 || center.Y<-200)
            {
                center.Y = -150;
                center.X = 750;
            }
            if(center.Y>230||center.X<250)
            {
                center.Y = 230;
                center.X = 250;
            }
            Drawing();
        }

        /// <summary>
        /// переход с дереву рисков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvwrisk.Items.Clear();
            if (ComboBox.SelectedItem.ToString() == "Общие риски")
            {
                for (int i = 0; i < AllRisklst.Count; i++)
                {
                    if(AllRisklst[i].TypeOfProject=="default")
                        lvwrisk.Items.Add(AllRisklst[i]);
                }
            }
            else
            {
                if (ComboBox.SelectedItem.ToString() == project.Type)
                {
                    for (int i = 0; i < AllRisklst.Count; i++)
                    {
                        if (AllRisklst[i].TypeOfProject == project.Type)
                            lvwrisk.Items.Add(AllRisklst[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < sourse.Count; i++)
                    {
                        if (ComboBox.SelectedItem.ToString() == sourse[i])
                        {
                            for (int j = 0; j < AllRisklst.Count; j++)
                            {
                                if ((AllRisklst[j].TypeOfProject == project.Type || AllRisklst[j].TypeOfProject == "default") && AllRisklst[j].SoursOfRisk == sourse[i])
                                {
                                    lvwrisk.Items.Add(AllRisklst[j]);
                                }
                            }

                        }
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            ((Risk)((CheckBox)sender).DataContext).Selected = true;
            SelectedRisks.Add((Risk)((CheckBox)sender).DataContext);
            SelRisks.Items.Add((Risk)((CheckBox)sender).DataContext);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            ((Risk)((CheckBox)sender).DataContext).Selected = false;
            SelectedRisks.Remove((Risk)((CheckBox)sender).DataContext);
            SelRisks.Items.Remove((Risk)((CheckBox)sender).DataContext);

        }

        private async void SetUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelRisks.SelectedItem != null && Double.Parse(Parsing(TBINf.Text)) != default(Double) && Double.Parse(Parsing(TBProb.Text)) != default(Double))
                {
                    ((Risk)SelRisks.SelectedItem).Influence = double.Parse(Parsing(TBINf.Text));
                    ((Risk)SelRisks.SelectedItem).Probability = double.Parse(Parsing(TBProb.Text));
                    DataCommands dc = new DataCommands();
                    await dc.IsertNewRisks((Risk)SelRisks.SelectedItem, project.Name, user);

                    SelRisks.Items.Clear();
                    for (int i = 0; i < SelectedRisks.Count; i++)
                    {
                        SelRisks.Items.Add(SelectedRisks[i]);

                    }
                    Drawing();
                }
                else
                {
                    MessageBox.Show("Wrong in enpty");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Wrong in enpty");

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

        private void SelRisks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelRisks.SelectedItem != null)
            {
                TBINf.Text = ((Risk)SelRisks.SelectedItem).Influence.ToString();
                TBProb.Text = ((Risk)SelRisks.SelectedItem).Probability.ToString();
            }
        }
    }
}
