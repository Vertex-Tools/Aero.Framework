// ====================================
// <copyright file="IObjectDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using CitizenFX.Core;

namespace Aero.API.Interfaces;

/// <summary>
/// Represents a driver interface for managing and interacting with objects within the system.
/// </summary>
public interface IObjectDriver : IDriver
{
    int CreateNetworkObject(uint modelHash, Vector3 position, Vector3 rotation);
    bool DeleteObject(int entityHandle);
    bool DoesObjectExist(int entityHandle);
}