// ====================================
// <copyright file="PlayerDamagedEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Combat;

/// <summary>
/// Represents an event triggered when a player takes damage in a combat scenario.
/// </summary>
public readonly struct PlayerDamagedEvent : IEvent
{
    public int PlayerId { get; }
    public int DamageAmount { get; }
    public uint WeaponHash { get; }

    public PlayerDamagedEvent(int playerId, int damageAmount, uint weaponHash)
    {
        PlayerId = playerId;
        DamageAmount = damageAmount;
        WeaponHash = weaponHash;
    }
}