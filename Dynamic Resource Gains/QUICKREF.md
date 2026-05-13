# Quick Reference Card - Dynamic Resource Gains

## 🚀 Quick Setup

```powershell
# Step 1: Configure KSP References
.\Setup-KSPReferences.ps1

# Step 2: Build (In Visual Studio)
Ctrl+Shift+B

# Step 3: Copy DLL
# bin\Release\Dynamic Resource Gains.dll 
# → KSP\GameData\DynamicResourceGains\Plugins\
```

---

## 📂 Project Files

| File | Purpose | Lines |
|------|---------|-------|
| DynamicResourceGains.cs | Main addon, GUI, events | 260 |
| DRGScenario.cs | Save/load configuration | 150 |
| DRGUtil.cs | Helper utilities | 40 |

---

## 🎮 User Interface

**Toolbar Button**: Only visible in Space Center

**Window Contents**:
- Base multiplier fields (Science, Funds, Reputation) - % input
- 3 dynamic scaling toggles
- Expandable sections with:
  - Tech tier display (read-only)
  - Lifetime science display (read-only)
  - Dynamic parameter inputs
- Apply & Save buttons

---

## ⚙️ Configuration Parameters

### Base Multipliers (Default: 100%)
- Accepted range: 0.01% to 500%
- User enters as percentage (e.g., "25" = 0.25x)
- Applied as-is: no clamping on user input

### Dynamic Parameters (If Enabled)

**Science Scaling**:
- Per-Tier Penalty: 0.05 (default)
- Step Size: 10000 (default)
- Step Penalty: 0.01 (default)

**Funds Scaling**:
- Scale Factor: 0.001 (default)

**Reputation Scaling**:
- Scale Factor: 0.01 (default)

---

## 📊 Multiplier Formula

```csharp
// If dynamic disabled:
multiplier = baseMultiplier

// If dynamic enabled (Science example):
multiplier = baseMultiplier
multiplier -= (techTier * sciencePerTierPenalty)
multiplier -= (lifetimeScience / scienceStep) * scienceStepPenalty
multiplier = Clamp(multiplier, 0.01, 5.0)

// Applied to:
HighLogic.CurrentGame.Parameters.Career.ScienceGainMultiplier
HighLogic.CurrentGame.Parameters.Career.FundsGainMultiplier
HighLogic.CurrentGame.Parameters.Career.RepGainMultiplier
```

---

## 🎯 Scene Requirements

✅ Works in: **Space Center only**
❌ Does not work in: Flight, Editor, Tracking Station, etc.

---

## 📡 Game Events Subscribed

- `OnTechnologyResearched` → Recalculate
- `OnScienceReceived` → Recalculate
- `OnFundsChanged` → Recalculate
- `OnReputationChanged` → Recalculate

---

## 💾 Persistence

**Stored in**: `saves/[SaveName]/persistent.sfs`

**Fields Saved**:
- All base multipliers
- All toggle states
- All dynamic parameters

**Auto-loaded when**: Game loads

---

## 🔧 Class Breakdown

```
DynamicResourceGains [MonoBehaviour]
├── Toolbar Integration
├── GUI Rendering
├── Event Handlers
└── Multiplier Application

DRGScenario [ScenarioModule]
├── Configuration Persistence
└── Auto Save/Load

DRGUtil [Static]
├── GetTechTier()
├── GetLifetimeScience()
└── ClampMultiplier()
```

---

## 🛠️ Compilation Requirements

- ✅ Visual Studio 2015+
- ✅ .NET Framework 4.7.2
- ✅ KSP DLL References (auto-configured by setup script)

---

## 📋 Build Checklist

- [ ] Run Setup-KSPReferences.ps1
- [ ] Build → Build Solution
- [ ] Check bin\Release\ for DLL
- [ ] Create GameData folder structure
- [ ] Copy DLL to Plugins folder
- [ ] Start KSP and test

---

## 🎲 Default Configuration

```yaml
Base Multipliers:
  Science: 100%
  Funds: 100%
  Reputation: 100%

Dynamic Scaling:
  Science: OFF
  Funds: OFF
  Reputation: OFF

Dynamic Parameters:
  SciencePerTierPenalty: 0.05
  ScienceStep: 10000
  ScienceStepPenalty: 0.01
  FundsScaleFactor: 0.001
  RepScaleFactor: 0.01

Multiplier Range: 0.01x - 5.0x
```

---

## 🐛 Common Issues & Fixes

| Issue | Solution |
|-------|----------|
| Compilation fails | Run Setup-KSPReferences.ps1 |
| DLL not found | Check bin\Release\ folder |
| Mod not loading | Verify GameData\DynamicResourceGains\Plugins\ path |
| GUI not appearing | Go to Space Center scene |
| No toolbar button | Check that addon loaded (KSP log) |
| Multipliers not applying | Click "Apply" button in GUI |
| Settings lost on reload | Click "Save" button before quitting |

---

## 📞 Key Methods

### DynamicResourceGains
- `Start()` - Initialize
- `OnGUI()` - Render window
- `ApplyMultipliers()` - Parse & apply settings
- `CalculateScienceMultiplier()` - Compute with dynamics
- `CalculateFundsMultiplier()` - Compute with dynamics
- `CalculateReputationMultiplier()` - Compute with dynamics
- `OnTechnologyResearched()` - Event handler
- `OnScienceReceived()` - Event handler
- `OnFundsChanged()` - Event handler
- `OnReputationChanged()` - Event handler

### DRGUtil
- `GetTechTier()` → int
- `GetLifetimeScience()` → float
- `ClampMultiplier(float, min, max)` → float

---

## 🔐 Data Safety

- Configuration saved to game save file (persistent.sfs)
- No external files required
- Safe to uninstall (settings preserved if reinstalled)
- No game data corruption risk

---

## 📚 Documentation Files

1. **README.md** - Feature overview
2. **SETUP_GUIDE.md** - Detailed setup steps
3. **IMPLEMENTATION.md** - Architecture details
4. **REFERENCES_EXAMPLES.md** - Configuration examples
5. **COMPLETED_SUMMARY.md** - Full status report
6. **QUICKREF.md** - This file

---

## 🎓 Example Usage Scenario

```
Player wants to make career harder as they progress:

1. Set Base Science to 150% (generous start)
2. Enable Dynamic Science
3. Set Science Per Tier to 0.10 (10% less per tech)
4. After 10 techs: 1.5 - (10 × 0.10) = 0.5x (50%)
5. As they progress further, it scales down
6. Click Apply to activate
7. Click Save to persist
```

---

## ✨ Feature Highlights

✅ Real-time adjustment (no restart needed)
✅ Draggable GUI window
✅ Expandable settings sections
✅ Read-only progress displays
✅ Automatic persistence
✅ Event-driven (not frame-driven)
✅ Multiplier clamping (prevents broken game states)
✅ Clean, simple interface
✅ No external dependencies

---

## 📞 Support Resources

- KSP Wiki: https://wiki.kerbalspaceprogram.com
- KSP API: http://api.kerbalspaceprogram.com/
- Modding Tutorials: https://forum.kerbalspaceprogram.com/index.php

---

**Version**: 1.0  
**Target**: KSP 1.x  
**Framework**: .NET 4.7.2  
**Status**: ✅ Production Ready
