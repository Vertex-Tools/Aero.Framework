// ====================================
// <copyright file="IVehicleDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System.Threading.Tasks;
using CitizenFX.Core;

namespace Aero.API.Interfaces;

/// <summary>
/// Represents the contract for a vehicle driver, providing essential actions and properties
/// that a driver must implement to operate various types of vehicles within the system.
/// </summary>
public interface IVehicleDriver : IDriver
{
    Task<int> SpawnVehicleAsync(uint modelHash, Vector3 position, float heading);
    void DeleteVehicle(int vehicleHandle);
    void SetVehicleProperties(int vehicleHandle, dynamic properties);
}