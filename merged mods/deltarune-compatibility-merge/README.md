# DELTARUNE Compatibility Merge

Deltamod-compatible compatibility merge targeting the supplied Windows launcher and Chapters 1–5.

## Current release

**Version 1.0.7 — Graze sprite canvas hotfix**

- Workspace archive: `DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip`
- ZIP SHA-256: `f6d0fa4ed8328b283f47fc95ebb5ff0c2c17527bede19a56c2c5bacf1f3e3b61`
- Deltamod package ID: `gladiatorgaming.deltarune.compatibilitymerge`

The project is archived here as a folder because the connected GitHub writer cannot transfer the 14 MB binary workspace ZIP. The repository contains the package control files, exact binary/output manifests, complete release history, credits, compatibility decisions, and validation results. See [`release/README.md`](release/README.md).

## Fully merged

- Custom Difficulty 1.8.3 — Emmahaha
- Better Saves v7 — thej01
- Deltarune 60 FPS 1.1.20 — BadArtAdventure
- Deltarune Chapter 1 Modernized 1.1.0 — Qbix1234
- No-Hat Ralsei 1.4 — Serif0S & theginger
- New ACTs in the Knight Fight 2.7 — ToyBoyC
  - Selected variant: **Normal**

The requested Pink Fight `pink.ogg` override is included in the validated workspace release. The visual Pink Background patch is excluded because it targets an incompatible legacy Chapter 5 source and forced application corrupts pointer-bearing GameMaker resources.

## Latest changes

Version 1.0.7 fixes the bottom clipping on `spr_bhero_graze` in Chapters 2–4 by expanding its logical canvas from 99×90 to 99×92. Its origin, collision margins, visible pixels, and draw position remain unchanged.

Earlier hotfixes repaired:

- Chapter 1 elevator-choice soul alignment;
- Chapter 1 smart/random bullet target handling;
- Better Saves sprite imports and INI lifetime handling;
- Chapter 1 Modernized sprite and sound asset linking;
- Custom Difficulty menu sprite linking;
- the complete Better Saves Chapter 1 menu renderer.

## Project files

```text
deltarune-compatibility-merge/
├── README.md
├── docs/
│   ├── CHANGELOG.md
│   ├── SOURCE-MODS.md
│   └── VALIDATION.md
├── package/
│   ├── README.txt
│   ├── __deltaID.json
│   ├── checksums.txt
│   ├── meta.json
│   ├── modding.xml
│   └── package-file-hashes.txt
└── release/
    ├── BINARY-MANIFEST.md
    ├── README.md
    └── SHA256SUMS.txt
```

## Validation summary

- Launcher and Chapters 1–5 were reopened with UndertaleModTool during merge validation.
- Every final xdelta was decoded against its exact clean source and compared byte-for-byte with the intended output.
- The packaged ZIP was extracted and retested from its packaged patch copies.
- Exact source hashes in `package/meta.json` prevent installation against unsupported game builds.
- A complete beginning-to-end playthrough of every route and difficulty combination is not claimed.

See [the detailed validation report](docs/VALIDATION.md) and [full changelog](docs/CHANGELOG.md).
