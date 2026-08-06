Item Giver Mode v0.1.2

Install this ZIP directly through Deltamod. Keep the archive intact.

WHAT CHANGED IN v0.1.2
- Fixed the startup crash caused by DELTARUNE's instance_create wrapper trying to look up the dynamically added Item Giver object's depth in __objectID2Depth.
- Item Giver is now created with instance_create_depth at an explicit depth, bypassing that internal lookup.
- F7 remains the menu hotkey.

OPENING THE MENU
- Press F7 outside battle to open or close Item Giver Mode.
- The menu intentionally cannot open during an enemy battle phase.

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

The menu reads the current chapter's own item-definition scripts at runtime. It lists every named definition from IDs 1-255, including named items added by compatible mods. Consumables use the game's native item-grant function and can enter pocket storage when the chapter supports it.

IMPORTANT SAVE WARNING
This is a debug utility. Some key items and unused/developer items are tied to plot flags or scripted events. Giving the item does not automatically set every flag normally associated with earning it. Use a backup save before experimenting with progression-sensitive items.

COMPATIBILITY
- Deltamod-compatible CSX patches for Chapters 1-5.
- F7 does not overlap Debug Mode v4.01's current function-key shortcuts.
- The v0.1.2 startup fix is isolated to Item Giver's guarded scr_gamestart append.
- Secret Boss Challenge compatibility behavior is unchanged from v0.1.1.
- Applying the same v0.1.2 patch twice produces a byte-identical data.win in every chapter.

Target: DELTARUNE Windows full release, launcher version v23.
