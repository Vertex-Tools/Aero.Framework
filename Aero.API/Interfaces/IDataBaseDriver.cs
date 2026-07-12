// ====================================
// <copyright file="IDataBaseDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System.Threading.Tasks;

namespace Aero.API.Interfaces;

/// <summary>
/// Provides an interface for database driver implementations, defining the necessary
/// operations and properties required for interacting with a database.
/// </summary>
public interface IDataBaseDriver : IDriver
{
    Task<T> QueryAsync<T>(string query, object parameters = null);
    Task<int> ExecuteAsync(string query, object parameters = null);
    void Execute(string query, object parameters = null);
}