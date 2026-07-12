// ====================================
// <copyright file="WeaponDamageEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Enums;
using Avian;

namespace Aero.API.Events.Combat;

/// <summary>
/// Represents an event that is triggered when a weapon inflicts damage in a combat scenario.
/// </summary>
public readonly struct WeaponDamageEvent : IEvent
{
    public int AttackerId { get; } 
    public uint TargetEntityId { get; } 
    public uint WeaponHash { get; }
    public DamageType DamageType { get; }
    public bool IsVehicleWeapon { get; }

    public WeaponDamageEvent(int attackerId, uint targetEntityId, uint weaponHash, DamageType damageType, bool isVehicleWeapon)
    {
        AttackerId = attackerId;
        TargetEntityId = targetEntityId; 
        WeaponHash = weaponHash; 
        DamageType = damageType; 
        IsVehicleWeapon = isVehicleWeapon; 
    }
}