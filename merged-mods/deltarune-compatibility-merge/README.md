# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.3 — Shared Custom Difficulty asset-link hotfix**

SHA-256: `5d61f39dc62452152e214e3f8597b6f902e988dc644d01bc792278c6c404ed2a`

### Hotfix changes

- Fixed the `obj_darkcontroller` Draw crash affecting Chapters 1–5.
- Imported Custom Difficulty's two missing dark-world menu sprites into every chapter:
  - `spr_darkmodsbt`
  - `spr_darkmodsfade`
- Recompiled `gml_Object_obj_darkcontroller_Draw_0` after those sprites were loaded, so both names are linked sprite constants rather than unresolved instance-variable reads.
- Recompiled Chapter 1's `gml_Object_obj_tensionbar_Draw_0` to correctly link the remaining Modernized sprite `spr_tensionbar_cutout`.
- Preserves the Better Saves sprite/INI-context fixes from version 1.0.1 and the Chapter 1 Modernized asset relinking from version 1.0.2.

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

- Chapters 1–5 were freshly reopened with UndertaleModTool CLI.
- Every asset added by Better Saves, Custom Difficulty, and Chapter 1 Modernized was confirmed present.
- Bytecode contains zero unresolved variable-style references to those added assets.
- Every version 1.0.3 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted, all internal hashes were checked, and all six patches were tested again from the packaged copies.

Windows runtime testing remains recommended, especially save-slot operations, difficulty selection, Knight ACTs, and timing-sensitive attacks.
