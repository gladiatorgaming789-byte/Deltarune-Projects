# Item Giver Mode

**Version:** 0.3.0  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod native `.g3mpatch` patches

Item Giver Mode is a standalone debug-style inventory browser for granting named definitions from the current chapter.

## Version 0.3.0

v0.3.0 replaces the hard-coded F7 shortcut with a normal editable keyboard binding.

- Default Item Giver key: **I**.
- A new **ITEM GIVER** row is available at the bottom of DELTARUNE's Controls screen, immediately after Finish.
- Select that row and press a keyboard key to rebind Item Giver.
- The binding is stored per save slot in the same `keyconfig_<slot>.ini` file used by the game's controls, under Item Giver's own section.
- Choosing **Reset to default** resets the Item Giver key to **I** too.
- Keys already assigned to DELTARUNE's seven main keyboard actions are rejected so Item Giver does not silently steal a gameplay control.
- The Item Giver footer displays the currently configured key.

The compatibility-focused architecture from v0.2.0 is preserved: each chapter still changes **one existing code entry only**, `gml_Object_obj_time_Draw_75`. The Controls row, binding persistence, input handling, item browser, and late GUI renderer all live in that same native event. No new GameMaker object, event, or helper-script resource is added.

## Categories

- Items
- Weapons
- Armor
- Key Items
- Light World items

## Item Giver controls

| Key | Action |
|---|---|
| Configured Item Giver key (default **I**) | Open or close |
| Left / Right | Change category |
| Up / Down | Move one entry |
| Page Up / Page Down | Jump ten entries |
| Home / End | First or last entry |
| Z / Enter | Give selected entry |
| R | Refresh item definitions |
| X / Escape | Close |

## Rebinding

Open DELTARUNE's normal **Controls** menu and move down past **Finish** to **ITEM GIVER**. Confirm it, then press the keyboard key you want. Escape cancels the rebind.

The binding defaults to `I` for a slot that has never saved an Item Giver key. Reset to default restores `I`.

## Inventory behavior

Item Giver uses DELTARUNE's native grant functions for each inventory type. It does not silently overwrite equipped gear or simulate every plot event normally associated with earning progression-sensitive items.

## Compatibility

### Secret Boss Challenge

Item Giver Mode v0.3.0 and Secret Boss Challenge v0.4.2 remain **separate mods**.

Item Giver's Chapter 5 patch still changes only `gml_Object_obj_time_Draw_75`. Secret Boss Challenge v0.4.2 changes 16 other Chapter 5 CodeEntries, so their packaged resource-name overlap is **zero**. No dedicated combined package is required.

The previous v0.2.0 / Secret Boss Challenge v0.4.1 releases were additionally merged with Deltamod 2.0.4's bundled G3MTool in both orders with zero conflicts. v0.3.0 preserves Item Giver's same one-resource footprint; the current v0.3.0 package was checked directly for zero resource overlap with Secret Boss Challenge v0.4.2.

### Other mods

The patch intentionally contains one changed CodeEntry per chapter and no new/deleted resources. This minimizes the collision surface for Deltamod's native G3M merge stage.

Compatibility cannot be universal. Deltamod 2.0.4 applies CSX patches after its G3M merge stage, so a third-party CSX package targeting the same chapter can replace previously merged G3M output even when the actual edits are unrelated.

## Installation

Install `Item_Giver_Mode_v0.3.0_Deltamod.zip` directly through Deltamod. The package ID remains `github.itemgivermode.gladiatorgaming`, so it updates older Item Giver releases in place.

**Release SHA-256:** `e84654a87c6844606cce6828c49fe3602caeee21eaa6df3bf0871af3b1add052`

The reproducible source is under [`release/`](release/). The builder upgrades the compact v0.2.0 append-body baseline to v0.3.0, compiles it against each clean chapter, and creates the native G3M patches.

## Save warning

Some key items and unused/developer definitions depend on plot flags or scripted acquisition sequences. Giving an item does **not** automatically set every flag normally associated with earning it. Back up progression-sensitive saves before experimenting.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
