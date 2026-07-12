// ====================================
// <copyright file="EntityDispatcher.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using Aero.API.Enums;
using Aero.API.Events.Network;
using Avian.Event;
using CitizenFX.Core;

namespace Aero.Events.Dispatchers
{
    /// <summary>
    /// The EntityDispatchers class is responsible for handling events related to the creation of entities within the system.
    /// It extends the BaseScript class and subscribes to entity-related events to publish corresponding events through a dispatcher.
    /// </summary>
    public class EntityDispatchers : BaseScript
    {
        private readonly Dispatcher _dispatcher;

        public EntityDispatchers(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            EventHandlers["entityCreating"] += new Action<int>(OnEntityCreating);
            EventHandlers["entityCreated"] += new Action<int>(OnEntityCreated);
        }

        private void OnEntityCreating(int entityHandle)
        {
            uint model = (uint)CitizenFX.Core.Native.API.GetEntityModel(entityHandle);
            int rawType = CitizenFX.Core.Native.API.GetEntityType(entityHandle);
            EntityType type = (EntityType)(1 << (rawType - 1));
            int owner = CitizenFX.Core.Native.API.NetworkGetEntityOwner(entityHandle);
            
            _dispatcher.Publish(new EntityCreatingEvent(entityHandle, model, type, owner));
        }
        
        private void OnEntityCreated(int entityHandle)
        {
            int rawType = CitizenFX.Core.Native.API.GetEntityType(entityHandle);
            EntityType type = (EntityType)(1 << (rawType - 1));
            
            _dispatcher.Publish(new EntityCreatedEvent(entityHandle, type));
        }
    }
}