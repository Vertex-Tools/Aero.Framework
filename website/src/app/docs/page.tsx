import React from 'react';
import Link from 'next/link';

export default function DocsHome() {
  return (
    <div className="space-y-10">
      <div>
        <h1 className="text-4xl font-extrabold text-zinc-900 tracking-tight mb-4">Introduction</h1>
        <p className="text-lg text-zinc-500 leading-relaxed">
          Welcome to the official documentation for the <strong>Aero Framework</strong>, a high-performance C# server-side development framework designed specifically for FiveM (Cfx.re) servers.
        </p>
      </div>

      <div className="h-px bg-zinc-200" />

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Why Aero Framework?</h2>
        <p className="text-zinc-600 leading-relaxed">
          Developing server resources in C# for FiveM historically introduces performance and engineering challenges, particularly regarding garbage collection overhead, native interop marshaling, and manual handler boilerplate.
        </p>
        <p className="text-zinc-600 leading-relaxed">
          Aero resolves these limitations by introducing a customized architecture optimized for the Cfx.re Mono runtime environment:
        </p>
        <div className="grid sm:grid-cols-2 gap-6 pt-4">
          <div className="p-5 border border-zinc-200 rounded-2xl bg-zinc-50/50">
            <h3 className="font-semibold text-zinc-900 mb-2">⚡ Circular-Buffer Event Dispatcher</h3>
            <p className="text-sm text-zinc-500 leading-relaxed">
              Handles server-side events using pre-allocated circular buffers, resulting in extremely low garbage collection pressure and optimal execution speed.
            </p>
          </div>
          <div className="p-5 border border-zinc-200 rounded-2xl bg-zinc-50/50">
            <h3 className="font-semibold text-zinc-900 mb-2">🔎 Zero-Touch Reflection Loader</h3>
            <p className="text-sm text-zinc-500 leading-relaxed">
              Dynamically scans your custom assemblies on startup to automatically map and register commands, event callbacks, and driver models.
            </p>
          </div>
        </div>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Getting Started</h2>
        <p className="text-zinc-600 leading-relaxed">
          Installing the Aero Framework is simplified by the automated console utility.
        </p>
        
        <div className="space-y-6 mt-4">
          <div className="space-y-2">
            <h3 className="font-semibold text-zinc-900 text-sm">1. Download the Installer</h3>
            <p className="text-sm text-zinc-500">
              Download <a href="/downloads/Aero.Installer.exe" className="text-zinc-900 underline font-medium">Aero.Installer.exe</a> and place it in the root folder of your FiveM server (the folder containing your <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">resources/</code> directory).
            </p>
          </div>

          <div className="space-y-2">
            <h3 className="font-semibold text-zinc-900 text-sm">2. Execute the Installer</h3>
            <p className="text-sm text-zinc-500">
              Run the executable in your console. The installer will automatically download the core modules, register all external dependencies, and construct the correct resource structure:
            </p>
            <pre className="code-container">
              <code>{`# Windows
./Aero.Installer.exe

# Linux
chmod +x Aero.Installer.exe
./Aero.Installer.exe`}</code>
            </pre>
          </div>

          <div className="space-y-2">
            <h3 className="font-semibold text-zinc-900 text-sm">3. Enable the Resource</h3>
            <p className="text-sm text-zinc-500">
              Open your server configuration file (usually <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">server.cfg</code>) and append the loading directive:
            </p>
            <pre className="code-container">
              <code>{`ensure aero-core`}</code>
            </pre>
          </div>
        </div>
      </section>

      <div className="h-px bg-zinc-200" />

      <div className="flex justify-between items-center pt-4">
        <span />
        <Link href="/docs/plugins" className="curved-btn-primary px-6 py-2.5 text-sm">
          Next: Plugins & Config →
        </Link>
      </div>
    </div>
  );
}
