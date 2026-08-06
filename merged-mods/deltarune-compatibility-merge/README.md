# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.2 — Chapter 1 asset-link hotfix**

SHA-256: `bbb558404c5257af7e609d5e69a1167df905b684ac95ef6811d860c5e812a9cc`

### Hotfix changes

- Fixed the Chapter 1 startup crash for `spr_numbersfontbig_gold`.
- Root cause: the Modernized sprites and sounds existed in the final file, but eight scripts had been compiled before those newly added resources were imported. GameMaker therefore compiled names such as `spr_numbersfontbig_gold` as instance-variable reads (`self.spr_numbersfontbig_gold`) instead of asset constants.
- Recompiled all eight affected scripts after the resources were loaded:
  - `gml_Object_obj_battleblcon_Draw_0`
  - `gml_GlobalScript_scr_miniface_init_clover`
  - `gml_Object_obj_initializer2_Create_0`
  - `gml_Object_obj_initializer2_Other_72`
  - `gml_Object_obj_initializer_Create_0`
  - `gml_Object_obj_pacifyspell_Step_0`
  - `gml_Object_obj_dkris_event_Draw_0`
  - `gml_GlobalScript_scr_mercyadd`
- Correctly linked the affected Modernized sprites and sounds, including `spr_numbersfontbig_gold`, `spr_spare_z`, `spr_battleblcon_parts`, Clover face sprites, wheelbarrow parts, `snd_mercyadd`, and `snd_pacify`.
- Preserves the Better Saves sprite and INI-context fixes from version 1.0.1.

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

- Chapter 1 was freshly reopened with UndertaleModTool CLI.
- Nineteen targeted Modernized and Better Saves asset references were checked at bytecode level and confirmed linked as asset constants rather than unresolved instance variables.
- Every version 1.0.2 xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted, its internal hashes were checked, and all six patches were tested again from the packaged copies.

Windows runtime testing remains recommended, especially save-slot operations, difficulty selection, Knight ACTs, and timing-sensitive attacks.
