// <copyright file="Model.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This will be the Model of the game, there will be the game defining objects, and properties.
    /// </summary>
    public class Model : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="w">The width of the screen.</param>
        /// <param name="h">The height of the screen.</param>
        /// <param name="lvl">The actual level of the game.</param>
        /// <param name="player">The player who plays the game.</param>
        public Model(double w, double h, int lvl = 1, SpaceShipItem player = null)
        {
            this.GameWidth = w;
            this.GameHeight = h;
            if (player == null)
            {
                this.PlayerShip = ModelFactory.CreateSpaceShip(this.GameWidth / 2, this.GameHeight / 2);
            }
            else
            {
                this.PlayerShip = player;
            }

            this.Level = lvl;

            this.CreateMeteors(lvl);

            this.CreateGasClouds(lvl);

            this.CreateEnemies(lvl);

            if (lvl == 1)
            {
                this.Station = ModelFactory.CreateStation((this.GameWidth / 2) - 100, (this.GameHeight / 2) - 100);
                this.Portal = ModelFactory.CreatePortal(3720 + (this.GameWidth / 2), -2050 + (this.GameHeight / 2));
            }
            else
            {
                this.Station = ModelFactory.CreateStation(4344, -975);
                this.Portal = ModelFactory.CreatePortal(5000, 5000);
                this.EnemyShips.Add(ModelFactory.CreateEnemy(-1856, 1036, 350, 350, 500, 500));
            }
        }

        /// <inheritdoc/>
        public double GameWidth { get; private set; }

        /// <inheritdoc/>
        public int BonusTime { get; set; } = 10000;

        /// <inheritdoc/>
        public int Level { get; set; }

        /// <inheritdoc/>
        public double GameHeight { get; private set; }

        /// <inheritdoc/>
        public bool IsBonusPicked { get; set; }

        /// <inheritdoc/>
        public SpaceShipItem PlayerShip { get; set; }

        /// <inheritdoc/>
        public SpaceStationItem Station { get; private set; }

        /// <inheritdoc/>
        public List<BulletItem> Bullets { get; private set; } = new List<BulletItem>();

        /// <inheritdoc/>
        public List<BulletItem> EnemyBullets { get; private set; } = new List<BulletItem>();

        /// <inheritdoc/>
        public List<GasCloudItem> GasClouds { get; private set; } = new List<GasCloudItem>();

        /// <inheritdoc/>
        public List<BulletItem> DeletableBullets { get; set; } = new List<BulletItem>();

        /// <inheritdoc/>
        public List<MeteorItem> Meteors { get; private set; } = new List<MeteorItem>();

        /// <inheritdoc/>
        public BonusItem Bonus { get; set; }

        /// <inheritdoc/>
        public List<EnemyShipItem> EnemyShips { get; set; } = new List<EnemyShipItem>();

        /// <inheritdoc/>
        public PortalItem Portal { get; set; }

        /// <inheritdoc/>
        public EnemyShipItem ExpEnemy { get; set; }

        /// <inheritdoc/>
        public double Dxb { get; set; }

        /// <inheritdoc/>
        public double Dyb { get; set; }

        /// <inheritdoc/>
        public int Szamlaloshield { get; set; }

        /// <inheritdoc/>
        public int ShieldCooldown { get; set; }

        private void CreateMeteors(int lvl = 1)
        {
            if (lvl == 1)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.meteorcoords.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.Meteors.Add(ModelFactory.CreateMeteor(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines(stream.ToString()).ToList().ForEach(s => this.Meteors.Add(new MeteorItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
            else
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.meteorcoordslvl2.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.Meteors.Add(ModelFactory.CreateMeteor(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines("GameModel.Coords.meteorcoordslvl2.txt").ToList().ForEach(s => this.Meteors.Add(new MeteorItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
        }

        private void CreateGasClouds(int lvl = 1)
        {
            if (lvl == 1)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.gascloudcoords.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.GasClouds.Add(ModelFactory.CreateGasCloud(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines("GameModel.Coords.gascloudcoords.txt").ToList().ForEach(s => this.GasClouds.Add(new GasCloudItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
            else
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.gascloudcoordslvl2.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.GasClouds.Add(ModelFactory.CreateGasCloud(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines("GameModel.Coords.gascloudcoordslvl2.txt").ToList().ForEach(s => this.GasClouds.Add(new GasCloudItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
        }

        private void CreateEnemies(int lvl = 1)
        {
            if (lvl == 1)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.enemycoords.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.EnemyShips.Add(ModelFactory.CreateEnemy(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines("GameModel.Coords.enemycoords.txt").ToList().ForEach(s => this.EnemyShips.Add(new EnemyShipItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
            else
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameModel.Coords.enemycoordslvl2.txt");
                StreamReader sr = new StreamReader(stream);

                while (!sr.EndOfStream)
                {
                    string helper = sr.ReadLine();
                    this.EnemyShips.Add(ModelFactory.CreateEnemy(double.Parse(helper.Split(',')[0]), double.Parse(helper.Split(',')[1])));
                }

                sr.Close();

                // File.ReadAllLines("GameModel.Coords.enemycoordslvl2.txt").ToList().ForEach(s => this.EnemyShips.Add(new EnemyShipItem(double.Parse(s.Split(',')[0]), double.Parse(s.Split(',')[1]))));
            }
        }
    }
}
