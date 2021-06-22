// <copyright file="ShootLogic.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Threading;
    using GameModel;

    /// <summary>
    /// Implements IShootLogic interface.
    /// </summary>
    public class ShootLogic : IShootLogic
    {
        private IGameModel model;
        private Random r = new Random();
        private string[] bonuses;
        private int szamlaloshoot;
        private string dir;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShootLogic"/> class.
        /// </summary>
        /// <param name="model">IGameModel implementation.</param>
        public ShootLogic(IGameModel model)
        {
            this.model = model;
            this.bonuses = new string[2];
            this.bonuses[0] = "InstaKill";
            this.bonuses[1] = "DoubleP";
            this.szamlaloshoot = 0;

            this.dir = Directory.GetCurrentDirectory();
            List<string> h = this.dir.Split('\\').ToList();
            this.dir = string.Empty;

            for (int i = 0; i < h.Count - 4; i++)
            {
                this.dir += h[i] + "\\";
            }

            this.dir += "GameLogic";
        }

        /// <inheritdoc/>
        public void BulletTick(BulletItem bullet)
        {
            bullet?.Move();
        }

        /// <inheritdoc/>
        public void Shoot()
        {
            this.model.Bullets.Add(ModelFactory.CreateBullet(this.model.PlayerShip.CX, this.model.PlayerShip.CY, this.model.Dxb, this.model.Dyb));
            new Task(() => { new SoundPlayer(this.dir + "\\Sounds\\shoot.wav").Play(); }, TaskCreationOptions.LongRunning).Start();
        }

        /// <inheritdoc/>
        public void ShootTick(DispatcherTimer timerAn, DispatcherTimer sht)
        {
            if (this.model.Bullets.Count > 0)
            {
                foreach (var item in this.model.Bullets)
                {
                    if (!this.model.Meteors.Any(m => m.IsCollision(item)))
                    {
                        EnemyShipItem enemy = this.model.EnemyShips.Where(i => i.IsCollision(item)).FirstOrDefault();
                        if (enemy != null)
                        {
                            enemy.Shield -= this.model.PlayerShip.Hit;
                            this.model.DeletableBullets.Add(item);
                            if (enemy.Shield <= 0)
                            {
                                enemy.Health -= this.model.PlayerShip.Hit;
                            }

                            if (enemy.Health <= 0)
                            {
                                new Task(() => { new SoundPlayer(this.dir + "\\Sounds\\explosion.wav").Play(); }, TaskCreationOptions.LongRunning).Start();
                                this.model.ExpEnemy = enemy;
                                timerAn?.Start();
                                this.model.EnemyShips.Remove(enemy);
                                this.model.PlayerShip.Kills++;
                                this.model.PlayerShip.Points += this.model.PlayerShip.GetPoints;

                                if (this.model.PlayerShip.Bonus == null && this.r.Next(0, 101) <= 40)
                                {
                                    this.model.PlayerShip.Bonus = new BonusItem(enemy.CX, enemy.CY, this.bonuses[this.r.Next(0, 2)]);
                                    this.model.Bonus = this.model.PlayerShip.Bonus;
                                }
                            }
                        }

                        if (item.CX < 0 || item.CY > 1080 || item.CX > 1920 || item.CY < 0)
                        {
                            this.model.DeletableBullets.Add(item);
                        }
                        else
                        {
                            this.BulletTick(item);
                        }
                    }
                    else
                    {
                        this.model.DeletableBullets.Add(item);
                    }
                }

                this.model.DeletableBullets.ForEach(b => this.model.Bullets.Remove(b));
            }
            else
            {
                sht?.Stop();
            }
        }

        /// <inheritdoc/>
        public void EnemyShoot(SpaceShipItem enemy)
        {
            if (enemy != null)
            {
                double cx = enemy.CX + this.model.PlayerShip.DX;
                double cy = enemy.CY + this.model.PlayerShip.DY;
                double shootDX = this.r.Next((int)(this.model.PlayerShip.PlayerPosX - enemy.CX + cx - 200), (int)(this.model.PlayerShip.PlayerPosX - enemy.CX + cx + 200));
                double shootDY = this.r.Next((int)(this.model.PlayerShip.PlayerPosY - enemy.CY + cy - 200), (int)(this.model.PlayerShip.PlayerPosY - enemy.CY + cy + 200));

                if (Math.Abs(enemy.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(enemy.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
                {
                    this.model.EnemyBullets.Add(ModelFactory.CreateBullet(cx, cy, shootDX, shootDY));
                }
            }
        }

        /// <inheritdoc/>
        public void EnemyShootTick()
        {
            if (this.model.EnemyBullets.Count > 0)
            {
                foreach (var item in this.model.EnemyBullets)
                {
                    if (!this.model.Meteors.Any(m => m.IsCollision(item)))
                    {
                        if (item.IsCollision(this.model.PlayerShip))
                        {
                            this.model.Szamlaloshield = 0;
                            this.model.DeletableBullets.Add(item);
                            if (this.model.PlayerShip.Shield <= 0)
                            {
                                if (this.model.PlayerShip.Health <= 0)
                                {
                                    this.model.PlayerShip.Health = 0;
                                }
                                else
                                {
                                    this.model.PlayerShip.Health -= 10;
                                }
                            }
                            else
                            {
                                this.model.PlayerShip.Shield -= 10;
                                if (this.model.PlayerShip.Shield < 0)
                                {
                                    this.model.PlayerShip.Shield = 0;
                                }
                            }

                            this.model.ShieldCooldown = 0;
                        }

                        if (item.CX < 0 || item.CY > 1080 || item.CX > 1920 || item.CY < 0)
                        {
                            this.model.DeletableBullets.Add(item);
                        }
                        else
                        {
                            this.BulletTick(item);
                        }
                    }
                    else
                    {
                        this.model.DeletableBullets.Add(item);
                    }
                }

                this.model.DeletableBullets.ForEach(b => this.model.EnemyBullets.Remove(b));
            }
        }

        /// <inheritdoc/>
        public void SpaceShipTick()
        {
            foreach (var item in this.model.EnemyShips)
            {
                this.EnemyShoot(item);
            }
        }

        /// <inheritdoc/>
        public void ShootOnetick()
        {
            this.EnemyShootTick();
            this.szamlaloshoot += 10;

            if (this.szamlaloshoot == 2000)
            {
                this.SpaceShipTick();
                this.szamlaloshoot = 0;
            }
        }
    }
}
