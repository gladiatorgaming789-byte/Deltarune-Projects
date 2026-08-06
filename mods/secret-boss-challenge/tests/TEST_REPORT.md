# Secret Boss Challenge — Test Report

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Wine 11.14 amd64-wow64
- Deltamod 2.0.4 Windows x64 installer supplied by the user
- Supplied DELTARUNE Windows full-release files, launcher version `v23`
- Chapter 1 source SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 source SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`

## Gameplay-patch checks

1. **Chapter 1 CSX application**
   - Loaded the untouched Chapter 1 `data.win` with UTMT CLI.
   - Applied `SecretBossChallenge_ch1.csx`.
   - UTMT compiled and wrote a rebuilt data file.

2. **Chapter 2 CSX application**
   - Loaded the untouched Chapter 2 `data.win` with UTMT CLI.
   - Applied `SecretBossChallenge_ch2.csx`.
   - UTMT compiled and wrote a rebuilt data file.

3. **Round-trip decompilation**
   - Reopened both rebuilt files with UTMT.
   - Re-decompiled every changed code entry.
   - Compared the results at the GML text level with the intended development sources.
   - Result: all changed code entries matched.

4. **Static behavior checks**
   - CONFIG menu has eight selectable rows and a persistent `Boss Challenge` toggle.
   - Setting defaults to OFF and is stored in `true_config.ini` under `MODS/SECRET_BOSS_CHALLENGE`.
   - Normal-mode boss stats and original single-reward branches remain present.
   - Challenge-mode stat and pattern branches are conditional on the setting.
   - Dual-reward pending states and two-stage chest recovery logic are present for both chapters.

5. **Wine startup smoke tests**
   - Root launcher entered the GameMaker main loop.
   - Rebuilt Chapter 1 was launched directly with `-game data.win` and entered the main loop.
   - Rebuilt Chapter 2 was launched directly with `-game data.win` and entered the main loop.

## Deltamod v0.1.1 packaging fix

The v0.1.0 archive incorrectly declared its raw UTMT scripts as `type="xdelta"`. Deltamod therefore sent each `.csx` file to G3MTool's `patch apply` command. G3MTool attempted to read the script as a packaged merge patch and failed with `End of Central Directory record could not be found`.

Version 0.1.1 changes both entries to `type="csx"`. Inspection of Deltamod 2.0.4's installed `GamePatching.js` confirmed that CSX entries are handled in a separate phase through UndertaleModCli.

Validation performed on the v0.1.1 archive:

- `meta.json` parses as valid JSON.
- Nested `metadata.version` is `0.1.1`.
- The package ID remains `github.secretbosschallenge.gladiatorgaming` so it can update the previous package.
- `modding.xml` contains two `type="csx"` entries targeting the Chapter 1 and Chapter 2 `data.win` files.
- Both referenced scripts exist at the archive paths declared by `modding.xml`.
- All required files are at the archive root rather than inside an extra wrapper directory.
- ZIP central-directory and compressed-data integrity checks passed.
- No original game executable, `data.win`, audio, or full decompiled source is included.

## Compatibility note

The scripts patch exact source anchors at install time. Mods changing unrelated code can coexist. A mod changing one of the same code anchors may produce an intentional anchor-conflict error and require a dedicated merged version.

## Not completed

A full manual playthrough of both encounters and every inventory-full permutation was not performed in the headless workspace. The reward paths were validated through source inspection, compilation, round-trip decompilation, and state-transition checks. The corrected ZIP was structurally validated against Deltamod 2.0.4's routing logic, but the complete Deltamod GUI installation flow was not run inside the user's own Windows game installation.
