# Merged Mods

This folder contains completed **Deltamod-compatible merged builds** that combine multiple DELTARUNE mods into one tested project.

## Published projects

### DELTARUNE Compatibility Merge

Folder: [`deltarune-compatibility-merge/`](deltarune-compatibility-merge/README.md)

- Current version: **1.0.7**
- Combines Custom Difficulty, Better Saves, 60 FPS, Chapter 1 Modernized, No-Hat Ralsei, and New ACTs in the Knight Fight.
- Includes package metadata, patch/output hashes, binary manifest, source-mod credits, complete changelog, and validation report.
- The validated 14 MB Deltamod ZIP remains in the current workspace; the connected GitHub writer cannot transfer that binary directly, so the release is archived here as a complete project folder rather than an empty archive.

## Folder layout

```text
merged mods/
├── README.md
└── deltarune-compatibility-merge/
    ├── README.md
    ├── docs/
    ├── package/
    └── release/
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
