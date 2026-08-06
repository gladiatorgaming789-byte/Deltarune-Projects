# Item Giver Mode

**Version:** 0.1.3  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod-compatible UndertaleModTool `.csx` patches

Item Giver Mode adds a debug-style inventory browser. Load a save and press **F7** outside battle to browse and grant every named item definition available in the current chapter.

## Version 0.1.3

This release replaces the custom runtime `obj_item_giver` architecture used by v0.1.0–v0.1.2.

- F7 input now runs from DELTARUNE's native persistent `obj_time` controller.
- The Item Giver interface is attached to `obj_time`'s Draw GUI event.
- No custom Item Giver instance is created at runtime.
- This avoids both the `__objectID2Depth` startup crash and cases where the custom object's input event failed to open the menu.
- Item Giver instance fields and helper functions are prefixed with `ig_` to reduce collisions with the base game and other mods.
- Keeps F7 as the hotkey to avoid Medal's F8 clipping shortcut.
- Keeps the corrected `noroom` inventory-full check.

## Categories

- Items
- Weapons
- Armor
- Key Items
- Light World items

The lists are generated from the game's own item-information scripts at runtime. IDs `1–255` are scanned and blank definitions are ignored, so named items added by compatible mods can also appear.

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

The menu intentionally refuses to open while `obj_battlecontroller` exists. Press F7 after loading a save and while in normal overworld control.

## Inventory behavior

Item Giver Mode uses DELTARUNE's native grant functions:

- Consumables: `scr_itemget`
- Weapons: `scr_weaponget`
- Armor: `scr_armorget`
- Key items: `scr_keyitemget`
- Light World items: `scr_litemget`

The menu reports when the destination inventory is full. It does not silently overwrite equipped equipment.

## Save warning

This is a debug utility. Some key items, unused items, and developer-facing definitions depend on plot flags or scripted acquisition sequences. Giving an item does **not** automatically set every flag normally associated with earning it. Back up the save before experimenting with progression-sensitive entries.

## Installation

Install `Item_Giver_Mode_v0.1.3_Deltamod.zip` through Deltamod. Do not manually overwrite `data.win`.

The package ID remains `github.itemgivermode.gladiatorgaming`.

Release SHA-256: `31bb9893ba38a7b54953487953e3bc7155d52860a7eb77729a9ba73f29a865e5`

## Compatibility and validation

- All five chapter patches compile against the supplied clean v23 chapter files.
- Applying v0.1.3 twice produces byte-identical output in Chapters 1–5.
- Round-trip decompilation confirms F7 handling is present in native `gml_Object_obj_time_Step_0` and the GUI is present in `gml_Object_obj_time_Draw_64`.
- Debug Mode v4.01 compiles successfully before and after Item Giver Mode in Chapters 1–5; both F10 and F7 handlers survive in `obj_time`.
- Secret Boss Challenge v0.4.0 compiles successfully before and after Item Giver Mode in Chapter 5.
- Scripts extracted from the final Deltamod ZIP reproduce the tested outputs byte-for-byte.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for details.
