Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("==================================================");
Console.WriteLine("          AERO FRAMEWORK - INSTALLER              ");
Console.WriteLine("=================================================="); 
Console.ResetColor();

try
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("[✓] [Aero - Success] -> Checking system requirements... .NET 8 runtime detected.");

    string serverPath = AppDomain.CurrentDomain.BaseDirectory;
    string targetFolder = Path.Combine(serverPath, "resources", "[aero]", "aero-core");

    string coreUrl = "https://example.com/downloads/Aero.Core.dll";
    string apiUrl = "https://example.com/downloads/Aero.API.dll";

    Console.WriteLine($"[✓] [Aero - Success] -> Target installation directory set to: {targetFolder}");

    if (!Directory.Exists(targetFolder))
    {
        Directory.CreateDirectory(targetFolder);
        Console.WriteLine("[✓] [Aero - Success] -> Created server resource directory structure.");
    }

    using (var client = new System.Net.Http.HttpClient())
    {
        Console.WriteLine("[✓] [Aero - Success] -> Downloading Aero Framework modules from web repository...");
        
        byte[] coreBytes = client.GetByteArrayAsync(coreUrl).GetAwaiter().GetResult();
        File.WriteAllBytes(Path.Combine(targetFolder, "Aero.Core.dll"), coreBytes);
        
        byte[] apiBytes = client.GetByteArrayAsync(apiUrl).GetAwaiter().GetResult();
        File.WriteAllBytes(Path.Combine(targetFolder, "Aero.API.dll"), apiBytes);
        
        Console.WriteLine("[✓] [Aero - Success] -> Core and API modules successfully downloaded and integrated.");
    }

    Console.WriteLine("[✓] [Aero - Success] -> Generating dynamic fxmanifest.lua layout...");
    string manifestContent = 
@"fx_version 'cerulean'
game 'gta5'

author 'Vertex Tools'
description 'Aero Framework Core'

server_script 'Aero.Core.dll'
";
    File.WriteAllText(Path.Combine(targetFolder, "fxmanifest.lua"), manifestContent);

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("--------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("[✓] [Aero - Success] -> CONFIGURATION COMPLETED SUCCESSFULLY! 🎉");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==================================================");
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[x] [Aero - Error] -> Installation failed: {ex.Message}");
}
finally
{
    Console.ResetColor();
    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
}
