Item Giver Mode v0.2.0

Install the built ZIP directly through Deltamod.

OPENING THE MENU
- Load a save, then press F7 outside battle.
- F7, X, or Escape closes the menu.

CONTROLS
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

COMPATIBILITY REWRITE
Version 0.2.0 is distributed as native .g3mpatch files instead of CSX installation patches. Each chapter changes only one existing DELTARUNE code entry: obj_time's Draw GUI End event. It adds no GameMaker object, no new event, and no helper-script resources. This deliberately minimizes Item Giver's merge footprint.

Item Giver v0.2.0 and Secret Boss Challenge v0.4.1 were merged with Deltamod 2.0.4's bundled G3MTool 1.2.1 in both orders for their shared Chapters 1, 2, and 5 with no G3M code conflicts.

LIMITATION
Deltamod 2.0.4 applies CSX patches after G3M merge patches. A separate third-party CSX mod targeting the same chapter data.win can therefore still overwrite earlier G3M results. No standalone mod can fully prevent that installer-level behavior. Native g3mpatch/xdelta mods are the intended compatibility path.

SAVE WARNING
Some key/unused items rely on story flags. Granting the item does not reproduce every story event or flag normally associated with acquiring it. Back up progression-sensitive saves before experimenting.
