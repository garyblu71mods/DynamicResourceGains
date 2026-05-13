# Project Reference Configuration Examples

This file contains example configurations for different KSP installations.

## Steam Installation (Standard)

If KSP is installed via Steam at the default location:

```xml
<ItemGroup>
    <Reference Include="Assembly-CSharp">
        <HintPath>C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\KSP_Data\Managed\UnityEngine.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
        <Private>False</Private>
    </Reference>
</ItemGroup>
```

## Steam Installation (Secondary Drive)

If KSP is on D: drive:

```xml
<ItemGroup>
    <Reference Include="Assembly-CSharp">
        <HintPath>D:\SteamLibrary\steamapps\common\Kerbal Space Program\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>D:\SteamLibrary\steamapps\common\Kerbal Space Program\KSP_Data\Managed\UnityEngine.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>D:\SteamLibrary\steamapps\common\Kerbal Space Program\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
        <Private>False</Private>
    </Reference>
</ItemGroup>
```

## GOG Installation

```xml
<ItemGroup>
    <Reference Include="Assembly-CSharp">
        <HintPath>C:\GOG Games\Kerbal Space Program\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>C:\GOG Games\Kerbal Space Program\KSP_Data\Managed\UnityEngine.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>C:\GOG Games\Kerbal Space Program\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
        <Private>False</Private>
    </Reference>
</ItemGroup>
```

## Relative Path (if in same parent folder)

If you keep KSP and the project in the same parent directory:

```
repos/
├── Kerbal Space Program/
│   └── KSP_Data/Managed/
└── Dynamic Resource Gains/
    └── Dynamic Resource Gains.csproj
```

You can use:

```xml
<ItemGroup>
    <Reference Include="Assembly-CSharp">
        <HintPath>..\..\Kerbal Space Program\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>..\..\Kerbal Space Program\KSP_Data\Managed\UnityEngine.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>..\..\Kerbal Space Program\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
        <Private>False</Private>
    </Reference>
</ItemGroup>
```

## Post-Build Copy (Release Configuration)

To automatically copy the built DLL to KSP after building:

```xml
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <PostBuildEvent>
        if exist "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\GameData\DynamicResourceGains\Plugins" (
            echo Copying DLL to KSP...
            copy "$(TargetPath)" "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program\GameData\DynamicResourceGains\Plugins\$(TargetFileName)"
            echo Copy complete.
        ) else (
            echo Warning: KSP plugin directory not found.
            echo Please create: GameData\DynamicResourceGains\Plugins\ in your KSP installation.
        )
    </PostBuildEvent>
</PropertyGroup>
```

## IMPORTANT NOTES

1. **Replace paths**: Update all paths to match YOUR KSP installation location
2. **Set `<Private>False</Private>`**: This prevents copying KSP DLLs when packaging
3. **Use absolute paths**: Relative paths can be fragile; absolute paths are more reliable
4. **Escape backslashes**: In XML, backslashes are okay but double-check your paths
5. **Check DLL versions**: Make sure the DLLs exist in your KSP version

## Verifying Paths

Run this PowerShell command to verify your KSP path:

```powershell
$kspPath = "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program"
$managedPath = Join-Path $kspPath "KSP_Data\Managed"

if (Test-Path $managedPath) {
    Get-ChildItem $managedPath -Filter "*.dll" | Select-Object Name
    Write-Host "KSP path is valid!" -ForegroundColor Green
} else {
    Write-Host "KSP path is INVALID!" -ForegroundColor Red
}
```

Or use the automated script:

```powershell
.\Setup-KSPReferences.ps1 -KSPPath "your_ksp_path"
```

## Minimal Example

Here's the minimum you need to add to the project file:

```xml
<ItemGroup>
    <Reference Include="Assembly-CSharp">
        <HintPath>PATH\TO\KSP\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>PATH\TO\KSP\KSP_Data\Managed\UnityEngine.dll</HintPath>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>PATH\TO\KSP\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
    </Reference>
</ItemGroup>
```

Then replace `PATH\TO\KSP` with your actual KSP installation path.
