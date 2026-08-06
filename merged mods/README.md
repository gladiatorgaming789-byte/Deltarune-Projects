# Merged Mods

This folder contains completed **Deltamod-compatible merged builds** that combine multiple DELTARUNE mods into one tested package.

## Published projects

_No completed merged build has been archived here yet._

Merged builds remain on the `merged-mods` branch while conflicts are being resolved and validation is still in progress.

## What belongs here

A merged build should be used when two or more mods change the same DELTARUNE resources and cannot be safely installed side by side. It must preserve the intended behavior of each source mod as closely as possible.

Each project folder should include:

- A Deltamod-compatible release archive.
- A README naming every included mod, version, author, and selected variant.
- Clear notes about compromises, omitted components, and unresolved conflicts.
- Installation and update instructions.
- Compatibility information for each affected chapter.
- Validation results for patching, launching, and important gameplay behavior.
- Credits and licensing information for all source projects.

## Merge standards

- Never silently remove a source mod feature.
- Document every intentional behavior change.
- Prefer source-level or resource-level merging over overwriting one mod with another.
- Keep checksums and patch anchors strict enough to stop on an unsupported game version.
- Re-test every affected chapter after changing the merge.
- Confirm that the final package installs through Deltamod.

## Suggested project layout

```text
merged mods/
└── merge-name/
    ├── README.md
    ├── Merge_Name_v1.0.0_Deltamod.zip
    ├── source-or-patches/
    └── tests/
```

[Return to the archive index](../README.md)
