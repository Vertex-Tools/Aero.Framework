// ====================================
// <copyright file="IConfig.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Interfaces;

/// <summary>
/// Represents the configuration settings for a plugin.
/// </summary>
public interface IConfig
{
    bool Enabled { get; set; } // Whether the plugin is enabled.
}