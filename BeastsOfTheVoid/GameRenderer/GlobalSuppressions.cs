// <copyright file="GlobalSuppressions.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to use int.parse.", Scope = "member", Target = "~M:GameRenderer.Renderer.DrawText(System.Windows.Media.DrawingContext)")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to convert an int into a string.", Scope = "member", Target = "~M:GameRenderer.Renderer.DrawBonusTime(System.Windows.Media.DrawingContext)")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It has to be a generic list.", Scope = "member", Target = "~P:GameRenderer.SpriteAnimation.Frames")]
[assembly: SuppressMessage("", "CA1014", Justification ="<NikGitStats>", Scope = "module")]
