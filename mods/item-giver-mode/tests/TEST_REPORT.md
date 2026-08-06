# Item Giver Mode v0.1.2 — Test Report

## Bug fixed

The user reported an immediate startup crash in `gml_GlobalScript_scr_gamestart`:

`global variable name '__objectID2Depth' ... not set before reading it`

The call chain was `scr_gamestart -> instance_create -> object_get_depth`. Item Giver adds `obj_item_giver` dynamically through UTMT, but DELTARUNE's internal object-ID-to-depth table is initialized for the original object set and does not contain that new object ID.

v0.1.2 changes the guarded startup creation from `instance_create(0, 0, obj_item_giver)` to `instance_create_depth(0, 0, -999999, obj_item_giver)`. This bypasses `object_get_depth` and the missing `__objectID2Depth` lookup entirely.

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Deltamod-compatible CSX package structure
- Supplied DELTARUNE Windows full-release files, launcher version `v23`

## Clean source checksums

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 3: `c1a0925343694ec9b9adcbf2f916a720b02fd1b999286cfe8fe6a52f3320f714`
- Chapter 4: `ed64789586238b52375e994e1c1cf13694dd2d0dab57d13e639b9c892e37d8f2`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Compilation and idempotency

The v0.1.2 CSX scripts compiled and wrote successfully against all five clean chapter files. Each matching script was then applied a second time; the second output was byte-identical to the first output in every chapter.

Rebuilt SHA-256 values:

- Chapter 1: `a3df0cd31dd74cc5d0f93b1dec01bae1ef3dcfb517c97b098138203c5e6a2f53`
- Chapter 2: `e72cee5ebbf9c0a5c45b12d6d748ad2cff71edd3b3c0b6e9cce84fc0ce0e5f6f`
- Chapter 3: `db2930c2be26ae7277771dabb234117d93d0c815cfff5fbe015c1368f9d27347`
- Chapter 4: `3186c0ed0787f6a0e64ddeb7d4dc7666bd0a06e798dfde5eb5dced01331831bd`
- Chapter 5: `58a71aa5d9c3e38682867bd25a627d2f6c0758fbe831798955eea846866d2f3b`

All five packaged CSX files contain `instance_create_depth(0, 0, -999999, obj_item_giver)` and contain no remaining `instance_create(0, 0, obj_item_giver)` startup call.

## Package validation

- `meta.json` parses as valid JSON.
- Version is `0.1.2`.
- Package ID remains `github.itemgivermode.gladiatorgaming`.
- `neededFiles` contains the five verified clean chapter hashes.
- `modding.xml` contains five `type="csx"` routes.
- Required files are at the archive root.
- ZIP compressed-data integrity test passed.
- ZIP SHA-256: `c6480fa7a84cccc8c6cf73b0b41be291b3845bdabe08a5550bb0f026cda7cab8`.

## Remaining manual check

The exact user-reported crash path has been removed and the replacement compiles in all five chapters. A fresh in-game startup/menu-opening test still needs to be confirmed by running the release in the user's normal Deltamod/Windows setup.
