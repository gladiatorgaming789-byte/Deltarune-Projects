# Secret Boss Challenge — Test Report

## Environment

- UndertaleModTool CLI 0.9.1.2 for Ubuntu
- Wine 11.14 amd64-wow64
- Supplied Windows full-release files, launcher version `v23`
- Chapter 1 source SHA-256: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2 source SHA-256: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`

## Passed checks

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
   - Compared the results byte-for-byte at the GML text level with the intended development sources.
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
   - The headless test environment had no audio device; GameMaker correctly fell back to dummy audio. Chapter 1 also reported a missing streamed music path because it was launched directly from the chapter directory rather than through the normal launcher. Neither condition was caused by the patch.

6. **Deltamod package checks**
   - `meta.json` parses as valid JSON.
   - `modding.xml` contains Chapter 1 and Chapter 2 patch entries.
   - Required files are at the archive root rather than inside an extra wrapper directory.
   - No original game executable, `data.win`, audio, or full decompiled source is included.

## Not completed

A full manual playthrough of both encounters and every inventory-full permutation was not performed in the headless workspace. The reward paths were validated through source inspection, compilation, round-trip decompilation, and state-transition checks.
