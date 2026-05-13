# Dynamic Resource Gains - Implementation Guide

## Overview

This KSP1 plugin provides dynamic resource multiplier adjustment based on player progress in career mode. The implementation includes three main classes and supports the full KSP plugin architecture.

## Project Structure

```
Dynamic Resource Gains/
├── DynamicResourceGains.cs      # Main addon class [KSPAddon]
├── DRGScenario.cs               # Persistence module [KSPScenario]
├── DRGUtil.cs                   # Utility functions
├── Properties/
│   └── AssemblyInfo.cs          # Assembly metadata
├── README.md                     # Feature documentation
├── SETUP_GUIDE.md               # Setup instructions
├── Setup-KSPReferences.ps1       # PowerShell setup helper
└── IMPLEMENTATION.md            # This file
```

## Class Architecture

### DynamicResourceGains : MonoBehaviour
**Purpose**: Main plugin class, handles GUI and game integration

**Key Attributes**:
- `[KSPAddon(KSPAddon.Startup.SpaceCenter, false)]` - Loads only in Space Center, once

**Key Methods**:
- `Start()` - Initialize toolbar button and event subscriptions
- `OnGUI()` - Render the draggable GUI window
- `ApplyMultipliers()` - Parse user input and apply to game
- `OnTechnologyResearched()` - Event handler
- `OnScienceReceived()` - Event handler
- `OnFundsChanged()` - Event handler
- `OnReputationChanged()` - Event handler

**Key Fields**:
- `baseScienceMultiplier` - Base science gain multiplier (0.01-5.0)
- `baseFundsMultiplier` - Base funds gain multiplier (0.01-5.0)
- `baseRepMultiplier` - Base reputation gain multiplier (0.01-5.0)
- `dynamicScience` - Enable/disable dynamic science scaling
- `dynamicFunds` - Enable/disable dynamic funds scaling
- `dynamicReputation` - Enable/disable dynamic reputation scaling
- `sciencePerTierPenalty` - How much to reduce science per tech tier
- `scienceStepPenalty` - How much to reduce science per science threshold
- `scienceStep` - Science threshold (e.g., 10000 science = 1 step)
- `fundsScaleFactor` - Scale factor for funds-based reduction
- `repScaleFactor` - Scale factor for reputation-based reduction

**GUI Components**:
- Base Multiplier Input Fields
- Dynamic Scaling Toggles
- Expandable Dynamic Settings Sections
- Apply & Save Buttons
- Draggable Window

### DRGScenario : ScenarioModule
**Purpose**: Handles configuration persistence using KSP's save system

**Key Attributes**:
- `[KSPScenario(...)]` - Registered with KSP's scenario system
- `[KSPField(isPersistant = true)]` - Auto-serialize to save file

**Key Methods**:
- `OnAwake()` - Called when loading
- `OnLoad(ConfigNode)` - Called when deserializing
- `OnSave(ConfigNode)` - Called when serializing
- `SaveMultipliers()` - Helper to persist current state
- `LoadMultipliers()` - Helper to restore saved state

**Persistent Fields**:
- All multipliers and settings are `[KSPField(isPersistant = true)]`
- Automatically saved/loaded by KSP

### DRGUtil : static class
**Purpose**: Utility functions for game state queries

**Key Methods**:
- `GetTechTier()` - Count researched technologies
- `GetLifetimeScience()` - Get career science balance
- `ClampMultiplier()` - Ensure valid multiplier range

## Game Integration

### Event Subscriptions

The plugin subscribes to these KSP GameEvents:

```csharp
GameEvents.OnTechnologyResearched.Add(OnTechnologyResearched);
GameEvents.OnScienceReceived.Add(OnScienceReceived);
GameEvents.OnFundsChanged.Add(OnFundsChanged);
GameEvents.OnReputationChanged.Add(OnReputationChanged);
```

When any event fires, the multipliers are recalculated and applied to:
```csharp
HighLogic.CurrentGame.Parameters.Career.ScienceGainMultiplier
HighLogic.CurrentGame.Parameters.Career.FundsGainMultiplier
HighLogic.CurrentGame.Parameters.Career.RepGainMultiplier
```

### Toolbar Integration

Uses KSP's ApplicationLauncher:
```csharp
ApplicationLauncher.Instance.AddModApplication(
    onTrue,      // OnAppLauncherButtonToggleOn
    onFalse,     // OnAppLauncherButtonToggleOff
    null,        // onEnable
    null,        // onDisable
    null,        // onToggle
    null,        // onAppLauncherDestroyed
    AppScenes.SPACECENTER,  // Only show in Space Center
    texture      // 38x38 PNG icon
);
```

### Scene Filtering

GUI only appears when:
```csharp
HighLogic.LoadedScene == GameScenes.SPACECENTER
```

## Multiplier Calculation

### Static Multiplier
When dynamic scaling is disabled:
```csharp
return baseScienceMultiplier;  // User-configured base value
```

### Dynamic Science Multiplier
```csharp
multiplier = baseScienceMultiplier;
multiplier -= (techTier * sciencePerTierPenalty);
multiplier -= (lifetimeScience / scienceStep) * scienceStepPenalty;
return Mathf.Clamp(multiplier, 0.01f, 5f);
```

Example:
- Base: 1.0 (100%)
- Tech Tier: 10, Penalty: 0.05 → -0.5
- Lifetime Science: 50000, Step: 10000, Step Penalty: 0.01 → -0.05
- Result: 1.0 - 0.5 - 0.05 = 0.45 (45%)

### Dynamic Funds Multiplier
```csharp
multiplier = baseFundsMultiplier;
multiplier -= (currentFunds * fundsScaleFactor);
return Mathf.Clamp(multiplier, 0.01f, 5f);
```

### Dynamic Reputation Multiplier
```csharp
multiplier = baseRepMultiplier;
multiplier -= (currentRep * repScaleFactor);
return Mathf.Clamp(multiplier, 0.01f, 5f);
```

## Configuration Persistence

Multipliers are saved via the ScenarioModule in the game save file:
```
saves/[SaveName]/persistent.sfs
```

Format:
```
SCENARIO
{
    name = DRGScenario
    BaseScienceMultiplier = 1.0
    BaseFundsMultiplier = 1.0
    BaseRepMultiplier = 1.0
    DynamicScience = False
    DynamicFunds = False
    DynamicReputation = False
    SciencePerTierPenalty = 0.05
    ...
}
```

## GUI Layout

```
┌─ Dynamic Resource Gains ──────────────┐
│                                       │
│ Base Multipliers (% of normal)        │
│ Science:  [100]  %                    │
│ Funds:    [100]  %                    │
│ Reputation: [100]  %                  │
│                                       │
│ Dynamic Scaling                       │
│ ☐ Enable Dynamic Science              │
│   ├─ Current Tech Tier: 5             │
│   ├─ Lifetime Science: 50000          │
│   ├─ Science per Tier: [0.05]         │
│   ├─ Science Step: [10000]            │
│   └─ Step Penalty: [0.01]             │
│ ☐ Enable Dynamic Funds                │
│ ☐ Enable Dynamic Reputation           │
│                                       │
│ [Apply] [Save]                        │
└───────────────────────────────────────┘
```

## Development Workflow

1. **Setup References** (see SETUP_GUIDE.md)
   - Run: `.\Setup-KSPReferences.ps1`
   - Or manually edit .csproj file

2. **Edit Code**
   - Modify the three main classes
   - Use Visual Studio IntelliSense

3. **Build**
   - Build → Build Solution
   - Output: `bin\Release\Dynamic Resource Gains.dll`

4. **Test**
   - Copy DLL to KSP GameData folder
   - Launch KSP and load a career save
   - Go to Space Center
   - Click toolbar button to test GUI

5. **Debug**
   - Check `KSP_Data\output_log.txt` for errors
   - Add `Debug.Log()` statements to code
   - Use Visual Studio debugger (requires KSP with debug symbols)

## Key Design Decisions

1. **Singleton Pattern**: Uses `instance` field to prevent duplicate addons
2. **IMGUI over Canvas**: KSP's OnGUI() is simpler and more compatible
3. **Event-Driven Updates**: Recalculates on any relevant event, not every frame
4. **ScenarioModule for Persistence**: Standard KSP approach, automatic serialization
5. **No External Dependencies**: Only uses core KSP and Unity APIs
6. **Clamp Multipliers**: Prevents extreme values (0.01-5.0 range)

## Common Modifications

### Change Default Base Multiplier
In `DynamicResourceGains.cs`:
```csharp
private float baseScienceMultiplier = 2.0f;  // Changed from 1.0f
```

### Change Multiplier Range
In calculation methods:
```csharp
return Mathf.Clamp(multiplier, 0.1f, 10f);  // Changed from 0.01f, 5f
```

### Add New Dynamic Parameter
1. Add field to `DynamicResourceGains.cs`
2. Add corresponding `[KSPField]` to `DRGScenario.cs`
3. Add GUI input field in `DrawWindow()`
4. Update calculation in `CalculateScienceMultiplier()` etc.
5. Parse user input in `ApplyMultipliers()`

### Customize GUI Size
In `OnGUI()`:
```csharp
GUILayout.MinWidth(400),  // Changed from 350
GUILayout.MinHeight(300)  // Changed from 200
```

## Testing Checklist

- [ ] Mod loads without errors in KSP
- [ ] Toolbar button appears only in Space Center
- [ ] GUI window opens/closes with button click
- [ ] GUI window is draggable
- [ ] Base multipliers can be edited and applied
- [ ] Dynamic scaling toggles enable/disable sections
- [ ] Tech tier and lifetime science display correctly
- [ ] Multipliers are calculated correctly
- [ ] Settings persist after save/load
- [ ] Event handlers fire and update multipliers
- [ ] Clamps prevent invalid multiplier values

## Debugging Tips

1. **Check Log File**
   ```powershell
   Get-Content "path\to\KSP\KSP_Data\output_log.txt" -Tail 20
   ```

2. **Add Logging**
   ```csharp
   Debug.Log("[DRG] Science multiplier: " + scienceMultiplier);
   ```

3. **Verify Event Subscriptions**
   Check that `OnDestroy()` properly unsubscribes to avoid duplicate handlers

4. **Test Persistence**
   - Change settings, save game
   - Load game, verify settings restored
   - Delete scenario save, verify defaults work

## Future Enhancements

- Support for other resource types
- Custom icon loading from GameData
- Configuration file (instead of GUI only)
- Integration with other mods (e.g., difficulty settings)
- Visual feedback for multiplier changes
- History/logging of multiplier changes
- Per-mission multiplier overrides
