// ====================================
// <copyright file="PlayerLeftScopeEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Network;

/// <summary>
/// Represents an event that is triggered when a player leaves the scope of another player in the networked environment.
/// </summary>
public readonly struct PlayerLeftScopeEvent : IEvent
{
    public int PlayerId { get; }
    public int TargetPlayerId { get; }
    
    public PlayerLeftScopeEvent(int playerId, int targetPlayerId)
    {
        PlayerId = playerId;
        TargetPlayerId = targetPlayerId;
    }
}