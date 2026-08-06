# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.7 — Graze sprite canvas hotfix**

SHA-256: `f6d0fa4ed8328b283f47fc95ebb5ff0c2c17527bede19a56c2c5bacf1f3e3b61`

### Hotfix changes

- Fixed the cut-off bottom point on `spr_bhero_graze` in Chapters 2–4.
- The sprite used a 99×90 canvas with `OriginY = 89`, and the visible heart point occupied the final row. With no transparent row below it, the point looked chopped off.
- Expanded the logical sprite canvas and frame bounding height from 90 to 92 pixels.
- Preserved the texture pixels, origin, collision margins, and draw position, so the sprite does not move or change collision behavior.
- Verified the repaired sprite exports as 99×92 with visible alpha ending at row 89 and two transparent rows beneath it.
- Preserves the Chapter 1 elevator cursor fix and the user-supplied Pink Fight `pink.ogg` from version 1.0.6.

## Sprite audit

- Chapter 1 Modernized and No-Hat imported frame PNGs and sprite metadata were compared against their donor files and match their intended donors.
- The standard `spr_grazeappear` and `spr_grazemask` resources match the clean game.
- `spr_bhero_graze` was the graze resource whose visible pixels reached the exact bottom canvas boundary.

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

- Chapters 2–4 were reopened with UndertaleModTool CLI and confirmed to contain `spr_bhero_graze` at height 92, origin Y 89, and frame bounding height 92.
- Every v1.0.7 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted, every internal hash was checked, and all six patches were tested again from the packaged copies.
