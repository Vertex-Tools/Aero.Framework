// ====================================
// <copyright file="ObjectDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Aero.API.Features;
using Aero.API.Interfaces;
using CitizenFX.Core;

namespace Aero.Drivers.Implementations
{
    /// <summary>
    /// The ObjectDriver class provides functionality for managing and interacting with objects within the system
    /// using the CitizenFX framework. It implements the IObjectDriver interface and extends the BaseScript class.
    /// The class allows for initialization, shutdown, creation of network objects, and managing the existence and deletion
    /// of objects.
    /// </summary>
    public class ObjectDriver : BaseScript, IObjectDriver
    {
        public void Initialize()
        {
            Log.Success("[Aero - Drivers] -> Object Driver Initialized");
        }

        public void Shutdown()
        {
            Log.Success("[Aero - Drivers] -> Object Driver Shutdown");  
        }

        public int CreateNetworkObject(uint modelHash, Vector3 position, Vector3 rotation)
        {
            int networkObject = CitizenFX.Core.Native.API.CreateObject((int)modelHash, position.X, position.Y, position.Z, true, true, false);
            CitizenFX.Core.Native.API.SetEntityRotation(networkObject, rotation.X, rotation.Y, rotation.Z, 2, true);
            return networkObject;
        }

        public bool DeleteObject(int entityHandle)
        {
            if (CitizenFX.Core.Native.API.DoesEntityExist(entityHandle))
            {
                CitizenFX.Core.Native.API.DeleteEntity(entityHandle);
                return true;
            }
            return false;
        }

        public bool DoesObjectExist(int entityHandle)
        {
            if (CitizenFX.Core.Native.API.DoesEntityExist(entityHandle))
                return true;
            return false;
        }
    }
}