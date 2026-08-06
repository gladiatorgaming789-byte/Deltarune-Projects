# Mods

This folder contains completed **original standalone DELTARUNE mods**.

## Published projects

### Secret Boss Challenge

Folder: [`secret-boss-challenge/`](secret-boss-challenge/README.md)

- Latest archived release: `Secret_Boss_Challenge_v0.4.0_Deltamod.zip`
- Includes earlier release archives, license, project documentation, and a validation report.
- Adds optional secret-boss challenge behavior and enhanced rewards across Chapters 1, 2, and 5.
- Packaged as Deltamod-compatible CSX patches.

### Item Giver Mode

Folder: [`item-giver-mode/`](item-giver-mode/README.md)

- Latest tested release: `Item_Giver_Mode_v0.1.1_Deltamod.zip`; reproducible package source is mirrored in [`release/`](item-giver-mode/release/).
- Adds an F7 debug-style item browser to Chapters 1–5.
- Supports consumable items, weapons, armor, key items, and Light World items.
- Reads the final item tables at runtime, allowing named items from compatible mods to appear.
- Includes source CSX patches, metadata, license, and a validation report.

## Project layout

```text
mods/
├── secret-boss-challenge/
│   ├── README.md
│   ├── LICENSE
│   ├── Secret_Boss_Challenge_v0.4.0_Deltamod.zip
│   └── tests/
└── item-giver-mode/
    ├── README.md
    ├── LICENSE
    ├── release/
    └── tests/
```

Merged builds do not belong in this folder. They must be placed under [`merged mods/`](../merged%20mods/README.md).

## Archive requirements

Published projects must:

- Be complete and documented.
- Use Deltamod for installable releases.
- Avoid original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.
- Include compatibility and validation information.
- Preserve authorship and licensing details.

[Return to the archive index](../README.md)
