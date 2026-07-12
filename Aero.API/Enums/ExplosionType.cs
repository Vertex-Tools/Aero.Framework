// ====================================
// <copyright file="ExplosionEnum.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Enums;

public enum ExplosionEnum
{
    None = 0, // _, Default
    Grenade = 1 << 0,
    Molotov = 1 << 1,
    Car = 1 << 2,
    Plane = 1 << 3,
    StickyBomb = 1 << 4,
    TankShell = 1 << 5,
    Propane = 1 << 6,
    NoDamage = 1 << 10,
    SuppressAudio = 1 << 11,
    SuppressVisuals = 1 << 12
}