// ====================================
// <copyright file="IEvent.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;

namespace Aero.API.Interfaces;

/// <summary>
/// Represents an event that can be registered and unregistered within the system.
/// </summary>
[Obsolete("Use the new [AeroEvent] attribute system instead. This interface will be removed in future versions.")]
public interface IEvent
{
    void Register();
    void Unregister();
}