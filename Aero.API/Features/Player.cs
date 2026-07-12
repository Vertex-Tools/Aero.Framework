// ====================================
// <copyright file="Player.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Aero.API.Features;

/// <summary>
/// Official Aero Player class where implements everything related to the native player...but easier!
/// </summary>
public class Player
{
    private static Dictionary<CitizenFX.Core.Player, Player> Cache = new(); // Cache of players.   
    public CitizenFX.Core.Player ReferenceHub { get; } // Base where we implement the native FiveM Player class.
    
    public string Name => ReferenceHub.Name ?? "Unknown";
    public string Handle => ReferenceHub.Handle;
    public int Ping => ReferenceHub.Ping;
    public string EndPoint => ReferenceHub.EndPoint;
    
    public int Id { get; } 
    public string License { get; }

    public Player(CitizenFX.Core.Player Refhub)
    {
        ReferenceHub = Refhub;
        
        if(int.TryParse(Refhub.Handle, out int id))
            Id = id;
        else
            Id = 0;
        License = ReferenceHub.Identifiers["license"] ?? "None";
    }

    #region Utilities
    
    public void Kick(string reason)
    {
        ReferenceHub.Drop(reason);
    }

    public void Trigger(string eventName, params object[] args)
    {
        ReferenceHub.TriggerEvent(eventName, args);
    }
    
    #endregion

    #region CacheManagementAndStaticMethods

    public static IReadOnlyCollection<Player> List => Cache.Values.ToList(); // Returns a list of all players.
    public int Count => Cache.Count;

    
    /// <summary>
    /// Here are written all the methods to get a player from the cache.
    /// </summary>
    /// <param name="refHub"></param>
    /// <returns></returns>
    public static Player Get(CitizenFX.Core.Player refHub)
    {
        if(refHub == null)
            return null;
        if (!Cache.TryGetValue(refHub, out var player))
        {
            player = new Player(refHub);
            Cache.Add(refHub, player);
        }
        return player;
    }

    public static Player Get(int playerId)
    {
        return Cache.Values.FirstOrDefault(x => x.Id == playerId);
    }

    public static Player Get(string license)
    {
        return Cache.Values.FirstOrDefault(p => p.License.Equals(license, StringComparison.OrdinalIgnoreCase));
    }

    // Removes a player from the cache.
    public static void Remove(CitizenFX.Core.Player refHub)
    {
        if(refHub != null)
            Cache.Remove(refHub);
    }
    public static Player Get(Player playerId)
    {
        return Cache.Values.FirstOrDefault(x => x.Id == playerId.Id);
    }
    #endregion

    #region PlayerStats

    /// <summary>
    /// Gets the player's health.'
    /// </summary>
    public int Health
    {
        get => CitizenFX.Core.Native.API.GetEntityHealth(CitizenFX.Core.Native.API.GetPlayerPed(Handle));
    }

    /// <summary>
    /// Gets the player's max health.'
    /// </summary>
    public int MaxHealth
    {
        get => CitizenFX.Core.Native.API.GetEntityMaxHealth(CitizenFX.Core.Native.API.GetPlayerPed(Handle));
    }
   
    /// <summary>
    /// Gets the player's armor.'
    /// </summary>
    public int Armor
    {
        get => CitizenFX.Core.Native.API.GetPedArmour(CitizenFX.Core.Native.API.GetPlayerPed(Handle));
        set => CitizenFX.Core.Native.API.SetPedArmour(CitizenFX.Core.Native.API.GetPlayerPed(Handle), value);
    }

    /// <summary>
    /// Gets the player's max armor.'
    /// </summary>
    public int MaxArmor => CitizenFX.Core.Native.API.GetPlayerMaxArmour(Handle);

    /// <summary>
    /// Gets a value indicating whether the player is alive.
    /// </summary>
    public bool IsAlive => !IsDead;

    /// <summary>
    /// Gets a value indicating whether the player is dead.
    /// </summary>
    public bool IsDead => Health <= 0;

    #endregion
}