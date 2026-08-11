# Secret Boss Challenge v0.4.3 — Test Report

## Scope

v0.4.3 is a full standalone replacement for v0.4.2 plus the temporary Shield hotfix. No companion hotfix is required.

The release preserves:

- Boss Challenge behavior in Chapters 1, 2, and 5;
- Pink Scarf, +3 Pink Coins, fourth regular flower purchase, and Pink's Staff reward rules;
- Pink Scarf +25% graze area and +10% graze TP gain;
- whole-party Shield at 75% TP with approximately 75% damage reduction through the next enemy attack phase.

The Shield cast implementation is redesigned to avoid the v0.4.2 `bbox_top` local-variable crash.

## Tools and clean inputs

- UndertaleModTool CLI 0.9.1.2
- G3MTool 1.2.1
- DELTARUNE Windows full release target `v23`

Clean SHA-256:

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Chapter 5 Shield architecture

v0.4.2 inserted Shield-specific party iteration locals directly into `scr_spell`. A real battle cast later failed with:

```text
local variable bbox_top(24) not set before reading it
```

v0.4.3 does **not modify `scr_spell` for Shield execution**. Spell ID 14 is intercepted in `gml_Object_obj_heroparent_Step_0` and routed to the unique helper `scr_gg_sbc_shield_apply`. Every other spell still calls the normal `scr_spell` path.

Round-trip decompilation confirmed the rebuilt `scr_spell` is identical to the clean Chapter 5 decompilation and contains no SBC Shield locals.

Pink reward/retry handling is similarly isolated in `scr_gg_sbc_rewards` instead of adding reward locals to the Pink encounter Step event.

## Chapter 5 round-trip verification

The compiled Chapter 5 output was reopened and decompiled. Confirmed:

- Boss Challenge CONFIG row is present.
- Pink Scarf is weapon ID 38 with 8 AT / 4 DF / 12 MAG.
- Pink's Staff is weapon ID 39 with 14 AT / 2 DF / 4 MAG.
- Pink Scarf metadata reports `Shield / Area +25%, TP +10%`.
- Pink reward helper records challenge eligibility, grants +3 Pink Coins once, grants/retries Pink Scarf, and handles Pink's Staff eligibility for Meaner Bombs.
- challenge-qualified flower progression uses four regular purchases before Flowery's special purchase; normal progression remains three.
- Shield spell ID 14 is a whole-party/no-target spell with internal cost `187.5` (75% of 250 TP).
- `obj_heroparent` routes spell 14 to `scr_gg_sbc_shield_apply()`.
- the helper activates whole-party Shield feedback and sets `global.spelldelay = 20`.
- direct and party-wide `scr_damage` paths reduce damage to `max(1, ceil(tdamage * 0.25))` while Shield is active.
- `scr_mnendturn` clears Shield before the party regains control.
- Pink Scarf adds `grazetpfactor += 0.1` and `grazesizefactor += 0.25`.

## G3M package validation

All three final patches validate with G3MTool 1.2.1.

Final patch SHA-256:

- Chapter 1: `43b8abebc86f52016d5ca3e47f52d391789f67b8d7f1437e276930a4440e7b13`
- Chapter 2: `a971e467a0a68c1aff3a386fad4c219630219337f7fa7a403588819b2602b881`
- Chapter 5: `1131256e9f7d52ebfb41cb4dca2ee710a716fcc0a43b4450979266704e0f8c16`

Chapter 5 final resource plan after removing UTMT-only Sound serialization noise:

- changed CodeEntries: **16**
- new CodeEntries: **2**
  - `gml_Script_scr_gg_sbc_rewards`
  - `gml_Script_scr_gg_sbc_shield_apply`
- new Scripts: **2**
  - `scr_gg_sbc_rewards`
  - `scr_gg_sbc_shield_apply`
- deleted resources: **0**
- Sound resources: **0**

The Chapter 5 patch was applied to the clean v23 file successfully. The G3M-applied output SHA-256 is:

`c91922f5c8c602890a86efcfdbb7ded2e47635ecf0b699f7844a8826cc0c43b4`

## Item Giver Mode v0.3.1 compatibility

Actual G3MTool 1.2.1 merges were run for every shared chapter in both priority orders:

| Chapter | Item Giver → SBC | SBC → Item Giver |
|---|---:|---:|
| 1 | 0 conflicts | 0 conflicts |
| 2 | 0 conflicts | 0 conflicts |
| 5 | 0 conflicts | 0 conflicts |

Chapter 1 and 2 each required three automatic code merges. Chapter 5 required three automatic code merges. No manual conflict resolution was required.

The merged Chapter 5 output was reopened with UndertaleModTool and confirmed to retain:

- Item Giver's `scr_gg_itemgiver_runtime`;
- Item Giver's `ITEM GIVER` Controls drawing;
- SBC's `Boss Challenge` CONFIG drawing;
- SBC's `scr_gg_sbc_shield_apply` caller/helper;
- no SBC Shield locals inside `scr_spell`.

## Final Deltamod ZIP

- Package ID: `github.secretbosschallenge.gladiatorgaming`
- Version: `0.4.3`
- Three native `type="g3mpatch"` routes
- ZIP central-directory/data integrity: passed
- ZIP SHA-256: `05f098a9268c43e80471263378609975d87641f10a7cd99f6651020c9d8207ba`

## Remaining runtime test

Static/merge validation is not a substitute for a complete interactive battle test. The decisive remaining user test is to cast Shield in a real Windows Chapter 5 battle and confirm:

1. the previous `bbox_top` crash is gone;
2. all active party members receive Shield;
3. direct, repeated, and party-wide attacks are reduced correctly;
4. Shield expires after the enemy attack phase;
5. Pink rewards and flower progression behave correctly on qualifying and non-qualifying saves.
