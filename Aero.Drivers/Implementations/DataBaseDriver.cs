// ====================================
// <copyright file="DataBaseDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Threading.Tasks;
using Aero.API.Features;
using Aero.API.Interfaces;
using CitizenFX.Core;

namespace Aero.Drivers.Implementations
{
    /// <summary>
    /// The DataBaseDriver class is responsible for managing database operations
    /// such as initialization, executing queries, and shutting down the database connection.
    /// It implements the IDataBaseDriver interface.
    /// </summary>
    public class DataBaseDriver : BaseScript, IDataBaseDriver
    {
        public void Initialize()
        {
            Log.Success("[Aero - Drivers] -> DataBase Driver Initialized");
        }

        public void Shutdown()
        {
            Log.Success("[Aero - Drivers] -> DataBase Driver Shutdown"); 
        }

        public async Task<T> QueryAsync<T>(string query, object parameters = null)
        {
            try
            {
                var result = await Exports["oxmysql"].query_async(query, parameters);
                return result != null ? (T)result : default;
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - Drivers] -> Failed to query database: {e.Message}");
                return default;
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            try
            {
                var rowsAffected = await Exports["oxmysql"].update_async(query, parameters);
                return Convert.ToInt32(rowsAffected);
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - Drivers] -> Failed to query database: {e.Message}");
                return 0;
            }
        }

        public void Execute(string query, object parameters = null)
        {
            try
            {
                Exports["oxmysql"].update(query, parameters);
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - Drivers] -> Failed to query database: {e.Message}");
            }
        }
    }
}