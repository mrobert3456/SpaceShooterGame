// <copyright file="IShootLogic.cs" company="V8K90F_WN1M1P">
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
    /// Specifies the Shoot logic behaviour.
    /// </summary>
    public interface IShootLogic
    {
        /// <summary>
        /// Adds a bullet to the player's bullet list.
        /// </summary>
        void Shoot();

        /// <summary>
        /// Moves a specified bullet.
        /// </summary>
        /// <param name="bullet">The bullet which will be moved.</param>
        void BulletTick(BulletItem bullet);

        /// <summary>
        /// Checks all the player's bullets for collision, and takes the neccessary steps, if one bullet is collided with other objects.
        /// </summary>
        /// <param name="timerAn">The timer, which used for display explosion.</param>
        /// <param name="sht">The timer, which used for the player's shoot method. </param>
        void ShootTick(DispatcherTimer timerAn, DispatcherTimer sht);

        /// <summary>
        /// Adds a bullet to the specified enemy's bullet list.
        /// </summary>
        /// <param name="enemy">The enemy, which is shooting.</param>
        void EnemyShoot(SpaceShipItem enemy);

        /// <summary>
        /// Checks all the enemy's bullets for collision, and takes the neccessary steps, if one bullet is collided with other objects.
        /// </summary>
        void EnemyShootTick();

        /// <summary>
        /// Adds all enemy's bullets to the EnemyBullets list.
        /// </summary>
        void SpaceShipTick();

        /// <summary>
        /// Handles all enemy's shooting.
        /// </summary>
        void ShootOnetick();
    }
}
