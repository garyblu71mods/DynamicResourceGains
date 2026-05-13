# FIX_CSPROJ_PATHS.ps1

Ten skrypt zmienia ścieżki `HintPath` w pliku `.csproj` dla referencji KSP:
- `Assembly-CSharp.dll`
- `UnityEngine.dll`
- `UnityEngine.UI.dll`

## Użycie

W PowerShell uruchom z folderu projektu:

```powershell
.\FIX_CSPROJ_PATHS.ps1
```

Albo wskaż ręcznie plik projektu:

```powershell
.\FIX_CSPROJ_PATHS.ps1 -ProjectPath "C:\Users\grzeg\source\repos\Dynamic Resource Gains\Dynamic Resource Gains\Dynamic Resource Gains.csproj"
```

## Co robi

- robi kopię zapasową `.csproj` do pliku `.bak_YYYYMMDD_HHMMSS`
- jeśli istnieje folder `libs`, ustawia referencje na pliki lokalne:
  - `..\libs\Assembly-CSharp.dll`
  - `..\libs\UnityEngine.dll`
  - `..\libs\UnityEngine.UI.dll`
- jeśli `libs` nie istnieje, ustawia referencje bezpośrednio na folder `KSP_x64_Data\Managed`

## Wymagania

- PowerShell
- Plik `.csproj` w standardowym formacie MSBuild
- KSP zainstalowany w lokalizacji Epic Games

## Po uruchomieniu

1. Zapisz plik `.csproj`
2. Zamknij i otwórz ponownie Visual Studio
3. Zbuduj projekt jeszcze raz
