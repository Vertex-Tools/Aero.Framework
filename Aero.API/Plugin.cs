// ====================================
// <copyright file="IPlugin.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Interfaces;

public abstract class Plugin<Tconfig> where Tconfig : class, IConfig, new()
{
    public abstract string Name { get; }
    public abstract string Author { get; }
    public abstract string Description { get; }
    public abstract System.Version Version { get; }
    public abstract System.Version RequiredFramework { get; }

    public abstract void OnLoad();
    public abstract void OnUnload();

    public void Enable()
    {
        OnLoad();
    }

    public void Disable()
    {
        OnUnload();
    }
}