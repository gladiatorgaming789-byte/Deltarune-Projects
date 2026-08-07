# Secret Boss Challenge

**Version:** 0.4.2  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod native `.g3mpatch` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting, harder secret-boss variants, and enhanced challenge rewards.

## Version 0.4.2

v0.4.2 updates **Pink Scarf** and **Shield** while preserving the standalone mergeable `g3mpatch` distribution introduced in v0.4.1.

- Pink Scarf graze-area bonus: **+10% → +25%**.
- Pink Scarf graze TP gain remains **+10%**.
- Shield now protects the **entire active party** instead of one selected ally.
- Shield cost: **65% TP → 75% TP**.
- Shield still reduces incoming damage by approximately **75%** for the next enemy attack phase.
- Package ID is unchanged, so v0.4.2 updates prior installations in place.

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
- Targeting: no ally-selection step; the spell protects the **whole active party**
- Damage reduction: approximately **75%**
- Duration: the next enemy attack phase
- Covers repeated direct hits and party-wide damage during that phase
- Shield clears when the party regains control

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

Secret Boss Challenge v0.4.2 and Item Giver Mode v0.2.0 remain **separate Deltamod mods**.

Item Giver changes only `gml_Object_obj_time_Draw_75` in Chapter 5. Secret Boss Challenge v0.4.2 still changes the same 16 Chapter 5 resources as v0.4.1, none of which overlap that Item Giver resource. The v0.4.1 G3M releases were previously merged with Deltamod 2.0.4's bundled G3MTool in both orders with zero conflicts; v0.4.2 keeps that resource separation.

### Other mods

The native `g3mpatch` format lets Deltamod/G3MTool resource-merge Secret Boss Challenge with other merge-aware packages. Compatibility is not universal: mods editing the same CONFIG, Pink reward, spell, damage, graze, or flower-shop code may require conflict resolution.

Deltamod 2.0.4 also runs third-party CSX patches after its G3M merge stage, so a CSX targeting the same chapter can overwrite previously merged output.

## Installation

Install `Secret_Boss_Challenge_v0.4.2_Deltamod.zip` directly through Deltamod.

**Release SHA-256:** `0771618faa283cf1caa2fa7e34be5601f5d960036a8b52d8946fdc73a55fc35f`

The reproducible source under [`release/`](release/) extracts the archived v0.4.0 gameplay source, applies [`update_ch5_v042.csx`](release/update_ch5_v042.csx) to Chapter 5, and converts the results to native `g3mpatch` files.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
