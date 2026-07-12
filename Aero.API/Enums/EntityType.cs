// ====================================
// <copyright file="EntityType.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;

namespace Aero.API.Enums;

/// <summary>
/// Defines a set of flags representing different types of entities within the system.
/// This enumeration supports a bitwise combination of its member values.
/// </summary>
[Flags]
public enum EntityType
{
    None = 0, // _, Default
    Ped = 1 << 0,
    Vehicle = 1 << 1,
    Object = 1 << 2
}