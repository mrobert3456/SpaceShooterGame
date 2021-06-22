// <copyright file="SaveGameMenu.xaml.cs" company="V8K90F_WN1M1P">
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using GameLogic;

    /// <summary>
    /// Interaction logic for SaveGameMenu.xaml.
    /// </summary>
    public partial class SaveGameMenu : UserControl
    {
        private IFileLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGameMenu"/> class.
        /// </summary>
        public SaveGameMenu()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGameMenu"/> class.
        /// </summary>
        /// <param name="logic">IFileLogic implementation.</param>
        public SaveGameMenu(IFileLogic logic)
            : this()
        {
            this.logic = logic;
        }

        private void Save_Game_Click(object sender, RoutedEventArgs e)
        {
            this.logic.SaveGame(this.PlayerName.Text);
            this.Content = null;
        }
    }
}
