<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>

<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Vertex-Tools/Aero.Framework">
    <img width="100" height="100" alt="image" src="https://github.com/user-attachments/assets/7ba40ee1-dcbf-4d12-a9ba-f5de586ac971" />
  </a>

  <h3 align="center">Aero Framework</h3>

  <p align="center">
    A high-performance C# server-side development framework for FiveM.
    <br />
    <a href="https://github.com/Vertex-Tools/Aero.Framework"><strong>Explore the docs » (In development)</strong></a>
    <br />
    <br />
    <a href="https://github.com/Vertex-Tools/Aero.Framework/issues">Report Bug</a>
    ·
    <a href="https://github.com/Vertex-Tools/Aero.Framework/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li>
      <a href="#plugin-development-guide">Plugin Development Guide</a>
      <ul>
        <li><a href="#1-creating-a-plugin">1. Creating a Plugin</a></li>
        <li><a href="#2-configuration-management">2. Configuration Management</a></li>
        <li><a href="#3-registering-commands">3. Registering Commands</a></li>
        <li><a href="#4-registering-events">4. Registering Events</a></li>
        <li><a href="#5-using-drivers">5. Using Drivers</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Aero Framework is a high-performance C# development framework for FiveM server resources, built by **Vertex Tools**. Designed to overcome standard C# FiveM limitations, it features a dedicated, low-overhead event dispatcher system, dynamic plugin loading, and robust abstraction drivers.

Key Features:
* **Circular-Buffer Event Dispatcher**: Optimized for processing native and custom events with high stability and extremely low memory footprint.
* **Dynamic Plugin Loader**: Scan, load, and manage assemblies dynamically from your `plugins/` directory. Compatible framework verification prevents outdated plugins from loading.
* **Driver Abstraction System**: Ready-to-use abstract interfaces for Databases, Players, Vehicles, and Objects.
* **Zero-Touch Registration**: Automatically scans and registers commands (`ICommand`) and events (`IEvent`) within your custom plugin assemblies upon loading.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* [.NET SDK 8.0 & .NET Framework 4.7.2](https://dotnet.microsoft.com/)
* [CitizenFX.Core.Server](https://fivem.net/)
* [Avian.Event](https://github.com/Vertex-Tools/Aero.Framework)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

* [.NET SDK 4.7.2](https://dotnet.microsoft.com/download)
* Rider, Visual Studio, or VS Code
* A FiveM Server environment

### Automatic Installation

1. From the release install the installer from github.
2. Restart the server and enjoy the framework!

### Manual Installation

1. Install all DLL's from the release on github.
2. Put all the DLL's in the resource folder in the FiveM server.
3. Build the manifest.lua to link everything.
4. Restart the server and enjoy the framework!

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- PLUGIN DEVELOPMENT GUIDE -->
## Plugin Development Guide

Aero Framework makes it extremely simple to extend your server functionality with modular plugins. To create a plugin, reference `Aero.API` in your project.

### 1. Creating a Plugin

Create a class that inherits from `Plugin<TConfig>`, where `TConfig` represents your configuration class.

```csharp
using Aero.API;
using Aero.API.Interfaces;

public class MyPluginConfig : IConfig
{
    public bool Enabled { get; set; } = true;
    public string GreetingMessage { get; set; } = "Hello from Aero Plugin!";
}

public class MyPlugin : Plugin<MyPluginConfig>
{
    public override string Name => "MyPlugin";
    public override string Author => "Vertex Tools";
    public override string Description => "An example plugin demonstrating Aero.Framework features.";
    public override System.Version Version => new System.Version(1, 0, 0);
    public override System.Version RequiredFramework => new System.Version(1, 0, 0);

    // This property is automatically populated by Aero Framework
    public static MyPluginConfig Config { get; set; }

    public override void OnLoad()
    {
        Log.Info($"{Name} Loaded! Greeting: {Config.GreetingMessage}");
    }

    public override void OnUnload()
    {
        Log.Warn($"{Name} Unloaded.");
    }
}
```

### 2. Configuration Management
Plugin configurations are automatically written and read from the plugin's folder using standard YAML. When your plugin is initialized, a config file named `<PluginName>.yml` will be generated/loaded automatically.

### 3. Registering Commands
Any class inside your plugin assembly implementing the `ICommand` interface will be automatically detected and registered to FiveM's command list when the plugin loads.

```csharp
using Aero.API.Interfaces;
using Aero.API.Features;

public class HelloCommand : ICommand
{
    public string Name => "hello";
    public string Description => "Greets the server.";
    public string Permission => "user";

    public void Execute(int source, string[] args)
    {
        Log.Success($"Hello command executed by source player {source}!");
    }
}
```

### 4. Registering Events
The framework automatically scans and registers event handlers using the [AeroEvent] attribute. You don't need to manually implement interfaces or register listeners.

```csharp
using Aero.Events;
using Aero.API.Events.Network;
using Aero.API.Features;

public class PlayerEventHandler
{
    // 🏷️ The framework will automatically detect this attribute on startup
    [AeroEvent(typeof(PlayerEnteringEvent))]
    public void OnPlayerEntering(PlayerEnteringEvent e)
    {
        // 🎨 Your color-coded logs work natively here!
        Log.Success($"Player {e.PlayerName} (ID: {e.PlayerId}) is entering the server! ^2✓");
    }
}
```

### 5. Using Drivers
Aero Framework features a centralized driver manager. You can retrieve driver instances to safely interact with core services like Databases, Players, Vehicles, and Objects.

```csharp
using Aero.Drivers.Registry;
using Aero.API.Interfaces;

// Accessing the Database Driver
var db = DriverRegistry.Singleton.Get<IDataBaseDriver>();
await db.ExecuteAsync("INSERT INTO audit_logs (message) VALUES (@msg)", new { msg = "Server start" });

// Accessing the Player Driver
var playerDriver = DriverRegistry.Singleton.Get<IPlayerDriver>();
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [x] Circular Buffer Event Dispatcher
- [x] YAML Injected Plugin Configurations
- [ ] Command Permission Middleware
- [ ] Hot Reloading for Plugins
- [ ] Direct Database ORM integration

See the [open issues](https://github.com/Vertex-Tools/Aero.Framework/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Vertex Tools - [@Vertex-Tools](https://github.com/Vertex-Tools)

Project Link: [https://github.com/Vertex-Tools/Aero.Framework](https://github.com/Vertex-Tools/Aero.Framework)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Vertex-Tools/Aero.Framework.svg?style=for-the-badge
[contributors-url]: https://github.com/Vertex-Tools/Aero.Framework/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Vertex-Tools/Aero.Framework.svg?style=for-the-badge
[forks-url]: https://github.com/Vertex-Tools/Aero.Framework/network/members
[stars-shield]: https://img.shields.io/github/stars/Vertex-Tools/Aero.Framework.svg?style=for-the-badge
[stars-url]: https://github.com/Vertex-Tools/Aero.Framework/stargazers
[issues-shield]: https://img.shields.io/github/issues/Vertex-Tools/Aero.Framework.svg?style=for-the-badge
[issues-url]: https://github.com/Vertex-Tools/Aero.Framework/issues
[license-shield]: https://img.shields.io/github/license/Vertex-Tools/Aero.Framework.svg?style=for-the-badge
[license-url]: https://github.com/Vertex-Tools/Aero.Framework/blob/main/LICENSE

## Acknowledgments

- **Cfx** - [CitizenFX.Core.Server](https://github.com/citizenfx/fivem): Thanks for its native API and communication FiveM - C#
- **Jnewkirk**, **Bradwilson** - [xunitv3](https://github.com/xunit/xunit): Thanks for their system to test all our systems in Aero.Tests
- **Antoine Aubry** - [YamlDotNet](https://github.com/aaubry/YamlDotNet): Thanks for building our ConfigBuilder which is essential for our core
- **Microsoft** - [System](https://github.com/dotnet): C# Native System
- **Vertex Tools** - [Avian](https://github.com/Vertex-Tools/Avian): Essential for our Event system
- **Vertex Tools** - [pyro.Avian](https://github.com/Vertex-Tools/pyro.Avian): Useful for packaging all the dependencies in one dll
