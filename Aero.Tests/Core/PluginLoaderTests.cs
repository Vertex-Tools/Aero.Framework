// ====================================
// <copyright file="PluginLoaderTests.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.IO;
using Aero.API;
using Aero.API.Core;
using Aero.API.Interfaces;
using Aero.Core.Loader;
using Xunit;
using Version = System.Version;

namespace Aero.Tests.Core;

public class TestConfig : IConfig
{
    public bool Enabled { get; set; } = true;
}

public class TestPlugin : Plugin<TestConfig>
{
    public override string Name => "TestPlugin";
    public override string Author => "TestAuthor";
    public override string Description => "TestDescription";
    public override Version Version => new Version(1, 0, 0);
    public override Version RequiredFramework => new Version(0, 1, 0);

    public override void OnLoad() { }
    public override void OnUnload() { }
}

public class HighVersionPlugin : Plugin<TestConfig>
{
    public override string Name => "HighVersionPlugin";
    public override string Author => "TestAuthor";
    public override string Description => "TestDescription";
    public override Version Version => new Version(1, 0, 0);
    public override Version RequiredFramework => new Version(2, 0, 0);

    public override void OnLoad() { }
    public override void OnUnload() { }
}

public class NonPluginClass
{
}

public class PluginLoaderTests
{
    public PluginLoaderTests()
    {
        Paths.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AeroTestRun"));
    }

    [Fact]
    public void IsPluginType_ShouldReturnTrueForPluginSubclass()
    {
        var loader = new PluginLoader();
        Assert.True(loader.IsPluginType(typeof(TestPlugin)));
        Assert.True(loader.IsPluginType(typeof(HighVersionPlugin)));
    }

    [Fact]
    public void IsPluginType_ShouldReturnFalseForNonPluginClass()
    {
        var loader = new PluginLoader();
        Assert.False(loader.IsPluginType(typeof(NonPluginClass)));
    }

    [Fact]
    public void RegisterPlugin_ShouldLoadPluginCorrectly()
    {
        var loader = new PluginLoader();
        PluginLoader.LoadedPlugins.Clear();

        loader.RegisterPlugin(typeof(TestPlugin));

        Assert.Single(PluginLoader.LoadedPlugins);
        Assert.IsType<TestPlugin>(PluginLoader.LoadedPlugins[0]);
    }

    [Fact]
    public void RegisterPlugin_ShouldSkipPluginWithHigherRequiredVersion()
    {
        var loader = new PluginLoader();
        PluginLoader.LoadedPlugins.Clear();

        loader.RegisterPlugin(typeof(HighVersionPlugin));

        Assert.Empty(PluginLoader.LoadedPlugins);
    }
}
