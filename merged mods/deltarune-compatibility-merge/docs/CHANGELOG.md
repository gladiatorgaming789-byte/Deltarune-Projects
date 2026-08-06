# Changelog

## 1.0.7 — Graze sprite canvas hotfix

- Expanded `spr_bhero_graze` from 99×90 to 99×92 in Chapters 2–4.
- Added two transparent rows beneath the visible point while preserving `OriginY = 89`, collision margins, texture pixels, and draw position.
- Audited Chapter 1 Modernized and No-Hat sprite imports against their donors.

## 1.0.6 — Chapter 1 neo-choice cursor hotfix

- Corrected the `Prison B1 / Floor 1F` elevator choice cursor.
- Removed the hidden leading formatting space and `#` before line-count and cursor-position calculation.
- Replaced the Pink Fight music override with the user-supplied `pink.ogg`.
- Removed the ineffective Chapters 3–5 cursor experiment from 1.0.5.

## 1.0.5 — Superseded cursor attempt

- Replaced `pink.ogg` with the requested version.
- Attempted to adjust an unrelated board-writer choice renderer; superseded by the correct Chapter 1 fix in 1.0.6.

## 1.0.4 — Chapter 1 damage target hotfix

- Restored Modernized's `target == 4` smart/random party-target remapping in `scr_damage`.
- Prevented `global.charinstance[4]` out-of-range crashes when bullets hit the soul.
- Preserved Custom Difficulty damage scaling, iframe behavior, Modernized elemental reductions, and Better Saves debug invulnerability.

## 1.0.3 — Shared Custom Difficulty asset hotfix

- Imported `spr_darkmodsbt` and `spr_darkmodsfade` into Chapters 1–5.
- Recompiled `obj_darkcontroller_Draw_0` after the assets existed.
- Correctly relinked Chapter 1's `spr_tensionbar_cutout`.

## 1.0.2 — Chapter 1 asset-link hotfix

- Recompiled eight Modernized scripts after their added sprites and sounds were loaded.
- Correctly linked `spr_numbersfontbig_gold`, Clover face assets, battle visuals, spare effects, wheelbarrow parts, `snd_mercyadd`, and `snd_pacify`.

## 1.0.1 — Better Saves runtime hotfix

- Imported the four Better Saves icon sprites into the launcher and all chapters.
- Restored the complete Better Saves Chapter 1 menu draw implementation.
- Restored `dr.ini` after Custom Difficulty temporarily opened `difficulty.ini`, fixing undefined-INI errors during save scanning.

## 1.0.0 — Initial compatibility merge

- Combined Custom Difficulty, Better Saves, Deltarune 60 FPS, Chapter 1 Modernized, No-Hat Ralsei, and the Normal variant of New ACTs in the Knight Fight.
- Added the Pink Fight music override while excluding its incompatible legacy Chapter 5 visual patch.
- Generated Deltamod xdelta patches for the launcher and Chapters 1–5.
