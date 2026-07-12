// ====================================
// <copyright file="ClearPedTasksEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian;

namespace Aero.API.Events.Security;

/// <summary>
/// Represents an event to clear the tasks of a pedestrian (ped) within a game environment.
/// This event is typically dispatched by a sender to target a specific pedestrian, instructing
/// them to cease all currently executing tasks or actions.
/// </summary>
public readonly struct ClearPedTasksEvent : IEvent
{
    public int SenderId { get; }
    public int TargetPedId { get; }
    
    public ClearPedTasksEvent(int senderId, int targetPedId)
    {
        SenderId = senderId;
        TargetPedId = targetPedId;
    }
}