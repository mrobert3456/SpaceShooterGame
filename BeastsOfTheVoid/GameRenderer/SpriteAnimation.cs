// <copyright file="SpriteAnimation.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRenderer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    /// <summary>
    /// Handles any animation.
    /// </summary>
    public class SpriteAnimation
    {
        private ImageBrush currentFrame;
        private List<ImageSource> spriteFrames;
        private int numFrames;
        private int frameWidth;
        private int frameHeight;

        private DispatcherTimer animTimer;
        private int frameDuration = 10;
        private int animIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimation"/> class.
        /// </summary>
        /// <param name="frames">Number of frames in the animation.</param>
        /// <param name="frameWidth">Width of each frame in pixels.</param>
        /// <param name="frameHeight">Height of each frame in pixels.</param>
        /// <param name="directoryName">The directory, which contains all the frame picture.</param>
        public SpriteAnimation(int frames, int frameWidth, int frameHeight, string directoryName)
        {
            this.numFrames = frames;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            this.spriteFrames = GetFrames(directoryName);

            this.currentFrame = new ImageBrush(this.spriteFrames[0]);

            this.animTimer = new DispatcherTimer();
            this.animTimer.Tick += (sender, e) => this.NextFrame();
        }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public ImageBrush Brush
        {
            get { return this.currentFrame; }
        }

        /// <summary>
        /// Gets the Timer.
        /// </summary>
        public DispatcherTimer Timer
        {
            get { return this.animTimer; }
        }

        /// <summary>
        /// Gets the frames list.
        /// </summary>
        public List<ImageSource> Frames
        {
            get { return this.spriteFrames; }
        }

        /// <summary>
        /// Gets or Sets the each frame's duration.
        /// </summary>
        public int FrameDuration
        {
            get { return this.frameDuration; }
            set { this.frameDuration = value; }
        }

        /// <summary>
        /// Gets or Sets the index of the current frame.
        /// </summary>
        public int AnimIndex
        {
            get
            {
                return this.animIndex;
            }

            set
            {
                if (this.animIndex == this.numFrames - 2)
                {
                    this.currentFrame.ImageSource = this.Frames[this.animIndex];
                }

                this.animIndex = value;
            }
        }

        /// <summary>
        /// Manually set the frame index. This will not stop the running animation nor change the animation index.
        /// </summary>
        /// <param name="index">Frame index.</param>
        public void SetFrame(int index)
        {
            this.currentFrame.ImageSource = this.spriteFrames[index];
        }

        /// <summary>
        /// Start sprite animation.
        /// </summary>
        public void StartAnimation()
        {
            this.animTimer.Interval = TimeSpan.FromMilliseconds(this.frameDuration);
            this.animTimer.Start();
        }

        /// <summary>
        /// Increase animation index by one or reset to zero if at end of list.
        /// </summary>
        public void NextFrame()
        {
            if (this.AnimIndex < this.numFrames - 1)
            {
                this.AnimIndex++;
                this.currentFrame.ImageSource = this.spriteFrames[this.AnimIndex];
            }
            else
            {
                this.AnimIndex = 0;
                this.currentFrame.ImageSource = this.Frames[0];
                this.animTimer.Stop();
            }
        }

        /// <summary>
        /// Gets the frames from the specified directory.
        /// </summary>
        /// <param name="directoryName">Directory name.</param>
        /// <returns>Returns a ImageSource list of frames.</returns>
        private static List<ImageSource> GetFrames(string directoryName)
        {
            List<ImageSource> frames = new List<ImageSource>();
            string[] files = Directory.GetFiles(directoryName);

            for (int frameCount = 0; frameCount < files.Length; frameCount++)
            {
                string file = files[frameCount];
                frames.Add(new BitmapImage(new Uri(file)));
            }

            return frames;
        }
    }
}
