// <copyright file="GameControl.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BeastsOfTheVoid
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using GameLogic;
    using GameModel;
    using GameRenderer;

    /// <summary>
    /// This will control how the game works.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private IGameModel model;
        private IGameLogic logic;
        private IFileLogic filelogic;
        private IShootLogic shootLogic;
        private Renderer renderer;
        private SpriteAnimation boomAnimation;
        private SpriteAnimation portalAnimation;
        private DispatcherTimer dt;
        private DispatcherTimer shootT;
        private DispatcherTimer enemyShootT;
        private BitmapImage nextlevel;
        private Window win;
        private int movex;
        private int movey;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Gets or Sets how the save of the game should be named.
        /// </summary>
        public static string SaveName { get; set; }

        /// <summary>
        /// Gets or Sets what level the game should render.
        /// </summary>
        public static int Level { get; set; } = 1;

        /// <summary>
        /// Renders the game.
        /// </summary>
        /// <param name="drawingContext">A drawing context that will be rendering the game.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null && drawingContext != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing(this.portalAnimation));
                drawingContext.DrawDrawing(this.renderer.BuildAnimation(this.boomAnimation));
                this.renderer.DrawText(drawingContext);
                this.renderer.DrawBonusTime(drawingContext);
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = ModelFactory.CreateModel(this.ActualWidth, this.ActualHeight, Level);
            this.shootLogic = LogicFactory.CreateShootLogic(this.model);
            this.logic = LogicFactory.CreateMainLogic(this.model);
            this.renderer = RenderFactory.CreateRender(this.model);
            this.filelogic = LogicFactory.CreateFileLogic(this.model);

            this.nextlevel = new BitmapImage();
            this.nextlevel.BeginInit();
            this.nextlevel.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("BeastsOfTheVoid.Images.sky.jpg");
            this.nextlevel.EndInit();

            string dir = Directory.GetCurrentDirectory();
            string dir2;
            List<string> h = dir.Split('\\').ToList();
            dir = string.Empty;

            for (int i = 0; i < h.Count - 3; i++)
            {
                dir += h[i] + "\\";
            }

            dir2 = dir;

            dir += "frames";
            dir2 += "portalFrames";

            this.portalAnimation = RenderFactory.CreateAnimation(124, 144, 212, dir2);
            this.portalAnimation.Timer.Interval = TimeSpan.FromMilliseconds(50);
            this.portalAnimation.Timer.Tick += this.Timer_Tick_Portal;

            this.boomAnimation = RenderFactory.CreateAnimation(60, 256, 256, dir);
            this.boomAnimation.Timer.Interval = TimeSpan.FromMilliseconds(50);
            this.boomAnimation.Timer.Tick += this.Timer_Tick;

            this.shootT = new DispatcherTimer();
            this.shootT.Interval = TimeSpan.FromMilliseconds(10);
            this.shootT.Tick += this.ShootT_Tick;

            this.enemyShootT = new DispatcherTimer();
            this.enemyShootT.Interval = TimeSpan.FromMilliseconds(10);
            this.enemyShootT.Tick += this.EnemyShootT_Tick;

            this.win = Window.GetWindow(this);
            if (SaveName != null)
            {
                this.model.EnemyShips = new List<EnemyShipItem>();
                this.filelogic.LoadGame(SaveName, this.model);
                SaveName = null;
                if (Level == 2)
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("BeastsOfTheVoid.Images.sky.jpg");
                    bmp.EndInit();
                    this.win.Background = new ImageBrush(bmp);
                }
            }

            if (this.win != null)
            {
                this.win.KeyDown += this.Win_KeyDown;
                this.win.KeyUp += this.Win_KeyUp;
                this.win.MouseMove += this.Win_MouseMove;

                this.win.MouseDown += this.Win_MouseDown;

                this.dt = new DispatcherTimer();
                this.dt.Interval = TimeSpan.FromMilliseconds(10);
                this.dt.Tick += this.Dt_Tick;
                this.dt.Start();
                this.enemyShootT.Start();
            }

            this.InvalidateVisual();
        }

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: this.movey = 0; break;
                case Key.A: this.movex = 0; break;
                case Key.D: this.movex = 0; break;
                case Key.S: this.movey = 0; break;
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (!this.logic.GameOver)
            {
                this.logic.OneTick(this.boomAnimation.Timer);

                this.InvalidateVisual();
            }
            else
            {
                this.enemyShootT.Stop();
                this.dt.Stop();
                PauseMenu pauseMenu = WindowFactory.CreatePauseMenu(this.win, this.filelogic);
                pauseMenu.ResButton.IsEnabled = false;
                pauseMenu.SaveButton.IsEnabled = false;
                MessageBox.Show("Game over");
                pauseMenu.ShowDialog();
                this.win.Close();
            }
        }

        private void Win_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.shootLogic.Shoot();

            this.shootT.Start();

            this.InvalidateVisual();
        }

        private void Win_MouseMove(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            this.logic.SetDegree(mp.X, mp.Y);
            this.InvalidateVisual();
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: this.movey = 15; break;
                case Key.A: this.movex = 15; break;
                case Key.D: this.movex = -15; break;
                case Key.S: this.movey = -15; break;
                case Key.F:
                    {
                        IGameModel newModel = this.logic.NextLevel();
                        if (this.model != newModel)
                        {
                            this.model = newModel;
                            GameControl.Level = 2;
                            this.shootLogic = LogicFactory.CreateShootLogic(this.model);
                            this.logic = LogicFactory.CreateMainLogic(this.model);
                            this.renderer = RenderFactory.CreateRender(this.model);
                            this.filelogic = LogicFactory.CreateFileLogic(this.model);
                            this.win.Background = new ImageBrush(this.nextlevel);
                        }
                    }

                    break;
                case Key.Escape:
                    {
                        this.enemyShootT.Stop();
                        this.dt.Stop();
                        this.PauseMenu();
                    }

                    break;
            }

            this.logic.MoveShip(this.movex, this.movey);

            this.InvalidateVisual();
        }

        private void EnemyShootT_Tick(object sender, EventArgs e)
        {
            this.shootLogic.ShootOnetick();
        }

        private void ShootT_Tick(object sender, EventArgs e)
        {
            this.shootLogic.ShootTick(this.boomAnimation.Timer, this.shootT);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.boomAnimation.NextFrame();
            this.InvalidateVisual();
        }

        private void Timer_Tick_Portal(object sender, EventArgs e)
        {
            this.portalAnimation.NextFrame();
            this.InvalidateVisual();
        }

        private void PauseMenu()
        {
            PauseMenu menu = WindowFactory.CreatePauseMenu(this.win, this.filelogic);
            menu.ShowDialog();
            this.dt.Start();
            this.enemyShootT.Start();
        }
    }
}
