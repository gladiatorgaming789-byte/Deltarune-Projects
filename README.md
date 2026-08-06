# DELTARUNE Compatibility Fixes

> Workspace branch for repairing existing DELTARUNE mods and updating them for current game builds.

Compatibility fixes should preserve the original mod's intended behavior while correcting outdated patch logic, broken resource references, installation problems, or conflicts with **Deltamod**.

## Current status

No dedicated compatibility-fixed mod has been published on this branch yet.

The existing [`merged-mods/deltarune-compatibility-merge`](merged-mods/deltarune-compatibility-merge/README.md) directory is shared legacy/reference merge material. It is a merged build, not a completed compatibility-fix release, and should not be used as the folder pattern for future repairs.

## Intended project layout

Future compatibility work should use a dedicated top-level folder:

```text
compatibility-fixed-mods/
└── original-mod-name/
    ├── README.md
    ├── Deltamod release archive
    ├── redistributable source or patch scripts
    └── tests or validation reports
```

Each repaired mod must remain separate from unrelated projects so its history, credits, fixes, and test results are easy to review.

## Compatibility-fix workflow

1. Record the original mod name, author, version, source, license, and intended behavior.
2. Reproduce the failure on the current target DELTARUNE build.
3. Identify outdated checksums, anchors, scripts, resource IDs, paths, or assumptions.
4. Make the smallest repair that restores the intended behavior.
5. Package the result for Deltamod.
6. Test patching, game startup, saves, and all affected gameplay.
7. Document every fix, limitation, conflict, and validation result.
8. Publish the completed project under `compatibility fixed mods/` on the `all-projects` branch.

## Review standards

A compatibility-fixed release should clearly state:

- What was broken and how the problem appeared.
- What changed and what behavior was deliberately preserved.
- The supported DELTARUNE version, launcher version, chapters, and target file checksums.
- Whether the fix changes saves, item IDs, scripts, assets, or gameplay behavior.
- Installation, update, and uninstall expectations.
- Known conflicts with mods that edit the same resources.
- Original authorship, credits, and license terms.

## Deltamod standards

- The finished package must install through Deltamod.
- Patch scripts must reject unsupported game files instead of applying blindly.
- Unrelated Deltamod packages should continue to coexist whenever possible.
- Resource overlaps must be documented and routed to a merged build when necessary.
- Do not include original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.

## Related branches

- `mod-development` — original standalone mods.
- `merged-mods` — compatibility merges between multiple mods.
- `all-projects` — completed and validated release archive.

---

This repository is an unofficial fan project and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
