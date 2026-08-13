Custom Training Playlist v0.1.4 POC
==================================

Chapter 1 proof of concept for the future Custom Training Playlist trainer.

CONTROLS
- N: Start Jevil Pattern 0 training.
- F7: Abort once the trainer battle is active.

START REQUIREMENTS
- Chapter 1 Dark World free roam with obj_mainchara active.
- No existing battle/encounter/end-battle transition.
- DELTARUNE must currently allow normal player interaction (global.interact == 0).
- A fixed three-person party is NOT required; the encounter uses the currently active party state.

v0.1.4 RUNTIME FIXES
- Fixed N appearing to do nothing because v0.1.3 over-gated valid launch states.
- Uses literal keycode 78 for N, independent of DELTARUNE's configurable action keys.
- Initializes global.turntimer before forcing Jevil into the enemy phase.
- Removed the separate cleanup-script call from the live controller path; teardown is self-contained.
- Tracks and removes the exact Jevil boss/body instances created by the trainer.
- Performs a post-obj_endbattle sweep of battle SOUL, graze, bullet, and transition helpers.
- Preserves the stock-exit, HP/state restoration, timer-race, target-selection, and Jevil adapter fixes.

POC SCOPE
- Only Jevil Pattern 0 is exposed by N.
- All 16 normal Jevil pattern adapters remain mapped for later playlist work.
- No playlist browser/editor UI yet.
- F8 is NOT part of the release. It was used only in the isolated runtime test harness.
- Training-session death/game-over policy is not implemented yet.

RUNTIME VALIDATION
Natural completion and F7 abort were both validated under Wine/Xvfb using real X11 XTEST key events. Both returned to the overworld with the process alive and no leftover Jevil, battle SOUL, bullets, or helper objects.

DELTAMOD COMPATIBILITY
- Native g3mpatch package; mergeSupport is enabled.
- Final patch footprint: 1 changed CodeEntry + 2 new Scripts + 2 new CodeEntries.
- No Sound resources are included.
- G3MTool 1.2.1 validation/application against clean Chapter 1 passed.
- Pairwise merge tests with Item Giver v0.3.1 and Secret Boss Challenge v0.4.3 passed in both orders with 0 conflicts.
- Two representative Item Giver + SBC + Trainer three-mod priority orders completed with 0 conflicts and 3 auto-merges.

The repository connector cannot commit binary g3mpatch/ZIP files, so this text release folder stores the metadata/source used for the validated package. See HASHES.txt for exact release hashes.
