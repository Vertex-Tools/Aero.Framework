import React from 'react';
import Link from 'next/link';

export default function DocsCommands() {
  return (
    <div className="space-y-10">
      <div>
        <h1 className="text-4xl font-extrabold text-zinc-900 tracking-tight mb-4">Command Registration</h1>
        <p className="text-lg text-zinc-500 leading-relaxed">
          Expose server commands instantly using the Zero-Touch Command registration system in the Aero Framework.
        </p>
      </div>

      <div className="h-px bg-zinc-200" />

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Zero-Touch Commands</h2>
        <p className="text-zinc-600 leading-relaxed">
          Traditional command registration in FiveM requires invoking registration natives manually or subscribing to event handlers inside your primary scripts.
        </p>
        <p className="text-zinc-600 leading-relaxed">
          Aero replaces this boilerplate entirely. Any class within your plugin assemblies that implements the <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">ICommand</code> interface is detected automatically during the plugin startup phase and registered directly into the Cfx.re command structure.
        </p>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">The `ICommand` Interface</h2>
        <p className="text-zinc-600 leading-relaxed">
          To define a command, implement the interface with the following properties:
        </p>
        <ul className="list-disc list-inside space-y-2 text-sm text-zinc-600">
          <li><strong>Name:</strong> The console execution trigger (e.g. <code className="px-1 py-0.5 bg-zinc-100 rounded">hello</code>).</li>
          <li><strong>Description:</strong> Documentation string describing the command's functionality.</li>
          <li><strong>Permission:</strong> The authorization level required to run the command (e.g. <code className="px-1 py-0.5 bg-zinc-100 rounded">admin</code> or <code className="px-1 py-0.5 bg-zinc-100 rounded">user</code>).</li>
          <li><strong>Execute(int source, string[] args):</strong> The execution handler block invoked upon calling.</li>
        </ul>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Code Example</h2>
        <p className="text-zinc-600 leading-relaxed">
          Here is a command implementation that spawns a vehicle using the framework features:
        </p>
        <pre className="code-container">
          <code>{`using Aero.API.Interfaces;
using Aero.API.Features;

public class SpawnCommand : ICommand
{
    public string Name => "car";
    public string Description => "Spawns a vehicle by model name.";
    public string Permission => "admin";

    public void Execute(int source, string[] args)
    {
        if (args.Length < 1)
        {
            Log.Error("Usage: /car [modelName]");
            return;
        }

        string modelName = args[0];
        Log.Info($"Player {source} requested spawning: {modelName}");
        
        // Spawn logic can be integrated using Vehicle Driver here
    }
}`}</code>
        </pre>
      </section>

      <div className="h-px bg-zinc-200" />

      <div className="flex justify-between items-center pt-4">
        <Link href="/docs/plugins" className="curved-btn-secondary px-6 py-2.5 text-sm">
          ← Back: Plugins & Config
        </Link>
        <Link href="/docs/events" className="curved-btn-primary px-6 py-2.5 text-sm">
          Next: Events →
        </Link>
      </div>
    </div>
  );
}
