# Secret Boss Challenge

**Version:** 0.4.1  
**Target:** DELTARUNE full release, launcher version `v23`  
**Chapters:** 1, 2, and 5  
**Installer:** Deltamod native `.g3mpatch` patches

Secret Boss Challenge adds a persistent **Boss Challenge: OFF/ON** setting, harder secret-boss variants, and enhanced challenge rewards.

## Version 0.4.1

v0.4.1 is a **compatibility/packaging update**. Gameplay is unchanged from v0.4.0.

- Distribution changed from Deltamod CSX installation patches to native `g3mpatch` files.
- This lets Deltamod/G3MTool resource-merge the mod with other merge-aware packages instead of one CSX-built `data.win` replacing another.
- The package ID is unchanged, so v0.4.1 updates earlier Secret Boss Challenge installations in place.
- Item Giver Mode v0.2.0 can now remain a **separate enabled mod**; no dedicated combined package is required.

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

- **Pink Scarf** — Ralsei weapon, **8 AT / 4 DF / 12 MAG**
- **3 additional Pink Coins**
- A fourth regular flower-shop purchase

**Boss Challenge ON + Meaner Bombs** also grants:

- **Pink's Staff** — Kris weapon, **14 AT / 2 DF / 4 MAG**

Eligibility is recorded when Pink is defeated. Turning Boss Challenge on afterward does not qualify an earlier clear.

## Pink Scarf: Shield and graze passive

Equipping Pink Scarf on Ralsei unlocks **Shield**:

- Cost: **65% TP**
- Target: one party member
- Damage reduction: approximately **75%**
- Duration: next enemy attack phase
- Recasting replaces the previous protected target
- Covers repeated direct hits and party-wide damage during that phase

Pink Scarf also gives approximately:

- **10% larger graze area**
- **10% more TP from grazing**

## Save migration and inventory recovery

- Weapon ID `38`: Pink Scarf
- Weapon ID `39`: Pink's Staff
- Spell ID `14`: Shield
- Eligible equipment that cannot fit remains pending and can be retried by the flower shop.
- Pink Scarf, Pink's Staff, and bonus Pink Coins retain separate one-time reward states.
- Rewards already received from older releases are not removed.

## Compatibility

### Item Giver Mode

**Secret Boss Challenge v0.4.1 and Item Giver Mode v0.2.0 are separate Deltamod mods and were tested together.**

Using the **G3MTool 1.2.1 binary bundled with Deltamod 2.0.4**, shared Chapters 1, 2, and 5 were merged in both mod orders with **0 G3M conflicts**. Round-trip checks retained both mods, including Pink Scarf and Shield in Chapter 5.

### Other mods

v0.4.1 is substantially more merge-friendly than the CSX-distributed release because Deltamod can resource-merge it with other `g3mpatch`/xdelta packages.

Compatibility is not universal. Deltamod 2.0.4 runs third-party CSX patches after the G3M merge stage, and a CSX targeting the same chapter can overwrite previously merged output. Mods editing the same boss/config/spell/damage/graze/shop code can also produce genuine semantic conflicts.

## Installation

Install `Secret_Boss_Challenge_v0.4.1_Deltamod.zip` directly through Deltamod. Do **not** install the retired Item Giver + Secret Boss Challenge combined package; use the two current standalone packages instead.

**Release SHA-256:** `cdaf7546bd78013dcb26264f91e71499454c7cb4600a016fe76aea252438709e`

The reproducible source under [`release/`](release/) retains the v0.4.0 gameplay CSX as source and converts its clean modified results to native `g3mpatch` files.

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
