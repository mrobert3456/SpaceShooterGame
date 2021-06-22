// <copyright file="GlobalSuppressions.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "That is just used for saving the game.", Scope = "type", Target = "~T:GameRepository.Repository")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to use int.parse when loading the data.", Scope = "member", Target = "~M:GameRepository.Repository.LoadGame(System.String,GameModel.IGameModel)")]
[assembly: SuppressMessage("", "CA1014", Justification ="<NikGitStats>", Scope = "module")]