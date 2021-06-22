// <copyright file="GlobalSuppressions.cs" company="V8K90F_WN1M1P">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("", "CA1014", Justification = "<NikGitStats>", Scope = "module")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "I had to use int.parse.", Scope = "member", Target = "~M:BeastsOfTheVoid.MainMenu.Button_Click(System.Object,System.Windows.RoutedEventArgs)")]
