# Item Giver Mode v0.1.1 — Test Report

## Change under test

Version 0.1.1 changes the open/close hotkey from **F8** to **F7** because Medal commonly reserves F8 for clipping and can intercept it before DELTARUNE receives the input.

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Deltamod-compatible CSX package structure
- Debug Mode v4.01 shortcut audit
- Supplied DELTARUNE Windows full-release files, launcher version `v23`

## Clean source checksums

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 3: `c1a0925343694ec9b9adcbf2f916a720b02fd1b999286cfe8fe6a52f3320f714`
- Chapter 4: `ed64789586238b52375e994e1c1cf13694dd2d0dab57d13e639b9c892e37d8f2`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Compilation

The v0.1.1 CSX script compiled and wrote successfully against all five clean chapter files.

Rebuilt SHA-256 values:

- Chapter 1: `7ecea26649ae6c3d91587f9b692995701df8f807d173bf18a0f2e6155410585c`
- Chapter 2: `8be59276a7e7bf77435369d064ff7f5311a0326c198a59a61100be0ce1c0dbdf`
- Chapter 3: `c86e3b0e8d39162c6e45ea43d2bedc4ae24c55a77ac6e5545d3da3f1f77531fd`
- Chapter 4: `1797e98b02f72e2f4e0d35c7785b1fd0e36edbd80095229fa36a78bc70209c46`
- Chapter 5: `7e11d8ee17af943df9aadbe09d1c2286422df9f9503461a2cbd3878b26e35cdd`

## Hotkey verification

Chapter 1 was reopened and the new object events were round-trip decompiled.

- `gml_Object_obj_item_giver_Step_0` contains `keyboard_check_pressed(vk_f7)` for opening and closing.
- `gml_Object_obj_item_giver_Draw_64` displays `X/Esc/F7: Close`.
- No `vk_f8` or F8 menu labels remain in the package source.
- Debug Mode v4.01's supplied scripts use F2, F5, F6, and F10 for their function-key shortcuts and contain no F7 shortcut.

## Idempotency

The matching v0.1.1 script was applied a second time to every rebuilt chapter. The second output was byte-identical to the first output in Chapters 1–5.

## Package validation

- `meta.json` parses as valid JSON.
- Version is `0.1.1`.
- Package ID is `github.itemgivermode.gladiatorgaming`.
- `neededFiles` contains all five verified clean chapter hashes.
- `modding.xml` contains five `type="csx"` routes.
- Every referenced patch exists.
- Required files are at the ZIP root.
- ZIP central-directory and compressed-data integrity checks passed.
- Extracted files are byte-identical to the tested source files.
- ZIP size: `19117` bytes.
- ZIP SHA-256: `b5c53fe5fad375d1de7ec8527d51c109d603eed2984744d83feea247696bebb9`.

## Continuing validation from v0.1.0

The underlying menu, grant paths, startup hook, and object name are unchanged from v0.1.0. That release compiled in both patch orders with Debug Mode v4.01 across Chapters 1–5 and with Secret Boss Challenge v0.4.0 in Chapters 1, 2, and 5.

## Not completed

A manual in-game keypress test could not be automated in the headless workspace. The F7 handler was verified in the compiled and decompiled game code, and the original F8 conflict was identified by the user as Medal's clipping shortcut.
