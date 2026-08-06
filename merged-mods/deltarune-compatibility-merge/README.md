# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Fully merged

- Custom Difficulty 1.8.3 — Emmahaha
- Better Saves v7 — thej01
- Deltarune 60 FPS 1.1.20 — BadArtAdventure
- Deltarune Chapter 1 Modernized 1.1.0 — Qbix1234
- Serif0S' No-Hat Ralsei Face Resprites 1.4 — Serif0S & theginger
- New ACTs in the Knight Fight 2.7 — ToyBoyC
  - Selected variant: Normal

## Improved Pink Fight Background

The `pink.ogg` override is included. The visual `data.win` patch is not included because both the uploaded archive and the currently published Deltamod archive require Chapter 5 SHA-256 `7e3e9c4a0ef84f0129b6a1c9e9f81091e83abbafbf66eb09893c2082cf5618de`, while the supplied Chapter 5 file is `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`. Forced application produced invalid pointer-bearing GameMaker resources, so it was excluded rather than shipping corruption.

## Compatibility decisions

- Better Saves remains authoritative for extra save slots.
- Custom Difficulty `difficulty.ini` sections follow Better Saves copy, delete, and slot-shift operations.
- Difficulty-adjusted timers are composed with 60 FPS scaling.
- Chapter 1 Modernized visuals and sounds are imported while No-Hat Ralsei wins its overlapping face sprite.
- Knight ACT additions are merged with Chapter 3 difficulty and timing code.

## Validation

- Launcher and Chapters 1–5 reopen and decompile with UndertaleModTool CLI.
- Every final xdelta patch was decoded against its clean source and compared byte-for-byte with the intended merged file.
- The packaged ZIP was extracted and all six patches were tested again from the packaged copies.
- Final package SHA-256: `277f2e94f1ad72acd1d9c462d8225863b45b2d472536fec173407e89e700bafd`

Full Windows gameplay testing is still recommended, especially save-slot operations, difficulty selection, Knight ACTs, and timing-sensitive attacks.
