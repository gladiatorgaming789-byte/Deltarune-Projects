# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.1 — Better Saves runtime hotfix**

SHA-256: `15cbc10c5d80d637799ce646447ed2a355b41ce7d4360394f473fa39e416f4f8`

### Hotfix changes

- Restored the four Better Saves icon sprites in the launcher and Chapters 1–5:
  - `spr_bettersaves_star`
  - `spr_bettersaves_crystal`
  - `spr_bettersaves_sideb`
  - `spr_bettersaves_star_sideb`
- Fixed the `DEVICE_MENU` Draw crash caused by those missing sprite resources.
- Restored the complete Better Saves Chapter 1 menu renderer. The previous merge had accidentally retained a large vanilla draw block while resolving 60 FPS timing edits.
- Fixed `Trying to write to undefined INI file` during save scanning. Custom Difficulty's `scr_gamestart()` temporarily switches to `difficulty.ini`; Better Saves now restores its active `dr.ini` before later metadata reads and writes.

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

- Launcher and Chapters 1–5 were freshly reopened with UndertaleModTool CLI.
- All four Better Saves icon sprites were confirmed present in every target.
- Every v1.0.1 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended repaired file.
- The packaged ZIP was extracted and all six patches were tested again from the packaged copies.

Windows runtime testing remains recommended, especially empty-save scanning, menu drawing, save copying/deletion, difficulty selection, Knight ACTs, and timing-sensitive attacks.
