# Item Giver Mode

**Version:** 0.3.1  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod native `.g3mpatch` patches

Item Giver Mode is a standalone debug-style inventory browser for granting named item definitions from the current chapter.

## Version 0.3.1

v0.3.1 is a corrective release for the broken v0.3.0 Controls integration.

v0.3.0 tried to emulate an extra Controls row from `gml_Object_obj_time_Draw_75`. In real gameplay, that pseudo-row did not appear/behave as a native Controls entry, and putting the entire menu/rebind implementation in `obj_time` created fragile local-variable metadata when another G3M mod was merged.

v0.3.1 changes the architecture:

- Default Item Giver key: **I**.
- **ITEM GIVER is now a genuine selectable row** in DELTARUNE's real Controls handler, below Finish.
- Confirm the row and press an unused keyboard key to rebind it.
- Escape cancels rebinding.
- Keys already assigned to DELTARUNE's seven normal keyboard actions are rejected.
- Reset to default restores Item Giver to **I**.
- The binding is stored per save slot in `keyconfig_<slot>.ini`, section `ITEM_GIVER`, key `KEYBOARD`.
- The Item Giver footer displays the configured key.

### Compatibility-focused implementation

The heavy runtime no longer lives inside `obj_time`.

Per chapter, v0.3.1 intentionally changes:

- `gml_Object_obj_darkcontroller_Step_0`
- `gml_Object_obj_darkcontroller_Draw_0`
- `gml_Object_obj_time_Draw_75`

and adds the uniquely named:

- Script: `scr_gg_itemgiver_runtime`
- CodeEntry: `gml_Script_scr_gg_itemgiver_runtime`

`gml_Object_obj_time_Draw_75` receives only:

```gml
scr_gg_itemgiver_runtime();
```

No Item Giver locals are injected into that Draw event anymore. This directly removes the local-heavy architecture implicated in the reported `bbox_top` merge crash.

The release builder also rejects unexpected GameMaker resource changes and removes UTMT sound-serialization noise before packaging, so Item Giver does not claim unrelated audio resources.

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

## Inventory behavior

Item Giver uses DELTARUNE's native grant functions for each inventory type. It does not silently overwrite equipped gear or simulate every plot event normally associated with earning progression-sensitive items.

## Compatibility

### Secret Boss Challenge

Item Giver Mode v0.3.1 and Secret Boss Challenge v0.4.2 remain **separate mods**.

The v0.3.1 redesign removes the large Item Giver body from `obj_time`, which addresses the fragile local-variable merge path behind the reported `bbox_top` crash. Secret Boss Challenge v0.4.2's documented Chapter 5 patch changes 16 CodeEntries and adds/deletes no resources; its v0.4.2 delta source does not target Item Giver's new Controls handlers or helper-script name.

A current v0.3.1 + SBC v0.4.2 both-order binary merge was **not** rerun in this workspace because the SBC v0.4.2 patch binary was not locally available. The combined Deltamod install therefore remains a required manual regression test rather than a claimed pass.

### Other mods

v0.3.1 uses native `g3mpatch` packaging and a uniquely named helper script. The release patches are constrained to the three existing CodeEntries and two new Item Giver resources listed above.

Compatibility cannot be universal. Deltamod 2.0.4 applies CSX patches after its G3M merge stage, so a third-party CSX package targeting the same chapter can still replace previously merged G3M output.

## Installation

Install `Item_Giver_Mode_v0.3.1_Deltamod.zip` directly through Deltamod. The package ID remains `github.itemgivermode.gladiatorgaming`, so it updates older Item Giver releases in place.

**Release SHA-256:** `444e81da9f2f73de802c306568e69ef74e06ce8ddf0adbc765699062c3a80cad`

The reproducible source is under [`release/`](release/). The builder compiles the real Controls hooks and isolated runtime against each clean chapter, creates G3M patches, removes known UTMT Sound serialization noise, rejects unexpected resource changes, and validates the result with G3MTool.

## Save warning

Some key items and unused/developer definitions depend on plot flags or scripted acquisition sequences. Giving an item does **not** automatically set every flag normally associated with earning it. Back up progression-sensitive saves before experimenting.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
