// ====================================
// <copyright file="EntityCreatedEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Enums;
using Avian;

namespace Aero.API.Events.Network;

/// <summary>
/// Represents an event triggered when a new entity is created within the system.
/// </summary>
public readonly struct EntityCreatedEvent : IEvent
{
    public int EntityId { get; }
    public EntityType EntityType { get; }

    public EntityCreatedEvent(int entityId, EntityType entityType)
    {
        EntityId = entityId;
        EntityType = entityType;
    }
}