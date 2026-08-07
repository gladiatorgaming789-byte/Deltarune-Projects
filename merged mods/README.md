# Merged Mods

This folder contains completed **Deltamod-compatible merged builds** that intentionally combine multiple DELTARUNE mods into one project.

## Published projects

### DELTARUNE Compatibility Merge

Folder: [`deltarune-compatibility-merge/`](deltarune-compatibility-merge/README.md)

- Current version: **1.0.7**
- [Download the Deltamod ZIP](deltarune-compatibility-merge/release/DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip)
- Combines Custom Difficulty, Better Saves, 60 FPS, Chapter 1 Modernized, No-Hat Ralsei, and New ACTs in the Knight Fight.
- Includes package metadata, patch/output hashes, binary manifest, source-mod credits, complete changelog, and validation report.

## Retired compatibility package

The former `item-giver-secret-boss-challenge/` combined project was removed. Item Giver Mode v0.2.0 and Secret Boss Challenge v0.4.1 now use Deltamod-native `g3mpatch` releases and are intended to remain **separate mods**. Their shared chapters were tested through Deltamod 2.0.4's bundled G3MTool in both patch orders with zero G3M conflicts.

## Folder layout

```text
merged mods/
├── README.md
└── deltarune-compatibility-merge/
    ├── README.md
    ├── docs/
    ├── package/
    └── release/
        └── DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip
```

## Publishing standards

- Never silently remove a source-mod feature.
- Document every intentional behavior change and selected variant.
- Prefer source/resource-level merging over overwriting one mod with another.
- Keep Deltamod source checksums strict enough to stop on unsupported game versions.
- Re-test every affected chapter after changing the merge.
- Preserve original authorship and licensing information.
- Do not commit original DELTARUNE executables or complete `data.win` files.

[Return to the archive index](../README.md)
