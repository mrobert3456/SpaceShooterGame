// <copyright file="FileLogic.cs" company="V8K90F_WN1M1P">
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
    using GameRepository;

    /// <summary>
    /// Implements IFileLogic interface.
    /// </summary>
    public class FileLogic : IFileLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogic"/> class.
        /// </summary>
        /// <param name="model">IGamemodel implementation.</param>
        public FileLogic(IGameModel model)
        {
            this.Model = model;
        }

        /// <inheritdoc/>
        public IGameModel Model { get; set; }

        /// <inheritdoc/>
        public void LoadGame(string fname, IGameModel model)
        {
            Repository.LoadGame(fname, model);
        }

        /// <inheritdoc/>
        public void SaveGame(string saveName)
        {
            Repository.SaveGame(saveName, this.Model);
        }
    }
}
