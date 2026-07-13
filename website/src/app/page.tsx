'use client';

import React, { useState } from 'react';
import Image from 'next/image';

export default function Home() {
  const [activeTab, setActiveTab] = useState<'plugin' | 'command' | 'event'>('plugin');
  const [copied, setCopied] = useState<string | null>(null);

  const copyToClipboard = (text: string, id: string) => {
    navigator.clipboard.writeText(text);
    setCopied(id);
    setTimeout(() => setCopied(null), 2000);
  };

  const codeSnippets = {
    plugin: `using Aero.API;
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
    public override System.Version Version => new System.Version(1, 0, 0);

    public static MyPluginConfig Config { get; set; }

    public override void OnLoad()
    {
        Log.Info($"\${Name} Loaded! Greeting: \${Config.GreetingMessage}");
    }
}`,
    command: `using Aero.API.Interfaces;
using Aero.API.Features;

public class HelloCommand : ICommand
{
    public string Name => "hello";
    public string Description => "Greets the server.";
    public string Permission => "user";

    public void Execute(int source, string[] args)
    {
        Log.Success($"Hello command executed by source player \${source}!");
    }
}`,
    event: `using Aero.Events;
using Aero.API.Events.Network;
using Aero.API.Features;

public class PlayerEventHandler
{
    // The framework will automatically detect this attribute on startup
    [AeroEvent(typeof(PlayerEnteringEvent))]
    public void OnPlayerEntering(PlayerEnteringEvent e)
    {
        Log.Success($"Player \${e.PlayerName} (ID: \${e.PlayerId}) is entering the server! ✓");
    }
}`
  };

  const files = [
    { name: 'Aero.Installer.exe', desc: 'Automated Installer (Windows & Linux)', size: '151.6 KB', path: '/downloads/Aero.Installer.exe' },
    { name: 'Aero.Core.net.dll', desc: 'Framework Core Script Assembly', size: '35.8 KB', path: '/downloads/Aero.Core.net.dll' },
    { name: 'Aero.API.dll', desc: 'Developer API Interface Library', size: '22.0 KB', path: '/downloads/Aero.API.dll' },
    { name: 'Aero.Drivers.dll', desc: 'Database, Player & Vehicle Abstractions', size: '17.4 KB', path: '/downloads/Aero.Drivers.dll' },
    { name: 'Aero.Events.dll', desc: 'Circular-Buffer Event Dispatchers', size: '14.3 KB', path: '/downloads/Aero.Events.dll' },
    { name: 'Aero.Shared.dll', desc: 'Shared framework utilities', size: '5.1 KB', path: '/downloads/Aero.Shared.dll' },
  ];

  const dependencies = [
    { name: 'Avian.dll', desc: 'Core compilation runtime dependency', size: '6.7 KB', path: '/downloads/Avian.dll' },
    { name: 'Avian.Event.dll', desc: 'Event manager core engine dependency', size: '7.7 KB', path: '/downloads/Avian.Event.dll' },
    { name: 'YamlDotNet.dll', desc: 'Configuration parser library', size: '298.5 KB', path: '/downloads/YamlDotNet.dll' },
  ];

  return (
    <div className="min-h-screen bg-zinc-50/50 flex flex-col">
      {/* Navigation */}
      <header className="sticky top-0 z-50 backdrop-blur-md bg-white/70 border-b border-zinc-200/50 px-6 py-4">
        <div className="max-w-6xl mx-auto flex items-center justify-between">
          <a href="#" className="flex items-center gap-3 hover:opacity-80 transition-opacity">
            <Image 
              src="/logo.png" 
              alt="Aero Logo" 
              width={36} 
              height={36} 
              className="object-contain"
            />
            <span className="font-semibold text-lg tracking-tight text-zinc-900">Aero Framework</span>
          </a>
          <nav className="hidden md:flex items-center gap-2 text-sm font-medium text-zinc-600">
            <a href="#features" className="px-4 py-2 rounded-full hover:bg-zinc-900 hover:text-white transition-all duration-200">Features</a>
            <a href="#code" className="px-4 py-2 rounded-full hover:bg-zinc-900 hover:text-white transition-all duration-200">Code Examples</a>
            <a href="#downloads" className="px-4 py-2 rounded-full hover:bg-zinc-900 hover:text-white transition-all duration-200">Downloads</a>
            <a href="#quickstart" className="px-4 py-2 rounded-full hover:bg-zinc-900 hover:text-white transition-all duration-200">Quickstart</a>
          </nav>
          <div className="flex items-center gap-3">
            <a 
              href="https://github.com/Vertex-Tools/Aero.Framework" 
              target="_blank" 
              rel="noopener noreferrer" 
              className="curved-btn-secondary px-4 py-2 text-sm flex items-center gap-2"
            >
              GitHub
            </a>
            <a href="#downloads" className="curved-btn-primary px-5 py-2 text-sm">
              Get Started
            </a>
          </div>
        </div>
      </header>

      {/* Hero Section */}
      <section className="px-6 pt-20 pb-24 text-center max-w-4xl mx-auto flex-1 flex flex-col justify-center items-center">
        <div className="inline-flex items-center gap-2 px-3 py-1.5 rounded-full bg-zinc-100 border border-zinc-200 text-xs font-semibold text-zinc-700 mb-8">
          <span className="w-2 h-2 rounded-full bg-emerald-500 animate-pulse"></span>
          Now v0.1.0 Stable Release
        </div>
        <h1 className="text-5xl md:text-6xl font-extrabold text-zinc-900 tracking-tight leading-[1.1] mb-6 max-w-3xl">
          High-Performance C# Development for FiveM
        </h1>
        <p className="text-lg md:text-xl text-zinc-500 max-w-2xl leading-relaxed mb-10">
          Aero Framework is a curved, ultra-fast, zero-touch server framework. Designed to bypass the standard limitations of C# on Cfx.re server environments.
        </p>
        <div className="flex flex-col sm:flex-row gap-4 w-full justify-center px-4 max-w-md">
          <a href="#downloads" className="curved-btn-primary px-8 py-3.5 text-center flex-1 whitespace-nowrap">
            Download Installer
          </a>
          <a href="#quickstart" className="curved-btn-secondary px-8 py-3.5 text-center flex-1 whitespace-nowrap">
            Quickstart Guide
          </a>
        </div>
      </section>

      {/* Feature Cards Grid */}
      <section id="features" className="px-6 py-24 bg-white border-y border-zinc-200/50">
        <div className="max-w-6xl mx-auto">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold tracking-tight text-zinc-900 mb-4">Engineered for Performance</h2>
            <p className="text-zinc-500 max-w-xl mx-auto">Built from the ground up to solve common threading and memory overhead in C# FiveM scripting.</p>
          </div>
          
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-6">
            <div className="curved-card p-6 flex flex-col justify-between">
              <div>
                <div className="w-10 h-10 rounded-2xl bg-zinc-100 flex items-center justify-center font-bold text-zinc-800 mb-5">⚡</div>
                <h3 className="font-semibold text-zinc-900 mb-2">Circular-Buffer</h3>
                <p className="text-sm text-zinc-500 leading-relaxed">Optimal execution structure. Processes custom events with near-zero latency and high stability.</p>
              </div>
            </div>

            <div className="curved-card p-6 flex flex-col justify-between">
              <div>
                <div className="w-10 h-10 rounded-2xl bg-zinc-100 flex items-center justify-center font-bold text-zinc-800 mb-5">📦</div>
                <h3 className="font-semibold text-zinc-900 mb-2">Dynamic Loader</h3>
                <p className="text-sm text-zinc-500 leading-relaxed">Dynamically loads compiled assemblies from folder structure with safety dependency checks.</p>
              </div>
            </div>

            <div className="curved-card p-6 flex flex-col justify-between">
              <div>
                <div className="w-10 h-10 rounded-2xl bg-zinc-100 flex items-center justify-center font-bold text-zinc-800 mb-5">🚗</div>
                <h3 className="font-semibold text-zinc-900 mb-2">Driver Managers</h3>
                <p className="text-sm text-zinc-500 leading-relaxed">High-level abstractions for Database connection, Players, Vehicles, and entity controls.</p>
              </div>
            </div>

            <div className="curved-card p-6 flex flex-col justify-between">
              <div>
                <div className="w-10 h-10 rounded-2xl bg-zinc-100 flex items-center justify-center font-bold text-zinc-800 mb-5">🔥</div>
                <h3 className="font-semibold text-zinc-900 mb-2">Zero-Touch</h3>
                <p className="text-sm text-zinc-500 leading-relaxed">Automatic reflection scan triggers command registering and event mapping instantly on startup.</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Code Playground */}
      <section id="code" className="px-6 py-24 bg-zinc-50/50">
        <div className="max-w-4xl mx-auto">
          <div className="text-center mb-12">
            <h2 className="text-3xl font-bold tracking-tight text-zinc-900 mb-4">Beautifully Clean APIs</h2>
            <p className="text-zinc-500">Creating features in C# has never been this intuitive.</p>
          </div>

          <div className="bg-white border border-zinc-200/80 rounded-[32px] p-6 shadow-sm">
            <div className="flex border-b border-zinc-200 mb-6 gap-2">
              <button 
                onClick={() => setActiveTab('plugin')}
                className={`pb-4 px-4 font-medium text-sm border-b-2 transition-all ${activeTab === 'plugin' ? 'border-zinc-900 text-zinc-900' : 'border-transparent text-zinc-400 hover:text-zinc-600'}`}
              >
                Plugin.cs
              </button>
              <button 
                onClick={() => setActiveTab('command')}
                className={`pb-4 px-4 font-medium text-sm border-b-2 transition-all ${activeTab === 'command' ? 'border-zinc-900 text-zinc-900' : 'border-transparent text-zinc-400 hover:text-zinc-600'}`}
              >
                HelloCommand.cs
              </button>
              <button 
                onClick={() => setActiveTab('event')}
                className={`pb-4 px-4 font-medium text-sm border-b-2 transition-all ${activeTab === 'event' ? 'border-zinc-900 text-zinc-900' : 'border-transparent text-zinc-400 hover:text-zinc-600'}`}
              >
                PlayerEventHandler.cs
              </button>
            </div>

            <div className="relative">
              <button 
                onClick={() => copyToClipboard(codeSnippets[activeTab], activeTab)}
                className="absolute right-4 top-4 px-3 py-1.5 rounded-lg bg-white border border-zinc-200 text-xs font-semibold text-zinc-600 hover:bg-zinc-50 transition-colors shadow-sm"
              >
                {copied === activeTab ? 'Copied! ✓' : 'Copy'}
              </button>
              <pre className="code-container font-mono text-sm overflow-x-auto whitespace-pre">
                <code>{codeSnippets[activeTab]}</code>
              </pre>
            </div>
          </div>
        </div>
      </section>

      {/* Downloads Section */}
      <section id="downloads" className="px-6 py-24 bg-white border-y border-zinc-200/50">
        <div className="max-w-4xl mx-auto">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold tracking-tight text-zinc-900 mb-4">Download Framework Modules</h2>
            <p className="text-zinc-500">Download the consolidated installer or configure your server resources manually.</p>
          </div>

          <div className="space-y-12">
            {/* Core Modules */}
            <div>
              <h3 className="font-semibold text-lg text-zinc-800 mb-5">Core modules</h3>
              <div className="border border-zinc-200 rounded-[28px] overflow-hidden bg-white shadow-sm">
                <div className="divide-y divide-zinc-100">
                  {files.map((file) => (
                    <div key={file.name} className="flex items-center justify-between p-5 hover:bg-zinc-50/50 transition-colors">
                      <div>
                        <h4 className="font-semibold text-zinc-900 text-sm">{file.name}</h4>
                        <p className="text-xs text-zinc-400 mt-0.5">{file.desc}</p>
                      </div>
                      <div className="flex items-center gap-5">
                        <span className="text-xs font-medium text-zinc-400">{file.size}</span>
                        <a 
                          href={file.path} 
                          download 
                          className="curved-btn-secondary px-4 py-1.5 text-xs"
                        >
                          Download
                        </a>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>

            {/* Third-Party Dependencies */}
            <div>
              <h3 className="font-semibold text-lg text-zinc-800 mb-5">Required external dependencies</h3>
              <div className="border border-zinc-200 rounded-[28px] overflow-hidden bg-white shadow-sm">
                <div className="divide-y divide-zinc-100">
                  {dependencies.map((dep) => (
                    <div key={dep.name} className="flex items-center justify-between p-5 hover:bg-zinc-50/50 transition-colors">
                      <div>
                        <h4 className="font-semibold text-zinc-900 text-sm">{dep.name}</h4>
                        <p className="text-xs text-zinc-400 mt-0.5">{dep.desc}</p>
                      </div>
                      <div className="flex items-center gap-5">
                        <span className="text-xs font-medium text-zinc-400">{dep.size}</span>
                        <a 
                          href={dep.path} 
                          download 
                          className="curved-btn-secondary px-4 py-1.5 text-xs"
                        >
                          Download
                        </a>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Quickstart Guide */}
      <section id="quickstart" className="px-6 py-24 bg-zinc-50/50">
        <div className="max-w-4xl mx-auto">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold tracking-tight text-zinc-900 mb-4">Quick Setup Guide</h2>
            <p className="text-zinc-500">Run the console utility to integrate the modules instantly.</p>
          </div>

          <div className="space-y-8">
            <div className="curved-card p-6">
              <h3 className="font-semibold text-zinc-900 mb-4 flex items-center gap-2">
                <span className="w-6 h-6 rounded-full bg-zinc-100 text-zinc-800 text-xs font-bold flex items-center justify-center">1</span>
                Automatic Install
              </h3>
              <p className="text-sm text-zinc-500 mb-5 leading-relaxed">
                Download the <strong>Aero.Installer.exe</strong> executable, place it in your FiveM server root folder and run it. The installer will dynamically download all core DLLs, dependencies and build the `fxmanifest.lua`.
              </p>
              <div className="relative">
                <button 
                  onClick={() => copyToClipboard('Aero.Installer.exe', 'cmd-install')}
                  className="absolute right-4 top-4 px-3 py-1.5 rounded-lg bg-white border border-zinc-200 text-xs font-semibold text-zinc-600 hover:bg-zinc-50 transition-colors shadow-sm"
                >
                  {copied === 'cmd-install' ? 'Copied! ✓' : 'Copy'}
                </button>
                <pre className="code-container font-mono text-sm overflow-x-auto">
                  <code>./Aero.Installer.exe</code>
                </pre>
              </div>
            </div>

            <div className="curved-card p-6">
              <h3 className="font-semibold text-zinc-900 mb-4 flex items-center gap-2">
                <span className="w-6 h-6 rounded-full bg-zinc-100 text-zinc-800 text-xs font-bold flex items-center justify-center">2</span>
                Start Resource
              </h3>
              <p className="text-sm text-zinc-500 mb-5 leading-relaxed">
                Add the following load line to your server configuration file (usually <strong>server.cfg</strong>):
              </p>
              <div className="relative">
                <button 
                  onClick={() => copyToClipboard('ensure aero-core', 'cfg-start')}
                  className="absolute right-4 top-4 px-3 py-1.5 rounded-lg bg-white border border-zinc-200 text-xs font-semibold text-zinc-600 hover:bg-zinc-50 transition-colors shadow-sm"
                >
                  {copied === 'cfg-start' ? 'Copied! ✓' : 'Copy'}
                </button>
                <pre className="code-container font-mono text-sm overflow-x-auto">
                  <code>ensure aero-core</code>
                </pre>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-white border-t border-zinc-200 px-6 py-12 text-center text-sm text-zinc-400">
        <div className="max-w-6xl mx-auto flex flex-col md:flex-row items-center justify-between gap-6">
          <div className="flex items-center gap-3">
            <Image 
              src="/logo.png" 
              alt="Aero Logo" 
              width={24} 
              height={24} 
              className="object-contain grayscale"
            />
            <span className="font-medium text-zinc-700">Aero Framework</span>
          </div>
          <p>© {new Date().getFullYear()} Vertex Tools. Released under the MIT License.</p>
        </div>
      </footer>
    </div>
  );
}
