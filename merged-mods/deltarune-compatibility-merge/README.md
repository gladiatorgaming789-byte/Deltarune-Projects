# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.6 — Chapter 1 neo-choice cursor hotfix**

SHA-256: `2d6f5e7ecddae052c32df352838a31f5a5437b4be2638e86f0470c0060bdaaa5`

### Hotfix changes

- Fixed the soul/cursor alignment on Chapter 1 Modernized's `Prison B1 / Floor 1F` elevator choice.
- The previous v1.0.5 change targeted the unrelated Chapter 3 board-writer renderer, which is why it had no visible effect.
- The actual Chapter 1 labels begin with a legacy space plus `#` blank-line marker.
- Modernized attempted to remove a leading `#` using string index `0`, but GameMaker's first character is index `1`, and the preceding space was not handled.
- The neo-choice initializer now strips leading formatting spaces and the first leading `#` before calculating line count and cursor position.
- The ineffective Chapters 3–5 cursor edits from v1.0.5 are not included.
- Replaced the Pink Fight music override with the newly supplied `pink.ogg`.
- Preserves the Better Saves, asset-link, Custom Difficulty menu, and Chapter 1 smart-target damage fixes from earlier releases.

## Fully merged

- Custom Difficulty 1.8.3 — Emmahaha
- Better Saves v7 — thej01
- Deltarune 60 FPS 1.1.20 — BadArtAdventure
- Deltarune Chapter 1 Modernized 1.1.0 — Qbix1234
- Serif0S' No-Hat Ralsei Face Resprites 1.4 — Serif0S & theginger
- New ACTs in the Knight Fight 2.7 — ToyBoyC
  - Selected variant: Normal

## Improved Pink Fight Background

The newly supplied `pink.ogg` override is included. The visual `data.win` patch is not included because the published patch requires Chapter 5 SHA-256 `7e3e9c4a0ef84f0129b6a1c9e9f81091e83abbafbf66eb09893c2082cf5618de`, while the supplied Chapter 5 file is `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`. Forced application produced invalid pointer-bearing GameMaker resources, so it was excluded rather than shipping corruption.

## Validation

- The repaired Chapter 1 file reopened successfully with UndertaleModTool CLI.
- The saved code decompiles with first-character index `1` and handles both leading spaces and the `#` marker.
- Every v1.0.6 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted, every internal hash was checked, and all six patches were tested again from the packaged copies.
