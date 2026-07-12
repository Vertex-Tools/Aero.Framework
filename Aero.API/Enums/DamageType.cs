// ====================================
// <copyright file="DamageType.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;

namespace Aero.API.Enums;

/// <summary>
/// Represents different types of damage that can be inflicted in the system.
/// Each value corresponds to a specific damage source and can be combined using bitwise operations due to the Flags attribute.
/// </summary>
[Flags]
public enum DamageType
{ 
    None = 0,  // _, Default
    Bullet = 1 << 0,
    Melee = 1 << 1,
    Explosive = 1 << 2,
    Fire = 1 << 3,
    Fall = 1 << 4,
    Electric = 1 << 5,
    VehicleUpset = 1 << 6    
}