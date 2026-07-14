// ====================================
// <copyright file="PluginLoader.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aero.API;
using Aero.API.Core;
using Aero.API.Features;
using Aero.API.Interfaces;
using Version = System.Version;

namespace Aero.Core.Loader
{
    public class PluginLoader
    {
        public static readonly List<object> LoadedPlugins = new(); // List of all loaded plugins.
        public CommandRegister CommandRegister { get; } = new(); // Command Register Handler.
        public bool IsPluginType(Type type)
        {
            var baseType = type;
            while (baseType != null && baseType != typeof(object))
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Plugin<>))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        } // Checks if the type is a plugin.
        
        /// <summary>
        /// Private void built to resolve embedded assemblies.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Assembly ResolveEmbeddedAssembly(object sender, ResolveEventArgs args)
        {
            var asmName = new AssemblyName(args.Name).Name + ".dll"; // Extension .dll
            var currentAsm = Assembly.GetExecutingAssembly(); // Current assembly
            var resourceName = currentAsm.GetManifestResourceNames() //string[]
                .FirstOrDefault(r => r.EndsWith(asmName));

            if (resourceName == null)
                return null;
            
            using (var stream = currentAsm.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    return null;
                
                //Reads all the bytes from the stream into a byte array.
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return Assembly.Load(data);
            }
        }

        /// <summary>
        /// Secondary Method that Loads all plugins in the Root Framework folder.
        /// </summary>
        /// <param name="pluginFolder">Root folder</param>
        private void LoadFromFolder(string pluginFolder)
        {
            if(!Directory.Exists(pluginFolder))
                return;
            
            var files = Directory.GetFiles(pluginFolder, "*.dll");

            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    CommandRegister.RegisterAssemblyCommands(assembly);
                    var pluginType = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && IsPluginType(t));
                    foreach (var type in pluginType)
                    {
                        // Register from the main void
                        RegisterPlugin(type);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"[Aero - PluginLoader] -> Failed to load plugin: {file}");
                }
            }
        }

        /// <summary>
        /// Official Main method to load plugins.
        /// </summary>
        /// <param name="type"></param>
        public void RegisterPlugin(Type type)
        {
            try
            {
                var pluginInstance = Activator.CreateInstance(type);
                string pluginName = GetProperty(pluginInstance, "Name");
                
                var configProperty = type.GetProperty("Config");
                if (configProperty != null && typeof(IConfig).IsAssignableFrom(configProperty.PropertyType))
                {
                    var configPath = Paths.GetPluginConfigPath(pluginName);
                    var configValue = ConfigBuilder.LoadConfig(
                        configProperty.PropertyType, configPath);
                    
                    configProperty.SetValue(pluginInstance, configValue);
                }

                var requireProp = type.GetProperty("RequiredFramework"); // Checks if the plugin requires a specific version of the framework.
                if (requireProp != null && typeof(Version).IsAssignableFrom(requireProp.PropertyType))
                {
                    var requiredVersion = (Version)requireProp.GetValue(pluginInstance);
                    if(requiredVersion > API.Version.CoreCurrent)
                    {
                        Log.Error($"[Aero - PluginLoader] -> Skipped loading plugin '{pluginName}' because it requires framework version {requiredVersion}, but current version is {API.Version.CoreCurrent}.");
                        return;
                    }
                }
                LoadedPlugins.Add(pluginInstance);
                bool enabled = true;

                if (configProperty != null)
                {
                    var enabledProp = configProperty.PropertyType.GetProperty("Enabled");
                    var cfg = configProperty.GetValue(pluginInstance);

                    if (enabledProp != null && enabledProp.GetValue(cfg) is bool b)
                        enabled = b;
                }
                
                //if it is enabled
                if (enabled)
                {
                    var onLoad = type.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    Log.SetPlugin(pluginName);
                    onLoad?.Invoke(pluginInstance, null);
                    Log.ClearPlugin();
                }
                //metadata
                string pluginAuthor = GetProperty(pluginInstance, "Author");
                string pluginVersion = GetProperty(pluginInstance, "Version");
                string pluginDescription = GetProperty(pluginInstance, "Description");
                
                Log.Info($"[Aero - PluginLoader] -> Loaded plugin: {pluginName}@{pluginVersion} by {pluginAuthor}, Description: {pluginDescription}");
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - PluginLoader] -> Failed to load plugin: {type.FullName}, Reason: {e.Message}");
            }
        }
        
        /// <summary>
        /// Official method to unload plugins.
        /// </summary>
        public void UnloadPlugins()
        {
            foreach (var plugin in LoadedPlugins)
            {
                try
                {   
                    var type = plugin.GetType();
                    string pluginName = GetProperty(plugin, "Name");
                    
                    var configProperty = type.GetProperty("Config");
                    if (configProperty != null)
                    {
                        var configPath = Paths.GetPluginConfigPath(pluginName);
                        var configValue = configProperty.GetValue(plugin);
                        ConfigBuilder.SaveConfig(configValue, configPath);
                    }
                    
                    Log.SetPlugin(pluginName);
                    var onUnload = type.GetMethod("OnUnload", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    onUnload?.Invoke(plugin, null);
                    Log.ClearPlugin();
                }
                catch (Exception e)
                {
                    // ingnored
                }
            }
        }
        
        /// <summary>
        /// Gets a property from a plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string GetProperty(object plugin, string propertyName)
        {
            var prop = plugin.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var value = prop?.GetValue(plugin);
            return value?.ToString() ?? "(unknown)";
        }
        
        public string PluginFolder => Paths.Plugins; // Plugin folder.
        public string ConfigFolder => Paths.Config; // Config folder.

        // List of all loaded plugins.
        public void ListPlugins()
        {
            Log.Info("[Aero - PluginLoader] -> Loaded plugins:");
            foreach (var plugin in LoadedPlugins)
            {
                string pluginName = GetProperty(plugin, "Name");
                Log.Info($" - {pluginName}");
            }
        }
    }
}