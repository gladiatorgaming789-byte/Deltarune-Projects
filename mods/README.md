# Mods

This folder contains completed **original standalone DELTARUNE mods**.

## Published projects

### Secret Boss Challenge

Folder: [`secret-boss-challenge/`](secret-boss-challenge/README.md)

- Base version: **0.4.2**
- Current runtime fix: **Shield Hotfix 0.4.2-hf1** (companion package)
- Chapters 1, 2, and 5
- Optional secret-boss challenge behavior and enhanced rewards
- Pink Scarf unlocks a 75% TP whole-party Shield
- Pink Scarf gives approximately +25% graze area and +10% graze TP gain
- v0.4.2 has a confirmed Shield-cast `scr_spell` local-variable crash; hf1 bypasses only Shield through a unique helper
- Base mod and hotfix are Deltamod-native `g3mpatch` packages
- Includes source and validation records for the hotfix

### Item Giver Mode

Folder: [`item-giver-mode/`](item-giver-mode/README.md)

- Current version: **0.3.1**
- Chapters 1–5
- Debug-style item browser with a genuine editable DELTARUNE Controls-menu binding
- Default Item Giver key: **I**
- Items, equipment, armor, key items, and Light World items
- Runtime scan can see named item definitions from successfully merged mods
- Distributed as Deltamod-native `g3mpatch` resources
- Real Controls logic lives in `obj_darkcontroller`; the heavy runtime is isolated in `scr_gg_itemgiver_runtime`
- `obj_time` receives only one helper call, avoiding the local-heavy v0.3.0 design
- Includes source, package metadata, license, and validation report

### Item Giver + Secret Boss Challenge compatibility

Keep the projects separate. The obsolete dedicated combined package remains retired.

Item Giver v0.3.1 and the SBC Shield hf1 G3M patch were merged with G3MTool 1.2.1 in **both orders with 0 conflicts**. The remaining real-Windows regression is to run Item Giver v0.3.1 + SBC v0.4.2 + hf1 together and cast Shield.

## Project layout

```text
mods/
├── secret-boss-challenge/
│   ├── README.md
│   ├── LICENSE
│   ├── release/
│   └── tests/
└── item-giver-mode/
    ├── README.md
    ├── LICENSE
    ├── release/
    └── tests/
```

Merged builds do not belong in this folder. They must be placed under [`merged mods/`](../merged%20mods/README.md).

## Compatibility note

`g3mpatch`/xdelta mods are merged by Deltamod's G3MTool stage. Deltamod 2.0.4 applies CSX mods afterward, so a third-party CSX targeting the same chapter can still overwrite G3M-merged output. This installer limitation means no standalone `data.win` mod can truthfully promise compatibility with every CSX package.

## Archive requirements

Published projects must:

- Be complete and documented.
- Use Deltamod for installable releases.
- Avoid original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.
- Include compatibility and validation information.
- Preserve authorship and licensing details.

[Return to the archive index](../README.md)
