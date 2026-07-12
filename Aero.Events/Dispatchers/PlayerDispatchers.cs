// ====================================
// <copyright file="PlayerDispatchers.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using Aero.API.Events.Network;
using Avian.Event;
using CitizenFX.Core;
using AeroPlayer = Aero.API.Features.Player;

namespace Aero.Events.Dispatchers
{
    /// <summary>
    /// Responsible for dispatching player-related events in a networked environment.
    /// </summary>
    public class PlayerDispatchers : BaseScript
    {
        private readonly Dispatcher _dispatcher;

        public PlayerDispatchers(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerJoining"] += new Action<Player>(OnPlayerJoining);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
        }
        
        private void OnPlayerConnecting([FromSource] Player native, string name, dynamic kickReason, dynamic deferrals)
        {
            if (native == null) return;
            
            AeroPlayer aeroPlayer = AeroPlayer.Get(native);
            _dispatcher.Publish(new PlayerJoiningEvent(aeroPlayer, kickReason, deferrals));
        }

        private void OnPlayerJoining([FromSource] Player native)
        {
            if (native == null) return;

            AeroPlayer aeroPlayer = AeroPlayer.Get(native);
            _dispatcher.Publish(new PlayerEnteringEvent(aeroPlayer.Id, aeroPlayer.Name));
        }

        private void OnPlayerDropped([FromSource] Player native, string reason)
        {
            if (native == null) return;

            AeroPlayer aeroPlayer = AeroPlayer.Get(native);
            _dispatcher.Publish(new PlayerLeftEvent(aeroPlayer.Id, aeroPlayer.Name, reason));
            
            AeroPlayer.Remove(native);
        }
    }
}