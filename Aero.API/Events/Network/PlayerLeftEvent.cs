// ====================================
// <copyright file="PlayerLeftEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Network
{
    public readonly struct PlayerLeftEvent : IEvent
    {
        public int PlayerId { get; }
        public string PlayerName { get; }
        public string Reason { get; }

        public PlayerLeftEvent(int playerId, string playerName, string reason)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            Reason = reason;
        }
    }
}