# DELTARUNE Projects

> Central archive for completed DELTARUNE modding projects maintained by GladiatorGaming.

All releases in this branch are organized by project type and must be packaged for **Deltamod** whenever an installable build is available. Development and testing happen in the workspace; only completed, documented, and validated projects belong here.

## Browse the archive

| Category | Purpose |
|---|---|
| [Mods](mods/README.md) | Original standalone mods. |
| [Merged Mods](merged%20mods/README.md) | Compatibility builds that combine two or more existing mods. |
| [Compatibility Fixed Mods](compatibility%20fixed%20mods/README.md) | Existing mods updated for newer DELTARUNE versions or repaired for Deltamod. |

## Published projects

### Mods

- [Secret Boss Challenge](mods/secret-boss-challenge/README.md) — Deltamod release packages, documentation, license, and validation report.

### Merged Mods

- [DELTARUNE Compatibility Merge](merged%20mods/deltarune-compatibility-merge/README.md) — completed merge documentation and validation notes. A release ZIP is not currently stored in the repository, so the project is archived as a folder.

## Branch guide

| Branch | Responsibility |
|---|---|
| `all-projects` | Completed and validated project archive. |
| `mod-development` | Source history for original standalone mods. |
| `merged-mods` | Source history for merged compatibility builds. |
| `compatibility-fixes` | Source history for repaired or updated mods. |

## Publishing requirements

A project should be added to this branch only when it includes the materials currently available for that completed project:

- A Deltamod-compatible release package when a distributable archive exists.
- A project README with features, installation or availability notes, compatibility information, known limitations, and credits.
- Source patch scripts or other redistributable development files when appropriate and available.
- A validation or test report, or documented validation results.
- A clear version number and identifiable latest release when an installable package exists.

## Repository rules

- Put original standalone mods under `mods/`.
- Put merged builds under `merged mods/`.
- Put repaired or updated existing mods under `compatibility fixed mods/`.
- Keep each project in its own folder within the correct category.
- Update an existing project in place instead of creating duplicate folders.
- Development, compilation, and testing happen in the workspace rather than temporary GitHub branches.
- Preserve original authorship and licensing information for repaired or merged mods.
- Do not commit original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.

## Deltamod compatibility

Every installable mod and merged build must work through Deltamod and use patching methods that can coexist with other compatible packages whenever possible. Projects that modify the same resources must document conflicts or provide a dedicated merged build.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
