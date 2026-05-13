# Dynamic Resource Gains - English User Guide

This guide explains what the mod does, how to install it, how to use the GUI, and what each setting means.

---

## What this mod does

Dynamic Resource Gains is a Kerbal Space Program 1 Career Mode mod that adjusts:
- Science gain
- Funds gain
- Reputation gain

It can use fixed base multipliers or dynamic scaling based on player progress.

---

## Requirements

- Kerbal Space Program 1.x
- Career Mode
- The mod button is available only in the Space Center scene

---

## Installation

Install the DLL here:

```text
KSP\GameData\DynamicResourceGains\Plugins\Dynamic Resource Gains.dll
```

Example path:

```text
C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\DynamicResourceGains\Plugins\Dynamic Resource Gains.dll
```

---

## How to open the mod

1. Start KSP
2. Load a Career save
3. Go to the Space Center
4. Click the DRG button in the toolbar

If you are not in Space Center, the button will not appear.

---

## GUI overview

The window contains:
- Base multipliers for Science, Funds, and Reputation
- Dynamic toggles for each category
- Extra settings for dynamic scaling
- Apply button
- Save button
- Vertical scrolling for longer content

---

## Base multipliers

These are percentage values.

### Default values
- Base Science Multiplier: `100`
- Base Funds Multiplier: `100`
- Base Reputation Multiplier: `100`

### Meaning
- `100` = normal gain
- `50` = half gain
- `200` = double gain

---

## Default dynamic settings

### Dynamic toggles
By default these are enabled:
- Dynamic Science: ON
- Dynamic Funds: ON
- Dynamic Reputation: ON

### Dynamic Science
Default values:
- Science per Tier Penalty: `0.02`
- Science Step: `3000`
- Science Step Penalty: `0.001`

Meaning:
- each unlocked tech tier reduces science multiplier slightly
- every 3000 lifetime science adds another penalty step

### Dynamic Funds
Default value:
- Funds Scale Factor: `0.00002`

Meaning:
- the more funds you have, the lower the funds multiplier becomes

### Dynamic Reputation
Default value:
- Reputation Scale Factor: `0.001`

Meaning:
- the more reputation you have, the lower the reputation multiplier becomes

---

## Buttons

### Apply
Applies the current values immediately in the current session.

### Save
Saves the current values so they persist after reloading the save.

Recommended workflow:

```text
Change values -> Apply -> Test in game -> Save
```

---

## Example setups

### Balanced default
- Science: `100`
- Funds: `100`
- Reputation: `100`
- Dynamic Science: ON
- Dynamic Funds: ON
- Dynamic Reputation: ON

### Easier early game
- Science: `125`
- Funds: `120`
- Reputation: `110`
- Keep all dynamic systems ON

### Harder career
- Science: `75`
- Funds: `80`
- Reputation: `85`
- Keep all dynamic systems ON

### Fixed multipliers only
- Science: `100`
- Funds: `100`
- Reputation: `100`
- Dynamic Science: OFF
- Dynamic Funds: OFF
- Dynamic Reputation: OFF

---

## Notes about the current GUI

- The old Minimize button was removed
- The window now supports vertical scrolling
- Use the mouse wheel or scroll interaction inside the window to move up and down

---

## Troubleshooting

### No icon in game
Check:
- Are you in Career Mode?
- Are you in Space Center?
- Is the DLL in the correct GameData path?

### Settings are not saved
You probably clicked Apply but not Save.

### Values do not seem to change anything
Make sure:
- you clicked Apply
- you are testing in Career Mode
- you performed an in-game action that uses Science, Funds, or Reputation

---

## File purpose

This file is the full English user guide for players.
