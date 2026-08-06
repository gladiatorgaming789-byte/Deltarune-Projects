# DELTARUNE Mod Development

> Workspace branch for creating original DELTARUNE mods maintained by GladiatorGaming.

Every project on this branch must be designed and packaged for **Deltamod**. Completed releases are copied to the `all-projects` archive only after their documentation and validation are complete.

## Active projects

| Project | Current version | Chapters | Description |
|---|---:|---|---|
| [Secret Boss Challenge](mods/secret-boss-challenge/README.md) | 0.4.0 | 1, 2, and 5 | Adds an optional challenge setting for harder secret bosses and enhanced rewards. |

## Repository layout

```text
mods/
└── project-name/
    ├── README.md
    ├── Deltamod release archive
    ├── redistributable source or patch scripts
    └── tests or validation reports
```

Each mod stays inside its own folder. Older release archives may remain available for testing or rollback, but the project README must clearly identify the current version.

## Development workflow

1. Create and test the mod in the current workspace.
2. Keep all project files together under `mods/<project-name>/`.
3. Build a Deltamod-compatible release archive.
4. Test installation, patch application, game startup, save behavior, and affected gameplay.
5. Update the project README and test report.
6. Publish the completed project under `mods/` on the `all-projects` branch.

## Project requirements

Every mod should document:

- Features and configuration options.
- Supported DELTARUNE version, launcher version, and chapters.
- Installation and update instructions.
- Compatibility notes, overlapping resources, and known conflicts.
- Save migration behavior when persistent data or item IDs are changed.
- Known issues, test coverage, credits, and license information.

## Deltamod standards

- Release packages must install through Deltamod.
- Patch scripts should stop safely on unsupported game files instead of forcing changes into an unknown layout.
- Unrelated mods should remain compatible whenever they do not modify the same resources.
- Resource overlaps must be documented so a dedicated merged build can be made.
- Do not include original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.

## Related branches

- `merged-mods` — resolving conflicts between multiple mods.
- `compatibility-fixes` — updating or repairing existing mods.
- `all-projects` — completed and validated release archive.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
