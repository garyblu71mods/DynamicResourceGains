#!/usr/bin/env pwsh
# Skrypt do budowania modułu DRG z pełnymi referencjami KSP

$projectPath = "C:\Users\grzeg\source\repos\Dynamic Resource Gains\Dynamic Resource Gains"
$csprojFile = "$projectPath\Dynamic Resource Gains.csproj"
$newCsprojFile = "$projectPath\DynamicResourceGains.csproj"
$buildOutput = "$projectPath\bin\Release"
$dllFile = "$buildOutput\Dynamic Resource Gains.dll"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Dynamic Resource Gains - Full Build" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Sprawdzenie KSP
Write-Host "1. Sprawdzanie KSP..." -ForegroundColor Yellow
$kspPath = "C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram"
$assemblyCSharp = "$kspPath\KSP_Data\Managed\Assembly-CSharp.dll"
$unityEngine = "$kspPath\KSP_Data\Managed\UnityEngine.dll"
$unityEngineUI = "$kspPath\KSP_Data\Managed\UnityEngine.UI.dll"

if ((Test-Path $assemblyCSharp) -and (Test-Path $unityEngine) -and (Test-Path $unityEngineUI)) {
    Write-Host "   ✓ Wszystkie assemblies znalezione" -ForegroundColor Green
} else {
    Write-Host "   ✗ Nie znaleziono assemblies KSP!" -ForegroundColor Red
    exit 1
}

# 2. Backup starego .csproj
Write-Host "2. Backup starego projektu..." -ForegroundColor Yellow
if (Test-Path $csprojFile) {
    Copy-Item $csprojFile "$csprojFile.backup" -Force
    Write-Host "   ✓ Backup utworzony" -ForegroundColor Green
}

# 3. Kopiowanie nowego .csproj
Write-Host "3. Przygotowanie nowego .csproj..." -ForegroundColor Yellow
if (Test-Path $newCsprojFile) {
    Copy-Item $newCsprojFile $csprojFile -Force
    Write-Host "   ✓ Plik projektu zaktualizowany" -ForegroundColor Green
} else {
    Write-Host "   ✗ Nie znaleziono nowego .csproj!" -ForegroundColor Red
    exit 1
}

# 4. Build
Write-Host "4. Budowanie projektu..." -ForegroundColor Yellow
Push-Location $projectPath
try {
    $msbuildPath = "C:\Program Files\Microsoft Visual Studio\2026\Community\MSBuild\Current\Bin\MSBuild.exe"
    if (-not (Test-Path $msbuildPath)) {
        $msbuildPath = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
    }

    if (-not (Test-Path $msbuildPath)) {
        Write-Host "   ⚠ MSBuild nie znaleziony, używam domyślnego..." -ForegroundColor Yellow
        msbuild "Dynamic Resource Gains.csproj" /p:Configuration=Release /v:minimal
    } else {
        & $msbuildPath "Dynamic Resource Gains.csproj" /p:Configuration=Release /v:minimal
    }

    if ($LASTEXITCODE -eq 0) {
        Write-Host "   ✓ Build powodzenie!" -ForegroundColor Green
    } else {
        Write-Host "   ✗ Build nieudany! Błędy:" -ForegroundColor Red
        exit 1
    }
} finally {
    Pop-Location
}

# 5. Sprawdzenie DLL
Write-Host "5. Sprawdzanie DLL..." -ForegroundColor Yellow
if (Test-Path $dllFile) {
    $size = (Get-Item $dllFile).Length / 1024
    Write-Host "   ✓ DLL znaleziony ($size KB)" -ForegroundColor Green
} else {
    Write-Host "   ✗ DLL nie został stworzony!" -ForegroundColor Red
    exit 1
}

# 6. Tworzenie folderu GameData
Write-Host "6. Przygotowanie GameData..." -ForegroundColor Yellow
$gameDataPath = "$kspPath\GameData\DynamicResourceGains\Plugins"
if (-not (Test-Path $gameDataPath)) {
    New-Item -Path $gameDataPath -ItemType Directory -Force | Out-Null
    Write-Host "   ✓ Folder GameData utworzony" -ForegroundColor Green
} else {
    Write-Host "   ✓ Folder GameData już istnieje" -ForegroundColor Green
}

# 7. Instalacja DLL
Write-Host "7. Instalacja DLL..." -ForegroundColor Yellow
$installPath = "$gameDataPath\Dynamic Resource Gains.dll"
Copy-Item $dllFile $installPath -Force
Write-Host "   ✓ DLL zainstalowany: $installPath" -ForegroundColor Green

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "  ✓ BUILD COMPLETED SUCCESSFULLY!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Następny krok:" -ForegroundColor Yellow
Write-Host "  1. Uruchom grę KSP" -ForegroundColor White
Write-Host "  2. Załaduj grę w trybie career" -ForegroundColor White
Write-Host "  3. Wejdź do Space Center" -ForegroundColor White
Write-Host "  4. Szukaj przycisku modułu na pasku narzędzi" -ForegroundColor White
Write-Host ""
