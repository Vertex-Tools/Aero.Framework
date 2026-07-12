// ====================================
// <copyright file="ExplosionEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Enums;
using Avian;
using CitizenFX.Core;

namespace Aero.API.Events.Combat;

/// <summary>
/// Represents an event data structure for an explosion occurrence within the game environment.
/// Holds information about the entity that triggered the explosion, the type of explosion, and its location.
/// </summary>
public readonly struct ExplosionEvent : IEvent
{
    public int SenderId { get; }
    public ExplosionType ExplosionType { get; }
    public Vector3 Position { get; }

    public ExplosionEvent(int senderId, ExplosionType explosionType, Vector3 position)
    {
        SenderId = senderId;
        ExplosionType = explosionType;
        Position = position;
    }
}