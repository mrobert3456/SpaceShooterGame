// <copyright file="MainLogic.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Threading;
    using GameModel;

    /// <summary>
    /// Implements IGameLogic interface.
    /// </summary>
    public class MainLogic : IGameLogic
    {
        private IGameModel model;
        private bool alreadyDeleted;
        private bool gameOver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainLogic"/> class.
        /// </summary>
        /// <param name="model">IGameModel implementation.</param>
        public MainLogic(IGameModel model)
        {
            this.model = model;
        }

        /// <inheritdoc/>
        public bool GameOver { get => this.gameOver; set => this.gameOver = value; }

        /// <inheritdoc/>
        public void MoveShip(double xInc, double yInc)
        {
            if (this.model.PlayerShip.DY + yInc >= 2160)
            {
                this.model.PlayerShip.DY = 2140;
            }
            else if (this.model.PlayerShip.DX + xInc >= 3840)
            {
                this.model.PlayerShip.DX = 3820;
            }
            else if (this.model.PlayerShip.DY + yInc <= -2160)
            {
                this.model.PlayerShip.DY = -2140;
            }
            else if (this.model.PlayerShip.DX + xInc <= -3840)
            {
                this.model.PlayerShip.DX = -3820;
            }
            else
            {
                this.model.PlayerShip.DX += xInc;
                this.model.PlayerShip.DY += yInc;
            }
        }

        /// <inheritdoc/>
        public IGameModel NextLevel()
        {
            if (this.model.Portal != null && this.model.PlayerShip.IsCollision(this.model.Portal))
            {
                return ModelFactory.CreateModel(this.model.GameWidth, this.model.GameHeight, 2, this.model.PlayerShip);
            }

            return this.model;
        }

        /// <inheritdoc/>
        public void SetDegree(double x, double y)
        {
            double x1 = x - this.model.PlayerShip.CX;
            double y1 = y - this.model.PlayerShip.CY;
            this.model.PlayerShip.Rad = Math.Atan2(y1, x1) * 180 / Math.PI;
            this.model.Dyb = y;
            this.model.Dxb = x;
        }

        /// <inheritdoc/>
        public void OneTick(DispatcherTimer timerAn)
        {
            if (this.model.Level == 2 && this.model.EnemyShips.Count == 1 && !this.alreadyDeleted)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.model.Meteors.RemoveAt(this.model.Meteors.Count - 1);
                }

                this.alreadyDeleted = true;
            }

            if (this.model.Meteors.Count > 0)
            {
                foreach (var item in this.model.Meteors)
                {
                    if (item.IsCollision(this.model.PlayerShip))
                    {
                        this.model.Szamlaloshield = 0;
                        if (this.model.PlayerShip.Shield == 0)
                        {
                            this.model.PlayerShip.Health = 0;
                            this.GameOver = true;
                        }
                        else
                        {
                            this.model.PlayerShip.Shield = 0;
                        }

                        double playerPosX = this.model.PlayerShip.CX - this.model.PlayerShip.DX;
                        double playerPosY = this.model.PlayerShip.CY - this.model.PlayerShip.DY;
                        if (playerPosX > item.CX)
                        {
                            this.model.PlayerShip.DX -= 20;
                        }
                        else if (playerPosX < item.CX)
                        {
                            this.model.PlayerShip.DX += 20;
                        }

                        this.model.ShieldCooldown = 0;
                    }
                }
            }

            this.model.Szamlaloshield += 10;
            if (this.model.ShieldCooldown < 2000)
            {
                this.model.ShieldCooldown += 10;
            }

            if (this.model.ShieldCooldown == 2000)
            {
                if (this.model.Szamlaloshield >= 250)
                {
                    if (this.model.PlayerShip.Shield < 50 && this.model.PlayerShip.Health > 0)
                    {
                        this.model.PlayerShip.Shield += 2;
                    }

                    this.model.Szamlaloshield = 0;
                }
            }

            if (this.model.GasClouds.Count > 0)
            {
                foreach (var item in this.model.GasClouds)
                {
                    if (item.IsCollision(this.model.PlayerShip))
                    {
                        this.model.PlayerShip.Shield = 0;
                    }
                }
            }

            if (this.model.Station.IsCollision(this.model.PlayerShip))
            {
                if (this.model.PlayerShip.Health < 100)
                {
                    this.model.PlayerShip.Health += 2;
                }
            }

            if (this.model.PlayerShip.Health <= 0)
            {
                this.gameOver = true;
            }

            if (this.model.Bonus != null)
            {
                this.model.Bonus.Time -= 10;
                if (this.model.Bonus.IsCollision(this.model.PlayerShip))
                {
                    if (this.model.Bonus.BonusType == "InstaKill")
                    {
                        this.model.PlayerShip.Hit = 100;
                    }
                    else if (this.model.Bonus.BonusType == "DoubleP")
                    {
                        this.model.PlayerShip.GetPoints *= 2;
                    }

                    this.model.Bonus = null;
                    this.model.IsBonusPicked = true;
                }
            }

            if (this.model.IsBonusPicked)
            {
                this.model.BonusTime -= 10;

                if (this.model.BonusTime == 0)
                {
                    this.model.PlayerShip.GetPoints = 2;
                    this.model.PlayerShip.Hit = 30;
                    this.model.IsBonusPicked = false;
                    this.model.Bonus = null;
                    this.model.PlayerShip.Bonus = null;
                    this.model.BonusTime = 10000;
                }
            }
        }
    }
}
