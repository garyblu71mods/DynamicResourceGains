# ✅ DLL ZAINSTALOWANY DO GAMEDATA!

## Status

**DLL został zainstalowany tutaj:**
```
C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\DynamicResourceGains\Plugins\Dynamic Resource Gains.dll
```

## 📍 Aktualna wersja

Ta wersja to **skeleton** - podstawowa struktura.

Aby mieć **PEŁNĄ WERSJĘ** z wszystkimi funkcjami, musisz:

---

## 🔧 Instrukcja: Pełna wersja z KSP API

### Kroki:

1. **Zamknij Visual Studio całkowicie**

2. **Otwórz plik w tekstowym edytorze:**
   ```
   C:\Users\grzeg\source\repos\Dynamic Resource Gains\Dynamic Resource Gains\Dynamic Resource Gains.csproj
   ```

3. **Zamień całą zawartość na to:**

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{DE3A69BE-2EDD-4D71-9637-61371816249F}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Dynamic_Resource_Gains</RootNamespace>
    <AssemblyName>Dynamic Resource Gains</AssemblyName>
    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\KSP_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\KSP_Data\Managed\UnityEngine.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="UnityEngine.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\KSP_Data\Managed\UnityEngine.UI.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Net.Http" />
    <Reference Include="System.Xml" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="DynamicResourceGains.cs" />
    <Compile Include="DRGScenario.cs" />
    <Compile Include="DRGUtil.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
```

4. **Zapisz plik**

5. **Otwórz Visual Studio** i załaduj projekt

6. **Zamień pliki na pełne wersje** (zawartość w plikach `*_PEŁNA.cs`)
   - DynamicResourceGains_PEŁNA.cs → DynamicResourceGains.cs
   - (reszta już jest)

7. **Build → Build Solution** (Ctrl+Shift+B)

8. **Skopiuj nowy DLL:**
   ```
   Z: C:\Users\grzeg\source\repos\Dynamic Resource Gains\Dynamic Resource Gains\bin\Debug\Dynamic Resource Gains.dll
   Do: C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\DynamicResourceGains\Plugins\
   ```

9. **Uruchom KSP i testuj!**

---

## 🎮 Testowanie

W grze KSP:
1. Załaduj grę w career mode
2. Wejdź do **Space Center**
3. Szukaj przycisku modułu na pasku narzędzi (górny lewy róg)
4. Kliknij żeby otworzyć GUI
5. Dostosuj mnożniki i testuj!

---

## 📝 Notatki

- **Testowy DLL**: Już zainstalowany, nie będzie full funcji ale można testować
- **Pełny DLL**: Postępuj wg instrukcji powyżej - będzie miał wszystkie funkcje
- **KSP API**: Automatycznie załaduje się gdy buildować będziesz z proper referencjami

---

**Pytania? Powiedz!**
