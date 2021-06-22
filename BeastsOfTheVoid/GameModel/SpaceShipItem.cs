// <copyright file="SpaceShipItem.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// The spaceship item of the game.
    /// </summary>
    public class SpaceShipItem : GameItem
    {
        /// <summary>
        /// The width of the ship.
        /// </summary>
        private int sizeW = 80;

        /// <summary>
        /// the height of the ship.
        /// </summary>
        private int sizeH = 80;

        /// <summary>
        /// The rotational degree of the ship.
        /// </summary>
        private double rotDegree;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceShipItem"/> class.
        /// </summary>
        /// <param name="cx">the x axis parameter.</param>
        /// <param name="cy">the y axis parameter.</param>
        /// <param name="sizew">The width of the ship.</param>
        /// <param name="sizeh">The height of the ship.</param>
        public SpaceShipItem(double cx, double cy, int sizew = 0, int sizeh = 0)
        {
            this.CX = cx;
            this.CY = cy;
            if (sizew > 0 && sizeh > 0)
            {
                this.sizeW = sizew;
                this.sizeH = sizeh;
            }

            this.area = new EllipseGeometry(new Point(this.CX, this.CY), this.sizeW, this.sizeH);
        }

        /// <summary>
        /// Gets or Sets the name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the x axis position that the player travelled on the map.
        /// </summary>
        public double DX { get; set; }

        /// <summary>
        /// Gets or Sets the y axis position that the player travelled on the map.
        /// </summary>
        public double DY { get; set; }

        /// <summary>
        /// Gets or Sets the health of the ship.
        /// </summary>
        public int Health { get; set; } = 100;

        /// <summary>
        /// Gets or Sets the shield of the ship.
        /// </summary>
        public int Shield { get; set; } = 50;

        /// <summary>
        /// Gets or Sets the Bonus perks of the ship.
        /// </summary>
        public BonusItem Bonus { get; set; }

        /// <summary>
        /// Gets or Sets the score of the ship.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or Sets the kills of the ship.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or Sets the hit damage of the ship.
        /// </summary>
        public int Hit { get; set; } = 10;

        /// <summary>
        /// Gets or Sets how many points the player earns from a defeated enemy.
        /// </summary>
        public int GetPoints { get; set; } = 2;

        /// <summary>
        /// Gets the width of the ship.
        /// </summary>
        public int SizeW
        {
            get
            {
                return this.sizeW;
            }
        }

        /// <summary>
        /// Gets the height of the ship.
        /// </summary>
        public int SizeH
        {
            get
            {
                return this.sizeH;
            }
        }

        /// <summary>
        /// Gets the real position of the ship on the x axis.
        /// </summary>
        public double PlayerPosX
        {
            get
            {
                return this.CX - this.DX;
            }
        }

        /// <summary>
        /// Gets the real position of the ship on the y axis.
        /// </summary>
        public double PlayerPosY
        {
            get
            {
                return this.CY - this.DY;
            }
        }

        /// <summary>
        /// Gets or Sets the rotational degree of the ship.
        /// </summary>
        public double Rad
        {
            get
            {
                return this.rotDegree; /*Math.PI * rotDegree / 180;*/
            }

            set
            {
                this.rotDegree = value; /*180 * value / Math.PI;*/
            }
        }
    }
}
