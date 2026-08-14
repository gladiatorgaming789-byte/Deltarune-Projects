# Custom Training Playlist

**Current version: 0.2.0**

Custom Training Playlist is a Chapter 1 practice/trainer mod for building and running custom playlists of Jevil attack patterns. Version 0.2.0 completes the recommended Stage 5–9 progression: Playlist Editor, Playlist Runner, Runtime Controls, Trainer-Safe Defeat Handling, and Session Statistics.

## Playlist editor

Press **N** during safe Chapter 1 free roam to open the editor.

- **Left / Right** — switch between the Pattern catalog and Playlist panes.
- **Up / Down** — move the highlighted entry.
- **Z / Enter** — add the highlighted Jevil pattern to the playlist.
- **Delete / Backspace** — remove the highlighted playlist entry.
- **Q / E** — move a playlist entry up / down.
- **C** — clear the playlist.
- **P** — practice the highlighted pattern immediately.
- **S** — start the complete playlist from entry 1.
- **X / Escape** — close the editor.
- Playlist capacity is currently **32 entries**.

All 16 normal Jevil patterns are available.

## Playlist runner

The trainer saves the original room, position, world mode, party, HP/max HP, character-control state, and relevant temporary battle state. It then stages practice in a neutral Dark World room with **Kris + Susie + Ralsei** and all three normal player turns enabled.

A playlist stays inside one trainer battle. After each selected attack finishes, DELTARUNE's stock end-turn path returns to the next player-command phase before the trainer advances to the next playlist entry. The selected Jevil controller is started only after the stock 12-frame enemy-phase startup and after the real battle SOUL exists.

When training ends, the original room, coordinates, world mode, party, HP, and saved battle state are restored.

## Runtime controls

During training:

- **R** — retry the current pattern.
- **K** — skip the current pattern. On a final/single pattern this ends training.
- **T** — restart the playlist from entry 1. In single-pattern practice this restarts the current pattern.
- **F7** — quit training and return to the saved location.

## Trainer-safe defeat handling

A full training-party wipe is intercepted before DELTARUNE enters its normal Game Over flow. The active pattern is stopped, the temporary party is restored to its pattern-start HP, and the trainer waits for one of the normal training controls:

- R — Retry
- K — Skip
- T — Restart
- F7 — Quit

## Session statistics

After training returns to the original room, a results panel reports:

- Attempts
- Patterns cleared
- No-hit clears
- Hits taken
- Damage taken
- Session time

Damage accounting measures positive HP lost and excludes DELTARUNE's negative downed-HP bookkeeping from inflating the result.

## Runtime validation

The final v0.2.0 candidate was built from clean Chapter 1 and tested under Wine/Xvfb using real X11 XTEST input. Validation includes editor add/move/remove behavior, a multi-entry playlist transition, Retry/Restart, trainer-safe defeat + Retry, F7 cleanup/return, the results panel, and final regressions for Retry/Restart in single-pattern practice.

The final applied patch also re-opened/decompiled with all trainer hooks and fixes intact.

## Deltamod compatibility

- Package ID: `github.customtrainingplaylist.gladiatorgaming`
- Target: DELTARUNE v23, Chapter 1
- Native merge-aware `g3mpatch`
- Final patch footprint: **3 changed CodeEntries + 3 new Scripts + 3 new CodeEntries**
- Changed existing CodeEntries:
  - `gml_Object_obj_time_Step_1`
  - `gml_Object_obj_time_Draw_64`
  - `gml_GlobalScript_scr_damage`
- **0 deleted resources** and **0 Sound resources**.
- G3MTool 1.2.1 validation/application against clean Chapter 1 passed.
- Item Giver v0.3.1 + Trainer: **0 conflicts** in both priority orders.
- Secret Boss Challenge v0.4.3 + Trainer: **0 conflicts** in both priority orders.
- Two tested Item Giver + Secret Boss Challenge + Trainer priority orders: **0 conflicts, 3 auto-merges**.

Deltamod 2.0.4 applies CSX patches after the G3M merge stage, so a later third-party CSX targeting the same chapter can still supersede previously merged G3M output.

## Release hashes

- Clean Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Final Chapter 1 G3M SHA-256: `eab7b95391d116cd7757b8d7a1fae953b3642655cb6a7fb104afe92bdda6e1b8`
- Final v0.2.0 Deltamod ZIP SHA-256: `d449f3b7d0cdf0620653165bb3fbba6e8f13c4a69668b5e58c625704cd8d909a`

## Remaining limitations

- Chapter 1 / Jevil only.
- Playlists are session-only and are not saved between game launches yet.
- Patterns are numbered rather than given descriptive names.
- Trainer keybinds are hardcoded rather than exposed in Controls.
- Statistics do not yet include persistent records, TP metrics, or graze analytics.
- Returning to the source location reloads that room, so purely transient room-local state may be recreated.

This is an unofficial fan mod and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
