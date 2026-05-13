$projectPath = "C:\Users\grzeg\source\repos\Dynamic Resource Gains\Dynamic Resource Gains"
$csprojFile = "$projectPath\Dynamic Resource Gains.csproj"
$newCsprojFile = "$projectPath\DynamicResourceGains.csproj"

# Kopiuj nowy projekt
Copy-Item $newCsprojFile $csprojFile -Force

# Builduj
Push-Location $projectPath
msbuild "Dynamic Resource Gains.csproj" /p:Configuration=Release /v:minimal
Pop-Location

# Sprawdź wynik
$dllFile = "$projectPath\bin\Release\Dynamic Resource Gains.dll"
if (Test-Path $dllFile) {
    Write-Host "SUCCESS: DLL created at $dllFile" -ForegroundColor Green

    # Kopiuj do GameData
    $kspPath = "C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram"
    $gameDataPath = "$kspPath\GameData\DynamicResourceGains\Plugins"
    New-Item -Path $gameDataPath -ItemType Directory -Force -ErrorAction SilentlyContinue
    Copy-Item $dllFile "$gameDataPath\Dynamic Resource Gains.dll" -Force
    Write-Host "INSTALLED: $gameDataPath\Dynamic Resource Gains.dll" -ForegroundColor Green
} else {
    Write-Host "ERROR: DLL not created" -ForegroundColor Red
}
