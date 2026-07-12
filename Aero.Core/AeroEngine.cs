// ====================================
// <copyright file="AeroEngine.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Threading.Tasks;
using Aero.API.Core;
using Aero.API.Features;
using Aero.Core.Loader;
using Aero.Drivers.Registry;
using Aero.Events.Dispatchers;
using Avian.Event;
using CitizenFX.Core;

namespace Aero.Core
{
    /// <summary>
    /// Main entry point of the framework.
    /// </summary>
    public class AeroEngine : BaseScript
    {
        private readonly PluginLoader _pluginLoader;
        private bool _isInitialized;
        private Dispatcher _dispatcher; // Dispatcher for Aero Event system.
        private DriverRegistry _driverRegistry = new();
        public static AeroEngine Singleton { get; private set; } // Instance of the Aero Engine.
        
        
        public AeroEngine()
        {
            AppDomain.CurrentDomain.AssemblyResolve += PluginLoader.ResolveEmbeddedAssembly;
            _pluginLoader = new PluginLoader();

            // Native fiveM event handlers.
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
        }

        private void OnResourceStart(string resourceName)
        {
            // Ensure we only bootstrap when OUR resource is started. 
            if(CitizenFX.Core.Native.API.GetCurrentResourceName() != resourceName)
                return;
            
            if(_isInitialized)
                return;

            Singleton = this;
            
            try
            {
                string resourcePath = CitizenFX.Core.Native.API.GetResourcePath(resourceName); // Resolve resource path.
                Paths.Init(resourcePath);
                
                Log.SendRaw("Aero Framework", ConsoleColor.Red);
                Log.SendRaw($"Core Version: {API.Version.CoreCurrent}", ConsoleColor.Red);
                Log.SendRaw($"API Version: {API.Version.ApiCurrent}", ConsoleColor.Red);
                Log.Info("Welcome to:");
                Log.SendRaw(@"
 ________  _______   ________  ________     
|\   __  \|\  ___ \ |\   __  \|\   __  \    
\ \  \|\  \ \   __/|\ \  \|\  \ \  \|\  \   
 \ \   __  \ \  \_|/_\ \   _  _\ \  \\\  \  
  \ \  \ \  \ \  \_|\ \ \  \\  \\ \  \\\  \ 
   \ \__\ \__\ \_______\ \__\\ _\\ \_______\
    \|__|\|__|\|_______|\|__|\|__|\|_______| #Made by Vertex Tools.
                                            
                                          ", ConsoleColor.White);
                Log.Info("Initializing Aero Drivers...");
                _driverRegistry.InitializeAll();
                Log.Success("Driver initialized ✓");
                
                Log.Info("Initializing Aero Events...");
                _dispatcher = new Dispatcher(32, 1024); // 32 types of events in 1024 slots is the perfect number for performance and stability.
                
                new CombatDispatchers(_dispatcher);
                new EntityDispatchers(_dispatcher);
                new NetworkDispatchers(_dispatcher);
                new PlayerDispatchers(_dispatcher);
                new SecurityDispatchers(_dispatcher);

                Tick += OnTick;
                
                Log.Success("Events initialized ✓");
                
                Log.Info("Scanning and loading external resources...");
                
                // Reflection to invoke internal method.
                var interalLoadMethod = typeof(PluginLoader).GetMethod("LoadFromFolder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (interalLoadMethod != null)
                {
                    interalLoadMethod.Invoke(_pluginLoader, new object[] { Paths.Plugins });
                }
                else
                {
                    Log.Warn("[Aero - AeroEngine] -> Direct LoadFromFolder method binding mismatch. Initializing default layout.");
                }
                _pluginLoader.ListPlugins();
                _isInitialized = true;
                
                Log.Success("Aero Engine initialized successfully ✓");
                Log.ClearPlugin();
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - AeroEngine] -> Critical failure during Aero Framework bootstrap phase: {e.Message}");
            }
        }

        /// <summary>
        /// On stop of the framework.
        /// </summary>
        /// <param name="resourceName"></param>
        private void OnResourceStop(string resourceName)
        {
            if(CitizenFX.Core.Native.API.GetCurrentResourceName() != resourceName)
                return;

            try
            {
                Log.SetPlugin("Aero Framework");
                Log.Warn("Stopping resource execution. Unloading active hooks...");
                
                Singleton = null;
                
                // Unhook all events to prevent memory leaks.
                Tick -= OnTick;
                _driverRegistry.ShutdownAll(); // Shutdown all drivers.
                _dispatcher = null;
                
                _pluginLoader.UnloadPlugins();
                _pluginLoader.Register.UnregisterAll();
                
                Log.Success("All components unhooked clean. Core down.");
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - AeroEngine] -> Critical failure during Aero Framework shutdown phase: {e.Message}");
            }
        }

        // Main loop for events.
        private async Task OnTick()
        {
            if (_isInitialized && _dispatcher != null)
                _dispatcher.Execute();
            await Delay(0);
        }
    }
}