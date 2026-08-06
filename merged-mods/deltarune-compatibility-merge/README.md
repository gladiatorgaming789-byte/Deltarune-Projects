# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.4 — Chapter 1 smart-target damage hotfix**

SHA-256: `59d47e5fd86a0ea1bac1c32aa5c2176e98baf2a3def371a75375822f7a803f95`

### Hotfix changes

- Fixed the Chapter 1 bullet-contact crash in `scr_damage`:
  - `Variable Index [4] out of range [3] - charinstance`
- Chapter 1 Modernized uses target value `4` as a smart/random-target marker. The original merge retained the newer `scr_randomtarget` behavior but omitted the corresponding `target == 4` conversion block, so bullets attempted to access `global.charinstance[4]` directly.
- Restored the complete smart-target flow:
  - choose a valid party member before reading `global.charinstance[target]`;
  - preserve Modernized damage calculation and elemental damage reduction;
  - preserve Custom Difficulty damage multipliers, down-state deficit, and iframe scaling;
  - preserve Better Saves debug invulnerability support;
  - restore the bullet's original target marker after damage processing.
- Chapters 2–5 were checked and already contained their correct target-4 remapping, so their binaries remain unchanged.
- Preserves the Better Saves and added-resource fixes from versions 1.0.1–1.0.3.

## Fully merged

- Custom Difficulty 1.8.3 — Emmahaha
- Better Saves v7 — thej01
- Deltarune 60 FPS 1.1.20 — BadArtAdventure
- Deltarune Chapter 1 Modernized 1.1.0 — Qbix1234
- Serif0S' No-Hat Ralsei Face Resprites 1.4 — Serif0S & theginger
- New ACTs in the Knight Fight 2.7 — ToyBoyC
  - Selected variant: Normal

## Improved Pink Fight Background

The `pink.ogg` override is included. The visual `data.win` patch is not included because the published patch requires Chapter 5 SHA-256 `7e3e9c4a0ef84f0129b6a1c9e9f81091e83abbafbf66eb09893c2082cf5618de`, while the supplied Chapter 5 file is `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`. Forced application produced invalid pointer-bearing GameMaker resources, so it was excluded rather than shipping corruption.

## Compatibility decisions

- Better Saves remains authoritative for extra save slots.
- Custom Difficulty `difficulty.ini` sections follow Better Saves copy, delete, and slot-shift operations.
- Difficulty-adjusted timers are composed with 60 FPS scaling.
- Chapter 1 Modernized visuals and sounds are imported while No-Hat Ralsei wins its overlapping face sprite.
- Knight ACT additions are merged with Chapter 3 difficulty and timing code.

## Validation

- The repaired Chapter 1 file was freshly reopened with UndertaleModTool CLI.
- `scr_damage` was decompiled from the saved output and confirmed to contain `target == 4` remapping, `__remtarget` restoration, Custom Difficulty damage/iframe logic, and Modernized element reduction.
- Every version 1.0.4 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted, every internal hash was checked, and all six patches were tested again from the packaged copies.

Windows runtime testing remains recommended, especially bullet damage in Chapter 1, save-slot operations, difficulty selection, Knight ACTs, and timing-sensitive attacks.
