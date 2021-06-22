// <copyright file="Renderer.cs" company="V8K90F">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRenderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using GameModel;

    /// <summary>
    /// This will render the game objects on the screen.
    /// </summary>
    public class Renderer
    {
        private readonly IGameModel model;
        private string bonusType = string.Empty;
        private FormattedText formattedText;
        private FormattedText bonusText;
        private FormattedText portalText;
        private Typeface font = new Typeface("Arial");
        private Point textlocation = new Point(10, 10);
        private Drawing oldBackground;
        private Drawing oldPlayer;
        private Drawing station;
        private Drawing oldHUD;
        private Drawing oldmap;
        private Drawing oldPlayerMap;
        private Drawing exp;
        private int oldhealth;
        private int oldshield;
        private double oldPlayerRad;
        private RotateTransform playerTrans;
        private GeometryGroup pl;
        private Drawing oldPortal;
        private Geometry starmanGeometry;
        private int starmanRot;
        private SolidColorBrush brushHud;
        private Pen redPen;
        private Point centerPoint;
        private Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();

        private Drawing starman;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="model">The model which is needed to make the renderer.</param>
        public Renderer(IGameModel model)
        {
            if (model != null)
            {
                this.model = model;
                this.playerTrans = new RotateTransform();
                this.pl = new GeometryGroup();
                Point p1 = new Point(5, 0);
                Point p2 = new Point(-5, -5);
                Point p3 = new Point(-5, 5);
                this.pl.Children.Add(new LineGeometry(p1, p2));
                this.pl.Children.Add(new LineGeometry(p1, p3));
                this.oldhealth = 0;
                this.oldshield = 0;

                this.brushHud = new SolidColorBrush();
                this.brushHud.Color = Color.FromRgb(89, 60, 31);
                this.brushHud.Opacity = 0.8;

                this.redPen = new Pen(Brushes.Red, 2);

                this.centerPoint = new Point(model.GameWidth - 120, model.GameHeight - 120);
            }
        }

        private Brush PlayerBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.prog_normandy_outline.png");
            }
        }

        private Brush MeteorBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.meteor.png");
            }
        }

        private Brush NebulaBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.Nebula.png");
            }
        }

        private Brush StationBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.station2.png");
            }
        }

        private Brush BackgroundBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.backgroundmeteors.png");
            }
        }

        private Brush DoublePointsBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.doublepoints.png");
            }
        }

        private Brush InstaKillBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.instakill.png");
            }
        }

        private Brush RoadsterBrush
        {
            get
            {
                return this.GetBrush("GameRenderer.Images.starman_easter_egg.png");
            }
        }

        /// <summary>
        /// This will collect everything for the game to be rendered.
        /// </summary>
        /// <param name="sa">SpriteAnimation object that is used for animation.</param>
        /// <returns>The collection of drawings that will be rendered.</returns>
        public Drawing BuildDrawing(SpriteAnimation sa)
        {
            DrawingGroup dg = new DrawingGroup();
            TranslateTransform tr = new TranslateTransform(this.model.PlayerShip.DX, this.model.PlayerShip.DY);
            dg.Children.Add(this.GetBackground());

            dg.Children.Add(this.GetPlayerMove(tr));

            if (this.GetStation(tr) != null)
            {
                dg.Children.Add(this.station);
            }

            if (sa != null && this.GetPortal(sa, tr) != null)
            {
                dg.Children.Add(this.oldPortal);
            }

            dg.Children.Add(this.GetPlayerRotate());
            if (this.GetStarman(tr) != null)
            {
                dg.Children.Add(this.starman);
            }

            foreach (var item in this.model.Bullets)
            {
                GeometryDrawing bulletDrawing = new GeometryDrawing(Brushes.DarkRed, this.redPen, item.BulletGeo());
                dg.Children.Add(bulletDrawing);
            }

            foreach (var item in this.model.EnemyShips)
            {
                if (Math.Abs(item.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(item.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
                {
                    double x = item.CX - this.model.PlayerShip.CX + this.model.PlayerShip.DX;
                    double y = item.CY - this.model.PlayerShip.CY + this.model.PlayerShip.DY;
                    double degree = Math.Atan2(y, x) * 180 / Math.PI;

                    RotateTransform t = new RotateTransform(degree + 90, item.CX + this.model.PlayerShip.DX, item.CY + this.model.PlayerShip.DY);
                    item.RealArea.Transform = tr;

                    item.ImageBrush.Transform = t;
                    GeometryDrawing enemyship = new GeometryDrawing(item.ImageBrush, null, item.RealArea);
                    dg.Children.Add(enemyship);
                }
            }

            foreach (var item in this.model.EnemyBullets)
            {
                GeometryDrawing bulletDrawing = new GeometryDrawing(Brushes.Green, this.redPen, item.BulletGeo());
                dg.Children.Add(bulletDrawing);
            }

            foreach (var item in this.model.Meteors)
            {
                if (Math.Abs(item.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(item.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
                {
                    item.RealArea.Transform = tr;
                    GeometryDrawing meteor = new GeometryDrawing(this.MeteorBrush, null, item.RealArea);
                    dg.Children.Add(meteor);
                }
            }

            foreach (var item in this.model.GasClouds)
            {
                if (Math.Abs(item.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(item.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
                {
                    Brush opacityHelper = this.NebulaBrush;
                    opacityHelper.Opacity = 0.7;
                    item.RealArea.Transform = tr;
                    GeometryDrawing smoke = new GeometryDrawing(opacityHelper, null, item.RealArea);
                    dg.Children.Add(smoke);
                }
            }

            if (this.model.Bonus != null && Math.Abs(this.model.Bonus.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(this.model.Bonus.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
            {
                this.model.Bonus.RealArea.Transform = tr;
                if (this.model.Bonus.BonusType == "DoubleP")
                {
                    GeometryDrawing bonus = new GeometryDrawing(this.DoublePointsBrush, null, this.model.Bonus.RealArea);
                    dg.Children.Add(bonus);
                    this.bonusType = this.model.Bonus.BonusType;
                }
                else
                {
                    GeometryDrawing bonus = new GeometryDrawing(this.InstaKillBrush, null, this.model.Bonus.RealArea);
                    dg.Children.Add(bonus);
                    this.bonusType = this.model.Bonus.BonusType;
                }

                if (this.model.Bonus.Time == 0)
                {
                    this.model.Bonus = null;
                }
            }

            dg.Children.Add(this.GetHUD());
            dg.Children.Add(this.GetMap());

            return dg;
        }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        /// <param name="animation">The animation class, which handles the animations.</param>
        /// <returns>Returns the current frame.</returns>
        public Drawing BuildAnimation(SpriteAnimation animation)
        {
            if (animation != null && animation.Timer.IsEnabled)
            {
                TranslateTransform tr = new TranslateTransform(this.model.PlayerShip.DX, this.model.PlayerShip.DY);
                this.model.ExpEnemy.RealArea.Transform = tr;
                this.exp = new GeometryDrawing(animation.Brush, null, this.model.ExpEnemy.RealArea);
            }
            else
            {
                this.exp = null;
            }

            if (this.exp != null)
            {
                return this.exp;
            }

            return null;
        }

        /// <summary>
        /// Writes the point and the kill count of the player on the screen.
        /// </summary>
        /// <param name="ctx">The drawign context that will be used to display the text.</param>
        public void DrawText(DrawingContext ctx)
        {
            this.formattedText = new FormattedText("Point: " + this.model.PlayerShip.Points.ToString() + "        Kills: " + this.model.PlayerShip.Kills.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.Red, 0);

            if (ctx != null)
            {
                ctx.DrawText(this.formattedText, this.textlocation);
            }
        }

        /// <summary>
        /// Displays how much time the player has before his perk is removed.
        /// </summary>
        /// <param name="ctx">The drawign context that will be used to display the text.</param>
        public void DrawBonusTime(DrawingContext ctx)
        {
            if (this.model.IsBonusPicked)
            {
                if (this.model.PlayerShip.Bonus.BonusType == "DoubleP")
                {
                    this.bonusText = new FormattedText("Double points: " + (this.model.BonusTime / 1000).ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.Red, 0);
                }
                else
                {
                    this.bonusText = new FormattedText("Insta kill+ " + (this.model.BonusTime / 1000).ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.Red, 0);
                }

                if (ctx != null)
                {
                    ctx.DrawText(this.bonusText, new Point(this.model.PlayerShip.CX, this.model.PlayerShip.CY + 100));
                }
            }

            if (this.model.Portal.IsCollision(this.model.PlayerShip))
            {
                this.portalText = new FormattedText("PRESS [F] TO MOVE TO THE NEXT LEVEL", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.Red, 0);
                if (ctx != null)
                {
                    ctx.DrawText(this.portalText, new Point((this.model.GameWidth / 2) - 200, this.model.GameHeight - 50));
                }
            }
        }

        /// <summary>
        /// Creates the required image if its not already created.
        /// </summary>
        /// <param name="fname">The name of the image that we want to load into the game.</param>
        /// <returns>An ImageBrush that can be used for representing images.</returns>
        private Brush GetBrush(string fname)
        {
            if (!this.brushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();

                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);

                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.brushes.Add(fname, ib);
            }

            return this.brushes[fname];
        }

        private Drawing GetPlayerMove(TranslateTransform tr)
        {
            if (this.model.PlayerShip.DY < 2160 && this.model.PlayerShip.DX < 3840 && this.model.PlayerShip.DY > -2160 && this.model.PlayerShip.DX > -3840)
            {
                ((GeometryDrawing)this.oldBackground).Geometry.Transform = tr;
            }

            return this.oldBackground;
        }

        private Drawing GetPlayerRotate()
        {
            ImageBrush ib = (ImageBrush)this.PlayerBrush;
            if (this.oldPlayer == null || this.oldPlayerRad != this.model.PlayerShip.Rad)
            {
                this.playerTrans.Angle = this.model.PlayerShip.Rad + 90;
                this.playerTrans.CenterX = this.model.GameWidth / 2;
                this.playerTrans.CenterY = this.model.GameHeight / 2;

                ib.Transform = this.playerTrans;

                this.oldPlayerRad = this.model.PlayerShip.Rad;
            }

            if (this.model.PlayerShip.Shield > 0)
            {
                this.oldPlayer = new GeometryDrawing(ib, new Pen(Brushes.Blue, 2), this.model.PlayerShip.RealArea);
            }
            else
            {
                this.oldPlayer = new GeometryDrawing(ib, null, this.model.PlayerShip.RealArea);
            }

            return this.oldPlayer;
        }

        private Drawing GetBackground()
        {
            if (this.oldBackground == null)
            {
                Geometry g = new RectangleGeometry(new Rect(-3840 + (this.model.GameWidth / 2), -2160 + (this.model.GameHeight / 2), 7680, 4320));
                this.oldBackground = new GeometryDrawing(this.BackgroundBrush, null, g);
            }

            return this.oldBackground;
        }

        private Drawing GetStation(TranslateTransform tr)
        {
            if (Math.Abs(this.model.Station.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(this.model.Station.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
            {
                this.model.Station.RealArea.Transform = tr;
                this.station = new GeometryDrawing(this.StationBrush, null, this.model.Station.RealArea);
                return this.station;
            }

            return null;
        }

        private Drawing GetPortal(SpriteAnimation sa, TranslateTransform tr)
        {
            if (Math.Abs(this.model.Portal.CX - this.model.PlayerShip.PlayerPosX) < 900 && Math.Abs(this.model.Portal.CY - this.model.PlayerShip.PlayerPosY) < 500)
            {
                sa.Timer.Start();
                this.model.Portal.RealArea.Transform = tr;
                this.oldPortal = new GeometryDrawing(sa.Brush, null, this.model.Portal.RealArea);
                return this.oldPortal;
            }

            return null;
        }

        private Drawing GetStarman(TranslateTransform tr)
        {
            ImageBrush ib = (ImageBrush)this.RoadsterBrush;
            if (this.starman == null)
            {
                this.starmanGeometry = new EllipseGeometry(new Point(-2637, -1413), 100, 100);
                this.starman = new GeometryDrawing(ib, null, this.starmanGeometry);
            }

            if (Math.Abs(-2637 - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth && Math.Abs(-1413 - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight)
            {
                if (this.starmanRot == 360)
                {
                    this.starmanRot = 1;
                }
                else
                {
                    this.starmanRot += 2;
                }

                this.starmanGeometry.Transform = tr;

                RotateTransform rt = new RotateTransform(this.starmanRot, (-2637) + this.model.PlayerShip.DX, (-1413) + this.model.PlayerShip.DY);
                ib.Transform = rt;
                this.starman = new GeometryDrawing(ib, null, this.starmanGeometry);
            }
            else
            {
                this.starman = null;
            }

            return this.starman;
        }

        private DrawingGroup GetHUD()
        {
            DrawingGroup g = new DrawingGroup();
            if (this.oldHUD == null)
            {
                Geometry hud = new RectangleGeometry(new Rect(10, this.model.GameHeight - 110, 300, 50), 10, 10);
                this.oldHUD = new GeometryDrawing(this.brushHud, null, hud);
            }

            g.Children.Add(this.oldHUD);
            if (this.oldhealth != this.model.PlayerShip.Health || this.oldshield != this.model.PlayerShip.Shield)
            {
                Geometry healthBar = new RectangleGeometry(new Rect(60, this.model.GameHeight - 100, 150 * this.model.PlayerShip.Health / 100, 10));
                Geometry shieldBar = new RectangleGeometry(new Rect(60, this.model.GameHeight - 80, 150 * this.model.PlayerShip.Shield / 100, 10));

                g.Children.Add(new GeometryDrawing(Brushes.Red, null, healthBar));
                g.Children.Add(new GeometryDrawing(Brushes.Blue, null, shieldBar));
            }

            return g;
        }

        private DrawingGroup GetMap()
        {
            DrawingGroup g = new DrawingGroup();
            if (this.oldmap == null)
            {
                Geometry map = new EllipseGeometry(this.centerPoint, 100, 100);
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromRgb(220, 220, 220);
                brush.Opacity = 0.6;
                this.oldmap = new GeometryDrawing(brush, null, map);
            }

            TransformGroup group = new TransformGroup();
            group.Children.Add(new TranslateTransform(this.model.GameWidth - 120, this.model.GameHeight - 120));
            group.Children.Add(new RotateTransform(this.model.PlayerShip.Rad, this.model.GameWidth - 120, this.model.GameHeight - 120));
            this.pl.Transform = group;

            Geometry player = this.pl.GetWidenedPathGeometry(new Pen(Brushes.White, 1));
            this.oldPlayerMap = new GeometryDrawing(Brushes.White, null, player);

            g.Children.Add(this.oldmap);
            g.Children.Add(this.oldPlayerMap);

            foreach (var item in this.model.EnemyShips)
            {
                if (Math.Abs(item.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth - 420 && Math.Abs(item.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameHeight - 420)
                {
                    Geometry enemymappoint = new EllipseGeometry(this.centerPoint, 2, 2);
                    TranslateTransform gk = new TranslateTransform((item.CX - this.model.PlayerShip.PlayerPosX) / 20, (item.CY - this.model.PlayerShip.PlayerPosY) / 20);
                    enemymappoint.Transform = gk;
                    GeometryDrawing enemyMap = new GeometryDrawing(Brushes.Red, this.redPen, enemymappoint);

                    g.Children.Add(enemyMap);
                }
            }

            if (Math.Abs(this.model.Station.CX - this.model.PlayerShip.PlayerPosX) < this.model.GameWidth - 420 && Math.Abs(this.model.Station.CY - this.model.PlayerShip.PlayerPosY) < this.model.GameWidth - 420)
            {
                Geometry station = new EllipseGeometry(this.centerPoint, 2, 2);
                TranslateTransform gk = new TranslateTransform((this.model.Station.CX - this.model.PlayerShip.PlayerPosX) / 20, (this.model.Station.CY - this.model.PlayerShip.PlayerPosY) / 20);
                station.Transform = gk;
                GeometryDrawing stationdrawing = new GeometryDrawing(Brushes.Green, new Pen(Brushes.Green, 2), station);
                g.Children.Add(stationdrawing);
            }

            return g;
        }
    }
}
