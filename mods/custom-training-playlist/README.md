# Custom Training Playlist

**Current version: 0.2.1**

Custom Training Playlist is a Chapter 1 practice/trainer mod for building and running custom playlists of Jevil attack patterns. Version 0.2.1 fixes the between-pattern turn sequencing race found in v0.2.0 while preserving the Stage 5–9 feature set: Playlist Editor, Playlist Runner, Runtime Controls, Trainer-Safe Defeat Handling, and Session Statistics.

## v0.2.1 sequencing fix

The next playlist entry is now hard-gated behind a genuinely fresh player phase. After a pattern ends, the runner must observe `mnfight == 0` before any later attack can arm. The trainer then waits for the party's new turn to finish before allowing the next enemy phase.

The fix also mirrors stock Jevil's pre-attack timer behavior: `global.turntimer` is held at 120 during the 12-frame SOUL transition. This prevents `obj_battlecontroller` from scheduling a stale end-turn alarm that could call `scr_mnendturn()` in the middle of the next Jevil pattern and restore Kris's command UI during the attack.

Live regression with all three party members kept alive confirmed:

- After Kris commits: `charturn=1`, next pattern has not started.
- After Susie commits: `charturn=2`, next pattern has not started.
- Only after Ralsei commits does the next selected Jevil pattern start.
- During that pattern: `mnfight=2`, `myfight=-1`, `charturn=3`; the player command phase is closed and the command menu is gone.
- The second pattern then completes normally and the trainer returns/results flow remains intact.

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
- Playlist capacity: **32 entries**.

All 16 normal Jevil patterns are available.

## Runtime controls

During training:

- **R** — Retry current pattern.
- **K** — Skip current pattern.
- **T** — Restart playlist; in single-pattern practice, restart that pattern.
- **F7** — Quit training and return to the saved location.

## Trainer-safe defeat / statistics

Training party wipes are intercepted before normal Game Over. Retry / Skip / Restart / Quit remain available. The results panel reports attempts, patterns cleared, no-hit clears, hits taken, damage taken, and session time.

## Deltamod compatibility

- Package ID: `github.customtrainingplaylist.gladiatorgaming`
- Target: DELTARUNE v23, Chapter 1
- Native merge-aware `g3mpatch`
- Final footprint: **3 changed CodeEntries + 3 new Scripts + 3 new CodeEntries**
- **0 deleted resources**, **0 Sound resources**
- G3MTool 1.2.1 validate/apply: PASS
- Applied patch round-trip decompile: PASS
- Item Giver v0.3.1 + Trainer: **0 conflicts** both orders
- Secret Boss Challenge v0.4.3 + Trainer: **0 conflicts** both orders
- Two tested three-mod priority orders: **0 conflicts, 3 auto-merges**

## Release hashes

- Clean Chapter 1 SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Final Chapter 1 G3M SHA-256: `6366c3b2c94c863ecbf981a961221e771254906fc12aa21357a376c7256d5f92`
- Final v0.2.1 Deltamod ZIP SHA-256: `1918ab0d75a7318294ff6ea35c245adae0cdfa50262e28e7a6ad386d41f9f66f`

## Remaining limitations

- Chapter 1 / Jevil only.
- Playlists are session-only and are not saved between game launches yet.
- Patterns are numbered rather than given descriptive names.
- Trainer keybinds are hardcoded rather than exposed in Controls.
- Statistics do not yet include persistent records, TP metrics, or graze analytics.
- Returning to the source location reloads that room, so purely transient room-local state may be recreated.

This is an unofficial fan mod and is not affiliated with or endorsed by Toby Fox or the DELTARUNE development team.
