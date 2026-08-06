# Secret Boss Challenge — Test Report

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Wine 11.14 amd64-wow64
- Deltamod-compatible CSX package structure
- Debug Mode v4.01 Chapter 5 CSX patch
- Supplied DELTARUNE Windows full-release files, launcher version `v23`
- Chapter 1 source SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 source SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5 source SHA-256: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Existing reward and difficulty checks

1. The Chapter 1 and Chapter 2 CSX scripts remain unchanged from v0.3.1.
2. Chapter 5 still records Boss Challenge eligibility when Pink is defeated.
3. Pink Scarf and the three Pink Coins require a Boss Challenge clear.
4. Pink's Staff requires Boss Challenge plus Meaner Bombs.
5. Challenge and non-challenge flower-shop purchase goals remain conditional.
6. Inventory-full recovery and one-time reward states remain present.

## Shield implementation

### Spell registration

- Shield uses spell ID `14`, unused in the tested Chapter 5 build.
- `scr_spellinfo` defines the name, descriptions, ally target type, and cost.
- The internal cost is `162.5` out of the game's `250` maximum tension.
- The battle UI calculates `floor((cost / maxtension) * 100)`, displaying **65% TP**.
- `scr_spellmenu_setup` dynamically adds Shield to Ralsei's spell list only while weapon ID `38` is equipped.
- The dynamic slot is removed when the scarf is no longer equipped, avoiding a permanently learned spell in the save data.

### Spell behavior

- Casting Shield records the selected party slot as the protected target.
- Recasting replaces the previous target.
- A built-in battle effect is spawned on the chosen ally for cast feedback.
- Both single-target and party-wide branches in `scr_damage` reduce protected-target damage to `ceil(damage * 0.25)`, with a minimum of 1.
- The effect is initialized to inactive when a battle begins.
- `scr_mnendturn` clears the effect when the party regains control after the enemy attack phase.
- Shield therefore protects against all hits during that enemy phase rather than disappearing after the first hit.

## Pink Scarf graze passive

- Weapon ID `38` remains Pink Scarf.
- Item metadata reports `0.1` graze gain and `0.1` graze size.
- The item ability string is `Shield / Graze +10%`.
- `obj_grazebox` adds `0.1` to its TP factor and size factor when `scr_weaponcheck_equipped_party(38) > 0`.
- Using the party-aware equipment check prevents the passive from remaining active when the wearer is absent from the active party.

## Compilation and round-trip checks

1. Applied the v0.4.0 Chapter 5 CSX to the untouched Chapter 5 `data.win`.
2. UndertaleModTool compiled and wrote the rebuilt file successfully.
3. Reopened the rebuilt file and dumped every code entry changed by Shield or the passive.
4. Confirmed the spell definition, dynamic menu insertion, cast behavior, battle text, both damage branches, turn expiry, battle reset, and graze factors.
5. Applied the same Chapter 5 patch a second time.
6. The second output was byte-identical to the first.

Rebuilt Chapter 5 SHA-256:

`9e546af99d3cca4a9953c26ce87798c3b444a98356a826a77f7ff749b84a5174`

## Debug Mode v4.01 compatibility

Both Chapter 5 patch orders compiled successfully:

1. Clean Chapter 5 → Debug Mode v4.01 → Secret Boss Challenge v0.4.0
2. Clean Chapter 5 → Secret Boss Challenge v0.4.0 → Debug Mode v4.01

Both outputs reopened and round-trip decompiled successfully. The following remained present in both orders:

- Shield spell definition and 65% TP cost
- dynamic Pink Scarf spell-menu gate
- single-target and party-wide 75% reduction hooks
- enemy-phase expiry and battle-start reset
- party-aware +10% graze-size and TP-gain passive
- Pink Scarf and Pink's Staff definitions
- victory-time Boss Challenge reward gate
- conditional flower-shop progression
- Chapter 5 CONFIG toggle

## Startup smoke test

The rebuilt Chapter 5 file was launched directly through the supplied DELTARUNE Windows runner under Wine and Xvfb. The process remained in the GameMaker main loop for the full 30-second test window and was then stopped by the test timeout. No immediate startup or data-load crash was emitted.

## Deltamod package validation

- `meta.json` parses as valid JSON.
- Nested `metadata.version` is `0.4.0`.
- The package ID remains `github.secretbosschallenge.gladiatorgaming` for in-place updates.
- `neededFiles` contains the verified Chapter 1, Chapter 2, and Chapter 5 checksums.
- `modding.xml` contains three `type="csx"` entries.
- Every referenced script exists at the declared archive path.
- Required files are at the archive root.
- ZIP central-directory and compressed-data integrity checks passed.
- Applying the Chapter 5 script extracted from the final ZIP produced a byte-identical copy of the tested rebuilt file.
- No original game executable, `data.win`, audio, or full decompiled source is included.

Release ZIP SHA-256:

`1a35cb79430307e45fd1ff82db30f7a0b814d66927896beb0687c3e6ae3c83e1`

## Not completed

A full manual battle playthrough covering every target, every multi-hit attack, every party-wide attack, Defend stacking, all inventory states, and every flower-selection order was not performed in the headless workspace. Those paths were validated through source inspection, successful compilation, round-trip decompilation, idempotency testing, package-output comparison, startup smoke testing, and cross-mod patch-order testing.
