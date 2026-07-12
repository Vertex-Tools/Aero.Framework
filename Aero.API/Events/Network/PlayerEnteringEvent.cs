// ====================================
// <copyright file="PlayerEnteringEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Network
{
    /// <summary>
    /// Represents an event triggered when a player is entering the network.
    /// </summary>
    public readonly struct PlayerEnteringEvent : IEvent
    {
        public int PlayerId { get; }
        public string PlayerName { get; }

        public PlayerEnteringEvent(int playerId, string playerName)
        {
            PlayerId = playerId;
            PlayerName = playerName;
        }
    }
}