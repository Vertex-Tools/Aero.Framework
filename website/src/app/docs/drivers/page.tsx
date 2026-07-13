import React from 'react';
import Link from 'next/link';

export default function DocsDrivers() {
  return (
    <div className="space-y-10">
      <div>
        <h1 className="text-4xl font-extrabold text-zinc-900 tracking-tight mb-4">Drivers Registry</h1>
        <p className="text-lg text-zinc-500 leading-relaxed">
          Interact with server systems safely using Aero's abstract Drivers Registry.
        </p>
      </div>

      <div className="h-px bg-zinc-200" />

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">The Central Driver Registry</h2>
        <p className="text-zinc-600 leading-relaxed">
          Aero abstracts core server-side services (like database connections, player listings, and vehicle entities) behind high-level interfaces called <strong>Drivers</strong>.
        </p>
        <p className="text-zinc-600 leading-relaxed">
          All drivers are managed by the <code className="px-1.5 py-0.5 rounded bg-zinc-100 border text-xs">DriverRegistry</code>. You can retrieve any active driver at runtime using the singleton instance getter:
        </p>
        <pre className="code-container">
          <code>{`using Aero.Drivers.Registry;
using Aero.API.Interfaces;

// Retrieve the database driver
var database = DriverRegistry.Singleton.Get<IDataBaseDriver>();

// Retrieve the player manager driver
var players = DriverRegistry.Singleton.Get<IPlayerDriver>();`}</code>
        </pre>
      </section>

      <section className="space-y-6">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">Supported Drivers & Interfaces</h2>

        {/* Database Driver */}
        <div className="space-y-3 p-5 border border-zinc-200 rounded-2xl bg-zinc-50/30">
          <h3 className="font-bold text-lg text-zinc-900">IDataBaseDriver</h3>
          <p className="text-sm text-zinc-500">
            Interfaces directly with the popular server resource <code className="px-1 py-0.5 bg-zinc-100 rounded text-xs">oxmysql</code> to handle asynchronous queries and commands with connection checking.
          </p>
          <pre className="code-container text-xs">
            <code>{`Task<T> QueryAsync<T>(string query, object parameters = null);
Task<int> ExecuteAsync(string query, object parameters = null);
void Execute(string query, object parameters = null);`}</code>
          </pre>
        </div>

        {/* Player Driver */}
        <div className="space-y-3 p-5 border border-zinc-200 rounded-2xl bg-zinc-50/30">
          <h3 className="font-bold text-lg text-zinc-900">IPlayerDriver</h3>
          <p className="text-sm text-zinc-500">
            Provides access to player structures, caching active user records, and persisting player data.
          </p>
          <pre className="code-container text-xs">
            <code>{`Player GetPlayer(int playerId);
IEnumerable<Player> GetAllPlayers();
void SavePlayerData(Player player);`}</code>
          </pre>
        </div>

        {/* Vehicle Driver */}
        <div className="space-y-3 p-5 border border-zinc-200 rounded-2xl bg-zinc-50/30">
          <h3 className="font-bold text-lg text-zinc-900">IVehicleDriver</h3>
          <p className="text-sm text-zinc-500">
            Handles asynchronous vehicle entity spawning, verification, deletion, and property updates.
          </p>
          <pre className="code-container text-xs">
            <code>{`Task<int> SpawnVehicleAsync(uint modelHash, Vector3 position, float heading);
void DeleteVehicle(int vehicleHandle);
void SetVehicleProperties(int vehicleHandle, dynamic properties);`}</code>
          </pre>
        </div>

        {/* Object Driver */}
        <div className="space-y-3 p-5 border border-zinc-200 rounded-2xl bg-zinc-50/30">
          <h3 className="font-bold text-lg text-zinc-900">IObjectDriver</h3>
          <p className="text-sm text-zinc-500">
            Spawns and monitors static or dynamic network object entities.
          </p>
          <pre className="code-container text-xs">
            <code>{`int CreateNetworkObject(uint modelHash, Vector3 position, Vector3 rotation);
bool DeleteObject(int entityHandle);
bool DoesObjectExist(int entityHandle);`}</code>
          </pre>
        </div>
      </section>

      <section className="space-y-4">
        <h2 className="text-2xl font-bold text-zinc-900 tracking-tight">C# Integration Example</h2>
        <p className="text-zinc-600 leading-relaxed">
          The following example shows how to query database records asynchronously using the database driver and spawn a vehicle for a player:
        </p>
        <pre className="code-container">
          <code>{`using System.Threading.Tasks;
using Aero.API.Interfaces;
using Aero.Drivers.Registry;
using CitizenFX.Core;

public class CarDealerService
{
    private readonly IDataBaseDriver _db;
    private readonly IVehicleDriver _vehicles;

    public CarDealerService()
    {
        _db = DriverRegistry.Singleton.Get<IDataBaseDriver>();
        _vehicles = DriverRegistry.Singleton.Get<IVehicleDriver>();
    }

    public async Task PurchaseAndSpawnCar(int playerId, uint carModelHash, Vector3 pos, float heading)
    {
        // 1. Check user bank balance asynchronously
        int balance = await _db.QueryAsync<int>("SELECT bank FROM users WHERE identifier = @Id", new { Id = playerId });

        if (balance >= 5000)
        {
            // 2. Deduct funds
            await _db.ExecuteAsync("UPDATE users SET bank = bank - 5000 WHERE identifier = @Id", new { Id = playerId });

            // 3. Spawn the vehicle entity
            int vehicleHandle = await _vehicles.SpawnVehicleAsync(carModelHash, pos, heading);
            
            // 4. Set custom plates
            _vehicles.SetVehicleProperties(vehicleHandle, new { Plate = "AERO1" });
            
            Log.Success($"Vehicle spawned successfully for player {playerId}!");
        }
    }
}`}</code>
        </pre>
      </section>

      <div className="h-px bg-zinc-200" />

      <div className="flex justify-between items-center pt-4">
        <Link href="/docs/events" className="curved-btn-secondary px-6 py-2.5 text-sm">
          ← Back: Events
        </Link>
        <span />
      </div>
    </div>
  );
}
