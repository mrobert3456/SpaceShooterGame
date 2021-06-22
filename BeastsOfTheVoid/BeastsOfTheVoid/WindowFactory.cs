// <copyright file="WindowFactory.cs" company="V8K90F_WN1M1P">
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
    using GameLogic;

    /// <summary>
    /// Creates Windows.
    /// </summary>
    public static class WindowFactory
    {
        /// <summary>
        /// Creates a Main menu.
        /// </summary>
        /// <returns>Returns a MainMenu.</returns>
        public static MainMenu CreateMainMenu()
        {
            return new MainMenu();
        }

        /// <summary>
        /// Creates a Pause menu.
        /// </summary>
        /// <param name="win">The window, that called the Pause Menu.</param>
        /// <param name="logic">IFileLogic implementation.</param>
        /// <returns>Returns a PauseMenu.</returns>
        public static PauseMenu CreatePauseMenu(Window win, IFileLogic logic)
        {
            return new PauseMenu(win, logic);
        }

        /// <summary>
        /// Crates a Main window.
        /// </summary>
        /// <returns>Returns a MainWindow.</returns>
        public static MainWindow CreateMainWindow()
        {
            return new MainWindow();
        }

        /// <summary>
        /// Creates a SaveGame menu.
        /// </summary>
        /// <param name="logic">IFileLogic implementation.</param>
        /// <returns>Returns a SaveGameMenu.</returns>
        public static SaveGameMenu CreateSaveGameMenu(IFileLogic logic)
        {
            return new SaveGameMenu(logic);
        }
    }
}
