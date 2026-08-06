# Secret Boss Challenge

**Version:** 0.2.0  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod-compatible UTMT `.csx` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting to the in-game CONFIG menu for the Chapter 1 and Chapter 2 secret bosses. Version 0.2.0 also adds a new reward and shop-economy extension for Chapter 5's secret boss, Pink.

## Features

### Chapters 1 and 2

When Boss Challenge is enabled:

- The Chapter 1 secret boss has increased HP, attack, defense, attack duration, and attack damage.
- The Chapter 2 secret boss has increased HP and attack and uses the game's built-in higher-intensity pattern behavior.
- Winning either encounter grants both route-dependent equipment rewards.
- If equipment storage is full, missing rewards remain recoverable from the existing post-boss treasure chest. When both are pending, the chest can be opened twice.

When Boss Challenge is disabled, the original boss stats, patterns, rewards, and route logic are preserved.

### Chapter 5: Pink reward update

Defeating Pink now grants:

- **Pink's Staff**, a Ralsei weapon with **8 AT, 4 DF, and 12 MAG**.
- **3 additional Pink Coins**, protected by a one-time save flag.
- Enough Pink Coins to purchase one additional regular flower item.

The flower shop now permits **four regular flower purchases instead of three**. Flowery's special scarf remains available afterward as the fifth and final purchase.

Pink's Chapter 5 reward is installed independently of the Chapter 1/2 Boss Challenge toggle.

## Existing saves and full inventories

- Saves that defeated Pink before installing v0.2.0 receive the new reward upon entering the flower shop.
- If WEAPON storage is full, Pink's Staff remains pending and the flower shop retries the grant on later visits.
- The Pink Coin reward cannot be duplicated by reinstalling or reapplying the patch.

## Installation

Install `Secret_Boss_Challenge_v0.2.0_Deltamod.zip` directly through Deltamod. Do not extract the archive into the game manually.

The package ID remains `github.secretbosschallenge.gladiatorgaming`, so v0.2.0 updates an existing v0.1.1 installation.

The archive patches:

- `chapter1_windows/data.win`
- `chapter2_windows/data.win`
- `chapter5_windows/data.win`

All entries are declared as `type="csx"`, so Deltamod routes them through UndertaleModCli rather than G3MTool's packaged-patch reader.

## Compatibility

The release metadata checks the exact game files used during development:

- Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5 SHA-256: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

The Chapter 5 patch was tested with Debug Mode v4.01 in both patch orders. Mods changing unrelated code can coexist. A mod changing the same Pink reward, weapon table, or flower-shop anchors may require a dedicated merged build.

A newer DELTARUNE update may require regenerated anchors and checksums. The scripts intentionally stop instead of applying to an unknown layout.

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
