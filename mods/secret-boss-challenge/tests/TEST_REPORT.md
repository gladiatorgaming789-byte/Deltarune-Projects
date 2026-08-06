# Secret Boss Challenge — Test Report

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Deltamod-compatible CSX package structure
- Debug Mode v4.01 Chapter 5 CSX patch
- Supplied DELTARUNE Windows full-release files, launcher version `v23`
- Chapter 1 source SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 source SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5 source SHA-256: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Existing Chapter 1 and Chapter 2 checks

1. Both CSX scripts load, compile, and write rebuilt game files with UndertaleModTool CLI.
2. Rebuilt files reopen successfully.
3. Changed code entries round-trip decompile to the intended mod logic.
4. The CONFIG toggle, conditional challenge stats, dual-reward paths, and inventory-full recovery logic remain present.
5. The rebuilt Chapter 1 and Chapter 2 files previously entered the GameMaker main loop in Wine smoke tests.

## Chapter 5 implementation checks

### CONFIG integration

- Chapter 5 loads `SECRET_BOSS_CHALLENGE` from `true_config.ini`.
- The CONFIG menu contains a Boss Challenge OFF/ON row.
- The new row has keyboard/controller navigation, persistent writing, and adjusted Return/Back rows.

### Pink Scarf

- Uses weapon ID `38`, preserving v0.2.0 inventory migration.
- Character: Ralsei.
- Stats: 8 AT, 4 DF, 12 MAG.
- Name, description, character reactions, icon, value, and equip restriction are defined through `scr_weaponinfo`.
- Save flag `2490` tracks the grant.

### Pink's Staff

- Uses weapon ID `39`, unused in the tested Chapter 5 build.
- Character: Kris.
- Stats: 14 AT, 2 DF, 4 MAG.
- Eligibility requires `global.flag[1914] == 2` (Meaner Bombs) and Boss Challenge ON at Pink's defeat.
- Save flag `2492` records one of four states: legacy/unknown, granted, eligible-pending, or ineligible.
- Changing Boss Challenge after a v0.3.0 clear does not change the recorded eligibility.
- Weapon inventory and equipped-item checks prevent duplicate grants.

### Pink Coins and flower shop

- Pink grants 3 Pink Coins through the existing `global.flag[1312]` counter.
- Save flag `2491` makes the coin reward one-time.
- The introductory text says to choose four flowers.
- Four regular flower purchases are allowed before the Flowery podium transition.
- Flowery's special scarf remains the fifth and final purchase.
- Full WEAPON storage leaves either equipment reward pending for later shop recovery.

## Compilation and round-trip checks

1. Applied `SecretBossChallenge_ch5.csx` to the untouched Chapter 5 `data.win`.
2. UndertaleModTool compiled and wrote the rebuilt file successfully.
3. Reopened the rebuilt file and dumped every changed code entry.
4. Confirmed the decompiled output contains:
   - Chapter 5 Boss Challenge CONFIG logic
   - Pink Scarf and Pink's Staff definitions and equip restrictions
   - Meaner Bombs plus Boss Challenge eligibility test
   - one-time scarf, staff, and coin states
   - full-inventory recovery logic
   - four-choice shop dialogue and updated purchase thresholds
5. Applied the same Chapter 5 patch a second time.
6. The second application succeeded and produced a byte-identical output file.

The rebuilt Chapter 5 output SHA-256 was:

`0a0cb70cfc2dbf8849e856dbf169255389980f93f9d1a8f0f1e0e25271613b0d`

## Debug Mode v4.01 compatibility

Both Chapter 5 patch orders compiled successfully:

1. Clean Chapter 5 → Debug Mode v4.01 → Secret Boss Challenge v0.3.0
2. Clean Chapter 5 → Secret Boss Challenge v0.3.0 → Debug Mode v4.01

Both outputs reopened and round-trip decompiled successfully. The following remained present in both orders:

- Pink Scarf and Pink's Staff
- Kris/Ralsei equip restrictions
- Meaner Bombs and Boss Challenge eligibility
- one-time reward states
- Chapter 5 CONFIG toggle
- expanded flower-shop thresholds

## Deltamod package validation

- `meta.json` parses as valid JSON.
- Nested `metadata.version` is `0.3.0`.
- The package ID remains `github.secretbosschallenge.gladiatorgaming` for in-place updates.
- `neededFiles` contains the verified Chapter 1, Chapter 2, and Chapter 5 checksums.
- `modding.xml` contains three `type="csx"` entries.
- Every referenced script exists at the declared archive path.
- Required files are at the archive root rather than inside an extra wrapper directory.
- ZIP central-directory and compressed-data integrity checks passed.
- Applying the Chapter 5 script extracted from the final ZIP produced a byte-identical copy of the tested rebuilt file.
- No original game executable, `data.win`, audio, or full decompiled source is included.

Release ZIP SHA-256:

`c706105490860ee8d6306998bc6f5240eda59c266ab2ca4ae55ac3b3a8bf8a30`

## Not completed

A full manual playthrough of Pink's encounter, every inventory state, every four-flower selection order, and the final Flowery purchase was not performed in the headless workspace. These paths were validated through source inspection, successful compilation, round-trip decompilation, idempotency testing, and cross-mod patch-order testing.
