# Custom Training Playlist

**Current version: 0.1.4 POC**

Custom Training Playlist is a DELTARUNE practice/trainer project intended to grow into a playlist system for chaining original enemy attack patterns. Version 0.1.4 is the Chapter 1 proof of concept for the core battle lifecycle and attack-adapter architecture.

## Current POC behavior

- Press **N** during safe Chapter 1 Dark World free roam to launch **Jevil Pattern 0** as a training encounter.
- Press **F7** during the active trainer battle to abort and return cleanly.
- The adapter already maps all **16 normal Jevil attack IDs** for later playlist work, although only Pattern 0 is exposed in v0.1.4.
- Training restores pre-session HP/max HP and temporary battle state after returning.
- The trainer uses DELTARUNE's stock battle-exit sequence and removes transient Jevil, SOUL, bullet, graze, and battle-helper objects before returning to the overworld.

## Runtime validation

The final production patch was applied to clean Chapter 1 and exercised under Wine/Xvfb with real X11 XTEST key events. A test-only F8 warp was layered on top of the applied test copy solely to reach a safe Dark World room quickly; **F8 is not part of the release**.

Both tested paths passed:

1. **Natural completion:** N -> battle start -> Jevil Pattern 0 -> stock cleanup -> clean overworld return.
2. **F7 abort:** N -> active Jevil Pattern 0 -> F7 -> stock cleanup -> clean overworld return.

The game process remained alive in both cases, with no leftover Jevil, battle SOUL, bullets, or helper objects visible after return.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for the detailed defect/fix history and compatibility results.

## Deltamod compatibility

- Deltamod package ID: `github.customtrainingplaylist.gladiatorgaming`
- Target: DELTARUNE v23, Chapter 1
- Native merge-aware `g3mpatch`
- Final patch footprint: **1 changed CodeEntry + 2 new Scripts + 2 new CodeEntries**
- No Sound resources are included.
- G3MTool 1.2.1 validation and application against clean Chapter 1 passed.

Final coexistence tests with **Item Giver v0.3.1** and **Secret Boss Challenge v0.4.3** passed in both pairwise priority orders with 0 conflicts. Two representative three-mod priority orders also completed with 0 conflicts and 3 auto-merges.

Deltamod 2.0.4 applies CSX patches after the G3M merge stage, so third-party CSX mods that overwrite the same resources can still supersede merged G3M output.

## Release hashes

- Clean Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Final Chapter 1 G3M SHA-256: `4403bd6f32178e8a484172b0d1e8f026e0f1fc434f242f58e97ee92f2b859466`
- Final v0.1.4 Deltamod ZIP SHA-256: `b347e4405523c78dcc0788ced0605ec179f7699c5da5cca8d0aaaaf9966819ec`

The connected repository writer can only commit UTF-8 text files, so the binary `.g3mpatch`/ZIP are not duplicated here through that interface. The release folder preserves the metadata, source, and installer script needed to reconstruct the modified Chapter 1 data from a clean v23 file before generating the G3M patch.

## Remaining POC limitations

- Only Jevil Pattern 0 is exposed to the player.
- There is no playlist browser/editor yet.
- N is hardcoded rather than exposed in DELTARUNE's Controls menu.
- A dedicated trainer death/game-over policy is not implemented yet.
- Chapters 2–5 are not yet supported by this trainer POC.

## Repository layout

```text
custom-training-playlist/
├── README.md
├── release/
│   ├── README.txt
│   ├── HASHES.txt
│   ├── build_poc.csx
│   ├── meta.json
│   ├── modding.xml
│   └── source/
│       ├── scr_gg_trainer_attack_start.gml
│       └── scr_gg_trainer_controller.gml
└── tests/
    └── TEST_REPORT.md
```

This is an unofficial fan mod and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
