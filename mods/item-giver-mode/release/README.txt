Item Giver Mode v0.3.0

Install this ZIP directly through Deltamod.

OPENING THE MENU
- Load a save, then press the Item Giver key outside battle.
- Default keyboard binding: I
- The Item Giver key, X, or Escape closes the menu.

REBINDING
- Open DELTARUNE's normal Controls menu.
- Move down past Finish to the ITEM GIVER row.
- Confirm the row, then press the keyboard key you want.
- The binding is stored in the current save slot's keyconfig_<slot>.ini file.
- Using Reset to default restores the Item Giver binding to I.
- Keys already assigned to DELTARUNE's seven normal keyboard actions are rejected to avoid accidental conflicts.

ITEM GIVER CONTROLS
- Left / Right: Change category
- Up / Down: Select entry
- Page Up / Page Down: Jump by 10
- Home / End: First / last entry
- Z / Enter: Give selected entry
- R: Refresh item definitions

CATEGORIES
- Items
- Weapons
- Armor
- Key Items
- Light Items

COMPATIBILITY
Version 0.3.0 remains a native .g3mpatch release. Each chapter still changes only one existing DELTARUNE code entry: obj_time's Draw GUI End event. The Controls row, binding persistence, Item Giver input, and overlay all live in that same entry. It adds no GameMaker object, no new event, and no helper-script resource.

This keeps Item Giver's resource footprint separate from Secret Boss Challenge v0.4.2 in Chapter 5.

LIMITATION
Deltamod 2.0.4 applies CSX patches after G3M merge patches. A third-party CSX mod targeting the same chapter data.win can still overwrite earlier G3M results.

SAVE WARNING
Some key/unused items rely on story flags. Granting the item does not reproduce every story event or flag normally associated with acquiring it. Back up progression-sensitive saves before experimenting.
