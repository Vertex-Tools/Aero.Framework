// ====================================
// <copyright file="DamageTierType.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;

namespace Aero.API.Enums;

/// <summary>
/// Represents different levels of damage severity that can be applied in a given context.
/// This enumeration is marked with the Flags attribute, allowing for bitwise combination of its member values.
/// </summary>
[Flags]
public enum DamageTierType
{
    None = 0,
    Light = 1 << 0,
    Medium = 1 << 1,
    Heavy = 1 << 2,
    Fatal = 1 << 3
}