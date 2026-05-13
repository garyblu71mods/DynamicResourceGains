# KSP Plugin Setup Script
# This script helps configure KSP assembly references in the project file

param(
    [string]$KSPPath = "",
    [switch]$Help
)

function Show-Help {
    Write-Host "KSP Plugin Setup Script" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Usage: .\Setup-KSPReferences.ps1 -KSPPath <path_to_ksp>" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Parameters:"
    Write-Host "  -KSPPath  : Full path to KSP installation directory"
    Write-Host "              Example: C:\Games\Kerbal Space Program"
    Write-Host "  -Help     : Show this help message"
    Write-Host ""
    Write-Host "Examples:"
    Write-Host "  .\Setup-KSPReferences.ps1 -KSPPath 'C:\Games\Kerbal Space Program'"
    Write-Host "  .\Setup-KSPReferences.ps1 -KSPPath 'D:\SteamLibrary\steamapps\common\Kerbal Space Program'"
}

if ($Help) {
    Show-Help
    exit
}

# If no KSPPath provided, try to find it
if (-not $KSPPath) {
    Write-Host "No KSP path specified. Searching for KSP installation..." -ForegroundColor Yellow

    $possiblePaths = @(
        "C:\Program Files\Steam\steamapps\common\Kerbal Space Program",
        "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program",
        "D:\SteamLibrary\steamapps\common\Kerbal Space Program",
        "C:\Games\Kerbal Space Program",
        "C:\Games\KSP",
        "${env:PROGRAMFILES}\Steam\steamapps\common\Kerbal Space Program"
    )

    foreach ($path in $possiblePaths) {
        if (Test-Path -Path $path) {
            $KSPPath = $path
            Write-Host "Found KSP at: $KSPPath" -ForegroundColor Green
            break
        }
    }
}

if (-not $KSPPath -or -not (Test-Path $KSPPath)) {
    Write-Host "Error: Cannot find KSP installation!" -ForegroundColor Red
    Write-Host "Please specify the KSP path manually:"
    Write-Host "  .\Setup-KSPReferences.ps1 -KSPPath <your_ksp_path>" -ForegroundColor Yellow
    exit 1
}

# Verify KSP structure
$assemblyPath = Join-Path $KSPPath "KSP_Data\Managed"
if (-not (Test-Path $assemblyPath)) {
    Write-Host "Error: KSP_Data\Managed folder not found at: $assemblyPath" -ForegroundColor Red
    Write-Host "Is this a valid KSP installation?" -ForegroundColor Yellow
    exit 1
}

$requiredDLLs = @("Assembly-CSharp.dll", "UnityEngine.dll", "UnityEngine.UI.dll")
$missingDLLs = @()

foreach ($dll in $requiredDLLs) {
    $dllPath = Join-Path $assemblyPath $dll
    if (-not (Test-Path $dllPath)) {
        $missingDLLs += $dll
    }
}

if ($missingDLLs.Count -gt 0) {
    Write-Host "Error: Missing required DLLs:" -ForegroundColor Red
    foreach ($dll in $missingDLLs) {
        Write-Host "  - $dll" -ForegroundColor Red
    }
    exit 1
}

Write-Host "KSP installation verified!" -ForegroundColor Green
Write-Host "Found assemblies:" -ForegroundColor Cyan
foreach ($dll in $requiredDLLs) {
    Write-Host "  ✓ $dll" -ForegroundColor Green
}

# Now update the project file
$projectFile = "Dynamic Resource Gains\Dynamic Resource Gains.csproj"

if (-not (Test-Path $projectFile)) {
    Write-Host "Error: Cannot find project file: $projectFile" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Updating project file: $projectFile" -ForegroundColor Cyan

# Load the project XML
$xml = [xml](Get-Content $projectFile)

# Define the namespace
$ns = New-Object System.Xml.XmlNamespaceManager $xml.NameTable
$ns.AddNamespace("default", "http://schemas.microsoft.com/developer/msbuild/2003")

# Find or create the KSP references ItemGroup
$itemGroups = $xml.SelectNodes("//default:ItemGroup[default:Reference]")
$targetGroup = $itemGroups[0]

if (-not $targetGroup) {
    Write-Host "Warning: Could not find existing ItemGroup with References" -ForegroundColor Yellow
    Write-Host "Creating new ItemGroup..." -ForegroundColor Yellow

    $project = $xml.SelectSingleNode("//default:Project")
    $targetGroup = $xml.CreateElement("ItemGroup", "http://schemas.microsoft.com/developer/msbuild/2003")
    $project.AppendChild($targetGroup) | Out-Null
}

# Create KSP references
$kspRefs = @(
    @{Include = "Assembly-CSharp"; DLL = "Assembly-CSharp.dll"},
    @{Include = "UnityEngine"; DLL = "UnityEngine.dll"},
    @{Include = "UnityEngine.UI"; DLL = "UnityEngine.UI.dll"}
)

$refCount = 0
foreach ($ref in $kspRefs) {
    # Check if reference already exists
    $existing = $targetGroup.SelectSingleNode("default:Reference[@Include='$($ref.Include)']", $ns)
    if ($existing) {
        # Update existing reference
        $hintPathNode = $existing.SelectSingleNode("default:HintPath", $ns)
        if (-not $hintPathNode) {
            $hintPathNode = $xml.CreateElement("HintPath", "http://schemas.microsoft.com/developer/msbuild/2003")
            $existing.AppendChild($hintPathNode) | Out-Null
        }
        $hintPathNode.InnerText = Join-Path $assemblyPath $ref.DLL
        Write-Host "Updated reference: $($ref.Include)" -ForegroundColor Yellow
        $refCount++
    }
    else {
        # Create new reference
        $refNode = $xml.CreateElement("Reference", "http://schemas.microsoft.com/developer/msbuild/2003")
        $refNode.SetAttribute("Include", $ref.Include)

        $hintPathNode = $xml.CreateElement("HintPath", "http://schemas.microsoft.com/developer/msbuild/2003")
        $hintPathNode.InnerText = Join-Path $assemblyPath $ref.DLL
        $refNode.AppendChild($hintPathNode) | Out-Null

        $privateNode = $xml.CreateElement("Private", "http://schemas.microsoft.com/developer/msbuild/2003")
        $privateNode.InnerText = "False"
        $refNode.AppendChild($privateNode) | Out-Null

        $targetGroup.AppendChild($refNode) | Out-Null
        Write-Host "Added reference: $($ref.Include)" -ForegroundColor Green
        $refCount++
    }
}

# Save the updated project file
$xml.Save($projectFile)

Write-Host ""
Write-Host "✓ Project file updated successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Close and reopen Visual Studio"
Write-Host "2. Build the solution"
Write-Host "3. The DLL will be output to: bin\Release\Dynamic Resource Gains.dll"
Write-Host ""
Write-Host "To install to KSP, copy the DLL to:" -ForegroundColor Yellow
Write-Host "  $KSPPath\GameData\DynamicResourceGains\Plugins\" -ForegroundColor White
