Custom Training Playlist v0.2.1
===============================

Chapter 1 Jevil training playlist editor + runner.

v0.2.1 FIX
- Fixed later playlist patterns beginning from the tail of the previous enemy phase.
- The runner now requires a genuinely fresh player turn before the next playlist entry can arm.
- Fixed stale turntimer=0 causing obj_battlecontroller to schedule scr_mnendturn() during the 12-frame startup of the next Jevil pattern.
- The trainer now mirrors stock Jevil by holding turntimer at 120 during that startup.
- Live regression confirmed Kris -> Susie -> Ralsei -> Jevil, with mnfight=2 / myfight=-1 / charturn=3 during the active next pattern and no command menu overlap.

EDITOR CONTROLS
- N: Open the trainer editor during safe free roam.
- Left / Right: Switch between Pattern catalog and Playlist panes.
- Up / Down: Move selection.
- Z / Enter: Add highlighted pattern to the playlist.
- Delete / Backspace: Remove highlighted playlist entry.
- Q / E: Move highlighted playlist entry up / down.
- C: Clear playlist.
- P: Practice highlighted pattern immediately.
- S: Start the full playlist.
- X / Escape: Close editor.
- Playlist capacity: 32 entries.

TRAINING CONTROLS
- R: Retry current pattern.
- K: Skip current pattern.
- T: Restart playlist from entry 1. In single-pattern practice, restart that pattern.
- F7: Quit training and return to the saved location.

FEATURES
- All 16 normal Jevil patterns.
- Playlist editor + multi-pattern runner.
- Correct Kris -> Susie -> Ralsei -> Jevil turn flow.
- Trainer-safe defeat handling.
- Retry / Skip / Restart / Quit controls.
- Session results: attempts, clears, no-hit clears, hits, damage, time.
- Original room/position/world/party/HP state restored when training ends.

DELTAMOD
- Native merge-aware g3mpatch.
- Final footprint: 3 changed CodeEntries + 3 new Scripts + 3 new CodeEntries.
- 0 deleted resources. 0 Sound resources.
- G3MTool 1.2.1 validate/apply: PASS.
- Applied patch round-trip decompile: PASS.
- Item Giver v0.3.1 merge: 0 conflicts both orders.
- Secret Boss Challenge v0.4.3 merge: 0 conflicts both orders.
- Two tested three-mod orders: 0 conflicts, 3 auto-merges.

KNOWN LIMITATIONS
- Chapter 1 / Jevil only.
- Playlists are not persisted between launches yet.
- Pattern names are numbered rather than descriptive.
- Trainer keybinds are hardcoded rather than exposed in Controls.
- Statistics do not yet include persistent records, TP, or graze analytics.

See HASHES.txt for exact release hashes.
