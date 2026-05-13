# Project Setup Guide - KSP Plugin Development

This guide explains how to set up the Dynamic Resource Gains project with KSP references.

## Step 1: Locate Your KSP Installation

Find where KSP 1.x is installed on your system. Common locations:
- Windows: `C:\Program Files\Steam\steamapps\common\Kerbal Space Program`
- Or: `C:\Games\KSP`
- Or: `D:\SteamLibrary\steamapps\common\Kerbal Space Program`

You need the KSP version that matches your game installation.

## Step 2: Update Project References

Open `Dynamic Resource Gains.csproj` in a text editor and add the following references to the `<ItemGroup>` that contains other references:

```xml
<ItemGroup>
    <!-- KSP and Unity References -->
    <Reference Include="Assembly-CSharp">
        <HintPath>PATH_TO_KSP\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine">
        <HintPath>PATH_TO_KSP\KSP_Data\Managed\UnityEngine.dll</HintPath>
        <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI">
        <HintPath>PATH_TO_KSP\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
        <Private>False</Private>
    </Reference>

    <!-- System References (usually present) -->
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Net.Http" />
    <Reference Include="System.Xml" />
</ItemGroup>
```

Replace `PATH_TO_KSP` with your actual KSP installation path. 

### Example for Windows:

```xml
<Reference Include="Assembly-CSharp">
    <HintPath>C:\Games\Kerbal Space Program\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
    <Private>False</Private>
</Reference>
```

## Step 3: Configure Build Output

To automatically copy the built DLL to KSP's GameData folder, add this to the Release PropertyGroup:

```xml
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <!-- Post-build to copy to KSP -->
    <PostBuildEvent>if exist "C:\Games\Kerbal Space Program\GameData\DynamicResourceGains\Plugins" copy "$(TargetPath)" "C:\Games\Kerbal Space Program\GameData\DynamicResourceGains\Plugins\"</PostBuildEvent>
</PropertyGroup>
```

Adjust the path to match your KSP installation.

## Step 4: Create KSP GameData Folder Structure

Create the following folder structure in your KSP installation:

```
KSP/
└── GameData/
    └── DynamicResourceGains/
        ├── Plugins/
        │   └── (DLL goes here after build)
        └── Icons/
            └── (38x38 PNG icon goes here)
```

## Step 5: Rebuild and Test

1. Close Visual Studio
2. Open `Dynamic Resource Gains.sln` in Visual Studio
3. Let Visual Studio restore/reload the project
4. Build the solution (Build → Build Solution)
5. Run KSP and test in Space Center

## Troubleshooting

### "The type or namespace name 'UnityEngine' could not be found"

- Double-check the `HintPath` in the project file
- Ensure the DLLs exist at the specified path
- Try using absolute paths instead of relative paths
- Verify you're using the correct KSP version

### Project won't load

- Make sure the XML in the .csproj file is valid
- Check that all tags are properly closed
- Visual Studio will show errors in the Error List if there are XML issues

### DLL doesn't appear in GameData folder

- Check the PostBuildEvent path is correct
- Verify the GameData folder structure exists
- Make sure you have write permissions to the folder
- Check the Build Output window for copy errors

### Mod not appearing in-game

- Ensure the DLL is in the correct GameData path
- Check the KSP debug log for errors (look in KSP_Data\output_log.txt)
- Verify the plugin has the correct KSPAddon attribute
- Make sure KSP can load the assembly (no missing dependencies)

## Development Tips

1. **Debug Logging**: Add `Debug.Log()` statements to track execution
2. **Check KSP Log**: After loading, check `KSP_Data\output_log.txt` for errors
3. **IntelliSense**: Make sure references are correctly added so IntelliSense works
4. **Don't redistribute KSP DLLs**: Set `<Private>False</Private>` on KSP references

## References

- KSP Modding Manual: https://wiki.kerbalspaceprogram.com/wiki/Modular_Plugins
- KSP API Documentation: http://api.kerbalspaceprogram.com/
- Toolbar Integration: https://github.com/blizzy78/ksp_toolbar

