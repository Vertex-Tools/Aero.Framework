// ====================================
// <copyright file="SecurityDispatchers.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using CitizenFX.Core;
using Aero.API.Events.Security;
using Avian.Event;

namespace Aero.Events.Dispatchers
{
    public class SecurityDispatchers : BaseScript
    {
        private readonly Dispatcher _dispatcher;

        public SecurityDispatchers(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            EventHandlers["clearPedTasksEvent"] += new Action<Player, dynamic>(OnClearPedTasks);
        }

        private void OnClearPedTasks([FromSource] Player sender, dynamic data)
        {
            if (sender == null) return;

            int senderId = int.Parse(sender.Handle);
            int targetPed = (int)(data.pedId ?? 0);

            _dispatcher.Publish(new ClearPedTasksEvent(senderId, targetPed));
        }
    }
}