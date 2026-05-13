# 🎮 Dynamic Resource Gains - Quick Summary

**Polish Documentation is available!** See [README.md](README.md) for links to Polish user guides.

---

## What This Mod Does

Dynamic Resource Gains is a KSP1 Career Mode mod that **dynamically adjusts Science, Funds, and Reputation multipliers** based on player progress.

### Features

✅ **Career Mode Only**
- Works exclusively in Career Mode
- Accessible from Space Center scene

✅ **Dynamic Scaling** (Optional)
- Science multiplier changes based on tech progression and lifetime science
- Funds multiplier scales with player wealth
- Reputation multiplier adjusts with current reputation

✅ **GUI Configuration**
- Toolbar button in Space Center
- Easy-to-use multiplier sliders/inputs
- Dynamic parameter controls
- Apply (test) and Save (persistent) buttons

✅ **Persistence**
- Settings saved per game
- Load game → multipliers stay configured

---

## Quick Start

1. **Install**: Copy `Dynamic Resource Gains.dll` to `GameData\DynamicResourceGains\Plugins\`
2. **Run**: Launch KSP in Career Mode
3. **Configure**: In Space Center, click DRG button → adjust multipliers → Apply → Save

---

## Documentation (Polish)

- **[INSTRUKCJA.md](INSTRUKCJA.md)** - Full user guide (Installation, GUI, parameters, examples, troubleshooting)
- **[PRZEPISY.md](PRZEPISY.md)** - 7 ready-made configurations
- **[CHEATSHEET.md](CHEATSHEET.md)** - Quick reference
- **[FAQ.md](FAQ.md)** - Common questions and answers

---

## For Developers

### Project Structure

```
Dynamic Resource Gains\
├── DynamicResourceGains.cs   (Main addon class, GUI, events)
├── DRGScenario.cs           (Persistence, configuration storage)
├── DRGUtil.cs               (Helper methods, calculations)
├── Properties\
│   └── AssemblyInfo.cs
└── Dynamic Resource Gains.csproj
```

### Requirements

- **.NET Framework 4.7.2**
- **KSP 1.x assemblies** (Assembly-CSharp.dll, UnityEngine.dll, UnityEngine.UI.dll)

### Building

```powershell
# Compile with:
msbuild "Dynamic Resource Gains.csproj"

# Or in Visual Studio:
# Build → Build Solution
```

### Key Classes

**DynamicResourceGains** (MonoBehaviour/KSPAddon)
- Toolbar button management
- GUI rendering (OnGUI)
- Event handlers
- Multiplier application

**DRGScenario** (ScenarioModule)
- Configuration persistence
- Save/load multiplier values
- ConfigNode serialization

**DRGUtil** (Static)
- Tech tier calculation
- Lifetime science retrieval
- Multiplier clamping

---

## Configuration Examples

### Easy Mode (Beginners)
```
Science: 300%
Funds: 250%
Reputation: 200%
```

### Progressive Difficulty (Recommended)
```
Science: 150% (base)
Dynamic Science: Enabled
  - Per Tier Penalty: 0.08
  - Science Step: 15000
  - Step Penalty: 0.02
```

→ See [PRZEPISY.md](PRZEPISY.md) for 7 complete recipes.

---

## Known Limitations

- **Career Mode Only** - Mod doesn't work in Sandbox
- **Space Center Only** - GUI only appears in Space Center scene
- **Single File DLL** - Currently distributed as single assembly
- **No Presets UI** - Presets must be entered manually (future enhancement?)

---

## Future Enhancements (Ideas)

- [ ] Preset configurations save/load
- [ ] Graph showing multiplier over time
- [ ] Export/import settings
- [ ] Science Mode support (if applicable)
- [ ] Multi-language support
- [ ] Localization (L10n)

---

## Troubleshooting

**Mod not appearing in Space Center?**
- Verify you're in Career Mode
- Check DLL location: `GameData\DynamicResourceGains\Plugins\`
- Check KSP log for `[DRG]` entries

**Settings not persisting?**
- Click "Save" button (not just "Apply")
- Ensure game saves after configuration

**Multipliers not applying?**
- Click "Apply" in GUI
- Perform an action (research tech, earn science)
- Check KSP log for errors

→ Full troubleshooting: [FAQ.md](FAQ.md)

---

## License

[Add your license information here]

---

## Credits

[Add credit information here]

---

**For Polish documentation, see [README.md](README.md)**
