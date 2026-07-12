// ====================================
// <copyright file="CommandRegister.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aero.API.Features;
using Aero.API.Interfaces;

namespace Aero.Core.Loader;

public class CommandRegister
{
    private readonly List<string> _registeredCommands = new(); // List of all registered commands.

    /// <summary>
    /// Main register method for all commands.
    /// </summary>
    /// <param name="assembly"></param>
    public void RegisterAssemblyCommands(Assembly assembly)
    {
        if (assembly == null)
            return;
        
        var commandType = assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in commandType)
        {
            try
            {
                var commandInstance = (ICommand)Activator.CreateInstance(type);
                CitizenFX.Core.Native.API.RegisterCommand(commandInstance.Name, new Action<int, List<object>, string>((
                source, args, raw) =>
                {
                    string[] stringArgs = args.Select(a => a?.ToString() ?? "").ToArray();
                    
                    //TODO: Perms check
                    
                    commandInstance.Execute(source, stringArgs);
                }), false);
                _registeredCommands.Add(commandInstance.Name);
                Log.Success($"[Aero - CommandRegister] -> Command registered: /{commandInstance.Name}, Description: {commandInstance.Description}");
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - CommandRegister] -> Failed to register command: {type.FullName}, Reason: {e.Message}");
            }
        }
    }
}