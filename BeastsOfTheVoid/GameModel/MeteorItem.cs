// <copyright file="MeteorItem.cs" company="V8K90F">
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
    /// The meteor items of the game. they will be the obstacles.
    /// </summary>
    public class MeteorItem : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeteorItem"/> class.
        /// </summary>
        /// <param name="cx">The x axis position of the meteor.</param>
        /// <param name="cy">The y axis position of the meteor.</param>
        public MeteorItem(double cx, double cy)
        {
            this.CX = cx;
            this.CY = cy;
            this.area = new EllipseGeometry(new Point(this.CX, this.CY), 100, 100);
        }
    }
}
