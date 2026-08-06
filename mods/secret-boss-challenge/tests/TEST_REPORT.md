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
4. The CONFIG menu toggle, conditional challenge stats, dual-reward paths, and inventory-full recovery logic remain present.
5. The rebuilt Chapter 1 and Chapter 2 files previously entered the GameMaker main loop in Wine smoke tests.

## Chapter 5 implementation checks

### Pink's Staff

- Added as Chapter 5 weapon ID `38`, an unused weapon slot in the tested build.
- Name: `Pink's Staff`
- Character: Ralsei
- Stats: 8 AT, 4 DF, 12 MAG
- Name, description, character reactions, icon, value, and equip restriction are defined through `scr_weaponinfo`.
- Weapon inventory and equipped-item checks prevent duplicate grants.

### Pink reward

- Pink's reward sequence grants Pink's Staff and 3 Pink Coins.
- Save flag `2490` tracks the staff grant.
- Save flag `2491` tracks the Pink Coin grant.
- The coin total uses the game's existing `global.flag[1312]` Pink Coin counter.
- The coin reward is one-time even if the staff remains pending because storage is full.
- Existing saves with Pink's defeat flag set receive the reward upon entering the flower shop.
- Full WEAPON storage leaves the staff pending and retries the grant on a later shop visit.

### Flower shop

- The introductory text now says to choose four flowers.
- Four regular flower purchases are allowed before the Flowery podium transition.
- Flowery's special scarf remains the final purchase after the four regular choices.
- Shop completion and podium-removal thresholds were moved consistently from 4 to 5 total purchases.

## Compilation and round-trip checks

1. Applied `SecretBossChallenge_ch5.csx` to the untouched Chapter 5 `data.win`.
2. UndertaleModTool compiled and wrote the rebuilt file successfully.
3. Reopened the rebuilt file and dumped every changed code entry.
4. Confirmed the decompiled output contains:
   - Pink's Staff definition and stats
   - one-time staff and coin flags
   - full-inventory recovery path
   - four-choice shop dialogue
   - updated four/five purchase thresholds
5. Applied the same Chapter 5 patch a second time.
6. The second application succeeded and produced a byte-identical output file, validating patch idempotency and duplicate-reward protection.

## Debug Mode v4.01 compatibility

Both Chapter 5 patch orders compiled successfully:

1. Clean Chapter 5 → Debug Mode v4.01 → Secret Boss Challenge v0.2.0
2. Clean Chapter 5 → Secret Boss Challenge v0.2.0 → Debug Mode v4.01

Round-trip decompilation of both outputs confirmed that Pink's Staff, the one-time reward flags, reward messages, and expanded flower-shop thresholds remained present.

## Deltamod package validation

- `meta.json` parses as valid JSON.
- Nested `metadata.version` is `0.2.0`.
- The package ID remains `github.secretbosschallenge.gladiatorgaming` for in-place updates.
- `neededFiles` contains the verified Chapter 1, Chapter 2, and Chapter 5 checksums.
- `modding.xml` contains three `type="csx"` entries.
- Every referenced script exists at the declared archive path.
- Required files are at the archive root rather than inside an extra wrapper directory.
- ZIP central-directory and compressed-data integrity checks passed.
- Applying the Chapter 5 script extracted from the final ZIP produced a byte-identical copy of the tested rebuilt file.
- No original game executable, `data.win`, audio, or full decompiled source is included.

## Not completed

A full manual playthrough of Pink's encounter, every possible inventory state, every four-flower selection order, and the final Flowery purchase was not performed in the headless workspace. These paths were validated through source inspection, successful compilation, round-trip decompilation, idempotency testing, and cross-mod patch-order testing.
