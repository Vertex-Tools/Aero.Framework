# Script to build the chat resource in a temp folder to bypass the Node.js brackets path bug on Windows
$ErrorActionPreference = "Stop"

$rootDir = Join-Path $PSScriptRoot "fivem-server"
$chatSource = "$rootDir/server-data/resources/[gameplay]/chat"
$chatTemp = "$rootDir/chat-temp"
$chatDestDist = "$rootDir/server-data/resources/[gameplay]/chat/dist"

Write-Host "=== COMPILAZIONE MANUALE RISORSA CHAT ===" -ForegroundColor Cyan

# 1. Rimuovi cartella temporanea se esiste
if (Test-Path -LiteralPath $chatTemp) {
    Remove-Item -LiteralPath $chatTemp -Recurse -Force
}

# 2. Copia la risorsa chat nella cartella temporanea (senza parentesi quadre nel path)
Write-Host "[INFO] Copia della risorsa in cartella temporanea..." -ForegroundColor Yellow
Copy-Item -LiteralPath $chatSource -Destination $chatTemp -Recurse -Force

# 3. Esegui npm install nella cartella temporanea
Write-Host "[INFO] Installazione dipendenze (npm install)..." -ForegroundColor Yellow
Start-Process -FilePath "cmd.exe" -ArgumentList "/c npm install" -WorkingDirectory $chatTemp -NoNewWindow -Wait

# 4. Esegui la build di webpack nella cartella temporanea con NODE_OPTIONS
Write-Host "[INFO] Compilazione dei file UI (npx webpack)..." -ForegroundColor Yellow
Start-Process -FilePath "cmd.exe" -ArgumentList "/c set NODE_OPTIONS=--openssl-legacy-provider && npx webpack" -WorkingDirectory $chatTemp -NoNewWindow -Wait

# 5. Verifica se la cartella dist e' stata creata
$tempDist = Join-Path $chatTemp "dist"
if (Test-Path -LiteralPath $tempDist) {
    Write-Host "[OK] Compilazione completata con successo." -ForegroundColor Green
    
    # Crea la cartella dist di destinazione se non esiste
    if (!(Test-Path -LiteralPath $chatDestDist)) {
        [System.IO.Directory]::CreateDirectory($chatDestDist) | Out-Null
    }
    
    # Copia i file compilati nella risorsa chat originale
    Write-Host "[INFO] Copia dei file compilati nella cartella originale..." -ForegroundColor Yellow
    Copy-Item -Path "$tempDist/*" -Destination $chatDestDist -Force
    Write-Host "[OK] File copiati con successo." -ForegroundColor Green
} else {
    Write-Error "La compilazione di webpack e' fallita. La cartella dist non esiste."
}

# 6. Pulisci la cartella temporanea
Write-Host "[INFO] Pulizia dei file temporanei..." -ForegroundColor Yellow
Remove-Item -LiteralPath $chatTemp -Recurse -Force

# 7. Rimuovi la dipendenza da webpack e yarn da fxmanifest.lua per non eseguirlo mai piu' all'avvio
Write-Host "[INFO] Rimozione di Webpack e Yarn da fxmanifest.lua..." -ForegroundColor Yellow
$manifestPath = "$chatSource/fxmanifest.lua"
if (Test-Path -LiteralPath $manifestPath) {
    $content = [System.IO.File]::ReadAllText($manifestPath)
    
    # Rimuovi il blocco dependencies
    $patternDeps = "(?s)dependencies\s*\{\s*'[a-zA-Z0-9_-]+'\s*,\s*'[a-zA-Z0-9_-]+'\s*\}"
    $content = $content -replace $patternDeps, ""
    
    # Rimuovi il blocco webpack_config
    $patternWebpack = "webpack_config\s+'webpack\.config\.js'"
    $content = $content -replace $patternWebpack, ""
    
    [System.IO.File]::WriteAllText($manifestPath, $content)
    Write-Host "[OK] Webpack rimosso con successo da fxmanifest.lua." -ForegroundColor Green
}

Write-Host "[OK] Fatto!" -ForegroundColor Green
