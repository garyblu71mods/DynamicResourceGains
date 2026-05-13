# Dynamic Resource Gains v1.0.0 - Release Notes

## 🎮 Overview

**Dynamic Resource Gains** is a Kerbal Space Program (KSP) Career Mode addon that dynamically adjusts Science, Funds, and Reputation gain multipliers based on your progression. Control your economy and difficulty with granular, easy-to-use configuration options.

## ✨ Key Features

### 🎯 Career Mode Integration
- Works exclusively in Career Mode
- Accessible from the Space Center scene only
- Easy toolbar button for quick access

### 📊 Dynamic Multiplier System
- **Science**: Adjusts based on tech tree progress and lifetime science earned
- **Funds**: Scales with your current bank balance (wealth penalty)
- **Reputation**: Adjusts based on your current reputation level

### ⚙️ Flexible Configuration
- Set base multipliers (100% = normal, 200% = double, 50% = half)
- Toggle dynamic scaling on/off for each resource
- Fine-tune penalty parameters (per-tier, per-step rates)
- Apply changes instantly or save for persistence

### 💾 Persistent Settings
- All settings saved per save game
- Load game → your multipliers are exactly as you configured them
- Apply button to test changes without saving

### 🖥️ User-Friendly GUI
- Clean, organized interface
- "Current Status" table shows real-time effective multipliers
- Manual/Help window with quick reference
- Tooltips on hover for parameter explanations

## 🔄 What's New in v1.0.0

### Initial Release Features
✅ Base multiplier controls (Science, Funds, Reputation)  
✅ Dynamic Science scaling (tech tier penalty + science step penalty)  
✅ Dynamic Funds scaling (wealth-based penalty)  
✅ Dynamic Reputation scaling (reputation-based penalty)  
✅ Real-time penalty calculation display  
✅ Apply/Save/Reset buttons  
✅ In-game help window  
✅ Persistent game-specific configuration  

### UI Improvements (Latest Build)
✅ **40% larger fonts** for better readability  
✅ **Optimized window size** (compact layout, no wasted space)  
✅ **Clearer penalty display** (combined Science tier+step penalty)  
✅ **Better labels** ("Penalty" instead of abbreviated "Pnl")  
✅ **Improved button sizing** for consistency  
✅ **Real-time Current Status table** showing:
  - Base multipliers you've set
  - Actual penalties being applied
  - Effective multipliers in use

## 📋 Installation

1. **Download** `DynamicResourceGains_v1.0.0.zip`
2. **Extract** the DLL and README to your KSP folder
3. **Organize** folder structure:
   ```
   KSP_Installation/
   └── GameData/
       └── DynamicResourceGains/
           └── Plugins/
               └── Dynamic Resource Gains.dll
   ```
4. **Launch KSP** in Career Mode
5. **Access** the mod from Space Center scene (toolbar button with graph icon)

## 🎮 Quick Start Guide

### Basic Setup
1. Click the DRG toolbar button in Space Center
2. Set base multipliers in "Career Multipliers" section
   - Example: Sci: 200, $: 100, Rep: 150 (science gains are 2x, funds normal, rep 1.5x)
3. Click **APPLY** to test changes immediately
4. Check "Current Status" table to see effective multipliers
5. Click **SAVE** to make changes persistent

### Understanding the Current Status Table
- **Base**: Your configured base multiplier values
- **Penalty**: Active penalties from dynamic scaling
- **Eff** (Effective): Actual multiplier the game is using (Base - Penalty)

### Dynamic Features (Optional)
Enable and configure these for automatic difficulty scaling:

**Dynamic Sci** (Science penalty increases as you progress)
- **Tier**: % penalty per unlocked tech tier
- **Step**: How much science triggers another penalty step
- **Penalty per Step**: Additional % penalty for each step

**Dynamic $** (Funds penalty increases as you get wealthy)
- **Step**: How many funds trigger a penalty step
- **Penalty per Step**: % penalty for each funds milestone

**Dynamic Rep** (Reputation penalty as you become famous)
- **Step**: How much reputation triggers a penalty step
- **Penalty per Step**: % penalty for each rep milestone

## 🛠️ System Requirements

- **KSP Version**: 1.x (tested on 1.12.x)
- **.NET Framework**: 4.7.2+
- **Game Mode**: Career Mode only

## 📖 Full Documentation

For detailed documentation, see:
- **README_EN.md** - Quick technical overview
- **[Polish Docs Available]** - Instrukcja.md, Przepisy.md, FAQ.md, Cheatsheet.md

## 🐛 Known Issues & Limitations

- Mod only works in Career Mode (Space Center scene)
- Multipliers are saved per individual save game, not globally
- Career contracts remain unaffected (only resource gains change)

## 🚀 Tips & Tricks

1. **Progressive Difficulty**: Start with base multipliers at 200% (easy), then enable Dynamic features to increase difficulty as you progress
2. **Testing**: Use APPLY button to test changes before SAVE
3. **No Penalties**: Set penalty values to 0 for linear/static multipliers
4. **Combined Effect**: Dynamic scaling applies on top of base multipliers (both can work together)

## 📞 Support & Feedback

- Report issues on GitHub: https://github.com/garyblu71mods/DynamicResourceGains
- Submit feature requests and bug reports
- Community feedback welcome!

## 📜 License & Credits

**Author**: garyblu71  
**Version**: 1.0.0  
**Release Date**: May 2026

---

## Version History

### v1.0.0 (2026-05-13)
- Initial release
- Core multiplier system
- Dynamic scaling for Science, Funds, Reputation
- Persistent configuration
- User-friendly GUI with help system
- UI optimizations for readability and usability

---

**Enjoy your balanced KSP career! Happy exploring! 🚀🌙**
