// ====================================
// <copyright file="IDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Interfaces;

/// <summary>
/// Represents a driver interface within the Aero framework. Implementations of this interface
/// are expected to provide the necessary functionality to control and operate a driver component.
/// </summary>
public interface IDriver
{
    /// <summary>
    /// Initializes the driver component. This method sets up the necessary configurations and
    /// prepares the driver for operation. It is expected to be called before any other operations
    /// are performed on the driver.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Shuts down the driver component. This method performs any necessary cleanup and terminates
    /// the driver's operations. It is intended to be called when the driver is no longer needed
    /// to free up resources and ensure a clean shutdown of the component.
    /// </summary>
    void Shutdown();
}