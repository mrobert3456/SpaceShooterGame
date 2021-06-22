// <copyright file="PauseMenu.xaml.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BeastsOfTheVoid
{
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
    using GameLogic;

    /// <summary>
    /// Interaction logic for PauseMenu.xaml.
    /// </summary>
    public partial class PauseMenu : Window
    {
        private Window win;
        private IFileLogic filelogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        public PauseMenu()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        /// <param name="win">The window, that called the Pause Menu.</param>
        /// <param name="logic">IFileLogic implementation.</param>
        public PauseMenu(Window win, IFileLogic logic)
            : this()
        {
            this.win = win;
            this.filelogic = logic;
        }

        private void Resume_Game(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu menu = WindowFactory.CreateMainMenu();
            this.win.Close();
            this.Close();
            menu.ShowDialog();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.LabelSave.Content = WindowFactory.CreateSaveGameMenu(this.filelogic);
        }
    }
}
