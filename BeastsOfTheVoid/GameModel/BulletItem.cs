// <copyright file="BulletItem.cs" company="V8K90F">
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
    /// The bullets that the ships will shoot.
    /// </summary>
    public class BulletItem : GameItem
    {
        private const int SIZEW = 10;
        private const int SIZEH = 10;
        private static double speed = 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletItem"/> class.
        /// </summary>
        /// <param name="cx">The x axis parameter of the bullet.</param>
        /// <param name="cy">The y axis parameter of the bullet.</param>
        /// <param name="x">Helps calculate where the bullet should travel on the x axis.</param>
        /// <param name="y">Helps calculate where the bullet should travel ont the y axis.</param>
        public BulletItem(double cx, double cy, double x, double y)
        {
            this.CX = cx;
            this.CY = cy;

            this.DX = x - this.CX;
            this.DY = y - this.CY;
            double h = Math.Sqrt(Math.Pow(this.DX, 2) + Math.Pow(this.DY, 2));
            this.DX = (this.DX / h) * speed;
            this.DY = (this.DY / h) * speed;
        }

        /// <summary>
        /// Gets the rotational degree of the bullet.
        /// </summary>
        public double RotDegree { get; private set; }

        /// <summary>
        /// Gets the x axis position that the bullet travelled on the map.
        /// </summary>
        public double DX { get; private set; }

        /// <summary>
        /// Gets the y axis position that the bullet travelled on the map.
        /// </summary>
        public double DY { get; private set; }

        /// <summary>
        /// Gets or Sets the rotational degree of the bullet.
        /// </summary>
        public double Rad
        {
            get
            {
                return this.RotDegree / 180 * Math.PI;
            }

            set
            {
                this.RotDegree = 180 * value / Math.PI;
            }
        }

        /// <summary>
        /// Calculates where the bullet should move.
        /// </summary>
        public void Move()
        {
            if (this.RotDegree >= 0 && this.RotDegree <= 90)
            {
                this.CX += this.DX;
                this.CY += this.DY;
            }
            else if (this.RotDegree > 90 && this.RotDegree <= 180)
            {
                this.CX -= this.DX;
                this.CY += this.DY;
            }
            else if (this.RotDegree > 180 && this.RotDegree <= 270)
            {
                this.CX -= this.DX;
                this.CY -= this.DY;
            }
            else
            {
                this.CX += this.DX;
                this.CY -= this.DY;
            }
        }

        /// <summary>
        /// Creates the geometry of the bullet.
        /// </summary>
        /// <param name="xx">The x axis starting point of the bullet.</param>
        /// <param name="yy">The y axis starting point of the bullet.</param>
        /// <returns>The geometry of the bullet.</returns>
        public Geometry BulletGeo(int xx = 0, int yy = 0)
        {
            if (xx != 0 && yy != 0)
            {
                this.CX = xx;
                this.CY = yy;
            }

            this.area = new EllipseGeometry(new Point(this.CX, this.CY), SIZEW, SIZEH);
            return this.area;
        }
    }
}
