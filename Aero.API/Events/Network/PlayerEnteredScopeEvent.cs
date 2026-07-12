// ====================================
// <copyright file="PlayerEnteredScopeEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Network;

/// <summary>
/// Represents an event that occurs when a player enters the scope of another player in the network.
/// </summary>
public readonly struct PlayerEnteredScopeEvent : IEvent
{
    public int PlayerId { get; }
    public int TargetPlayerId { get; }

    public PlayerEnteredScopeEvent(int playerId, int targetPlayerId)
    {
        PlayerId = playerId;
        TargetPlayerId = targetPlayerId;
    }
}