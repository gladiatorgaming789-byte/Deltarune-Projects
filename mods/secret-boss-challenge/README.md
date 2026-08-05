# Secret Boss Challenge

**Version:** 0.1.0  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1 and 2  
**Installer:** Deltamod-compatible `.csx` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting to the in-game CONFIG menu.

When enabled:

- The Chapter 1 secret boss has increased HP, attack, defense, attack duration, and attack damage.
- The Chapter 2 secret boss has increased HP and attack and uses the game's built-in higher-intensity pattern behavior.
- Winning either encounter grants both of that boss's route-dependent equipment rewards.
- If an equipment inventory is full, the missing reward or rewards remain recoverable from the existing post-boss treasure chest. When both are pending, the chest can be opened twice.

When disabled, the original boss stats, patterns, rewards, and route logic are preserved.

## Installation

Install `Secret_Boss_Challenge_v0.1.0_Deltamod.zip` through Deltamod. Do not extract the archive into the game manually.

The archive uses two source-level `.csx` patch instructions:

- `chapter1_windows/data.win`
- `chapter2_windows/data.win`

The scripts patch exact code anchors at install time and produce a clear conflict error if another mod has changed the same section incompatibly. This avoids distributing game files and allows Deltamod to apply the mod alongside compatible patches.

## Compatibility

The release metadata checks the exact Chapter 1 and Chapter 2 files used during development:

- Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`

A newer game update may require regenerated anchors and checksums. The source patch scripts intentionally stop rather than applying to an unknown layout.

## Balance values

### Chapter 1

- HP: 3500 → 4550
- Attack: 10 → 12
- Defense: 5 → 6
- Attack duration: +15% for normal attack turns
- Bullet damage: +15%, rounded upward

### Chapter 2

- HP: 4809 → 6000
- Attack: 13 → 15
- Existing built-in high-intensity pattern mode enabled

## Development

The distributable scripts are included inside the release ZIP. They contain only patch anchors and original mod code; no original `data.win` or complete decompiled game source is included.

See [tests/TEST_REPORT.md](tests/TEST_REPORT.md) for validation details.
