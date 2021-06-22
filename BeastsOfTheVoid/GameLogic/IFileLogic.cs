// <copyright file="IFileLogic.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModel;

    /// <summary>
    /// Specifies the File logic behaviour.
    /// </summary>
    public interface IFileLogic
    {
        /// <summary>
        /// Gets or Sets the Game model.
        /// </summary>
        IGameModel Model { get; set; }

        /// <summary>
        /// Saves the current state of the game.
        /// </summary>
        /// <param name="saveName">The name of the save.</param>
        void SaveGame(string saveName);

        /// <summary>
        /// Loads the game from a file.
        /// </summary>
        /// <param name="fname">The name of the saved game.</param>
        /// <param name="model">IGameModel implementation, which stores the data of the file.</param>
        void LoadGame(string fname, IGameModel model);
    }
}
