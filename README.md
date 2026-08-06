# DELTARUNE Merged Mods

> Workspace branch for building and validating compatibility merges between DELTARUNE mods.

A merged build is needed when two or more mods change the same game resources and cannot safely be installed together. Every finished package on this branch must remain compatible with **Deltamod** and clearly document all included source mods.

## Current merged build

| Project | Current version | Status | Summary |
|---|---:|---|---|
| [DELTARUNE Compatibility Merge](merged-mods/deltarune-compatibility-merge/README.md) | 1.0.7 | Validated release | Combines several gameplay, save, performance, visual, and battle mods for the supplied Windows Chapters 1–5 build. |

The project README contains the exact included mod versions, selected variants, exclusions, checksums, hotfix details, and validation results.

## Repository layout

```text
merged-mods/
└── merge-name/
    ├── README.md
    ├── Deltamod release archive
    ├── merge scripts or redistributable patches
    └── tests or validation reports
```

The hidden `.merge-tools/` directory contains supporting patch utilities used by the merge workflow. It is not a user-installable mod.

## Merge workflow

1. Inventory every source mod, version, author, license, and supported game build.
2. Identify overlapping scripts, sprites, rooms, objects, tables, audio, and other resources.
3. Merge conflicting changes intentionally instead of allowing one mod to overwrite another.
4. Build a single Deltamod-compatible package.
5. Decode or apply every packaged patch against clean target files.
6. Reopen patched files with the appropriate tooling and verify affected resources.
7. Test startup and important gameplay across every affected chapter.
8. Document compromises, exclusions, known conflicts, and validation results.

## Merge standards

- Preserve the intended behavior of every included mod whenever technically possible.
- Never silently omit a feature or substitute a different variant.
- Record every source mod's author and version.
- Keep checksum and anchor validation strict enough to reject unsupported game files.
- Re-test the entire package whenever one component changes.
- Do not include original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.

## Publishing

Completed merged builds are archived under `merged mods/` on the `all-projects` branch. Development copies should remain here until the package and documentation agree and validation is complete.

## Related branches

- `mod-development` — original standalone mods.
- `compatibility-fixes` — repairs and updates for existing mods.
- `all-projects` — completed and validated release archive.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
