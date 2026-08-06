# Item Giver Mode v0.1.3 — Test Report

## Reported issue

After the v0.1.2 startup crash was fixed, pressing F7 still did not open the Item Giver menu in the user's game.

## Root-cause mitigation

Versions 0.1.0–0.1.2 depended on a dynamically added persistent `obj_item_giver` instance to receive Step and Draw GUI events. v0.1.3 removes that runtime dependency completely.

The Item Giver now patches DELTARUNE's native persistent `obj_time` object:

- Create event: initializes Item Giver state and helper functions.
- Step event: handles F7, navigation, refresh, and grant input.
- Draw GUI event: renders the Item Giver overlay and status messages.
- Clean Up event: restores interaction state if needed.

No `obj_item_giver` instance is created by v0.1.3.

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

## Clean compilation

v0.1.3 compiled successfully against all five clean chapters.

Rebuilt SHA-256 values:

- Chapter 1: `d4df0619bd2a4a21d9b7d8e8f8eeaa42ed35bf82ba0fa4337a4e52b41694774c`
- Chapter 2: `46e658654656cc1b292492999be33768d9e89ff9b1010956601e4dcab51354ea`
- Chapter 3: `9ceaaef56ca884aea38e1df2e481ff8b16e00d466ff63522a133db6c9e093461`
- Chapter 4: `6af064d20508762bf94362f8c7324013695fcc7ff7ba204c5bc2128b56a0b01e`
- Chapter 5: `37c0920f80bc6869ea8a5f362b43696d9cd3400432eb29d9b0cd50589e42b86a`

## Round-trip verification

Chapter 1 was reopened and the patched native events were decompiled.

Confirmed:

- `gml_Object_obj_time_Create_0` contains `ig_itemgiver_version = "0.1.3"` and `ig_build_lists`.
- `gml_Object_obj_time_Step_0` contains the F7 open/close handler.
- `gml_Object_obj_time_Draw_64` contains the `ITEM GIVER` GUI.
- The runtime input path no longer depends on `obj_item_giver` or `instance_create`.

## Idempotency

Applying v0.1.3 a second time to every rebuilt chapter produced byte-identical output in Chapters 1–5.

## Debug Mode v4.01 compatibility

Both patch orders compiled successfully for Chapters 1–5:

1. Clean → Debug Mode → Item Giver v0.1.3
2. Clean → Item Giver v0.1.3 → Debug Mode

Chapter 1 round-trip checks confirmed both outputs retain:

- Debug Mode's `vk_f10` handler
- Item Giver's `vk_f7` handler
- Item Giver's Draw GUI event

## Secret Boss Challenge v0.4.0 compatibility

Both Chapter 5 patch orders compiled successfully:

1. Clean → Secret Boss Challenge → Item Giver v0.1.3
2. Clean → Item Giver v0.1.3 → Secret Boss Challenge

## Package validation

- `meta.json` parses successfully and reports version `0.1.3`.
- Package ID remains `github.itemgivermode.gladiatorgaming`.
- `modding.xml` contains five Deltamod CSX patch fragments.
- Every referenced chapter patch exists in the archive.
- ZIP integrity check passed.
- Scripts extracted from the final ZIP are byte-identical to the tested source scripts.
- Applying those extracted scripts reproduces the tested chapter outputs byte-for-byte.
- ZIP size: `20215` bytes.
- ZIP SHA-256: `31bb9893ba38a7b54953487953e3bc7155d52860a7eb77729a9ba73f29a865e5`.

## Not completed

A full interactive in-game keypress test was not automated in the headless workspace. The important change in v0.1.3 is that F7 is now evaluated by DELTARUNE's existing persistent `obj_time` Step event rather than a dynamically added runtime object. The user should replace v0.1.2 with v0.1.3 and test F7 after loading a save in the overworld.
