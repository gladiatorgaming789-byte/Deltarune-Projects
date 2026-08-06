# Merged Mods

This folder contains completed **Deltamod-compatible merged builds** that combine multiple DELTARUNE mods into one tested package.

## Published projects

### DELTARUNE Compatibility Merge

Folder: [`deltarune-compatibility-merge/`](deltarune-compatibility-merge/README.md)

- Current documented version: **1.0.7**
- Combines multiple DELTARUNE mods into one compatibility-tested project.
- Documents the included mod set, Pink Fight audio handling, graze sprite hotfix, hashes, and validation.
- No distributable ZIP is currently stored in the repository, so the completed project is preserved as a folder rather than an empty or misleading archive.

## What belongs here

A merged build belongs here when two or more mods are combined into one compatibility-tested project, especially when they modify overlapping DELTARUNE resources.

Each project folder should include the materials currently available for that completed merge:

- A Deltamod-compatible release archive when one is available.
- A README naming every included mod, version, author, and selected variant.
- Clear notes about compromises, omitted components, and unresolved conflicts.
- Installation and update instructions when a release exists.
- Compatibility information for each affected chapter.
- Validation results for patching, launching, and important gameplay behavior.
- Credits and licensing information for all source projects.

## Merge standards

- Never silently remove a source mod feature.
- Document every intentional behavior change.
- Prefer source-level or resource-level merging over overwriting one mod with another.
- Keep checksums and patch anchors strict enough to stop on an unsupported game version.
- Re-test every affected chapter after changing the merge.
- Confirm that installable packages work through Deltamod.

## Project layout

```text
merged mods/
└── deltarune-compatibility-merge/
    └── README.md
```

[Return to the archive index](../README.md)
