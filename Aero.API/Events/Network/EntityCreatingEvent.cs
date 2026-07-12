// ====================================
// <copyright file="EntityCreatingEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Enums;
using Avian;

namespace Aero.API.Events.Network;

/// <summary>
/// Represents an event that is triggered when a new entity is being created in the network context.
/// </summary>
public readonly struct EntityCreatingEvent : IEvent
{
    public int EntityId { get; }
    public uint ModelHash { get; }
    public EntityType EntityType { get; }
    public int OwnerId { get; }
    
    public EntityCreatingEvent(int entityId, uint modelHash, EntityType entityType, int ownerId)
    {
        EntityId = entityId;
        ModelHash = modelHash;
        EntityType = entityType;
        OwnerId = ownerId;
    }
}