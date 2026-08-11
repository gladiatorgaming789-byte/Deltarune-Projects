# Item Giver Mode v0.3.0 — Test Report

## Scope

v0.3.0 replaces the fixed F7 shortcut with a rebindable keyboard control while preserving Item Giver's one-CodeEntry-per-chapter G3M footprint.

Implemented behavior:

- Default binding: **I** (`73`).
- Item Giver is exposed as an **ITEM GIVER** row at the bottom of DELTARUNE's Controls menu.
- Confirming that row enters keyboard rebind mode.
- Escape cancels the rebind.
- Keys already used by DELTARUNE's seven normal keyboard actions are rejected.
- Binding is stored per save slot in `keyconfig_<slot>.ini`, section `ITEM_GIVER`, key `KEYBOARD`.
- DELTARUNE's **Reset to default** action also resets Item Giver to I.
- The Item Giver footer shows the currently configured key.

## Compatibility architecture

The implementation remains entirely inside:

`gml_Object_obj_time_Draw_75`

for every chapter.

The Controls row is a late-drawn pseudo-row using the existing Controls cursor state rather than a modification to `obj_darkcontroller`. This was chosen specifically to avoid increasing Item Giver's resource collision surface.

Each packaged patch reports:

- Changed CodeEntries: **1**
- New resources: **0**
- Deleted resources: **0**
- Changed entry: `gml_Object_obj_time_Draw_75`

The helper files in the v0.3.0 G3M patches are byte-identical to their v0.2.0 equivalents because the asset/resource layout is unchanged.

## Clean compilation

The v0.3.0 one-CodeEntry source compiled successfully with UndertaleModTool in Chapters 1–5.

Source-build SHA-256:

- Chapter 1: `4992bf034590697effbc53aeea55f36a1a53e894a525c1ae531287e8337278ef`
- Chapter 2: `185618028c7c28bfaafccf1606002edc7e0256d557cf26d4616da417119d3c00`
- Chapter 3: `0ad564b085102c61f0783b53202b827ca31f2b76c179e60c4cd6064894df1e1c`
- Chapter 4: `7c45c32d0fdbd78d2025284024ef80294b7f9a6cda4c99b2af7434e9647a3053`
- Chapter 5: `4891ec2f104369e2474e194dbaffa368990cfb9fd93eb794a4411eb9421aea9b`

Round-trip decompilation of every chapter confirmed:

- default key value `73` / I
- `ITEM_GIVER` + `KEYBOARD` INI read/write logic
- Controls pseudo-row index 9
- `keyboard_lastkey` rebind logic
- `PRESS A KEY` feedback
- current-binding input check
- no remaining F7 handler/reference

## Packaged G3M patches

SHA-256:

- Chapter 1: `627d5e093e73a60f69823dea3095142053d93899564abdabb8dca0fe067011a1`
- Chapter 2: `b6e8bfe8a689a8fe3641ba242e79c76353491c2871f45f6bab65a3c3f669e01f`
- Chapter 3: `d4dc2e79b1739a66472a9f41923e923066957db54b15a8fa90157f1c07634109`
- Chapter 4: `52eaea510cd11e643966fc47de75f43fd503c2a72a08c69e5786c1e75c5fbda8`
- Chapter 5: `170fcc2b426339cdb322d706aee9a5a977adc12331ee7c88209fa03c3995a7f8`

Nested G3M ZIP integrity checks passed for all five patches. Their manifests retain the clean chapter identities and the one-changed-CodeEntry resource plan.

## Secret Boss Challenge v0.4.2 compatibility

The packaged Chapter 5 manifests were compared directly.

- Item Giver v0.3.0 changes: `gml_Object_obj_time_Draw_75`
- Secret Boss Challenge v0.4.2 changes: 16 other Chapter 5 CodeEntries
- Direct changed-resource-name overlap: **0**

A fresh G3MTool binary merge was not repeated for this v0.3.0 build. The compatibility conclusion for v0.3.0 is therefore based on the preserved one-resource Item Giver architecture plus the verified zero-overlap manifest check, not a new runtime merge log.

## Startup smoke test

The Chapter 1 v0.3.0 source build was launched with the supplied Windows DELTARUNE runner under Wine 11.14 and Xvfb. The process remained running in the GameMaker loop for the full **20-second** timeout and was then stopped by the test harness. No immediate startup/data-load crash or runtime error was emitted.

This smoke test does not replace an interactive Controls-menu/rebinding test.

## Final Deltamod ZIP

- Package ID: `github.itemgivermode.gladiatorgaming`
- Version: `0.3.0`
- Five `type="g3mpatch"` routes
- ZIP central-directory/data integrity: passed
- ZIP size: `1,888,802` bytes
- ZIP SHA-256: `e84654a87c6844606cce6828c49fe3602caeee21eaa6df3bf0871af3b1add052`

## Manual verification still needed

The source and package checks verify the Controls integration logic, but an interactive Windows test is still useful for the final UX details: moving below Finish, rebinding to another key, restarting/loading the save, Reset to default, and opening/closing Item Giver with the saved key.
