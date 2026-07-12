// ====================================
// <copyright file="NetworkDispatchers.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using Aero.API.Events.Network;
using Avian.Event;
using CitizenFX.Core;

namespace Aero.Events.Dispatchers
{
    public class NetworkDispatchers : BaseScript
    {
        private readonly Dispatcher _dispatcher;

        public NetworkDispatchers(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            
            EventHandlers["playerEnteredScope"] += new Action<string, string>(OnPlayerEnteredScope);
            EventHandlers["playerLeftScope"] += new Action<string, string>(OnPlayerLeftScope);
            EventHandlers["ptFxEvent"] += new Action<Player, dynamic>(OnPtFxEvent);
        }
        
        private void OnPlayerEnteredScope(string playerStr, string targetStr)
        {
            if (int.TryParse(playerStr, out int pId) && int.TryParse(targetStr, out int tId))
            {
                _dispatcher.Publish(new PlayerEnteredScopeEvent(pId, tId));
            }
        }

        private void OnPlayerLeftScope(string playerStr, string targetStr)
        {
            if (int.TryParse(playerStr, out int pId) && int.TryParse(targetStr, out int tId))
            {
                _dispatcher.Publish(new PlayerLeftScopeEvent(pId, tId));
            }
        }

        private void OnPtFxEvent([FromSource] Player sender, dynamic data)
        {
            if (sender == null) return;

            int senderId = int.Parse(sender.Handle);
            uint assetHash = (uint)(data.asset ?? 0);

            _dispatcher.Publish(new ParticleEffectEvent(senderId, assetHash));
        }
    }
}