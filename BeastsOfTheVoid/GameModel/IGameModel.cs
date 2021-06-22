// <copyright file="IGameModel.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Specifies the Game model behaviour.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets the Game width.
        /// </summary>
        double GameWidth { get; }

        /// <summary>
        /// Gets the Game height.
        /// </summary>
        double GameHeight { get; }

        /// <summary>
        /// Gets or Sets the level.
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// Gets or Sets the remaning time on the map for bonus perk.
        /// </summary>
        int BonusTime { get; set; }

        /// <summary>
        /// Gets or Sets the X coordinate of the mouse's position.
        /// </summary>
        double Dxb { get; set; }

        /// <summary>
        ///  Gets or Sets the Y coordinate of the mouse's position.
        /// </summary>
        double Dyb { get; set; }

        /// <summary>
        ///  Gets or Sets the time for shield regeneration.
        /// </summary>
        int Szamlaloshield { get; set; }

        /// <summary>
        ///  Gets or Sets the time for shield cooldown.
        /// </summary>
        int ShieldCooldown { get; set; }

        /// <summary>
        /// Gets or Sets the enemy which is about to be exploded.
        /// </summary>
        EnemyShipItem ExpEnemy { get; set; }

        /// <summary>
        ///  Gets or Sets the player.
        /// </summary>
        SpaceShipItem PlayerShip { get; set; }

        /// <summary>
        ///  Gets the station.
        /// </summary>
        SpaceStationItem Station { get; }

        /// <summary>
        /// Gets the bullets list of the player.
        /// </summary>
        List<BulletItem> Bullets { get; }

        /// <summary>
        /// Gets or Sets the bonus perk.
        /// </summary>
        BonusItem Bonus { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether to bonus is picked or not.
        /// </summary>
        bool IsBonusPicked { get; set; }

        /// <summary>
        /// Gets the bullet list of the enemies.
        /// </summary>
        List<BulletItem> EnemyBullets { get; }

        /// <summary>
        /// Gets the list of gasclouds.
        /// </summary>
        List<GasCloudItem> GasClouds { get; }

        /// <summary>
        /// Gets the portal.
        /// </summary>
        PortalItem Portal { get; }

        /// <summary>
        /// Gets a list of bullets which should be deleted.
        /// </summary>
        List<BulletItem> DeletableBullets { get; }

        /// <summary>
        /// Gets the meteors.
        /// </summary>
        List<MeteorItem> Meteors { get; }

        /// <summary>
        /// Gets or Sets the enemy ships.
        /// </summary>
        List<EnemyShipItem> EnemyShips { get; set; }
    }
}
