# Merged Mods

This folder contains completed **Deltamod-compatible merged builds** that combine multiple DELTARUNE mods into one tested project.

## Published projects

### DELTARUNE Compatibility Merge

Folder: [`deltarune-compatibility-merge/`](deltarune-compatibility-merge/README.md)

- Current version: **1.0.7**
- [Download the Deltamod ZIP](deltarune-compatibility-merge/release/DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip)
- Combines Custom Difficulty, Better Saves, 60 FPS, Chapter 1 Modernized, No-Hat Ralsei, and New ACTs in the Knight Fight.
- Includes package metadata, patch/output hashes, binary manifest, source-mod credits, complete changelog, and validation report.

### Item Giver + Secret Boss Challenge

Folder: [`item-giver-secret-boss-challenge/`](item-giver-secret-boss-challenge/README.md)

- Current version: **0.1.0**
- Combines Item Giver Mode v0.1.4 and Secret Boss Challenge v0.4.0.
- Uses one Deltamod CSX per chapter so both source patches are materialized together rather than depending on cross-mod G3MTool merging.
- Chapters 1, 2, and 5 include both mods; Chapters 3 and 4 include Item Giver because Secret Boss Challenge does not target those chapters.
- Includes a reproducible [`package/`](item-giver-secret-boss-challenge/package/) builder, exact output hashes, idempotency checks, Debug Mode compatibility checks, and a validation report.

## Folder layout

```text
merged mods/
├── README.md
├── deltarune-compatibility-merge/
│   ├── README.md
│   ├── docs/
│   ├── package/
│   └── release/
│       └── DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip
└── item-giver-secret-boss-challenge/
    ├── README.md
    ├── LICENSES.md
    ├── package/
    │   ├── build_release.py
    │   ├── meta.json
    │   ├── modding.xml
    │   ├── README.txt
    │   └── LICENSES.md
    └── tests/
```

## Publishing standards

- Never silently remove a source-mod feature.
- Document every intentional behavior change and selected variant.
- Prefer source-level or resource-level merging over overwriting one mod with another.
- Keep Deltamod source checksums strict enough to stop on unsupported game versions.
- Re-test every affected chapter after changing the merge.
- Preserve original authorship and licensing information.
- Do not commit original DELTARUNE executables or complete `data.win` files.

[Return to the archive index](../README.md)
