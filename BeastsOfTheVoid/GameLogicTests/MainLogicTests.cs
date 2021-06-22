// <copyright file="MainLogicTests.cs" company="V8K90F_WN1M1P">
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
    /// Contains the MainLogic test methods.
    /// </summary>
    public class MainLogicTests
    {
        /// <summary>
        /// Tests the SetDegree method.
        /// </summary>
        [Test]
        public void TestSetDegree()
        {
            Mock<IGameModel> mockedModel = new Mock<IGameModel>();

            mockedModel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(100, 100));
            MainLogic logic = new MainLogic(mockedModel.Object);

            logic.SetDegree(10, 10);

            double degree = Math.Atan2(10 - mockedModel.Object.PlayerShip.CX, 10 - mockedModel.Object.PlayerShip.CY) * 180 / Math.PI;

            Assert.That(mockedModel.Object.PlayerShip.Rad, Is.EqualTo(degree));
        }

        /// <summary>
        /// Tests the Move method of the player's ship.
        /// </summary>
        /// <param name="x">The X axis of the mouse position.</param>
        /// <param name="y">The Y axis of the mouse position.</param>
        [TestCase(10, 10)]
        [TestCase(15, 20)]
        [TestCase(100, 100)]
        [TestCase(-10, -50)]
        public void TestMove(int x, int y)
        {
            Mock<IGameModel> mockedModel = new Mock<IGameModel>();
            mockedModel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(100, 100));
            MainLogic logic = new MainLogic(mockedModel.Object);

            double resultx = mockedModel.Object.PlayerShip.DX + x;
            double resulty = mockedModel.Object.PlayerShip.DY + y;
            logic.MoveShip(x, y);

            Assert.That(mockedModel.Object.PlayerShip.DX, Is.EqualTo(resultx));
            Assert.That(mockedModel.Object.PlayerShip.DY, Is.EqualTo(resulty));
        }

        /// <summary>
        /// Test the NextLevel method.
        /// </summary>
        /// <param name="player">Player's ship coordinate.</param>
        /// <param name="portal">Portal object coordinate.</param>
        [TestCase(100, 100)]
        [TestCase(100, 500)]

        public void TestNextLevel(int player, int portal)
        {
            Mock<IGameModel> mockedModel = new Mock<IGameModel>();

            mockedModel.Setup(i => i.Level).Returns(1);
            mockedModel.Setup(i => i.Portal).Returns(new PortalItem(portal, portal));
            mockedModel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(player, player));

            MainLogic logic = new MainLogic(mockedModel.Object);

            IGameModel model = logic.NextLevel();

            int expected = player == portal ? 2 : 1;
            Assert.That(model.Level, Is.EqualTo(expected));
        }

        /// <summary>
        /// Test the collision between the player and a meteor.
        /// </summary>
        /// <param name="player">Player's ship coordinate.</param>
        /// <param name="meteor">A meteor's coordinate.</param>
        [TestCase(100, 100)]
        [TestCase(100, 500)]
        public void TestCollision(int player, int meteor)
        {
            Mock<IGameModel> mockedModel = new Mock<IGameModel>();
            mockedModel.Setup(i => i.PlayerShip).Returns(new SpaceShipItem(player, player));
            mockedModel.Setup(i => i.Meteors).Returns(new List<MeteorItem>() { new MeteorItem(meteor, meteor) });
            MainLogic logic = new MainLogic(mockedModel.Object);

            bool exp = player == meteor ? true : false;
            Assert.That(mockedModel.Object.PlayerShip.IsCollision(mockedModel.Object.Meteors[0]), Is.EqualTo(exp));
        }
    }
}
