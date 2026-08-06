# DELTARUNE Projects

> Central archive for completed DELTARUNE modding projects maintained by GladiatorGaming.

All completed work is organized by project type. Installable builds must remain compatible with **Deltamod**. Development and testing happen in the current workspace; finished, documented, and validated projects are published here.

## Browse the archive

| Category | Purpose |
|---|---|
| [Mods](mods/README.md) | Original standalone mods. |
| [Merged Mods](merged%20mods/README.md) | Compatibility builds combining two or more existing mods. |
| [Compatibility Fixed Mods](compatibility%20fixed%20mods/README.md) | Existing mods repaired or updated for newer DELTARUNE builds. |

## Completed projects

### Standalone mods

- [Secret Boss Challenge](mods/secret-boss-challenge/README.md)
  - Versioned Deltamod ZIP releases
  - Source/project documentation
  - License and tests
- [Item Giver Mode](mods/item-giver-mode/README.md)
  - Current version: **0.1.3**
  - [Deltamod package source](mods/item-giver-mode/release/)
  - F7 item browser for Chapters 1–5
  - Items, weapons, armor, key items, and Light World items
  - Uses DELTARUNE's native persistent `obj_time` controller for reliable input and GUI handling
  - Source CSX patches and validation report

### Merged mods

- [DELTARUNE Compatibility Merge](merged%20mods/deltarune-compatibility-merge/README.md)
  - Current version: **1.0.7**
  - [Download the Deltamod ZIP](merged%20mods/deltarune-compatibility-merge/release/DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip)
  - Package metadata and Deltamod patch map
  - Exact source, patch, override, release, and output hashes
  - Source-mod credits and compatibility decisions
  - Complete changelog and validation report

## Branch guide

| Branch | Responsibility |
|---|---|
| `all-projects` | Completed and validated project archive. |
| `mod-development` | Development history for original standalone mods. |
| `merged-mods` | Historical development branch for merged compatibility builds. |
| `compatibility-fixes` | Development history for repaired or updated mods. |

## Publishing requirements

A project belongs here when it includes the materials available for the completed work:

- A Deltamod-compatible release archive when an installable build exists.
- A README with features, compatibility information, known limitations, and installation notes.
- Source patch scripts or other redistributable development files when available.
- A validation or test report.
- A clear latest version and release checksum.

## Repository rules

- Put original standalone mods under `mods/`.
- Put merged builds under `merged mods/`.
- Put repaired or updated existing mods under `compatibility fixed mods/`.
- Keep each project in its own folder.
- Update an existing project in place rather than creating duplicates.
- Work in the current workspace and publish only completed results here.
- Preserve original authorship and licensing information.
- Do not commit original DELTARUNE executables, complete `data.win` files, or other unmodified game files.

## Deltamod compatibility

Every installable mod and merged build must work through Deltamod. Projects modifying overlapping resources must document conflicts or provide a dedicated merged build.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
