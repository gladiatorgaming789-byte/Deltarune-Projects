Secret Boss Challenge v0.4.3
============================

This is the complete standalone release. No separate Shield hotfix is needed.

BOSS CHALLENGE
- Chapters 1 and 2: harder secret-boss variants and expanded route rewards while Boss Challenge is enabled.
- Chapter 5: Boss Challenge toggle in CONFIG.
- Pink + Boss Challenge ON: Pink Scarf, 3 extra Pink Coins, and a fourth regular flower purchase.
- Pink + Boss Challenge ON + Meaner Bombs: also grants Pink's Staff.

PINK SCARF
- Ralsei equipment: 8 AT, 4 DF, 12 MAG.
- Unlocks Shield for Ralsei.
- Shield costs 75% TP.
- Shield protects the entire active party for the next enemy attack phase.
- Protected party members take about 25% of normal damage.
- Passive graze area bonus: about +25%.
- Passive graze TP gain: about +10%.

V0.4.3 SHIELD FIX
Shield no longer adds its whole-party locals to scr_spell. Spell 14 is routed through the unique scr_gg_sbc_shield_apply helper, preventing the bbox_top local-table crash reported in v0.4.2. Pink reward/retry logic is also isolated in scr_gg_sbc_rewards.

DELTAMOD COMPATIBILITY
The package uses native g3mpatch routes for Chapters 1, 2, and 5. G3MTool 1.2.1 merged SBC v0.4.3 with Item Giver Mode v0.3.1 in both orders for every shared chapter with 0 conflicts.

Package ID: github.secretbosschallenge.gladiatorgaming
Target: DELTARUNE Windows full release v23
License: MIT
