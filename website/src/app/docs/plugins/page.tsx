import React from 'react';
import Link from 'next/link';

export default function DocsPlugins() {
  return (
    <div className="space-y-10">
      <div>
        <h1 className="text-4xl font-extrabold text-zinc-900 tracking-tight mb-4">Plugins & Configuration</h1>
        <p className="text-lg text-zinc-500 leading-relaxed">
          Learn how to build modular plugins for the Aero Framework, handle configurations automatically using YAML, and manage plugin loading lifecycles.
        </p>
      </div>

      <div className="h-px bg-zinc-200" />

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Creating a Plugin</h2>
        <p className="text-zinc-600 leading-relaxed">
          Plugins in Aero are self-contained assembly libraries (compiled DLLs) loaded dynamically from the server resource's <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">Plugins/</code> directory. 
        </p>
        <p className="text-zinc-600 leading-relaxed">
          To start developing a plugin, reference <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">Aero.API.dll</code> in your C# class library and inherit from the base class <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">Plugin&lt;TConfig&gt;</code>.
        </p>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Plugin Configuration (`IConfig`)</h2>
        <p className="text-zinc-600 leading-relaxed">
          Aero features automated YAML configuration serialization. By defining a configuration model that implements <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">IConfig</code>, the framework automatically generates and loads a YAML config file named <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">&lt;PluginName&gt;.yml</code> inside the plugin directory.
        </p>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Code Implementation</h2>
        <p className="text-zinc-600 leading-relaxed">
          Here is a complete template showcasing a plugin implementation with its configuration file setup:
        </p>
        <pre className="code-container">
          <code>{`using System;
using Aero.API;
using Aero.API.Interfaces;

// 1. Define your configuration class
public class HelloPluginConfig : IConfig
{
    public bool Enabled { get; set; } = true;
    public string Message { get; set; } = "Welcome to the server!";
    public int MaxConnections { get; set; } = 100;
}

// 2. Inherit from Plugin<TConfig>
public class HelloPlugin : Plugin<HelloPluginConfig>
{
    public override string Name => "HelloPlugin";
    public override string Author => "Vertex Tools";
    public override string Description => "A demo plugin managing welcome configs.";
    public override Version Version => new Version(1, 0, 0);
    public override Version RequiredFramework => new Version(1, 0, 0);

    // This property is populated by the loader automatically
    public static HelloPluginConfig Config { get; set; }

    public override void OnLoad()
    {
        Log.Info($"[{Name}] loaded successfully!");
        if (Config.Enabled)
        {
            Log.Success($"Greeting: {Config.Message}");
        }
    }

    public override void OnUnload()
    {
        Log.Warn($"[{Name}] has been unloaded.");
    }
}`}</code>
        </pre>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Lifecycle Pipeline</h2>
        <p className="text-zinc-600 leading-relaxed">
          Aero handles assembly loading through the following sequential pipeline:
        </p>
        <ol className="list-decimal list-inside space-y-2 text-sm text-zinc-600">
          <li><strong>Assembly Scanning:</strong> Checks the <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">Plugins/</code> folder for `.dll` extensions.</li>
          <li><strong>Framework Verification:</strong> Verifies that the plugin's <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">RequiredFramework</code> version matches or is compatible with the core engine version.</li>
          <li><strong>Config Instantiation:</strong> Automatically loads/creates the config file and populates the static <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">Config</code> property.</li>
          <li><strong>Bootstrap:</strong> Triggers the <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">OnLoad()</code> override.</li>
        </ol>
      </section>

      <div className="h-px bg-zinc-200" />

      <div className="flex justify-between items-center pt-4">
        <Link href="/docs" className="curved-btn-secondary px-6 py-2.5 text-sm">
          ← Back: Introduction
        </Link>
        <Link href="/docs/commands" className="curved-btn-primary px-6 py-2.5 text-sm">
          Next: Commands →
        </Link>
      </div>
    </div>
  );
}
