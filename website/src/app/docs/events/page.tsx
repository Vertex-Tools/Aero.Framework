import React from 'react';
import Link from 'next/link';

export default function DocsEvents() {
  return (
    <div className="space-y-10">
      <div>
        <h1 className="text-4xl font-extrabold text-zinc-900 tracking-tight mb-4">Event System (`[AeroEvent]`)</h1>
        <p className="text-lg text-zinc-500 leading-relaxed">
          Leverage Aero's high-performance circular-buffer dispatcher and register handlers dynamically using attributes.
        </p>
      </div>

      <div className="h-px bg-zinc-200" />

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">The Modern Event Pipeline</h2>
        <p className="text-zinc-600 leading-relaxed">
          FiveM's standard event system in C# relies on registering strings in the event handler dictionary. This introduces overhead due to delegate registration and allocations.
        </p>
        <p className="text-zinc-600 leading-relaxed">
          Aero introduces a custom <strong>Circular-Buffer Event Dispatcher</strong>. Native and net events are intercepted and processed inside pre-allocated structural buffers. This drastically reduces Garbage Collection (GC) pressure and maintains high tick rates.
        </p>
        <div className="p-4 border-l-4 border-zinc-950 bg-zinc-50 rounded-r-2xl text-sm text-zinc-600">
          <strong>Note:</strong> The legacy <code className="px-1 py-0.5 rounded bg-zinc-200/50">IEvent</code> interface is obsolete. Handlers should now be declared using the modern <code className="px-1 py-0.5 rounded bg-zinc-200/50">[AeroEvent]</code> method attribute system.
        </div>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Using `[AeroEvent]` Attribute</h2>
        <p className="text-zinc-600 leading-relaxed">
          To hook an event handler, declare a public method inside any class in your plugin and decorate it with <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">[AeroEvent(typeof(EventType))]</code>. The method signature must accept the matching event struct as its single parameter.
        </p>
        <pre className="code-container">
          <code>{`using Aero.Events;
using Aero.API.Events.Network;
using Aero.API.Events.Combat;
using Aero.API.Features;

public class GameEventsListener
{
    // Automatically hooked on startup by Aero Engine
    [AeroEvent(typeof(PlayerEnteringEvent))]
    public void OnPlayerEntering(PlayerEnteringEvent e)
    {
        Log.Info($"Player {e.PlayerName} (ID: {e.PlayerId}) has joined!");
    }

    [AeroEvent(typeof(PlayerDeathAdvancedEvent))]
    public void OnPlayerDied(PlayerDeathAdvancedEvent e)
    {
        Log.Warn($"Player ID {e.VictimId} was killed by {e.KillerId} with weapon hash {e.WeaponHash}");
    }
}`}</code>
        </pre>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Available Core Events</h2>
        <p className="text-zinc-600 leading-relaxed">
          Aero exposes typed event structures representing major native and network callbacks:
        </p>
        <div className="border border-zinc-200 rounded-[24px] overflow-hidden bg-white shadow-sm">
          <table className="w-full text-left text-sm divide-y divide-zinc-200">
            <thead className="bg-zinc-50 text-zinc-500 font-semibold">
              <tr>
                <th className="p-4">Event Struct</th>
                <th className="p-4">Category</th>
                <th className="p-4">Description</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-zinc-100 text-zinc-600">
              <tr>
                <td className="p-4 font-mono font-semibold text-zinc-900">PlayerEnteringEvent</td>
                <td className="p-4">Network</td>
                <td className="p-4">Triggered when a player starts entering the server.</td>
              </tr>
              <tr>
                <td className="p-4 font-mono font-semibold text-zinc-900">PlayerLeftEvent</td>
                <td className="p-4">Network</td>
                <td className="p-4">Triggered when a player disconnects from the session.</td>
              </tr>
              <tr>
                <td className="p-4 font-mono font-semibold text-zinc-900">PlayerDeathAdvancedEvent</td>
                <td className="p-4">Combat</td>
                <td className="p-4">Detailed combat info including victim, killer, and weapon hash.</td>
              </tr>
              <tr>
                <td className="p-4 font-mono font-semibold text-zinc-900">ExplosionEvent</td>
                <td className="p-4">Combat</td>
                <td className="p-4">Triggered upon any network explosion entity creation.</td>
              </tr>
              <tr>
                <td className="p-4 font-mono font-semibold text-zinc-900">EntityCreatedEvent</td>
                <td className="p-4">Network</td>
                <td className="p-4">Triggered when any vehicle, ped, or object is network spawned.</td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <div className="h-px bg-zinc-200" />

      <div className="flex justify-between items-center pt-4">
        <Link href="/docs/commands" className="curved-btn-secondary px-6 py-2.5 text-sm">
          ← Back: Commands
        </Link>
        <Link href="/docs/drivers" className="curved-btn-primary px-6 py-2.5 text-sm">
          Next: Drivers Registry →
        </Link>
      </div>
    </div>
  );
}
