// ====================================
// <copyright file="Log.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Reflection;
using System.Threading;

namespace Aero.API.Features;

public static class Log
{
    private static readonly AsyncLocal<string> CurrentPlugin = new();
    public static void SetPlugin(string pluginName) => CurrentPlugin.Value = pluginName;
    public static string ClearPlugin() => CurrentPlugin.Value = null;

    // Send a raw message to the console.
    public static void SendRaw(object message, ConsoleColor color) => Console.WriteLine($"[{CurrentPlugin.Value}] {message}", color);
    public static void Info(object message) =>
        SendRaw($"[Aero - Info] - [{Assembly.GetCallingAssembly().GetName().Name}] {message}]", ConsoleColor.Cyan);
    public static void Warn(object message) =>
        SendRaw($"[Aero - Warn] - [{Assembly.GetCallingAssembly().GetName().Name}] {message}]", ConsoleColor.Yellow);
    public static void Error(object message) =>
        SendRaw($"[Aero - Error] - [{Assembly.GetCallingAssembly().GetName().Name}] {message}]", ConsoleColor.Red);
    public static void Debug(object message) =>
        SendRaw($"[Aero - Debug] - [{Assembly.GetCallingAssembly().GetName().Name}] {message}]", ConsoleColor.DarkMagenta);
    public static void Success(object message) =>
        SendRaw($"[Aero - Success] - [{Assembly.GetCallingAssembly().GetName().Name}] {message}]", ConsoleColor.Green);
}