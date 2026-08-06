# Validation report — v1.0.7

## Package identity

- Archive: `DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip`
- SHA-256: `f6d0fa4ed8328b283f47fc95ebb5ff0c2c17527bede19a56c2c5bacf1f3e3b61`
- Deltamod package ID: `gladiatorgaming.deltarune.compatibilitymerge`
- Target version: DELTARUNE 1.49 Windows launcher and Chapters 1–5

## Structural validation

- Launcher and Chapters 1–5 were reopened with UndertaleModTool CLI during the merge and hotfix stages.
- Every final xdelta was decoded against its exact clean source file.
- Each reconstructed result was compared byte-for-byte with the intended merged `data.win` output.
- The final ZIP was extracted to a clean directory before the packaged patches were tested again.
- Package metadata uses exact source SHA-256 values so Deltamod stops on unsupported game builds.

## Feature-specific validation

- Better Saves icon sprites exist in the launcher and every chapter.
- Better Saves reopens `dr.ini` after Custom Difficulty initializes `difficulty.ini`.
- Added Custom Difficulty sprites resolve as asset constants rather than instance variables.
- Chapter 1 Modernized sprites and sounds were relinked after resource import.
- Chapter 1 `scr_damage` remaps target marker `4` before indexing party arrays.
- Chapter 1 elevator choices strip their hidden leading formatting marker before cursor placement.
- Chapters 2–4 contain `spr_bhero_graze` at 99×92 with origin Y 89 and two transparent bottom rows.
- The Normal Knight ACT variant is used.
- The requested Pink Fight audio override is represented by the hash in `release/BINARY-MANIFEST.md`.

## Known limitation

The Improved Pink Fight Background visual `data.win` patch is not included. The published donor targets Chapter 5 SHA-256 `7e3e9c4a0ef84f0129b6a1c9e9f81091e83abbafbf66eb09893c2082cf5618de`, while the supplied Chapter 5 source is `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`. Forced decoding produced invalid pointer-bearing GameMaker resources, so the visual patch was excluded instead of shipping corruption.

## Runtime status

Reported startup, menu, asset-link, damage, cursor, and graze-display defects through v1.0.7 were addressed. A full beginning-to-end gameplay playthrough of every route and difficulty combination is not claimed.
