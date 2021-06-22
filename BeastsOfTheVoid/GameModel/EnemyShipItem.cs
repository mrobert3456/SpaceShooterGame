// <copyright file="EnemyShipItem.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// The enemy ship game item.
    /// </summary>
    public class EnemyShipItem : SpaceShipItem
    {
        private ImageBrush imageBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyShipItem"/> class.
        /// </summary>
        /// <param name="cx">the x axis parameter.</param>
        /// <param name="cy">the y axis parameter.</param>
        /// <param name="sizew">The width of the ship.</param>
        /// <param name="sizeh">The height of the ship.</param>
        /// <param name="health">Space ship's health.</param>
        /// <param name="shield">Space ship's shield.</param>
        public EnemyShipItem(double cx, double cy, int sizew = 0, int sizeh = 0, int health = 100, int shield = 100)
            : base(cx, cy, sizew, sizeh)
        {
            BitmapImage bmp = new BitmapImage();

            bmp.BeginInit();
            bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Images.enemy2.png");

            bmp.EndInit();
            this.ImageBrush = new ImageBrush(bmp);

            this.Health = health;
            this.Shield = shield;
        }

        /// <summary>
        /// Gets or Sets the image of this object.
        /// </summary>
        public ImageBrush ImageBrush { get => this.imageBrush; set => this.imageBrush = value; }
    }
}
