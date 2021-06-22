// <copyright file="GlobalSuppressions.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("", "CA1014", Justification = "<NikGitStats>", Scope = "module")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to convert a string into a double.", Scope = "member", Target = "~M:GameModel.Model.CreateEnemies(System.Int32)")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to convert a string into a double.", Scope = "member", Target = "~M:GameModel.Model.CreateGasClouds(System.Int32)")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "I have to change the list somewhere else.", Scope = "member", Target = "~P:GameModel.Model.EnemyShips")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "I have to change the list somewhere else.", Scope = "member", Target = "~P:GameModel.Model.DeletableBullets")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I have to convert a string into a double.", Scope = "member", Target = "~M:GameModel.Model.CreateMeteors(System.Int32)")]
[assembly: SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Random next is perfectly fine for us.", Scope = "member", Target = "~M:GameModel.GasCloudItem.#ctor(System.Double,System.Double)")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We have to use it in the descendant calsses.", Scope = "member", Target = "~F:GameModel.GameItem.area")]
[assembly: SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "It has to be used in the descendant classes.", Scope = "member", Target = "~F:GameModel.GameItem.area")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "It has to be writeable for the level loading.", Scope = "member", Target = "~P:GameModel.IGameModel.EnemyShips")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.Bullets")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.DeletableBullets")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.EnemyBullets")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.EnemyShips")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.GasClouds")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "It should be a generic list.", Scope = "member", Target = "~P:GameModel.IGameModel.Meteors")]