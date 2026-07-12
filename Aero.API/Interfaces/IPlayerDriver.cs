// ====================================
// <copyright file="IPlayerDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System.Collections.Generic;
using Aero.API.Features;

namespace Aero.API.Interfaces;

/// <summary>
/// Represents the contract for controlling player actions within the system.
/// </summary>
public interface IPlayerDriver : IDriver
{
    Player GetPlayer(int playerId);
    IEnumerable<Player> GetAllPlayers();
    void SavePlayerData(Player player);
}