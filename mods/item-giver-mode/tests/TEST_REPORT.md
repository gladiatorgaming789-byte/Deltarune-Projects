# Item Giver Mode — Test Report

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Deltamod-compatible CSX package structure
- Debug Mode v4.01
- Secret Boss Challenge v0.4.0
- Supplied DELTARUNE Windows full-release files, launcher version `v23`

## Clean source checksums

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 3: `c1a0925343694ec9b9adcbf2f916a720b02fd1b999286cfe8fe6a52f3320f714`
- Chapter 4: `ed64789586238b52375e994e1c1cf13694dd2d0dab57d13e639b9c892e37d8f2`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Feature implementation

Each chapter patch adds one persistent `obj_item_giver` object with:

- Create event containing the dynamic category scanners and preview helpers.
- Step event containing F8 toggle, keyboard navigation, refresh, inventory-full handling, and native grant calls.
- Draw GUI event containing the item browser and status display.
- Clean Up event restoring the previous interaction state.

`scr_gamestart` receives an idempotent instance-creation append guarded by `instance_exists`.

The runtime scanner uses the chapter’s own `scr_iteminfo`, `scr_weaponinfo`, `scr_armorinfo`, `scr_keyiteminfo`, and `scr_litemname` definitions. It scans IDs 1–255 and includes only nonblank names.

## Compilation and round-trip checks

All five clean chapter files:

1. Loaded successfully in UndertaleModTool.
2. Applied their matching Item Giver CSX script.
3. Compiled and wrote successfully.
4. Reopened successfully.
5. Round-trip decompiled all four new object events.
6. Confirmed the F8 handler, native grant functions, and guarded `scr_gamestart` creation code remained present.

Rebuilt SHA-256 values:

- Chapter 1: `2a304c0ba4aa197ca4f9dfc8f9916cce8fbeb1160a956b1b7e623a8a5daaf56b`
- Chapter 2: `c54a0019a4bd21eb68b3752e357c7521368c7f3bd2db94f0b18523eb52c48770`
- Chapter 3: `e9bae5c69a2c169125f81d4b98797ce721052b30b630146e0dd73c3d940fa938`
- Chapter 4: `1bdaa7e1167a19374bc7dc99d271f40d93aba6afec6413cff0ef9d351e90cf59`
- Chapter 5: `7eed70a2e825b7f9f28cc17253c0e43a2620e60a078e0d3343f48f264b355a5a`

## Idempotency

The matching Item Giver script was applied a second time to every rebuilt chapter. The second output was byte-identical to the first output in Chapters 1–5.

## Debug Mode v4.01 compatibility

Both patch orders compiled successfully for every chapter:

1. Clean chapter → Debug Mode → Item Giver Mode
2. Clean chapter → Item Giver Mode → Debug Mode

The resulting files retained the Item Giver object, F8 input handler, native grant calls, and guarded startup creation. Debug Mode’s own changes also compiled in both orders.

## Secret Boss Challenge v0.4.0 compatibility

Both patch orders compiled successfully in Chapters 1, 2, and 5:

1. Clean chapter → Secret Boss Challenge → Item Giver Mode
2. Clean chapter → Item Giver Mode → Secret Boss Challenge

Chapter 5 round-trip checks confirmed both outputs retained:

- Item Giver’s weapon grant path
- Pink Scarf
- Pink’s Staff
- Shield-related Pink Scarf data

Because the item list is generated at runtime from the final weapon table, the challenge mod’s named equipment is included when both mods are installed.

## Package validation

- `meta.json` parses as valid JSON.
- Version is `0.1.0`.
- Package ID is `github.itemgivermode.gladiatorgaming`.
- `neededFiles` contains all five verified clean chapter hashes.
- `modding.xml` contains five `type="csx"` routes.
- Every referenced patch exists.
- Required files are at the ZIP root.
- ZIP central-directory and compressed-data integrity checks passed.
- Scripts extracted from the final ZIP reproduced the tested chapter outputs byte-for-byte.
- No original game binaries, `data.win` files, music, or full decompiled source are included.

## Not completed

A full manual playthrough selecting every listed entry, every inventory-full permutation, and every plot-sensitive key item was not performed in the headless workspace. The mod was validated through source inspection, successful compilation, round-trip decompilation, idempotency testing, and cross-mod patch-order testing. Wine prefix initialization did not complete reliably in this workspace, so no additional launch smoke result is claimed for this release.
