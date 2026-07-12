// ====================================
// <copyright file="PlayerJoiningEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Network;

/// <summary>
/// Represents an event that occurs when a player is joining the server.
/// This event provides properties to access the player, potential kick reason, and any deferrals during the joining process.
/// </summary>
public readonly struct PlayerJoiningEvent : IEvent
{
    public Aero.API.Features.Player Player { get; }
    public dynamic KickReason { get; }
    public dynamic Deferrals { get; }

    public PlayerJoiningEvent(Aero.API.Features.Player player, dynamic kickReason, dynamic deferrals)
    {
        Player = player;
        KickReason = kickReason;
        Deferrals = deferrals; 
    }
}
