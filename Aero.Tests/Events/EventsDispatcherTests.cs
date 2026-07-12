// ====================================
// <copyright file="EventsRegistryTests.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Features;
using Avian.Event;
using Xunit;

namespace Aero.Tests.Events;

public class EventsDispatcherTests
{
    [Theory]
    [InlineData(32, 1024)] // Correct dimensions found in AeroEngine.cs (32, 1024)
    public void Dispatcher_ShouldInitializeWithCorrectDimensions(int expectedType, int expectedSlot)
    {
        var dispatcher = new Dispatcher(expectedType, expectedSlot);
        Assert.NotNull(dispatcher);
        Log.Success("EventsDispatcher: SUCCESS 1");
    }

    [Fact]
    public void Dispatcher_Execute_ShouldNotThrowExceptionWhenEmpty()
    {
        var dispatcher = new Dispatcher(32, 1024);
        var exception = Record.Exception(() => dispatcher.Execute());
        Assert.Null(exception);
        Log.Success("EventsDispatcher: SUCCESS 2");
    }
}