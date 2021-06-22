// <copyright file="RenderFactory.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRenderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModel;

    /// <summary>
    /// Creates render instances.
    /// </summary>
    public static class RenderFactory
    {
        /// <summary>
        /// Creates a Renderer.
        /// </summary>
        /// <param name="model">IGameModel implementation.</param>
        /// <returns>Returns a Renderer.</returns>
        public static Renderer CreateRender(IGameModel model)
        {
            return new Renderer(model);
        }

        /// <summary>
        /// Creates a SpriteAnimation.
        /// </summary>
        /// <param name="frames">Number of frames in the animation.</param>
        /// <param name="frameWidth">Width of each frame in pixels.</param>
        /// <param name="frameHeight">Height of each frame in pixels.</param>
        /// <param name="directoryName">The directory, which contains all the frame picture.</param>
        /// <returns>Returns a SpriteAnimation.</returns>
        public static SpriteAnimation CreateAnimation(int frames, int frameWidth, int frameHeight, string directoryName)
        {
            return new SpriteAnimation(frames, frameWidth, frameHeight, directoryName);
        }
    }
}
