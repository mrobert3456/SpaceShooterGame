// <copyright file="LogicFactory.cs" company="V8K90F_WN1M1P">
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
    /// Creates logic instances.
    /// </summary>
    public static class LogicFactory
    {
        /// <summary>
        /// Creates a FileLogic.
        /// </summary>
        /// <param name="model">IGamemodel implementation.</param>
        /// <returns>Returns a FileLogic.</returns>
        public static FileLogic CreateFileLogic(IGameModel model)
        {
            return new FileLogic(model);
        }

        /// <summary>
        /// Creates a ShootLogic.
        /// </summary>
        /// <param name="model">IGamemodel implementation.</param>
        /// <returns>Returns a ShootLogic.</returns>
        public static ShootLogic CreateShootLogic(IGameModel model)
        {
            return new ShootLogic(model);
        }

        /// <summary>
        /// Creates a MainLogic.
        /// </summary>
        /// <param name="model">IGamemodel implementation.</param>
        /// <returns>Returns a MainLogic.</returns>
        public static MainLogic CreateMainLogic(IGameModel model)
        {
            return new MainLogic(model);
        }
    }
}
