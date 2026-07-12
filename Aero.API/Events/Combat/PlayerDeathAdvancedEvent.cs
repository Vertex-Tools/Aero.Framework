// ====================================
// <copyright file="PlayerDeathAdvancedEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Enums;
using Avian;

namespace Aero.API.Events.Combat;

/// <summary>
/// Represents an event that occurs when a player dies in a combat situation.
/// This event provides detailed information about the circumstances of the player's death, including the victim and attacker identifiers, the weapon used, whether it was a headshot, and the damage tier.
/// </summary>
public readonly struct PlayerDeathAdvancedEvent : IEvent
{
    public int VictimId { get; }
    public int AttackerId { get; }
    public uint WeaponHash { get; }
    public bool IsHeadShot { get; }
    public DamageTierType DamageTier { get; }

    public PlayerDeathAdvancedEvent(int victimId, int attackerId, uint weaponHash, bool isHeadshot, DamageTierType damageTier)
    {
        VictimId = victimId;
        AttackerId = attackerId;
        WeaponHash = weaponHash;
        IsHeadShot = isHeadshot;
        DamageTier = damageTier;
    }
}