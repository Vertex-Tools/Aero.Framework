using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aero.Installer
{
    class Program
    {
        private const string BaseDownloadUrl = "https://aeroframework.net/downloads/";
        
        private static readonly List<string> CoreFiles = new()
        {
            "Aero.Core.net.dll",
            "Aero.API.dll",
            "Aero.Drivers.dll",
            "Aero.Events.dll",
            "Aero.Shared.dll",
            "Avian.dll",
            "Avian.Event.dll",
            "YamlDotNet.dll"
        };

        static async Task Main(string[] args)
        {
            try
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.Clear();
                }
            }
            catch { }
            PrintHeader();

            try
            {
                PrintStatus("Checking system requirements...", ConsoleColor.Yellow);
                string osName = OperatingSystem.IsWindows() ? "Windows" : (OperatingSystem.IsLinux() ? "Linux" : "Unknown OS");
                PrintSuccess($"Detected OS: {osName}");
                PrintSuccess(".NET 8.0 Runtime active.");

                string targetFolder = DetectInstallationPath();
                PrintStatus($"Target folder set to: {targetFolder}", ConsoleColor.Cyan);

                if (!Directory.Exists(targetFolder))
                {
                    PrintStatus("Creating target directory structure...", ConsoleColor.Yellow);
                    Directory.CreateDirectory(targetFolder);
                    PrintSuccess("Directory structure created successfully.");
                }

                using (var client = new HttpClient())
                {
                    PrintStatus("Downloading Aero Framework modules and dependencies...", ConsoleColor.Yellow);
                    
                    foreach (var fileName in CoreFiles)
                    {
                        string fileUrl = $"{BaseDownloadUrl}{fileName}";
                        string destPath = Path.Combine(targetFolder, fileName);
                        
                        PrintStatus($" -> Downloading {fileName}...", ConsoleColor.DarkGray);
                        
                        try
                        {
                            byte[] fileBytes = await client.GetByteArrayAsync(fileUrl);
                            await File.WriteAllBytesAsync(destPath, fileBytes);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Failed to download {fileName}: {ex.Message}");
                        }
                    }
                    PrintSuccess("All modules and dependencies downloaded and integrated successfully.");
                }

                PrintStatus("Generating fxmanifest.lua configuration...", ConsoleColor.Yellow);
                string manifestPath = Path.Combine(targetFolder, "fxmanifest.lua");
                string manifestContent = GenerateManifestContent();
                await File.WriteAllTextAsync(manifestPath, manifestContent);
                PrintSuccess("fxmanifest.lua generated successfully.");

                PrintDivider();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [✓] CONFIGURATION COMPLETED SUCCESSFULLY! 🎉");
                Console.WriteLine(" Aero Framework is ready to start.");
                Console.WriteLine(" Add 'ensure aero-core' to your server.cfg.");
                PrintDivider();
            }
            catch (UnauthorizedAccessException)
            {
                PrintError("Access Denied. Please run this installer with Administrator privileges (Windows) or using sudo (Linux).");
            }
            catch (Exception ex)
            {
                PrintError($"Installation failed: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("\nPress any key to exit...");
                try
                {
                    if (!Console.IsInputRedirected)
                    {
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.Read();
                    }
                }
                catch { }
            }
        }

        private static string DetectInstallationPath()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string? detectedPath = FindResourcesFolder(currentDir);

            if (detectedPath != null)
            {
                return Path.Combine(detectedPath, "[aero]", "aero-core");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [!] Could not automatically locate the FiveM 'resources' folder.");
            Console.Write("     Please enter the absolute path to your server's 'resources' folder\n     (or press Enter to install in the current directory): ");
            Console.ResetColor();
            
            string? userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return Path.Combine(currentDir, "[aero]", "aero-core");
            }

            userInput = userInput.Trim();
            if (Directory.Exists(userInput))
            {
                return Path.Combine(userInput, "[aero]", "aero-core");
            }

            return Path.Combine(userInput, "[aero]", "aero-core");
        }

        private static string? FindResourcesFolder(string startDir)
        {
            string? current = startDir;
            for (int i = 0; i < 5; i++)
            {
                if (current == null) break;

                string checkPath = Path.Combine(current, "resources");
                if (Directory.Exists(checkPath))
                {
                    return checkPath;
                }

                current = Path.GetDirectoryName(current);
            }
            return null;
        }

        private static string GenerateManifestContent()
        {
            return @"fx_version 'cerulean'
game 'gta5'

author 'Vertex Tools'
description 'Aero Framework Core'

server_script 'Aero.Core.net.dll'

files {
    'Aero.API.dll',
    'Aero.Shared.dll',
    'Aero.Events.dll',
    'Aero.Drivers.dll',
    'Avian.dll',
    'Avian.Event.dll',
    'YamlDotNet.dll'
}
";
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("          AERO FRAMEWORK - INSTALLER              ");
            Console.WriteLine("            (Windows / Linux Support)             ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }

        private static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }

        private static void PrintStatus(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($" [~] {message}");
            Console.ResetColor();
        }

        private static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" [✓] {message}");
            Console.ResetColor();
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" [x] {message}");
            Console.ResetColor();
        }
    }
}