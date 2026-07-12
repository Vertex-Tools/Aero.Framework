// ====================================
// <copyright file="IEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Interfaces;

/// <summary>
/// Represents an event that can be registered and unregistered within the system.
/// </summary>
public interface IEvent
{
    void Register();
    void Unregister();
}