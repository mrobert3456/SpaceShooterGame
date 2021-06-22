// <copyright file="PortalItem.cs" company="V8K90F">
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
    /// This will be the portal. This will be used for travelling between the two maps.
    /// </summary>
    public class PortalItem : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortalItem"/> class.
        /// </summary>
        /// <param name="cx">The x axis position of the portal.</param>
        /// <param name="cy">The y axis position of the portal.</param>
        public PortalItem(double cx, double cy)
        {
            this.CX = cx;
            this.CY = cy;
            this.area = new EllipseGeometry(new Point(this.CX, this.CY), 50, 100);
        }
    }
}
