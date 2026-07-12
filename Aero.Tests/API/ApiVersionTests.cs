// ====================================
// <copyright file="ApiVersionTests.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Features;
using Xunit;

namespace Aero.Tests.API;

public class ApiVersionTests
{
    [Theory]
    [InlineData("0.1.0")]
    public void CoreCurrent_Version_ShouldMatchExpected(string expectedVersion)
    {
        string currentVersion = Aero.API.Version.CoreCurrent.ToString();
        Assert.Equal(expectedVersion, currentVersion);
        Log.Success("API: SUCCESS 1");
    }

    [Fact]
    public void ApiCurrent_Version_ShouldNotBeNullOrEmpty()
    {
        Version currentVersion = Aero.API.Version.ApiCurrent;
        Assert.False(string.IsNullOrEmpty(currentVersion.ToString()));
        Log.Success("API: SUCCESS 2");
    }
}