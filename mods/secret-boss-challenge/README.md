# Secret Boss Challenge

**Version:** 0.3.0  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod-compatible UTMT `.csx` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting and enhanced rewards for DELTARUNE's secret bosses.

## Features

### Chapters 1 and 2

When Boss Challenge is enabled:

- The Chapter 1 secret boss has increased HP, attack, defense, attack duration, and attack damage.
- The Chapter 2 secret boss has increased HP and attack and uses the game's built-in higher-intensity pattern behavior.
- Winning either encounter grants both route-dependent equipment rewards.
- If equipment storage is full, missing rewards remain recoverable from the existing post-boss treasure chest.

When Boss Challenge is disabled, the original boss stats, patterns, rewards, and route logic are preserved.

### Chapter 5: Pink rewards

The Boss Challenge toggle is now available in Chapter 5's CONFIG menu.

Defeating Pink always grants:

- **Pink Scarf**, a Ralsei weapon with **8 AT, 4 DF, and 12 MAG**.
- **3 additional Pink Coins**, protected by a one-time save state.
- Enough Pink Coins to purchase one additional regular flower item.

Defeating Pink while both conditions are active grants an additional reward:

1. The bomb dial is set to **Meaner Bombs**.
2. **Boss Challenge** is ON when Pink is defeated.

The additional reward is **Pink's Staff**, a Kris weapon with **14 AT, 2 DF, and 4 MAG**. Eligibility is recorded at the moment of victory, so enabling Boss Challenge afterward does not qualify a new clear.

The flower shop permits **four regular flower purchases instead of three**. Flowery's special scarf remains available afterward as the fifth and final purchase.

## Existing saves and v0.2.0 migration

- Weapon ID `38`, used by v0.2.0's Pink's Staff, now defines Pink Scarf. Existing copies therefore migrate without being deleted.
- Eligible v0.2.0 saves can receive the new Kris-only Pink's Staff from the flower shop.
- If WEAPON storage is full, Pink Scarf or Pink's Staff remains pending and the flower shop retries the grant on later visits.
- The Pink Coin and equipment rewards cannot be duplicated by reinstalling or reapplying the patch.

## Installation

Install `Secret_Boss_Challenge_v0.3.0_Deltamod.zip` directly through Deltamod. Do not extract the archive into the game manually.

The package ID remains `github.secretbosschallenge.gladiatorgaming`, so v0.3.0 updates an existing installation.

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

The Chapter 5 patch was tested with Debug Mode v4.01 in both patch orders. Mods changing unrelated code can coexist. A mod changing the same CONFIG, Pink reward, weapon table, or flower-shop anchors may require a dedicated merged build.

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
