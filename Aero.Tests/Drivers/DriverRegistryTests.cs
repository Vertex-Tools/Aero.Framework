// ====================================
// <copyright file="DriverRegistryTests.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Features;
using Aero.Drivers.Registry;
using Xunit;

namespace Aero.Tests.Drivers;

public class DriverRegistryTests
{
    [Fact]
    public void Singleton_ShouldInitializeAndClearCorrectly()
    {
        var registry = new DriverRegistry();
        Assert.NotNull(DriverRegistry.Singleton);
        registry.ShutdownAll();
        Assert.Null(DriverRegistry.Singleton);
        Log.Success("DriverRegistry: SUCCESS 1");
    }
    
    [Theory] 
    [InlineData(typeof(DriverRegistry))] 
    public void Singleton_ShouldMatchExpectedRegistryType(Type expectedType)
    {
        var registry = new DriverRegistry();
        Assert.IsType(expectedType, DriverRegistry.Singleton);
        registry.ShutdownAll(); // Clears the singleton.
        Log.Success("DriverRegistry: SUCCESS 2");
    }
}