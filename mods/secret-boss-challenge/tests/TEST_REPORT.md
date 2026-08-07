# Secret Boss Challenge v0.4.2 — Test Report

## Scope

v0.4.2 changes only the Chapter 5 Pink Scarf / Shield behavior:

- Pink Scarf graze-area bonus: approximately **+10% → +25%**.
- Pink Scarf graze TP gain remains approximately **+10%**.
- Shield now protects the **entire active party** instead of one selected ally.
- Shield cost: **65% TP → 75% TP**.
- Shield damage reduction remains approximately **75%** through the next enemy attack phase.

Chapters 1 and 2 are unchanged from v0.4.1.

## Tools and source

- UndertaleModTool CLI 0.9.1.2
- Supplied DELTARUNE Windows full release, launcher target `v23`
- Archived Secret Boss Challenge v0.4.0 gameplay CSX source
- v0.4.2 Chapter 5 delta source: `release/update_ch5_v042.csx`

Clean Chapter 5 SHA-256:

`370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Compilation

1. Applied the archived v0.4.0 Chapter 5 CSX to the clean Chapter 5 file.
2. The resulting source build matched the known v0.4.0 Chapter 5 SHA-256:

   `9e546af99d3cca4a9953c26ce87798c3b444a98356a826a77f7ff749b84a5174`

3. Applied `update_ch5_v042.csx` to that build.
4. UndertaleModTool compiled and wrote the updated Chapter 5 file successfully.
5. v0.4.2 Chapter 5 source-build SHA-256:

   `ac8b2b724046e7178a47cd5a7140ce43e53859ce404f681bd4d64e24a7bb21b1`

## Round-trip code verification

The v0.4.2 Chapter 5 build was reopened and the affected code entries were decompiled.

Confirmed:

- `scr_spellinfo`
  - Shield short description is `Shield#party`.
  - Shield is a no-target/all-party spell (`spelltarget = 0`).
  - Internal cost is `187.5`, which is **75%** of the game's 250 maximum tension value.
- `scr_spell`
  - Shield sets the shared active flag once.
  - Cast feedback is created for each active party slot instead of one selected target.
- `scr_spelltext`
  - Cast text states that Shield is applied to the party.
- `scr_damage`
  - The direct-hit path reduces damage whenever Shield is active, without requiring a target match.
  - The party-wide path does the same for every active party member.
  - Damage remains `ceil(damage * 0.25)` with a minimum of 1.
- `scr_mnendturn`
  - Existing Shield expiry remains unchanged, clearing the effect when the party regains control.
- `obj_grazebox` Create
  - Pink Scarf keeps `grazetpfactor += 0.1`.
  - Pink Scarf now uses `grazesizefactor += 0.25`.
- Pink Scarf equipment metadata
  - Graze TP amount remains `0.1`.
  - Graze-size metadata is now `0.25`.
  - Ability text reports `Shield / Area +25%, TP +10%`.

## Deltamod / G3M package

v0.4.2 remains a standalone native `g3mpatch` package.

Chapter patch SHA-256:

- Chapter 1: `49527c84993260378bc1b45357bb1f010ef95c1d8d315486083c21beef13e722` (unchanged)
- Chapter 2: `f68ec65bf763cf6f1f7c65b9c2d464ede8ca288e0bb00f60e000a41930bd8aca` (unchanged)
- Chapter 5: `712a4e47267eed8cb6bc8b5b88a5f7c7729e307f6b905985373a1247061c38e1`

Chapter 5 patch manifest:

- 16 changed CodeEntries
- 0 new resources
- 0 deleted resources
- no embedded exact/xdelta fallback
- modified semantic-source MD5: `e6b5897a42fbdf501937c3f2a8d9474b`

## Item Giver Mode v0.2.0 compatibility

Item Giver v0.2.0 changes only:

`gml_Object_obj_time_Draw_75`

Secret Boss Challenge v0.4.2 changes the same 16 Chapter 5 resources as v0.4.1, and none are that Item Giver entry. Therefore the two standalone G3M patches still have **zero direct Chapter 5 resource-name overlap**.

The v0.4.1 G3M release was previously merged with Item Giver v0.2.0 using Deltamod 2.0.4's bundled G3MTool in both orders with zero conflicts. A fresh G3MTool binary merge was not repeated in the current workspace for v0.4.2; compatibility is supported by the unchanged resource set and zero-overlap check rather than a new runtime merge log.

## Final ZIP validation

- Package ID: `github.secretbosschallenge.gladiatorgaming`
- Version: `0.4.2`
- Three `type="g3mpatch"` routes: Chapters 1, 2, and 5
- ZIP central-directory/data integrity: passed
- ZIP size: `1,287,377` bytes
- ZIP SHA-256: `0771618faa283cf1caa2fa7e34be5601f5d960036a8b52d8946fdc73a55fc35f`

## Limitations

A complete interactive battle playthrough was not automated. In particular, the final Windows gameplay behavior of whole-party Shield across every possible enemy attack and party composition still benefits from an in-game manual test. Third-party CSX packages targeting the same chapter can also overwrite G3M output because of Deltamod 2.0.4's CSX stage behavior.
