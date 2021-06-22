// <copyright file="SpaceStationItem.cs" company="V8K90F">
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
    /// This will be the space station, this will heal up the player.
    /// </summary>
    public class SpaceStationItem : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceStationItem"/> class.
        /// </summary>
        /// <param name="cx">The x axis position of the portal.</param>
        /// <param name="cy">The y axis position of the portal.</param>
        public SpaceStationItem(double cx, double cy)
        {
            this.CX = cx;
            this.CY = cy;
            this.area = new EllipseGeometry(new Point(this.CX, this.CY), 200, 200);
        }
    }
}
