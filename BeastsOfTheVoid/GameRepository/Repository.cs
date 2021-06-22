// <copyright file="Repository.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using GameModel;

    /// <summary>
    /// This will be the Game's Repository. It will be used for saving, and loading the game.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Saves the game's state into an XML file.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="model">The model which contains the data of the game.</param>
        public static void SaveGame(string name, IGameModel model)
        {
            if (model != null)
            {
                XDocument doc = new XDocument();

                XElement[] playerData = new XElement[]
                {
                    new XElement("Point", model.PlayerShip.Points),
                    new XElement("Kills", model.PlayerShip.Kills),
                    new XElement("Dx", model.PlayerShip.DX),
                    new XElement("Dy", model.PlayerShip.DY),
                    new XElement("Health", model.PlayerShip.Health),
                    new XElement("Shield", model.PlayerShip.Shield),
                };

                List<XElement> enemies = new List<XElement>();
                foreach (var item in model.EnemyShips)
                {
                    enemies.Add(new XElement(
                                            "Enemy",
                                            new XElement("Cx", item.CX),
                                            new XElement("Cy", item.CY),
                                            new XElement("Sizew", item.SizeW),
                                            new XElement("Sizeh", item.SizeH),
                                            new XElement("Health", item.Health),
                                            new XElement("Shield", item.Shield)));
                }

                doc.Add(new XElement(
                            "Game",
                            new XElement("Level", model.Level),
                            new XElement("Player", playerData),
                            new XElement("Enemies", enemies)));

                DateTime helper = DateTime.Now;

                string dir = Directory.GetCurrentDirectory();
                List<string> h = dir.Split('\\').ToList();
                dir = string.Empty;

                for (int i = 0; i < h.Count - 4; i++)
                {
                    dir += h[i] + "\\";
                }

                dir += "GameRepository";

                string mentes_string = name + " " + helper.Year + "-" + helper.Month + "-" + helper.Day;
                doc.Save(dir + "\\SaveData\\" + mentes_string + ".xml");
            }
        }

        /// <summary>
        /// Used for loading a game state.
        /// </summary>
        /// <param name="name">The loadable file's name.</param>
        /// <param name="model">The model in which the game should be loaded.</param>
        public static void LoadGame(string name, IGameModel model)
        {
            string dir = Directory.GetCurrentDirectory();
            List<string> h = dir.Split('\\').ToList();
            dir = string.Empty;

            for (int i = 0; i < h.Count - 4; i++)
            {
                dir += h[i] + "\\";
            }

            dir += "GameRepository";

            XDocument doc = XDocument.Load(dir + "\\SaveData\\" + name + ".xml");

            doc.Descendants("Player").ToList().ForEach(i =>
            {
                model.PlayerShip.Points = int.Parse(i.Element("Point").Value);
                model.PlayerShip.Kills = int.Parse(i.Element("Kills").Value);
                model.PlayerShip.DX = int.Parse(i.Element("Dx").Value);
                model.PlayerShip.DY = int.Parse(i.Element("Dy").Value);
                model.PlayerShip.Health = int.Parse(i.Element("Health").Value);
                model.PlayerShip.Shield = int.Parse(i.Element("Shield").Value);
            });

            doc.Descendants("Enemy").ToList().ForEach(i =>
            {
                int cx = int.Parse(i.Element("Cx").Value);
                int cy = int.Parse(i.Element("Cy").Value);
                int sizew = int.Parse(i.Element("Sizew").Value);
                int sizeh = int.Parse(i.Element("Sizeh").Value);
                model.EnemyShips.Add(new EnemyShipItem(cx, cy, sizew, sizeh)
                {
                    Health = int.Parse(i.Element("Health").Value),
                    Shield = int.Parse(i.Element("Shield").Value),
                });
            });
        }
    }
}
