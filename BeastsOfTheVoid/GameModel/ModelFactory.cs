// <copyright file="ModelFactory.cs" company="V8K90F_WN1M1P">
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
    /// Creates model entities.
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// Creates BonusItem.
        /// </summary>
        /// <param name="cx">The x axis coordinate where the perk will be.</param>
        /// <param name="cy">The y axis coordinate where the perk will be.</param>
        /// <param name="bt">The type of the perk.</param>
        /// <returns>Returns a BinusItem.</returns>
        public static BonusItem CreateBonusItem(double cx, double cy, string bt)
        {
            return new BonusItem(cx, cy, bt);
        }

        /// <summary>
        /// Crates a Bullet.
        /// </summary>
        /// <param name="cx">The x axis parameter of the bullet.</param>
        /// <param name="cy">The y axis parameter of the bullet.</param>
        /// <param name="x">Helps calculate where the bullet should travel on the x axis.</param>
        /// <param name="y">Helps calculate where the bullet should travel ont the y axis.</param>
        /// <returns>Returns a BulletItem.</returns>
        public static BulletItem CreateBullet(double cx, double cy, double x, double y)
        {
            return new BulletItem(cx, cy, x, y);
        }

        /// <summary>
        /// Creates an Enemy.
        /// </summary>
        /// <param name="cx">the x axis parameter.</param>
        /// <param name="cy">the y axis parameter.</param>
        /// <param name="sizew">The width of the ship.</param>
        /// <param name="sizeh">The height of the ship.</param>
        /// <param name="health">Space ship's health.</param>
        /// <param name="shield">Space ship's shield.</param>
        /// <returns>Returns an EnemyShipItem.</returns>
        public static EnemyShipItem CreateEnemy(double cx, double cy, int sizew = 0, int sizeh = 0, int health = 100, int shield = 100)
        {
            return new EnemyShipItem(cx, cy, sizew, sizeh, health, shield);
        }

        /// <summary>
        /// Creates a Gas cloud.
        /// </summary>
        /// <param name="cx">the x axis position of the nebula.</param>
        /// <param name="cy">The y axis position of the nebula.</param>
        /// <returns>Return a GasCloudItem.</returns>
        public static GasCloudItem CreateGasCloud(double cx, double cy)
        {
            return new GasCloudItem(cx, cy);
        }

        /// <summary>
        /// Creates a meteor.
        /// </summary>
        /// <param name="cx">The x axis position of the meteor.</param>
        /// <param name="cy">The y axis position of the meteor.</param>
        /// <returns>Return a MeteorItem.</returns>
        public static MeteorItem CreateMeteor(double cx, double cy)
        {
            return new MeteorItem(cx, cy);
        }

        /// <summary>
        /// Creates a portal.
        /// </summary>
        /// <param name="cx">The x axis position of the portal.</param>
        /// <param name="cy">The y axis position of the portal.</param>
        /// <returns>Returns a PortalItem.</returns>
        public static PortalItem CreatePortal(double cx, double cy)
        {
            return new PortalItem(cx, cy);
        }

        /// <summary>
        /// Creates the player's spaceship.
        /// </summary>
        /// <param name="cx">the x axis parameter.</param>
        /// <param name="cy">the y axis parameter.</param>
        /// <param name="sizew">The width of the ship.</param>
        /// <param name="sizeh">The height of the ship.</param>
        /// <returns>Returns an SpaceShipItem.</returns>
        public static SpaceShipItem CreateSpaceShip(double cx, double cy, int sizew = 0, int sizeh = 0)
        {
            return new SpaceShipItem(cx, cy, sizew, sizeh);
        }

        /// <summary>
        /// Creates a space station.
        /// </summary>
        /// <param name="cx">The x axis position of the portal.</param>
        /// <param name="cy">The y axis position of the portal.</param>
        /// <returns>Returns a SpaceStationItem.</returns>
        public static SpaceStationItem CreateStation(double cx, double cy)
        {
            return new SpaceStationItem(cx, cy);
        }

        /// <summary>
        /// Creates a model.
        /// </summary>
        /// <param name="w">The width of the screen.</param>
        /// <param name="h">The height of the screen.</param>
        /// <param name="lvl">The actual level of the game.</param>
        /// <param name="player">The player who plays the game.</param>
        /// <returns>Returns a model.</returns>
        public static Model CreateModel(double w, double h, int lvl = 1, SpaceShipItem player = null)
        {
            return new Model(w, h, lvl, player);
        }
    }
}
