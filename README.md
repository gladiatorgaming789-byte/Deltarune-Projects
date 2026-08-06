# DELTARUNE Projects

> Central archive for completed DELTARUNE modding projects maintained by GladiatorGaming.

All releases in this branch are organized by project type and must be packaged for **Deltamod**. Development and testing happen elsewhere; only completed, documented, and validated projects belong here.

## Browse the archive

| Category | Purpose |
|---|---|
| [Mods](mods/README.md) | Original standalone mods created for this project. |
| [Merged Mods](merged%20mods/README.md) | Compatibility builds that combine two or more existing mods. |
| [Compatibility Fixed Mods](compatibility%20fixed%20mods/README.md) | Existing mods updated for newer DELTARUNE versions or repaired for Deltamod. |

## Current archive status

The archive categories are ready, but no completed release has been published to this branch yet. Active work remains on its appropriate development branch until validation is complete.

## Branch guide

| Branch | Responsibility |
|---|---|
| `all-projects` | Completed and validated project archive. |
| `mod-development` | Development of original standalone mods. |
| `merged-mods` | Creation and testing of merged compatibility builds. |
| `compatibility-fixes` | Repairing or updating existing mods. |

## Publishing requirements

A project should be added to this branch only when it includes:

- A Deltamod-compatible release package.
- A project README with features, installation instructions, compatibility notes, known limitations, and credits.
- Source patch scripts or other redistributable development files when appropriate.
- A validation or test report describing what was tested.
- A clear version number and an identifiable latest release.

## Repository rules

- Keep each project inside its own folder under the correct category.
- Update an existing project in place instead of creating duplicate folders.
- Preserve original authorship and licensing information for repaired or merged mods.
- Do not commit original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.
- Do not publish a release that bypasses Deltamod or requires users to overwrite game files manually unless the project documentation clearly explains an unavoidable exception.

## Deltamod compatibility

Every published mod and merged build must install through Deltamod and use patching methods that can coexist with other compatible packages whenever possible. Projects that modify the same resources must document conflicts or provide a dedicated merged build.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
