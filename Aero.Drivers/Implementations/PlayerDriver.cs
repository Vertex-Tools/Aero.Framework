// ====================================
// <copyright file="PlayerDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Collections.Generic;
using Aero.API.Features;
using Aero.API.Interfaces;
using CitizenFX.Core;
using Player = Aero.API.Features.Player;

namespace Aero.Drivers.Implementations
{
    /// <summary>
    /// The PlayerDriver class is responsible for managing player-related functions
    /// such as initialization, shutdown, and access to player data. It implements
    /// the IPlayerDriver interface providing the core functionalities needed to
    /// control player actions within the Aero system.
    /// </summary>
    public class PlayerDriver : BaseScript, IPlayerDriver
    {
        private readonly Dictionary<int, Player> _activePlayers = new();
        
        public void Initialize()
        {
            Log.Success("[Aero - Drivers] -> Player Driver Initialized");
        }

        public void Shutdown()
        {
            Log.Success("[Aero - Drivers] -> Player Driver Shutdown");
        }

        public Player GetPlayer(int playerId)
        {
            if (_activePlayers.TryGetValue(playerId, out var player))
                return player;
            return null;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return Player.List;
        }

        public async void SavePlayerData(Player player)
        {
            try
            {
                var dbPlayer = Registry.DriverRegistry.Singleton.Get<IDataBaseDriver>();
                string query = "UPDATE users SET last_known_name = @Name WHERE license = @License";

                var parameters = new
                {
                    Name = player.Name,
                    License = player.License
                };
                
                int rowsAffected = await dbPlayer.ExecuteAsync(query, parameters);

                if (rowsAffected > 0)
                    Log.Success($"[Aero - Drivers] -> Player ({player.Name}) Data Saved");
            }
            catch (Exception e)
            {
                Log.Error($"[Aero - Drivers] -> Failed to save player data: {e.Message}");
            }
        }
    }
}