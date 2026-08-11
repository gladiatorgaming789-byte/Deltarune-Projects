# Secret Boss Challenge

**Version:** 0.4.3  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod native `.g3mpatch` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting, harder secret-boss variants, and enhanced challenge rewards.

## Version 0.4.3

v0.4.3 replaces the broken v0.4.2 Shield cast implementation. **There is no separate Shield hotfix to install.** The fix is part of Secret Boss Challenge itself.

The v0.4.2 crash occurred because whole-party Shield locals were added directly to `scr_spell`, which could produce an invalid GameMaker local table after merging. v0.4.3 leaves `scr_spell` unchanged and routes SBC's unique spell ID `14` through `scr_gg_sbc_shield_apply` from `obj_heroparent` instead.

Pink reward/retry processing is also isolated in `scr_gg_sbc_rewards`, reducing local-variable changes inside the Pink encounter.

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

**Boss Challenge OFF** keeps vanilla Pink reward/flower progression.

**Boss Challenge ON** grants after defeating Pink:

- **Pink Scarf** — Ralsei equipment, **8 AT / 4 DF / 12 MAG**
- **3 additional Pink Coins**
- A fourth regular flower-shop purchase

**Boss Challenge ON + Meaner Bombs** also grants:

- **Pink's Staff** — Kris equipment, **14 AT / 2 DF / 4 MAG**

Eligibility is recorded when Pink is defeated. Turning Boss Challenge on afterward does not qualify an earlier clear. Eligible equipment that cannot fit remains pending and is retried from the flower shop.

## Pink Scarf: Shield and graze passive

Equipping Pink Scarf on Ralsei unlocks **Shield**:

- Cost: **75% TP**
- Targeting: whole active party
- Damage reduction: approximately **75%**
- Duration: next enemy attack phase
- Covers direct and party-wide damage paths
- Clears when the party regains control

Pink Scarf also gives approximately:

- **25% larger graze area**
- **10% more TP from grazing**

## Compatibility

### Item Giver Mode

Secret Boss Challenge v0.4.3 and Item Giver Mode v0.3.1 remain separate Deltamod mods; the retired combined package is not needed.

The actual G3M patches were merged with **G3MTool 1.2.1** in both orders for Chapters 1, 2, and 5. All six merge tests completed with **0 conflicts**. The merged Chapter 5 output was reopened and confirmed to retain Item Giver's runtime/Controls hooks, SBC's Boss Challenge hooks, and the safe Shield helper while `scr_spell` contained no SBC Shield locals.

### Other mods

The native `g3mpatch` format lets Deltamod/G3MTool resource-merge SBC with other merge-aware packages. Compatibility is not universal: mods editing the same CONFIG, boss, reward, spell, damage, graze, flower-shop, or hero spell-caller logic may require conflict resolution.

Deltamod 2.0.4 also runs third-party CSX patches after its G3M merge stage, so a CSX targeting the same chapter can overwrite previously merged G3M output.

## Installation

Install and enable **only** `Secret_Boss_Challenge_v0.4.3_Deltamod.zip` for Secret Boss Challenge. Do not install the old v0.4.2 Shield companion hotfix with v0.4.3.

**Release SHA-256:** `05f098a9268c43e80471263378609975d87641f10a7cd99f6651020c9d8207ba`

Chapter patch SHA-256:

- Chapter 1: `43b8abebc86f52016d5ca3e47f52d391789f67b8d7f1437e276930a4440e7b13`
- Chapter 2: `a971e467a0a68c1aff3a386fad4c219630219337f7fa7a403588819b2602b881`
- Chapter 5: `1131256e9f7d52ebfb41cb4dca2ee710a716fcc0a43b4450979266704e0f8c16`

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
