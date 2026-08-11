# Item Giver Mode v0.3.1 — Test Report

## Why v0.3.1 exists

The real Windows test of v0.3.0 exposed two failures that static source checks had missed:

1. The pseudo **ITEM GIVER** Controls row did not appear/work as a real selectable Controls entry and pressing the documented default `I` did nothing.
2. Enabling Item Giver together with Secret Boss Challenge could crash in `gml_Object_obj_time_Draw_75` with `local variable bbox_top(24) not set before reading it`.

The v0.3.0 test process compiled and decompiled the pseudo-row source but never performed the required interactive Controls test. v0.3.1 replaces that architecture rather than trying to patch around it.

## v0.3.1 architecture

Per chapter, the final G3M patch contains exactly:

### Changed CodeEntries

- `gml_Object_obj_darkcontroller_Step_0`
- `gml_Object_obj_darkcontroller_Draw_0`
- `gml_Object_obj_time_Draw_75`

### New resources

- Script `scr_gg_itemgiver_runtime`
- CodeEntry `gml_Script_scr_gg_itemgiver_runtime`

### Important obj_time invariant

`gml_Object_obj_time_Draw_75` contains one Item Giver call:

```gml
scr_gg_itemgiver_runtime();
```

and no Item Giver local-variable declarations. The menu, inventory logic, persistence, and drawing code live in the uniquely named helper script.

## Clean compilation and round-trip verification

UndertaleModTool CLI 0.9.1.2 compiled the v0.3.1 source successfully for Chapters 1–5.

The outputs were independently reopened/decompiled. Every chapter confirmed:

- exactly one `scr_gg_itemgiver_runtime()` call in `obj_time` Draw GUI End;
- no Item Giver local variables in that event;
- real Controls navigation permits row 9 on non-console builds;
- custom keyboard rebind state `3` is present;
- the Controls Draw event contains `ITEM GIVER`;
- default Item Giver key is `73` / `I`;
- runtime version is `0.3.1`.

## G3MTool 1.2.1 validation

G3MTool 1.2.1 was used to create, validate, and apply the patches.

UTMT serialization caused G3MTool to initially report unrelated Sound resources as changed. Item Giver does not edit audio. The release pipeline now removes those Sound-only serialization diffs and rejects any other unexpected resource type/name.

Final patch resource plan for **every chapter**:

- changed CodeEntries: **3**;
- new CodeEntries: **1**;
- new Scripts: **1**;
- deleted resources: **0**;
- Sound resources: **0**.

All five cleaned patches validated with G3MTool 1.2.1, applied to their clean chapter `data.win`, and the applied outputs were reopened/decompiled with the expected v0.3.1 markers intact.

Final patch SHA-256:

- Chapter 1: `ee88093622f0289f1a0093721fe37d6d7d9ad5a3e555fa632b98b632ce63c23c`
- Chapter 2: `690bcae922d0e7550e8004131a02f44dd45e44b8172cce985fc3df8111bb57f3`
- Chapter 3: `6bffcb80ea38a175b5d8edfb8d03ec2e21276525db98bc8b069df5d66d942b0f`
- Chapter 4: `b7e2af8de409589c1138c92eb0229caf799e3974eb7d11d3bdecda1680d16b90`
- Chapter 5: `626d47da976cc78ff95a7f6f3494b418bdb00198e1ad339586c05ef42ab39fd7`

G3M-applied output SHA-256:

- Chapter 1: `1af6579ea9eb85f2abf4187471b717f1979d3a42bd5fa7c0988cb3062e445893`
- Chapter 2: `b5c4b8f5b2bec8403edca9598d6e4a81fab7c4bf4dc5a25d7c2b69bd6c7964cd`
- Chapter 3: `019fe0ea967705991acf4f6dbfc66dbff214ccd612005f863d8cd402de77310c`
- Chapter 4: `a95e9a484b51debc700761fa1538555e3a24e313702860137c4e49cbbcf9f3a8`
- Chapter 5: `67a25340ce19bbf1cccb114453a4d3f7984a09c63b9ca0a8d1baa709e0b9697a`

## Release-builder verification

The cleaned repository `build_release.py` was itself exercised against all five clean chapters. Chapters 1–5 each compiled and reached G3MTool validation with the expected **3 changed + 2 new** resource counts after Sound-noise removal.

## Real Wine input smoke

A test-only Chapter 1 build added temporary debug messages to the v0.3.1 runtime. Those messages are **not** present in the release.

The supplied DELTARUNE Windows runner was launched under Wine 11.14/Xvfb. After GameMaker entered its main loop, an actual `I` keyboard event was sent to the game window.

Observed runtime markers:

```text
ITEMGIVER_SMOKE_KEY_PRESSED
ITEMGIVER_SMOKE_MENU_OPEN
```

This confirms the rebuilt runtime receives the default `I` key and reaches the menu-open path in a running GameMaker build. An unrelated missing test-environment music stream warning was observed because the smoke directory did not contain the full `mus` folder; it is not part of Item Giver.

## Secret Boss Challenge v0.4.2 compatibility status

The reported `bbox_top` failure occurred in the local-heavy v0.3.0 `obj_time` architecture. v0.3.1 removes that architecture: `obj_time` now gains one helper call and no Item Giver locals.

Secret Boss Challenge v0.4.2 is documented as a native G3M patch with 16 changed Chapter 5 CodeEntries and no new/deleted resources. Its published v0.4.2 delta source does not target Item Giver's new `obj_darkcontroller` Controls handlers or `scr_gg_itemgiver_runtime` helper name.

However, the exact SBC v0.4.2 binary patch was not locally available in this workspace, so a fresh current-version both-order G3M binary merge was not performed. Do **not** treat the combined-mod runtime as fully validated until the user tests both current packages together through Deltamod.

## Final Deltamod ZIP

- Package ID: `github.itemgivermode.gladiatorgaming`
- Version: `0.3.1`
- Five `type="g3mpatch"` routes
- ZIP central-directory/data integrity: passed
- ZIP SHA-256: `444e81da9f2f73de802c306568e69ef74e06ce8ddf0adbc765699062c3a80cad`

## Manual regression still needed

The remaining user-side tests are:

1. Open Controls and confirm **ITEM GIVER** appears below Finish.
2. Rebind it from `I` to another unused key.
3. Leave/reopen Controls and confirm the key persists.
4. Confirm the rebound key opens/closes Item Giver.
5. Confirm Reset to default restores `I`.
6. Launch Item Giver v0.3.1 together with Secret Boss Challenge v0.4.2 through Deltamod and confirm the previous `bbox_top` crash is gone.
