// ====================================
// <copyright file="AeroEngineTests.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Features;
using Aero.Core;
using Xunit;

namespace Aero.Tests.Core;

public class AeroEngineTests
{
    [Fact]
    public void Engine_Singleton_ShouldBeNullBeforeInitialization()
    {
        Assert.Null(AeroEngine.Singleton);
        Log.Success("AeroEngine: SUCCESS");
    }
}