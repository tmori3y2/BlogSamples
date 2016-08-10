// <copyright file="GlobalSuppressions.cs" company="tmori3y2">
// Copyright (c) 2016 tmori3y2. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/08/04</date>
// <summary>Implements the global suppressions class</summary>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "Pending")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Conflict with IDE0003", Scope = "member", Target = "~M:CultureHelper.NumberFormatInfoSummary.#ctor(System.Globalization.CultureInfo)")]
