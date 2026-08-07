# Item Giver Mode

**Version:** 0.2.0  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod native `.g3mpatch` patches

Item Giver Mode is a standalone debug-style inventory browser. Load a save and press **F7** outside battle to browse and grant named definitions from the current chapter.

## Version 0.2.0

v0.2.0 is a compatibility-focused rewrite. The user-facing menu is retained, but the patch footprint and Deltamod format changed substantially:

- Distribution changed from `type="csx"` to native `type="g3mpatch"`.
- Each chapter changes **one existing code entry only**: `gml_Object_obj_time_Draw_75`.
- No new GameMaker object is created.
- No new Step, Cleanup, or helper-script resources are added.
- F7 input, lazy initialization, item scanning, granting, and the late GUI renderer all live in DELTARUNE's existing persistent `obj_time` Draw GUI End event.
- The menu still reads the final in-game item tables at runtime, allowing named definitions added by successfully merged mods to appear.

This minimizes Item Giver's resource collision surface and lets Deltamod/G3MTool merge it with other native merge patches instead of one CSX-built `data.win` replacing another.

## Categories

- Items
- Weapons
- Armor
- Key Items
- Light World items

## Controls

| Key | Action |
|---|---|
| F7 | Open or close |
| Left / Right | Change category |
| Up / Down | Move one entry |
| Page Up / Page Down | Jump ten entries |
| Home / End | First or last entry |
| Z / Enter | Give selected entry |
| R | Refresh item definitions |
| X / Escape | Close |

## Inventory behavior

Item Giver uses DELTARUNE's native grant functions for each inventory type. It does not silently overwrite equipped gear or simulate every plot event normally associated with earning progression-sensitive items.

## Compatibility

### Secret Boss Challenge

**Item Giver Mode v0.2.0 and Secret Boss Challenge v0.4.1 are separate mods and can be enabled together.**

Their packaged patch resources were tested with the **G3MTool 1.2.1 binary bundled inside Deltamod 2.0.4** using the same command style Deltamod uses for multi-mod merging. Chapters 1, 2, and 5 were merged in both mod orders with:

- **0 G3M conflicts**
- Item Giver's F7 code retained
- Secret Boss Challenge retained
- Pink Scarf and Shield retained in Chapter 5

No dedicated Item Giver + Secret Boss Challenge merged package is required.

### Other mods

The v0.2.0 patch contains one changed CodeEntry and no new/deleted resources, which is intentionally small. It should merge cleanly with mods that do not conflict with `obj_time`'s Draw GUI End code, and G3MTool can resource-merge many independent changes.

Compatibility cannot be guaranteed with every mod. In particular, **Deltamod 2.0.4 runs CSX patches after its G3M merge stage and rebuilds each CSX from the chapter backup**. A third-party mod still distributed as CSX for the same `data.win` can therefore replace previously merged G3M output even when the mods edit unrelated resources. That is an installer-level limitation rather than an Item Giver resource conflict.

For best multi-mod compatibility, use native `g3mpatch`/xdelta releases when available.

## Installation

Install `Item_Giver_Mode_v0.2.0_Deltamod.zip` directly through Deltamod. The package ID remains `github.itemgivermode.gladiatorgaming`, so it updates earlier Item Giver releases in place.

The release source under [`release/`](release/) documents how the `.g3mpatch` files are generated from clean chapter files using UndertaleModCli and G3MTool.

**Release SHA-256:** `c42703ffa546a74bd0c02c4112e849edcaeb8330001a42f83684efb2b20772f5`

## Save warning

Some key items and unused/developer definitions depend on plot flags or scripted acquisition sequences. Giving an item does **not** automatically set every flag normally associated with earning it. Back up progression-sensitive saves before experimenting.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
