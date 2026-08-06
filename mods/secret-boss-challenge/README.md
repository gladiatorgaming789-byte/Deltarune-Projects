# Secret Boss Challenge

**Version:** 0.4.0  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod-compatible UTMT `.csx` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting, harder secret-boss variants, and enhanced challenge rewards.

## Features

### Chapters 1 and 2

When Boss Challenge is enabled:

- The Chapter 1 secret boss has increased HP, attack, defense, attack duration, and attack damage.
- The Chapter 2 secret boss has increased HP and attack and uses the game's built-in higher-intensity pattern behavior.
- Winning either encounter grants both route-dependent equipment rewards.
- If equipment storage is full, missing rewards remain recoverable from the existing post-boss treasure chest.

When Boss Challenge is disabled, the original boss stats, patterns, rewards, and route logic are preserved.

### Chapter 5: Pink rewards

The Boss Challenge toggle is available in Chapter 5's CONFIG menu.

#### Boss Challenge OFF

Defeating Pink keeps the original reward behavior and flower-shop progression:

- No Pink Scarf
- No bonus Pink Coins
- Three regular flower purchases before Flowery's special scarf
- No Pink's Staff, including on Meaner Bombs

#### Boss Challenge ON

Defeating Pink grants:

- **Pink Scarf**, a Ralsei weapon with **8 AT, 4 DF, and 12 MAG**
- **3 additional Pink Coins**, protected by a one-time save state
- A fourth regular flower-shop purchase before Flowery's special scarf

Defeating Pink with both **Meaner Bombs** and **Boss Challenge ON** also grants:

- **Pink's Staff**, a Kris weapon with **14 AT, 2 DF, and 4 MAG**

Reward eligibility is recorded when Pink is defeated. Enabling Boss Challenge afterward does not qualify an earlier clear.

## Pink Scarf ability

Equipping Pink Scarf on Ralsei unlocks **Shield** in Ralsei's battle spell menu.

- Cost: **65% TP**
- Target: one party member
- Effect: reduces damage to that party member by approximately **75%**
- Duration: the next enemy attack phase
- Recasting Shield replaces the previously protected target
- Applies to direct single-target hits and party-wide damage
- Shield is cleared when the party regains control and is reset at the start of each battle

Pink Scarf also provides a passive bonus while its wearer is in the active party:

- Approximately **10% larger graze hitbox**
- Approximately **10% more TP from grazing**

The item card reports `Shield / Graze +10%`, and the standard graze-stat metadata is populated for equipment comparisons.

## Save migration and full inventories

- Weapon ID `38` remains Pink Scarf, preserving the v0.2.x item-slot migration.
- Weapon ID `39` remains Pink's Staff.
- Spell ID `14` is used for Shield; it was unused in the tested Chapter 5 build.
- If WEAPON storage is full, an eligible item stays pending and the flower shop retries the grant later.
- Pink Scarf, Pink's Staff, and the three Pink Coins use separate one-time save states.
- Older completed clears without saved proof that Boss Challenge was active are treated as non-challenge clears.
- Rewards already granted by earlier versions are not removed from existing saves.

## Installation

Install `Secret_Boss_Challenge_v0.4.0_Deltamod.zip` directly through Deltamod when using Secret Boss Challenge by itself. Do not extract the archive into the game manually.

If Item Giver Mode is also wanted, use the dedicated [`Item Giver + Secret Boss Challenge`](../../merged%20mods/item-giver-secret-boss-challenge/README.md) compatibility build instead and disable/remove both standalone copies.

The package ID remains `github.secretbosschallenge.gladiatorgaming`, so v0.4.0 updates an existing installation.

The archive patches:

- `chapter1_windows/data.win`
- `chapter2_windows/data.win`
- `chapter5_windows/data.win`

All entries are declared as `type="csx"`, so Deltamod routes them through UndertaleModCli.

## Compatibility

The release metadata checks the exact game files used during development:

- Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5 SHA-256: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

The Chapter 5 patch was tested with Debug Mode v4.01 in both patch orders. Mods changing unrelated code can coexist. A mod changing the same CONFIG, Pink reward, weapon table, spell, damage, graze, or flower-shop anchors may require a dedicated merged build.

Item Giver Mode is a documented special case: use the dedicated merged package rather than enabling the two standalone Deltamod packages together.

A newer DELTARUNE update may require regenerated anchors and checksums. The scripts stop instead of applying to an unknown layout.

## Chapter 1 challenge values

- HP: 3500 → 4550
- Attack: 10 → 12
- Defense: 5 → 6
- Attack duration: +15% for normal attack turns
- Bullet damage: +15%, rounded upward

## Chapter 2 challenge values

- HP: 4809 → 6000
- Attack: 13 → 15
- Existing built-in high-intensity pattern mode enabled

## Development

The distributable scripts are included inside the release ZIP. They contain source anchors and original mod code only; no original `data.win`, executable, audio, or complete decompiled game source is included.

See [tests/TEST_REPORT.md](tests/TEST_REPORT.md) for validation details.
