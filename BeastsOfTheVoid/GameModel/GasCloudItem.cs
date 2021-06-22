// <copyright file="GasCloudItem.cs" company="V8K90F">
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
    /// The Nebula items. They will bring the player's ship down.
    /// </summary>
    public class GasCloudItem : GameItem
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="GasCloudItem"/> class.
        /// </summary>
        /// <param name="cx">the x axis position of the nebula.</param>
        /// <param name="cy">The y axis position of the nebula.</param>
        public GasCloudItem(double cx, double cy)
        {
            this.CX = cx;
            this.CY = cy;
            int rando = Rnd.Next(800, 1000);
            this.area = new EllipseGeometry(new Point(this.CX, this.CY), rando, rando);
        }
    }
}
