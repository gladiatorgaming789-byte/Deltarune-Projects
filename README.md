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
  - Current version: **0.4.2**
  - Native Deltamod `g3mpatch` release source for Chapters 1, 2, and 5
  - Boss Challenge, Pink rewards, whole-party Shield, +25% graze area, and +10% graze TP gain
- [Item Giver Mode](mods/item-giver-mode/README.md)
  - Current version: **0.3.0**
  - Native Deltamod `g3mpatch` release source for Chapters 1–5
  - Item browser with an editable Controls-menu binding; default **I**
  - One changed existing CodeEntry per chapter to minimize merge conflicts

Item Giver Mode v0.3.0 and Secret Boss Challenge v0.4.2 remain **separate mods**. Their Chapter 5 G3M changed-resource-name sets have no direct overlap.

### Merged mods

- [DELTARUNE Compatibility Merge](merged%20mods/deltarune-compatibility-merge/README.md)
  - Current version: **1.0.7**
  - Combines Custom Difficulty, Better Saves, 60 FPS, Chapter 1 Modernized, No-Hat Ralsei, and New ACTs in the Knight Fight
  - Includes package metadata, hashes, credits, changelog, and validation report

The obsolete Item Giver + Secret Boss Challenge combined package was removed after both standalone mods were converted to Deltamod's native merge-patch format.

## Branch guide

| Branch | Responsibility |
|---|---|
| `all-projects` | Completed and validated project archive. |
| `mod-development` | Historical development branch for original standalone mods. |
| `merged-mods` | Historical development branch for merged compatibility builds. |
| `compatibility-fixes` | Historical development branch for repaired or updated mods. |

## Publishing requirements

A project belongs here when it includes the materials available for the completed work:

- A Deltamod-compatible release archive when one can be stored intact, otherwise a reproducible release/package folder.
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
- Do not create development branches unless explicitly requested.
- Preserve original authorship and licensing information.
- Do not commit original DELTARUNE executables, complete `data.win` files, or other unmodified game files.

## Deltamod compatibility

Native `g3mpatch`/xdelta packages are preferred for projects intended to coexist with other `data.win` mods because Deltamod can merge those resources through G3MTool.

Deltamod 2.0.4 runs CSX patches after its G3M merge stage; a third-party CSX patch targeting the same chapter can still replace previously merged output. Projects should document that limitation rather than claim universal compatibility.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
