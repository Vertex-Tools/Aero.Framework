# Setup script for FiveM test server running Aero Framework
$ErrorActionPreference = "Stop"

$licenseKey = "YOUR_LICENSE_KEY_HERE"
$rootDir = Join-Path $PSScriptRoot "fivem-server"
$artifactDir = Join-Path $rootDir "artifact"
$serverDataDir = Join-Path $rootDir "server-data"
$resourcesDir = Join-Path $serverDataDir "resources"
$aeroCoreDir = Join-Path $resourcesDir "[aero]" | Join-Path -ChildPath "aero-core"
$aeroPluginsDir = Join-Path $aeroCoreDir "Plugins"

Write-Host "=== INIZIALIZZAZIONE SERVER DI TEST FIVEM ===" -ForegroundColor Cyan

# 1. Crea la struttura delle cartelle
if (!(Test-Path -LiteralPath $rootDir)) {
    New-Item -LiteralPath $rootDir -ItemType Directory | Out-Null
    Write-Host "[OK] Cartella radice $rootDir creata." -ForegroundColor Green
}
if (!(Test-Path -LiteralPath $artifactDir)) {
    New-Item -LiteralPath $artifactDir -ItemType Directory | Out-Null
    Write-Host "[OK] Cartella artifact $artifactDir creata." -ForegroundColor Green
}

# 2. Scarica ed estrae FXServer se non presente
$fxServerExe = Join-Path $artifactDir "FXServer.exe"
if (!(Test-Path -LiteralPath $fxServerExe)) {
    Write-Host "[INFO] Ricerca dell'ultimo artifact raccomandato..." -ForegroundColor Yellow
    $url = "https://runtime.fivem.net/artifacts/fivem/build_server_windows/master/"
    $resp = Invoke-WebRequest -Uri $url -UseBasicParsing
    $recommended = $resp.Links | Where-Object { $_.outerHTML -like "*recommended*" -or $_.innerText -like "*recommended*" } | Select-Object -First 1

    if ($null -eq $recommended) {
        Write-Error "Impossibile trovare il link per l'artifact raccomandato di FiveM."
    }

    $cleanHref = $recommended.href.Replace("./", "")
    $downloadUrl = $url + $cleanHref
    $archivePath = Join-Path $rootDir "server.7z"

    Write-Host "[INFO] Download dell'artifact da: $downloadUrl ..." -ForegroundColor Yellow
    Invoke-WebRequest -Uri $downloadUrl -OutFile $archivePath

    Write-Host "[INFO] Estrazione dei file dell'artifact..." -ForegroundColor Yellow
    # Utilizziamo tar.exe per estrarre l'archivio .7z
    tar.exe -xf $archivePath -C $artifactDir

    # Rimuovi l'archivio scaricato
    Remove-Item -Path $archivePath -Force
    Write-Host "[OK] FXServer scaricato ed estratto con successo in $artifactDir." -ForegroundColor Green
} else {
    Write-Host "[OK] FXServer gia presente in $artifactDir." -ForegroundColor Green
}

# 3. Clona le risorse di base (cfx-server-data)
if (!(Test-Path -LiteralPath $resourcesDir)) {
    Write-Host "[INFO] Clonazione delle risorse di default (cfx-server-data)..." -ForegroundColor Yellow
    git clone https://github.com/citizenfx/cfx-server-data.git $serverDataDir
    Write-Host "[OK] Risorse di default clonate in $serverDataDir." -ForegroundColor Green
} else {
    Write-Host "[OK] Risorse di default gia presenti in $serverDataDir." -ForegroundColor Green
}

# 4. Genera il server.cfg
$serverCfgPath = Join-Path $serverDataDir "server.cfg"
Write-Host "[INFO] Generazione del file server.cfg..." -ForegroundColor Yellow
$serverCfgContent = @"
# Solo IP / Porta di ascolto
endpoint_add_tcp "0.0.0.0:30120"
endpoint_add_udp "0.0.0.0:30120"

# Risorse di default
ensure mapmanager
ensure chat
ensure spawnmanager
ensure sessionmanager
ensure basic-gamemode
ensure hardcap

# Risorsa Aero Framework
ensure aero-core

sv_scriptHookAllowed 0
rcon_password ""
sets tags "default, aero"
sets locale "it-IT"
sv_hostname "Aero Framework Test Server"
sv_projectDescription "Server di test per Aero.Framework"
sv_projectName "Aero Test Server"

# Nascondi gli endpoint nei log
sv_endpointprivacy true

# Limite player
sv_maxclients 48

# License key
sv_licenseKey "$licenseKey"
"@

[System.IO.File]::WriteAllText($serverCfgPath, $serverCfgContent)
Write-Host "[OK] server.cfg generato." -ForegroundColor Green

# 5. Compila la soluzione Aero.Framework.sln
Write-Host "[INFO] Compilazione di Aero.Framework.sln..." -ForegroundColor Yellow
dotnet build (Join-Path $PSScriptRoot "Aero.Framework.sln") -c Debug

# 6. Prepara le cartelle delle risorse
if (!(Test-Path -LiteralPath $aeroCoreDir)) {
    New-Item -LiteralPath $aeroCoreDir -ItemType Directory | Out-Null
}
if (!(Test-Path -LiteralPath $aeroPluginsDir)) {
    New-Item -LiteralPath $aeroPluginsDir -ItemType Directory | Out-Null
}

# 7. Copia le DLL compilate dell'Aero Framework (escludendo CitizenFX)
Write-Host "[INFO] Distribuzione del framework nel server FiveM..." -ForegroundColor Yellow
$buildOutputDir = Join-Path $PSScriptRoot "Aero.Core\bin\Debug"

Copy-Item -Path (Join-Path $buildOutputDir "*.dll") -Destination $aeroCoreDir -Exclude "CitizenFX.Core.Server.dll" -Force
Copy-Item -Path (Join-Path $buildOutputDir "*.pdb") -Destination $aeroCoreDir -Exclude "CitizenFX.Core.Server.pdb" -Force
Write-Host "[OK] Core DLLs copiate in $aeroCoreDir." -ForegroundColor Green

# 8. Genera il fxmanifest.lua per aero-core
$manifestPath = Join-Path $aeroCoreDir "fxmanifest.lua"
$manifestContent = @"
fx_version 'cerulean'
game 'gta5'

author 'Vertex Tools'
description 'Aero Framework Core'

server_scripts {
    'Aero.Core.dll'
}
"@
[System.IO.File]::WriteAllText($manifestPath, $manifestContent)
Write-Host "[OK] fxmanifest.lua generato." -ForegroundColor Green

# 9. Compila ed esegui la copia del plugin di esempio
Write-Host "[INFO] Compilazione e deploy di Aero.SamplePlugin..." -ForegroundColor Yellow
$samplePluginBuildDir = Join-Path $PSScriptRoot "Aero.SamplePlugin\bin\Debug"
if (Test-Path -LiteralPath (Join-Path $samplePluginBuildDir "Aero.SamplePlugin.dll")) {
    Copy-Item -Path (Join-Path $samplePluginBuildDir "Aero.SamplePlugin.dll") -Destination $aeroPluginsDir -Force
    Copy-Item -Path (Join-Path $samplePluginBuildDir "Aero.SamplePlugin.pdb") -Destination $aeroPluginsDir -Force
    Write-Host "[OK] Aero.SamplePlugin distribuito in $aeroPluginsDir." -ForegroundColor Green
}

# 10. Crea lo script di avvio run_server.bat
$runBatPath = Join-Path $serverDataDir "run_server.bat"
$runBatContent = @"
@echo off
title Aero Framework Test Server
cd /d "%~dp0"
..\artifact\FXServer.exe +exec server.cfg
pause
"@
[System.IO.File]::WriteAllText($runBatPath, $runBatContent)
Write-Host "[OK] run_server.bat creato in $serverDataDir." -ForegroundColor Green

Write-Host "`n=== CONFIGURAZIONE COMPLETATA CON SUCCESSO! ===" -ForegroundColor Green
Write-Host "Puoi avviare il server usando il file batch:"
Write-Host " -> $runBatPath" -ForegroundColor Cyan
