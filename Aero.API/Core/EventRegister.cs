// ====================================
// <copyright file="EventRegister.cs" company="Vertex Tools">
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

namespace Aero.API.Core;

public class EventRegister
{
    private readonly List<IEvent> _events = new(); // List of all registered events.

    /// <summary>
    /// Registers ALL the assemblies with the interface IEvent.
    /// </summary>
    /// <param name="assembly"></param>
    public void RegisterAssembly(Assembly assembly)
    {
        if (assembly == null) 
            return;
        
        // Gets only the types that implement the interface IEvent.
        var types = assembly.GetTypes()
            .Where(t => typeof(IEvent).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            try
            {
                var instance = (IEvent) Activator.CreateInstance(type);
                instance.Register();
                _events.Add(instance);
                Log.Success($"[Aero - EventRegister] -> [{assembly.GetName().Name}] Event registered: {type.FullName}"); // Prints the result
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - EventRegister] -> [{assembly.GetName().Name}] Failed to register event: {type.FullName}"); // Prints the error
            }
        }
    }

    /// <summary>
    /// Unregisters ALL the registered events.
    /// </summary>
    public void UnregisterAll()
    {
        foreach (var @event in _events)
        {
            try
            {
                @event.Unregister();
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - EventRegister] -> Failed to unregister event: {@event.GetType().FullName}");
            }
        }
        _events.Clear();
    }
}