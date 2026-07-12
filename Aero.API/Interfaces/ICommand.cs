// ====================================
// <copyright file="ICommand.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

namespace Aero.API.Interfaces;

/// <summary>
/// Represents a command that can be executed within the application.
/// </summary>
public interface ICommand
{
    string Name { get; }
    string Description { get; }
    string Permission { get; }
    
    void Execute(int source, string[] args);
}