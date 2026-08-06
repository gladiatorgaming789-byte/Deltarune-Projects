# Source mods and credits

The compatibility merge combines the following projects while preserving their authorship:

| Included project | Version | Author / credit | Coverage or selection |
|---|---:|---|---|
| Custom Difficulty | 1.8.3 | Emmahaha | Chapters 1–5; difficulty menu, gameplay scaling, and `difficulty.ini` data |
| Better Saves | v7 | thej01 | Launcher and Chapters 1–5; expanded save slots and menu UI |
| Deltarune 60 FPS | 1.1.20 | BadArtAdventure | Launcher and Chapters 1–4 timing conversion |
| Deltarune Chapter 1 Modernized | 1.1.0 | Qbix1234 | Chapter 1 code, graphics, sounds, and English localization override |
| No-Hat Ralsei Face Resprites | 1.4 | Serif0S & theginger | Chapters 1–5 face sprites; wins the overlapping Modernized no-hat sprite |
| New ACTs in the Knight Fight | 2.7 | ToyBoyC | Chapter 3; **Normal** variant selected |
| Improved Pink Fight Background | 2.0 audio component | evokaf | User-supplied `pink.ogg` override; incompatible visual patch excluded |

## Compatibility decisions

- Better Saves remains authoritative for slot count, copy, deletion, and shifting behavior.
- Custom Difficulty's `difficulty.ini` sections move with the corresponding Better Saves slot.
- Difficulty-adjusted gameplay values are calculated before final 60 FPS timing conversion.
- No-Hat Ralsei takes precedence over Chapter 1 Modernized where both modify the same face sprite.
- Knight ACT additions coexist with Chapter 3 difficulty and timing edits.
- No original DELTARUNE `data.win` file or executable is included in the archived project folder.
