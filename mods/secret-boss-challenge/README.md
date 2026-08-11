# Secret Boss Challenge

**Base version:** 0.4.2  
**Current runtime fix:** Shield Hotfix `0.4.2-hf1`  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod native `.g3mpatch` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting, harder secret-boss variants, and enhanced challenge rewards.

## Important: v0.4.2 Shield runtime hotfix

A real battle test found that casting Shield in the base v0.4.2 package can crash in `gml_Script_scr_spell` with:

```text
local variable bbox_top(24) not set before reading it
```

The earlier v0.4.2 compile/decompile validation did not catch this because interactive Shield casting had remained an explicit manual-test item.

Use **Secret Boss Challenge v0.4.2 and the Shield Hotfix hf1 together**. The hotfix is a separate companion G3M package so the existing v0.4.2 gameplay patch remains installed.

Hotfix design:

- Vanilla Chapter 5 has no spell case 14; case 14 is SBC's Shield.
- The hotfix intercepts spell 14 in `gml_Object_obj_heroparent_Step_0` before the broken v0.4.2 `scr_spell` branch runs.
- Shield casting is moved to the unique helper `scr_gg_sbc_shield_apply`.
- Every non-Shield spell still calls the normal `scr_spell` unchanged.
- The final hotfix G3M footprint is only **1 changed CodeEntry + 1 new Script + 1 new CodeEntry**, with no unrelated Sound resources.

The companion hotfix was validated/applied with G3MTool 1.2.1 and round-trip decompiled successfully. Item Giver Mode v0.3.1 + the hotfix were also merged in both orders with **0 conflicts**.

See [`tests/SHIELD_HOTFIX_HF1.md`](tests/SHIELD_HOTFIX_HF1.md) for the exact validation record and [`release/shield_hotfix_v042_hf1.csx`](release/shield_hotfix_v042_hf1.csx) for the source.

## Version 0.4.2 gameplay

- Pink Scarf graze-area bonus: **+25%**.
- Pink Scarf graze TP gain: **+10%**.
- Shield protects the **entire active party** instead of one selected ally.
- Shield cost: **75% TP**.
- Shield reduces incoming damage by approximately **75%** for the next enemy attack phase.

## Features

### Chapters 1 and 2

When Boss Challenge is enabled:

- Chapter 1 secret boss: 3500 → 4550 HP, 10 → 12 AT, 5 → 6 DF, +15% attack duration, +15% bullet damage rounded upward.
- Chapter 2 secret boss: 4809 → 6000 HP, 13 → 15 AT, and the built-in higher-intensity pattern behavior is enabled.
- Winning either encounter grants both route-dependent equipment rewards.
- Full equipment storage keeps missing rewards recoverable from the existing post-boss treasure chest.

When Boss Challenge is disabled, original stats, patterns, rewards, and route logic are preserved.

### Chapter 5: Pink rewards

Boss Challenge is available in Chapter 5 CONFIG.

**Boss Challenge OFF** keeps the vanilla Pink reward/flower progression:

- No Pink Scarf
- No bonus Pink Coins
- Three regular flower purchases before Flowery's special scarf
- No Pink's Staff, even with Meaner Bombs

**Boss Challenge ON** grants after defeating Pink:

- **Pink Scarf** — Ralsei equipment, **8 AT / 4 DF / 12 MAG**
- **3 additional Pink Coins**
- A fourth regular flower-shop purchase

**Boss Challenge ON + Meaner Bombs** also grants:

- **Pink's Staff** — Kris equipment, **14 AT / 2 DF / 4 MAG**

Eligibility is recorded when Pink is defeated. Turning Boss Challenge on afterward does not qualify an earlier clear.

## Pink Scarf: Shield and graze passive

Equipping Pink Scarf on Ralsei unlocks **Shield**:

- Cost: **75% TP**
- Targeting: whole active party
- Damage reduction: approximately **75%**
- Duration: the next enemy attack phase
- Covers repeated direct hits and party-wide damage during that phase
- Clears when the party regains control

Pink Scarf also gives approximately:

- **25% larger graze area**
- **10% more TP from grazing**

## Save migration and inventory recovery

- Pink Scarf remains item ID `38`.
- Pink's Staff remains item ID `39`.
- Shield remains spell ID `14`.
- Eligible equipment that cannot fit remains pending and can be retried by the flower shop.
- Pink Scarf, Pink's Staff, and bonus Pink Coins retain separate one-time reward states.
- Rewards already received from older releases are not removed.

## Compatibility

### Item Giver Mode

Secret Boss Challenge and Item Giver Mode remain **separate Deltamod mods**. Do not recreate the retired combined package.

Item Giver v0.3.1 changes its own Controls/Item Giver resources. The Shield hotfix changes `gml_Object_obj_heroparent_Step_0` and adds a uniquely named Shield helper. The two G3M patches were merged with G3MTool 1.2.1 in both orders with zero conflicts.

### Other mods

The native `g3mpatch` format lets Deltamod/G3MTool resource-merge Secret Boss Challenge with other merge-aware packages. Compatibility is not universal: mods editing the same CONFIG, Pink reward, spell, damage, graze, flower-shop, or hero spell-caller code may require conflict resolution.

Deltamod 2.0.4 also runs third-party CSX patches after its G3M merge stage, so a CSX targeting the same chapter can overwrite previously merged output.

## Installation

1. Install and enable `Secret_Boss_Challenge_v0.4.2_Deltamod.zip`.
2. Install and enable `Secret_Boss_Challenge_v0.4.2_Shield_Hotfix_Deltamod.zip` alongside it.
3. Item Giver Mode, if used, stays as its own separate mod.

Base v0.4.2 SHA-256: `0771618faa283cf1caa2fa7e34be5601f5d960036a8b52d8946fdc73a55fc35f`

Shield Hotfix hf1 ZIP SHA-256: `0fb3ece57a20291e95100972ab55cd8da9ae2368d8631ac76bdbb7adfa95c27f`

Shield Hotfix Chapter 5 G3M SHA-256: `0e3779aeb1f67594d0810fdd8be61b1113f7ad2d9721cc0cd749d79608d381ee`

## Reproducibility note

The current GitHub copy of the archived v0.4.0 Chapter 5 CSX is malformed: its ZIP member has a bad stored CRC and the decompressed source tail is corrupted. The previously generated full v0.4.2 binary package is also not available in this workspace. For that reason, hf1 is deliberately published as a companion hotfix rather than falsely claiming a fully reproducible v0.4.3 replacement package.

The v0.4.2 gameplay delta source remains under [`release/update_ch5_v042.csx`](release/update_ch5_v042.csx).
