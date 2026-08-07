# Mods

This folder contains completed **original standalone DELTARUNE mods**.

## Published projects

### Secret Boss Challenge

Folder: [`secret-boss-challenge/`](secret-boss-challenge/README.md)

- Current version: **0.4.2**
- Chapters 1, 2, and 5
- Optional secret-boss challenge behavior and enhanced rewards
- Pink Scarf unlocks a 75% TP whole-party Shield
- Pink Scarf gives approximately +25% graze area and +10% graze TP gain
- Distributed as Deltamod-native `g3mpatch` resources for improved multi-mod merging
- Includes source, package metadata, license, and validation report

### Item Giver Mode

Folder: [`item-giver-mode/`](item-giver-mode/README.md)

- Current version: **0.2.0**
- Chapters 1–5
- F7 debug-style item browser
- Items, equipment, armor, key items, and Light World items
- Runtime scan can see named item definitions from successfully merged mods
- Distributed as Deltamod-native `g3mpatch` resources
- Changes only `gml_Object_obj_time_Draw_75` in each chapter to minimize its merge footprint
- Includes source, package metadata, license, and validation report

### Item Giver + Secret Boss Challenge compatibility

Use the **two standalone mods above**. Secret Boss Challenge v0.4.2 preserves the same Chapter 5 resource set as v0.4.1, and it still has zero direct resource-name overlap with Item Giver v0.2.0. The older dedicated combined package remains retired.

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
