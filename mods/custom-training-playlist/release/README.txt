Custom Training Playlist v0.2.0
===============================

Chapter 1 Jevil training playlist editor + runner.

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

STAGES COMPLETED IN v0.2.0
- Stage 5: Playlist Editor.
- Stage 6: Playlist Runner.
- Stage 7: Runtime Controls.
- Stage 8: Trainer-Safe Defeat Handling.
- Stage 9: Session Statistics.

BEHAVIOR
- All 16 normal Jevil patterns are available.
- The trainer temporarily stages Kris + Susie + Ralsei in a neutral Dark World room.
- Party turn order remains Kris -> Susie -> Ralsei -> Jevil.
- The selected Jevil controller starts only after the stock 12-frame enemy-phase startup and after the real battle SOUL exists.
- Multi-pattern playlists remain in one trainer battle and return through DELTARUNE's stock end-turn path between patterns.
- Trainer defeat is intercepted before the normal Game Over flow; Retry/Skip/Restart/Quit remain available.
- Original room, position, world state, party, HP, and saved temporary battle state are restored when training ends.

SESSION RESULTS
- Attempts
- Patterns cleared
- No-hit clears
- Hits taken
- Damage taken
- Session time

FINAL CORRECTNESS FIXES
- Retry and Restart now correctly re-arm Jevil even in single-pattern practice.
- Damage accounting clamps a downing hit to the character's remaining positive HP, preventing DELTARUNE's negative downed-HP bookkeeping from inflating results.

RUNTIME VALIDATION
- Playlist add/move/remove: PASS.
- Two-pattern automatic advance: PASS.
- Runtime Retry/Skip/Restart: PASS.
- Trainer-safe defeat + Retry: PASS.
- Single-pattern Retry/Restart regression: PASS.
- F7 cleanup/return: PASS.
- Results panel: PASS.
- G3M apply + round-trip decompile: PASS.

DELTAMOD
- Native merge-aware g3mpatch.
- Final footprint: 3 changed CodeEntries + 3 new Scripts + 3 new CodeEntries.
- 0 deleted resources. 0 Sound resources.
- G3MTool 1.2.1 validate/apply: PASS.
- Item Giver v0.3.1 merge: 0 conflicts in both orders.
- Secret Boss Challenge v0.4.3 merge: 0 conflicts in both orders.

KNOWN LIMITATIONS
- Chapter 1 / Jevil only.
- Playlists are not persisted between launches yet.
- Pattern names are numbered rather than descriptive.
- Trainer keybinds are hardcoded rather than exposed in Controls.
- Statistics do not yet include persistent records, TP, or graze analytics.

See HASHES.txt for exact release hashes.
