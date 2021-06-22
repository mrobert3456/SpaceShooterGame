// <copyright file="ShootLogicTests.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace GameLogicTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameLogic;
    using GameModel;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Contains the ShootLogic test methods.
    /// </summary>
    public class ShootLogicTests
    {
        /// <summary>
        /// Tests the enemy's shooting.
        /// </summary>
        [Test]
        public void TestShootEnemy()
        {
            Mock<IGameModel> mockedmodel = new Mock<IGameModel>(MockBehavior.Loose);
            ShootLogic shootLogic = new ShootLogic(mockedmodel.Object);

            mockedmodel.Setup(i => i.GameHeight).Returns(100);
            mockedmodel.Setup(i => i.GameWidth).Returns(100);
            mockedmodel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(10, 10));
            mockedmodel.Setup(i => i.EnemyShips).Returns(new List<EnemyShipItem>() { new EnemyShipItem(20, 10) });
            mockedmodel.Setup(i => i.EnemyBullets).Returns(new List<BulletItem>());

            shootLogic.EnemyShoot(mockedmodel.Object.EnemyShips[0]);

            Assert.That(mockedmodel.Object.EnemyBullets.Count, Is.AtLeast(1));
        }

        /// <summary>
        /// Tests the player's shoot method.
        /// </summary>
        [Test]
        public void TestShoot()
        {
            Mock<IGameModel> mockedmodel = new Mock<IGameModel>(MockBehavior.Loose);
            ShootLogic shootLogic = new ShootLogic(mockedmodel.Object);
            mockedmodel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(10, 10));
            mockedmodel.Setup(i => i.Bullets).Returns(new List<BulletItem>());
            shootLogic.Shoot();
            Assert.That(mockedmodel.Object.Bullets.Count, Is.AtLeast(1));
        }
    }
}
