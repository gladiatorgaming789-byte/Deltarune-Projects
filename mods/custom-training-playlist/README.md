# Custom Training Playlist

**Current version: 0.1.9 POC**

Custom Training Playlist is a DELTARUNE trainer project intended to grow into a system for chaining practice patterns. Version 0.1.9 completes Stage 4 of the proof of concept by adding an in-game Jevil attack browser on top of the runtime-confirmed v0.1.8 battle lifecycle.

## Stage 4 behavior

- Press **N** during safe Chapter 1 free roam to open the trainer browser in the current room.
- Use **Up / Down** to move through the current page.
- Use **Left / Right** to switch between Patterns 1–8 and 9–16.
- Press **Z** or **Enter** to start the selected pattern.
- Press **X**, **Escape**, or **N** to close the browser without entering training.
- Press **F7** during an active trainer battle to abort and return to the saved location.
- All **16 normal Jevil attack IDs** mapped by the existing adapter are exposed.

When a pattern is confirmed, the trainer saves the current room, Kris position, world mode, party, HP, character-control state, and relevant temporary battle state. It moves to the neutral `room_DARKempty`, enters Dark World mode, temporarily supplies Kris + Susie + Ralsei, runs the normal party turn, starts the selected Jevil pattern only after all three party members finish their actions, then restores the original room, position, world mode, party, and saved state.

## Runtime validation

The final production patch was applied to clean Chapter 1 and exercised under Wine/Xvfb with real X11 keyboard input.

Validated Stage 4 behavior includes:

1. N opens the browser in a Light World room without moving Kris.
2. Page 1 displays Patterns 1–8.
3. Right switches to Page 2 and selection movement reaches Pattern 10.
4. N cancel closes the browser and returns to free roam without launching training.
5. Pattern 10 selection reaches internal adapter ID 9 and Jevil controller type 48.
6. Pattern 1 completes the full lifecycle: browser selection → neutral Dark World room → Kris/Susie/Ralsei turn → Jevil attack → cleanup → original Light World room/position restored.
7. The game process remains alive after the successful round trip.

A first Stage 4 build exposed a GameMaker local-variable metadata failure in the Draw helper. The final browser Draw helper therefore uses no local temporary variables. The browser hook was also moved from Draw GUI End to Draw GUI after G3M merge testing found that Item Giver v0.3.1 also modifies Draw GUI End.

## Deltamod compatibility

- Package ID: `github.customtrainingplaylist.gladiatorgaming`
- Target: DELTARUNE v23, Chapter 1
- Native merge-aware `g3mpatch`
- Final patch footprint: **2 changed CodeEntries + 3 new Scripts + 3 new CodeEntries**
- Changed existing CodeEntries:
  - `gml_Object_obj_time_Step_1`
  - `gml_Object_obj_time_Draw_64`
- No Sound resources are included.
- G3MTool 1.2.1 validation and application against clean Chapter 1 passed.
- Item Giver v0.3.1 and the trainer merge in both priority orders with **0 conflicts**.
- Secret Boss Challenge v0.4.3 and the trainer merge in both priority orders with **0 conflicts**.

Deltamod 2.0.4 applies CSX patches after its G3M merge stage, so a third-party CSX that overwrites the same resources can still supersede G3M-merged output.

## Release hashes

- Clean Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Final Chapter 1 G3M SHA-256: `f8037e06c9989b718104dc613d1aacb6414ce3013f771f3efbc2c8d773568d0e`
- Final v0.1.9 Deltamod ZIP SHA-256: `b7a2a374009be0388785630705114a17734f2053688e1a4569398b474c8a125a`

## Remaining POC limitations

- This is an individual-attack browser, not the playlist editor yet.
- Only Jevil is currently exposed.
- Not all 16 patterns have received a full natural-completion gameplay pass yet.
- A dedicated trainer death/game-over policy is not implemented yet. If the party is defeated before F7 is used, normal DELTARUNE game-over behavior may take over.
- N is hardcoded rather than configurable through Controls.
- Returning reloads the original room before restoring position, so purely room-local transient state may be recreated.
- Chapters 2–5 are not yet supported by this trainer POC.

This is an unofficial fan mod and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
