# Item Giver Mode

**Version:** 0.1.2  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod-compatible UTMT `.csx` patches

Item Giver Mode adds a standalone debug-style inventory browser. Press **F7** outside battle to browse and grant every named item definition available in the current chapter.

## Version 0.1.2

- Fixed the startup crash `global variable name '__objectID2Depth' ... not set before reading it`.
- The crash came from DELTARUNE's `instance_create` wrapper calling `object_get_depth` for the newly added `obj_item_giver`, whose dynamic object ID is not present in the game's startup depth table.
- Startup now uses `instance_create_depth(0, 0, -999999, obj_item_giver)` so no internal object-depth lookup is required.
- F7 remains the open/close hotkey.

## Categories

- Items
- Weapons
- Armor
- Key Items
- Light World items

The lists are generated from the game’s own item-information scripts at runtime. The scanner checks IDs `1–255`, ignores blank definitions, and therefore also detects named items added by compatible mods.

## Controls

| Key | Action |
|---|---|
| F7 | Open or close the menu |
| Left / Right | Change category |
| Up / Down | Move one entry |
| Page Up / Page Down | Jump ten entries |
| Home / End | First or last entry |
| Z / Enter | Give selected entry |
| R | Refresh item definitions |
| X / Escape | Close |

The menu cannot be opened during battle. While open in the overworld, it temporarily blocks normal interaction and restores the previous interaction state when closed.

## Inventory behavior

Item Giver Mode calls DELTARUNE’s native grant functions:

- Consumables use `scr_itemget`; where supported, a full normal inventory can spill into pocket storage.
- Weapons use `scr_weaponget`.
- Armor uses `scr_armorget`.
- Key items use `scr_keyitemget`.
- Light World items use `scr_litemget`.

The menu reports when the destination inventory is full. It does not automatically remove existing entries or silently overwrite equipped equipment.

## Save warning

This is a debug utility. Some key items, unused items, and developer-facing definitions depend on plot flags or scripted acquisition sequences. Spawning an item does **not** automatically set every flag normally associated with earning it. Back up the save before experimenting with progression-sensitive entries.

## Installation

Install `Item_Giver_Mode_v0.1.2_Deltamod.zip` directly through Deltamod. Do not extract it into the game manually.

The GitHub project mirrors the package source under [`release/`](release/). Run `build_release.py` inside that folder, then ZIP the folder contents so `meta.json` and `modding.xml` are at the archive root.

The package ID remains `github.itemgivermode.gladiatorgaming`, so v0.1.2 updates earlier versions in place.

Release SHA-256: `c6480fa7a84cccc8c6cf73b0b41be291b3845bdabe08a5550bb0f026cda7cab8`

## Compatibility

- All five chapter patches are declared as `type="csx"` for Deltamod’s UndertaleModCli stage.
- F7 does not overlap Debug Mode v4.01's current function-key shortcuts.
- The startup change is isolated to Item Giver's guarded `scr_gamestart` append.
- Secret Boss Challenge compatibility behavior is unchanged from v0.1.1.
- Applying Item Giver Mode v0.1.2 twice produced byte-identical chapter files in Chapters 1–5.

Mods that replace `scr_gamestart` outright or define another object named `obj_item_giver` may require a dedicated compatibility build.

## Source and validation

The tested release ZIP and mirrored `release/` folder contain the distributable CSX source. No original DELTARUNE executable, `data.win`, music, or complete decompiled game source is included.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
