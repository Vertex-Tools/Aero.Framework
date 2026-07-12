// ====================================
// <copyright file="DriverRegistry.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Collections.Generic;
using Aero.API.Interfaces;
using Aero.Drivers.Implementations;

namespace Aero.Drivers.Registry
{
    /// <summary>
    /// Represents a registry for drivers within the Aero framework.
    /// This class is responsible for managing the registration, access, and organization of various driver instances.
    /// It provides mechanisms to register new drivers and retrieve them based on specific criteria.
    /// </summary>
    public class DriverRegistry
    {
        private readonly Dictionary<Type, IDriver> _drivers = new(); // Dictionary to store registered drivers.
        public static DriverRegistry Singleton { get; private set; } // Singleton instance of the DriverRegistry.
        
        public DriverRegistry()
        {
            Singleton = this;
            
            _drivers.Add(typeof(IVehicleDriver), new VehicleDriver());
            _drivers.Add(typeof(IDataBaseDriver), new DataBaseDriver());
            _drivers.Add(typeof(IPlayerDriver), new PlayerDriver());
            _drivers.Add(typeof(IObjectDriver), new ObjectDriver());
        }

        public T Get<T>() where T : IDriver
        {
            if (_drivers.TryGetValue(typeof(T), out var driver))
                return (T)driver;
            throw new Exception($"[Aero - Drivers] -> Driver {typeof(T).FullName} not found.");
        }

        public void InitializeAll()
        {
            foreach (var driver in _drivers.Values)
                driver.Initialize();    
        }
        
        public void ShutdownAll()
        {
            foreach (var driver in _drivers.Values)
                driver.Shutdown();
            Singleton = null; // prevents memory leaks.
        }
    }
}