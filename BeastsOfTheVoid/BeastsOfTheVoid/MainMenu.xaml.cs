// <copyright file="MainMenu.xaml.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BeastsOfTheVoid
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
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
    using System.Xml.Linq;

    /// <summary>
    /// Interaction logic for MainMenu.xaml.
    /// </summary>
    public partial class MainMenu : Window
    {
        private string dir;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            this.InitializeComponent();
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("BeastsOfTheVoid.Images.mainmenu.jpg");
            bmp.EndInit();
            this.Background = new ImageBrush(bmp);

            this.dir = Directory.GetCurrentDirectory();
            List<string> h = this.dir.Split('\\').ToList();
            this.dir = string.Empty;

            for (int i = 0; i < h.Count - 4; i++)
            {
                this.dir += h[i] + "\\";
            }

            this.dir += "GameRepository";
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            GameControl.Level = 1;
            MainWindow win = WindowFactory.CreateMainWindow();
            this.Close();
            win.ShowDialog();
        }

        private void Load_Game(object sender, RoutedEventArgs e)
        {
            this.LoadGrid.Visibility = Visibility.Visible;

            string[] files = Directory.GetFiles(this.dir + "\\SaveData");

            foreach (var item in files)
            {
                this.LoadFilesLB.Items.Add(item.Split('\\')[item.Split('\\').Length - 1].Split('.')[0]);
            }
        }

        private void SelectLoad_Click(object sender, RoutedEventArgs e)
        {
            string save = this.LoadFilesLB.SelectedItem.ToString();
            XDocument doc = XDocument.Load(this.dir + "\\SaveData\\" + save + ".xml");
            GameControl.Level = int.Parse(doc.Descendants("Level").First().Value, System.Globalization.CultureInfo.CurrentCulture);
            GameControl.SaveName = save;
            MainWindow win = WindowFactory.CreateMainWindow();
            this.Close();
            win.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            GameControl.Level = 1;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.HighScoreGrid.Visibility == Visibility.Visible)
            {
                this.HighScoreBox.Items.Clear();
                this.HighScoreGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                this.HighScoreGrid.Visibility = Visibility.Visible;

                XDocument xdoc = new XDocument();
                string[] files = Directory.GetFiles(this.dir + "\\SaveData");

                Dictionary<string, int> highsc = new Dictionary<string, int>();

                for (int i = 0; i < files.Length; i++)
                {
                    xdoc = XDocument.Load(files[i]);

                    highsc.Add(files[i].Split('\\')[files[i].Split('\\').Length - 1].Split('.')[0], int.Parse(xdoc.Root.Element("Player").Element("Point").Value));
                }

                var list = highsc.OrderByDescending(x => x.Value);

                foreach (var item in list)
                {
                    this.HighScoreBox.Items.Add(item.Key + " - " + item.Value);
                }
            }
        }
    }
}
