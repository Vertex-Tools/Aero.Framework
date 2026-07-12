using System;
using Aero.API;
using Aero.API.Interfaces;
using Aero.API.Features;
using Version = System.Version;

namespace Aero.SamplePlugin
{
    public class SampleConfig : IConfig
    {
        public bool Enabled { get; set; } = true;
        public string GreetingMessage { get; set; } = "Ciao dal server di test Aero Framework!";
    }

    public class SamplePlugin : Plugin<SampleConfig>
    {
        public override string Name => "Aero.SamplePlugin";
        public override string Author => "Antigravity AI";
        public override string Description => "Un plugin di esempio per testare Aero.Framework.";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredFramework => new Version(1, 0, 0);

        public static SampleConfig Config { get; set; }

        public override void OnLoad()
        {
            Log.Success($"{Name} caricato con successo!");
            Log.Info($"Messaggio di benvenuto configurato: {Config.GreetingMessage}");
        }

        public override void OnUnload()
        {
            Log.Warn($"{Name} scaricato.");
        }
    }

    public class HelloCommand : ICommand
    {
        public string Name => "hello";
        public string Description => "Comando di test di Aero Framework.";
        public string Permission => "user";

        public void Execute(int source, string[] args)
        {
            Player pl = Player.Get(source);
            Log.Success($"[Aero] Il comando /hello e' stato eseguito con successo dall'utente {pl.Name}!");
        }
    }
}
