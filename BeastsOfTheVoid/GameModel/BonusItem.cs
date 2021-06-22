// <copyright file="BonusItem.cs" company="V8K90F">
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
    /// The perks that the player can pick up.
    /// </summary>
    public class BonusItem : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BonusItem"/> class.
        /// </summary>
        /// <param name="cx">The x axis coordinate where the perk will be.</param>
        /// <param name="cy">The y axis coordinate where the perk will be.</param>
        /// <param name="bt">The type of the perk.</param>
        public BonusItem(double cx, double cy, string bt)
        {
            this.CX = cx;
            this.CY = cy;
            this.area = new RectangleGeometry(new Rect(this.CX, this.CY, 50, 50));
            this.BonusType = bt;
        }

        /// <summary>
        /// Gets or Sets what the perks Type is.
        /// </summary>
        public string BonusType { get; set; }

        /// <summary>
        /// Gets or Sets how long the perk will be active.
        /// </summary>
        public int Time { get; set; } = 10000;
    }
}
