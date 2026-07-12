// ====================================
// <copyright file="CombatDispatchers.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System;
using Aero.API.Enums;
using Aero.API.Events.Combat;
using Avian.Event;
using CitizenFX.Core;
using AeroPlayer = Aero.API.Features.Player; 

namespace Aero.Events.Dispatchers
{
    /// <summary>
    /// The CombatDispatchers class is responsible for handling and dispatching events related to combat in the game environment.
    /// Inherits from BaseScript to integrate the event-driven architecture commonly used in CitizenFX-based applications.
    /// </summary>
    public class CombatDispatchers : BaseScript
    {
        private readonly Dispatcher _dispatcher;
        
        public CombatDispatchers(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            EventHandlers["weaponDamageEvent"] += new Action<CitizenFX.Core.Player, dynamic>(OnWeaponDamage);
            EventHandlers["explosionEvent"] += new Action<CitizenFX.Core.Player, dynamic>(OnExplosion);
            EventHandlers["gameEventTriggered"] += new Action<string, dynamic>(OnGameEventTriggered);
        }

        private void OnWeaponDamage([FromSource] CitizenFX.Core.Player attacker, dynamic data)
        {
            if (attacker == null)
                return;
            
            AeroPlayer aeroPlayer = AeroPlayer.Get(attacker); // Change the Native Player class to our advanced player class.
            
            int attacketId = aeroPlayer.Id; 
            uint targeEntity = (uint)(data.HitGlobalId ?? 0);
            uint weaponHash = (uint)(data.weaponType ?? 0);
            DamageType damageType = (DamageType)(data.damageType ?? 0);
            bool isVehicle = (bool)(data.hasVehicleWeapon ?? false);
            
            _dispatcher.Publish(new WeaponDamageEvent(attacketId, targeEntity, weaponHash, damageType, isVehicle));
        }

        private void OnExplosion([FromSource] CitizenFX.Core.Player sender, dynamic data)
        {
            if (sender == null) 
                return;
                
            AeroPlayer aeroPlayer = AeroPlayer.Get(sender);
            
            int senderId = aeroPlayer.Id;
            int rawType = (int)(data.explosionType ?? 0);
            ExplosionType type = (ExplosionType)(1 << rawType);
            
            Vector3 position = new Vector3((float)data.x, (float)data.y, (float)data.z);
            _dispatcher.Publish(new ExplosionEvent(senderId, type, position));
        }

        private void OnGameEventTriggered(string name, dynamic args)
        {
            if (name != "CEventNetworkPlayerDamage")
                return;
                
            int victimId = (int)(args[0] ?? 0);
            int attackerId = (int)(args[1] ?? 0);
            uint weaponHash = (uint)(args[2] ?? 0);
            bool isHeadshot = (int)(args[3] ?? 0) == 1;
            DamageTierType tierType = (DamageTierType)(args[4] ?? 0);
            
            _dispatcher.Publish(new PlayerDeathAdvancedEvent(victimId, attackerId, weaponHash, isHeadshot, tierType));
        }
    }
}