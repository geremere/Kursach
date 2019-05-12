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
    /// Логика взаимодействия для Gra_hicForRiskManager.xaml
    /// </summary>
    public partial class Gra_hicForRiskManager : Window
    {
        Point center = new Point(500, 50);//точка центра
        const double radius = 250;
        const int K = 100;
        Point mousePos;// точка, в которой щапонимается знчени мыши
        List<Risk> SelectedRisks = null;
        Point nowcenter;
        bool flag = true;
        Project project = null;
        User user = null;
        public Gra_hicForRiskManager(Project project, User user)
        {
            this.user=user;
            this.project = project;
            InitializeComponent();
        }
        /// <summary>
        /// метод при открытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_Activated(object sender, EventArgs e)
        {
            if (flag)
            {
                DataCommands dc = new DataCommands();
                SelectedRisks = await dc.GiveOwnersRisks(project, user);
                ADDToSelctes();
                RisksCommand rc = new RisksCommand();
                Drawing();
                FindDangerougeRisks();
                flag = false;
            }
        }
        private void ADDToSelctes()
        {
            for (int i = 0; i < SelectedRisks.Count; i++)
            {
                SelRisks.Items.Add(SelectedRisks[i]);
            }
        }
        /// <summary>
        /// отрисовываем точки и геперболу
        /// </summary>
        private void Drawing()
        {
            cnv.Children.Clear();
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
                if (SelectedRisks[i].Probability != default(Double) && SelectedRisks[i].Influence != default(Double))
                {
                    ((Risk)SelRisks.Items[i]).point.X = 425 * ((Risk)SelRisks.Items[i]).Probability + 75;
                    ((Risk)SelRisks.Items[i]).point.Y = -350 * ((Risk)SelRisks.Items[i]).Influence + 400;
                    Ellipse elipse = new Ellipse();
                    elipse.Height = 10;
                    elipse.Width = 10;
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
            if (e.GetPosition(null).X < 540 && e.GetPosition(null).Y < 450)
            {
                mousePos = e.GetPosition(null);
                nowcenter = center;
            }
        }

        /// <summary>
        /// кнопка установления риску вероятности и эффекта и также в бивани его в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SetUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelRisks.SelectedItems != null &&
                    Double.Parse(Parsing(TBINf.Text)) != default(Double) &&
                    Double.Parse(Parsing(TBProb.Text)) != default(Double))
                {
                    ((Risk)SelRisks.SelectedItem).Influence = double.Parse(Parsing(TBINf.Text));
                    ((Risk)SelRisks.SelectedItem).Probability = double.Parse(Parsing(TBProb.Text));
                    DataCommands dc = new DataCommands();
                    await dc.UpdateRisks((Risk)SelRisks.SelectedItem, project.Name);
                    SelRisks.Items.Clear();
                    SelectedRisks = await dc.GiveAllRisks(project);
                    for (int i = 0; i < SelectedRisks.Count; i++)
                    {
                        SelRisks.Items.Add(SelectedRisks[i]);
                    }
                    Drawing();
                }
                else
                {
                    MessageBox.Show("Wrong in enpty1");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong in enpty2");

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

        /// <summary>
        /// вводим значение рисков в текстбоксы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelRisks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelRisks.Items.Count != 0)
            {
                TBINf.Text = ((Risk)SelRisks.SelectedItem).Influence.ToString();
                TBProb.Text = ((Risk)SelRisks.SelectedItem).Probability.ToString();
            }
        }

        /// <summary>
        /// перемещие гиперболы во время движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cnv_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(null).X < 540 && e.GetPosition(null).Y < 450 && e.LeftButton == MouseButtonState.Pressed)
            {
                center.X = nowcenter.X - mousePos.X + e.GetPosition(null).X;
                center.Y = nowcenter.Y + (mousePos.X - e.GetPosition(null).X) / 1.2;
                if (center.X > 650 || center.Y < -100)
                {
                    center.Y = -100;
                    center.X = 650;
                }
                if (center.Y > 230 || center.X < 250)
                {
                    center.Y = 230;
                    center.X = 250;
                }
                Drawing();
                FindDangerougeRisks();
            }
        }

        private void FindDangerougeRisks()
        {
            DanRisks.Items.Clear();
            if (SelRisks.Items.Count != 0)
            {
                for (int i = 0; i < SelRisks.Items.Count; i++)
                {
                    if (Math.Sqrt((((Risk)SelRisks.Items[i]).point.X - center.X) * (((Risk)SelRisks.Items[i]).point.X - center.X) +
                        (((Risk)SelRisks.Items[i]).point.Y - center.Y) * (((Risk)SelRisks.Items[i]).point.Y - center.Y)) < radius)
                    {
                        DanRisks.Items.Add((Risk)SelRisks.Items[i]);
                    }

                }
            }
        }

        private void Cnv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SelRisks.Items.Count != 0)
            {
                List<Risk> click = new List<Risk>();
                for (int i = 0; i < SelRisks.Items.Count; i++)
                {
                    if (Math.Abs(((Risk)SelRisks.Items[i]).point.X - e.GetPosition(null).X) <= 10
                        && Math.Abs(((Risk)SelRisks.Items[i]).point.Y - e.GetPosition(null).Y) <= 10)
                    {
                        click.Add((Risk)SelRisks.Items[i]);
                    }
                }
                if (click.Count != 0)
                {
                    MessageBox.Show(CReateLine(click), "INformation about Selected Risks");
                }
            }
        }

        /// <summary>
        /// строка для вывода информации при нажатии праой кнопки мыши
        /// </summary>
        /// <param name="click"></param>
        /// <returns></returns>
        private string CReateLine(List<Risk> click)
        {
            string line = "";
            for (int i = 0; i < click.Count; i++)
            {
                line += $"RiskName: {click[i].RiskName}\nSourse: {click[i].SoursOfRisk}";
            }
            return line;
        }

        /// <summary>
        /// прописать удаление 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delite_Click(object sender, RoutedEventArgs e)
        {

            DataCommands dc = new DataCommands();
            await dc.DeliteRisk((Risk)((Button)sender).DataContext);
            SelectedRisks.Remove((Risk)((Button)sender).DataContext);
            SelRisks.Items.Remove((Risk)((Button)sender).DataContext);
            Drawing();
        }

    }
}