# Item Giver + Secret Boss Challenge

**Version:** 0.1.0  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Components:** Item Giver Mode v0.1.4 + Secret Boss Challenge v0.4.0  
**Installer:** Deltamod-compatible UTMT `.csx` patches

This is the dedicated compatibility build for **Item Giver Mode** and **Secret Boss Challenge**. Use this package instead of enabling the two standalone packages together.

## Why a merged build is needed

Deltamod's multi-mod path is powered by G3MTool. G3MTool derives each input independently from the same original data file before its resource merge. That differs from applying the two UTMT scripts one after another. The standalone mods compile sequentially, but that did not guarantee the same result through Deltamod's multi-mod merger.

This release removes that ambiguity: Chapters 1, 2, and 5 contain both source patches in **one CSX execution**, while Chapters 3 and 4 contain Item Giver only because Secret Boss Challenge does not target those chapters.

## Installation

Install `Item_Giver_Secret_Boss_Challenge_Merged_v0.1.0_Deltamod.zip` through Deltamod.

**Disable/remove the standalone Item Giver Mode and standalone Secret Boss Challenge packages first.** This merged package already contains both mods.

Package ID: `github.gladiatorgaming.itemgiversecretbossmerge`

## Item Giver controls

| Key | Action |
|---|---|
| F7 | Open / close |
| Left / Right | Change category |
| Up / Down | Move selection |
| Page Up / Page Down | Jump ten entries |
| Home / End | First / last entry |
| Z / Enter | Give selected entry |
| R | Refresh item definitions |
| X / Escape | Close |

The late-rendered v0.1.4 interface and its text-overlap fixes are included.

## Secret Boss Challenge content

The package includes the full v0.4.0 challenge implementation in Chapters 1, 2, and 5, including the Chapter 5 CONFIG toggle and Pink reward system.

- Boss Challenge ON Pink clear: Pink Scarf, 3 Pink Coins, and the extra flower purchase.
- Boss Challenge ON + Meaner Bombs: the above plus Pink's Staff.
- Pink Scarf is Ralsei equipment and grants **Shield** (65% TP, about 75% damage reduction for the protected target through the enemy phase).
- Pink Scarf also increases graze area and graze TP gain by about 10% while equipped by an active party member.

## Validation

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md).

No original DELTARUNE executable, `data.win`, music, or other unmodified game assets are included.
