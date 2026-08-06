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

## Chapter 5 reward gating

A new save state, `global.flag[2493]`, records Pink's victory-time challenge eligibility:

- `0`: unknown or legacy
- `1`: Pink was defeated while Boss Challenge was ON
- `2`: Pink was defeated without Boss Challenge

The round-trip decompiled result confirms:

- Pink Scarf and the three Pink Coins are granted only while `global.flag[2493] == 1`.
- Pink's Staff requires both `global.flag[2493] == 1` and `global.flag[1914] == 2` (Meaner Bombs).
- Enabling Boss Challenge after the battle does not alter the recorded result.
- Boss Challenge OFF leaves the new reward flags untouched and does not grant the extra coins.

## Conditional flower-shop progression

The flower shop now defines dynamic goals:

- Non-challenge clear: `regularpurchasegoal = 3`, `totalpurchasegoal = 4`
- Challenge clear: `regularpurchasegoal = 4`, `totalpurchasegoal = 5`

Round-trip decompilation confirmed that the following all use those goals consistently:

- introductory three-choice/four-choice dialogue
- Flowery's price transition
- podium removal
- regular-purchase completion transition
- final Flowery item transition

A migration count preserves the five-stage progression for saves that already made four regular purchases under v0.3.0.

## Items and inventory recovery

### Pink Scarf

- Weapon ID: `38`
- Character: Ralsei
- Stats: 8 AT, 4 DF, 12 MAG
- Save flag: `2490`

### Pink's Staff

- Weapon ID: `39`
- Character: Kris
- Stats: 14 AT, 2 DF, 4 MAG
- Save flag: `2492`

### Pink Coins

- Amount: 3
- Counter: `global.flag[1312]`
- One-time save flag: `2491`

Inventory and equipped-item checks prevent duplicates. Eligible equipment that cannot fit remains pending and is retried from the flower shop.

## Compilation and round-trip checks

1. Applied the v0.3.1 Chapter 5 CSX to the untouched Chapter 5 `data.win`.
2. UndertaleModTool compiled and wrote the rebuilt file successfully.
3. Reopened the rebuilt file and dumped the changed code entries.
4. Confirmed the reward gates, item definitions, CONFIG integration, dynamic shop goals, and both dialogue branches.
5. Applied the same Chapter 5 patch a second time.
6. The second output was byte-identical to the first.

Rebuilt Chapter 5 SHA-256:

`7b585ecf7febe77edfeedc302efb0473407acae501bb679b83b71ca70dd3d8a8`

## Debug Mode v4.01 compatibility

Both Chapter 5 patch orders compiled successfully:

1. Clean Chapter 5 → Debug Mode v4.01 → Secret Boss Challenge v0.3.1
2. Clean Chapter 5 → Secret Boss Challenge v0.3.1 → Debug Mode v4.01

Both outputs reopened and round-trip decompiled successfully. The following remained present in both orders:

- victory-time Boss Challenge reward gate
- Meaner Bombs plus Boss Challenge Staff gate
- Pink Scarf and Pink's Staff definitions
- Kris/Ralsei equip restrictions
- vanilla and challenge flower-shop goals
- three-choice and four-choice dialogue branches
- v0.3.0 flower-purchase migration
- Chapter 5 CONFIG toggle

## Deltamod package validation

- `meta.json` parses as valid JSON.
- Nested `metadata.version` is `0.3.1`.
- The package ID remains `github.secretbosschallenge.gladiatorgaming` for in-place updates.
- `neededFiles` contains the verified Chapter 1, Chapter 2, and Chapter 5 checksums.
- `modding.xml` contains three `type="csx"` entries.
- Every referenced script exists at the declared archive path.
- Required files are at the archive root.
- ZIP central-directory and compressed-data integrity checks passed.
- Applying the Chapter 5 script extracted from the final ZIP produced a byte-identical copy of the tested rebuilt file.
- No original game executable, `data.win`, audio, or full decompiled source is included.

Release ZIP SHA-256:

`e803b6220bbf69b152b5f22a08738da5b242cb77bfc7a3fdbb8979cb46a77a21`

## Not completed

A full manual playthrough of Pink with Boss Challenge both OFF and ON, every inventory state, every flower-selection order, and the final Flowery purchase was not performed in the headless workspace. Those paths were validated through source inspection, compilation, round-trip decompilation, idempotency testing, migration-state analysis, and cross-mod patch-order testing.
