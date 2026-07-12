// ====================================
// <copyright file="StartProjectileEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;
using CitizenFX.Core;

namespace Aero.API.Events.Combat;

/// <summary>
/// Represents an event that is triggered when a projectile is initiated in the game.
/// </summary>
public readonly struct StartProjectileEvent : IEvent
{
    public int ShooterId { get; }
    public uint WeaponHash { get; }
    public Vector3 InitialPosition { get; }

    public StartProjectileEvent(int shooterId, uint weaponHash, Vector3 initialPosition)
    {
        ShooterId = shooterId;
        WeaponHash = weaponHash;
        InitialPosition = initialPosition;
    }
}