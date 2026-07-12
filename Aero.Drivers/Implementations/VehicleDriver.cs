// ====================================
// <copyright file="VehicleDriver.cs" company="Vertex Tools">
// Copyright (c) Aero.Framework. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using System.Threading.Tasks;
using Aero.API.Features;
using Aero.API.Interfaces;
using CitizenFX.Core;

namespace Aero.Drivers.Implementations
{
    /// <summary>
    /// The VehicleDriver class implements the IVehicleDriver interface, providing functionalities to
    /// initialize, shutdown, and manage vehicle entities within a game environment. This includes
    /// spawning, deleting, and configuring vehicle properties such as the license plate.
    /// The class extends the BaseScript, enabling it to utilize the scripting capabilities provided by
    /// the CitizenFX framework. As a key component of the Aero framework's vehicle handling system,
    /// it ensures seamless vehicle interactions.
    /// </summary>
    public class VehicleDriver : BaseScript, IVehicleDriver
    {
        public void Initialize()
        {
            Log.Success("[Aero - Drivers] -> Vehicle Driver Initialized");
        }

        public void Shutdown()
        {
            Log.Success("[Aero - Drivers] -> Vehicle Driver Shutdown");   
        }

        public async Task<int> SpawnVehicleAsync(uint modelHash, Vector3 position, float heading)
        {
            int vehicleHandle = CitizenFX.Core.Native.API.CreateVehicle(modelHash, position.X, position.Y, position.Z, heading, true, true);
            int timeout = 0;
            while (!CitizenFX.Core.Native.API.DoesEntityExist(vehicleHandle) && timeout < 100)
            {
                await Delay(10);
                timeout++;
            }
            return vehicleHandle;
        }

        public void DeleteVehicle(int vehicleHandle)
        {
            if(CitizenFX.Core.Native.API.DoesEntityExist(vehicleHandle))
                CitizenFX.Core.Native.API.DeleteEntity(vehicleHandle);
        }

        public void SetVehicleProperties(int vehicleHandle, dynamic properties)
        {
            if (!CitizenFX.Core.Native.API.DoesEntityExist(vehicleHandle))
                return;
            
            if (properties.Plate != null)
                CitizenFX.Core.Native.API.SetVehicleNumberPlateText(vehicleHandle, (string)properties.Plate);
            
        }
    }
}