// <copyright file="IGameLogic.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Threading;
    using GameModel;

    /// <summary>
    /// Specifies the Game logic behaviour.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
       /// Gets or sets a value indicating whether the game is ended or not.
       /// </summary>
        bool GameOver { get; set; }

        /// <summary>
        /// Moves the player's ship.
        /// </summary>
        /// <param name="xInc">The X velocity of the player's ship.</param>
        /// <param name="yInc">The Y velocity of the player's ship.</param>
        void MoveShip(double xInc, double yInc);

        /// <summary>
        /// Specifies the behavior of the game in each time period.
        /// </summary>
        /// <param name="timerAn">DispatcherTimer used for display animation.</param>
        void OneTick(DispatcherTimer timerAn);

        /// <summary>
        /// Loads the next level of the game.
        /// </summary>
        /// <returns>Return a new GameModel with the next level.</returns>
        IGameModel NextLevel();

        /// <summary>
        /// Sets the ratation degree of the player's ship.
        /// </summary>
        /// <param name="x">The X axis of the mouse position.</param>
        /// <param name="y">The Y axis of the mouse position.</param>
        void SetDegree(double x, double y);
    }
}
