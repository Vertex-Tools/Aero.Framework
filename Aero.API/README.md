# Aero Framework API

Aero Framework is a high-performance C# server-side development framework designed specifically for FiveM (Cfx.re) servers.

This package (`Aero.API`) provides the developers with interfaces, attributes, and base classes to write modular plugins, register chat commands, hook native events with zero allocation pressure, and utilize built-in registry drivers.

## Features

- **Modular Plugins**: Inherit from `Plugin<TConfig>` to build plug-and-play assembly extensions.
- **Zero-Touch Commands**: Implement `ICommand` to automatically register server commands.
- **Low GC Event Dispatcher**: Use the `[AeroEvent]` attribute to hook events handled in pre-allocated circular buffers.
- **Centralized Drivers**: Interact with MySQL databases (`oxmysql`), players, peds, and vehicles safely via `DriverRegistry`.

## Installation

Add the package to your C# class library project:

```bash
dotnet add package Aero.API
```

## Basic Plugin Example

```csharp
using System;
using Aero.API;
using Aero.API.Interfaces;
using Aero.Events;
using Aero.API.Events.Network;

public class MyConfig : IConfig
{
    public string Message { get; set; } = "Hello World!";
}

public class MyPlugin : Plugin<MyConfig>
{
    public override string Name => "MyAwesomePlugin";
    public override string Author => "Vertex Tools";
    public override Version Version => new Version(1, 0, 0);

    public static MyConfig Config { get; set; }

    public override void OnLoad()
    {
        Log.Success($"[{Name}] Loaded! Welcome message: {Config.Message}");
    }

    [AeroEvent(typeof(PlayerEnteringEvent))]
    public void OnPlayerEntering(PlayerEnteringEvent e)
    {
        Log.Info($"Player {e.PlayerName} (ID: {e.PlayerId}) is joining the game.");
    }
}
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.
