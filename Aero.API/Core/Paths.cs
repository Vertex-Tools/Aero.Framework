// ====================================
// <copyright file="Paths.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.IO;

namespace Aero.API.Core
{
    public static class Paths
    {
        public static string AppData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Application Data folder.
        public static string Root { get; private set; } // Root folder of the framework.
        public static string Plugins { get; private set; }
        public static string Config { get; private set; }

        public static void Init(string rootDirectory = null)
        {
            rootDirectory ??= Path.Combine(AppData, "Aero.Framework");
            Root = rootDirectory;
            
            Plugins = Path.Combine(Root, "Plugins");
            Config = Path.Combine(Root, "Config");
            
            Directory.CreateDirectory(Root);
            Directory.CreateDirectory(Plugins);
            Directory.CreateDirectory(Config);
        }

        public static string GetPluginConfigFolder(string pluginName)
        {
            string folder = Path.Combine(Config, pluginName);
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GetPluginConfigPath(string pluginName)
        {
            return Path.Combine(GetPluginConfigFolder(pluginName), "config.yml");
        }
    }
}