Item Giver Mode v0.1.3

Install this ZIP directly through Deltamod. Keep the archive intact.

OPENING THE MENU
- Load a save, then press F7 outside battle to open or close Item Giver Mode.
- v0.1.3 moves the F7 listener and GUI onto DELTARUNE's native persistent obj_time controller.
- The mod no longer creates a custom runtime obj_item_giver instance.

CONTROLS
- Left / Right: Change category
- Up / Down: Select an item
- Page Up / Page Down: Jump by 10 entries
- Home / End: Jump to the first or last entry
- Z or Enter: Give the selected item
- R: Refresh the item tables
- X, Escape, or F7: Close

CATEGORIES
- Items
- Weapons
- Armor
- Key Items
- Light Items

The menu reads the current chapter's own item-definition scripts at runtime. It lists every named definition from IDs 1-255, including named items added by compatible mods.

IMPORTANT SAVE WARNING
This is a debug utility. Some key items and unused/developer items are tied to plot flags or scripted events. Giving the item does not automatically set every flag normally associated with earning it. Use a backup save before experimenting with progression-sensitive items.

FIXES IN v0.1.3
- Removed the dynamically created Item Giver runtime object.
- F7 input now runs from native persistent obj_time every frame.
- The GUI is attached to obj_time's Draw GUI event.
- Item Giver instance variables are prefixed with ig_ to avoid collisions.
- Keeps the v0.1.2 explicit-depth crash fix unnecessary by eliminating the custom runtime instance entirely.
- Retains the corrected noroom inventory-full check.

COMPATIBILITY
- Deltamod-compatible CSX patches for Chapters 1-5.
- Tested with Debug Mode v4.01 in both patch orders for Chapters 1-5.
- Tested with Secret Boss Challenge v0.4.0 in both patch orders for Chapter 5.
- Applying the same v0.1.3 patch twice produces a byte-identical data.win in every chapter.

Target: DELTARUNE Windows full release, launcher version v23.
