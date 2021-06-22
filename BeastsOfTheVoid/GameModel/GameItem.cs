// <copyright file="GameItem.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;

    /// <summary>
    /// Simple item that defines the basic definition of an the game elements.
    /// </summary>
    public abstract class GameItem
    {
        /// <summary>
        /// The area of the game item.
        /// </summary>
        protected Geometry area;

        /// <summary>
        /// Gets or Sets the x axis position of the game item.
        /// </summary>
        public double CX { get; set; }

        /// <summary>
        /// Gets or Sets the y axis position of the game item.
        /// </summary>
        public double CY { get; set; }

        /// <summary>
        /// Gets the area of the game item.
        /// </summary>
        public Geometry RealArea
        {
            get
            {
                return this.area;
            }
        }

        /// <summary>
        /// Decides whether the item collided with another item.
        /// </summary>
        /// <param name="other">The other item that this item could've collided with.</param>
        /// <returns>Whether they collided or not.</returns>
        public bool IsCollision(GameItem other)
        {
            if (other != null)
            {
                return Geometry.Combine(this.area, other.area, GeometryCombineMode.Intersect, null).GetArea() > 0;
            }

            return false;
        }
    }
}
