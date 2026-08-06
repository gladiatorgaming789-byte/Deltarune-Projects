# Compatibility Fixed Mods

This folder contains existing DELTARUNE mods that have been repaired or updated for newer game builds while preserving the original mod's intended behavior.

## Published projects

_No completed compatibility-fixed mod has been archived here yet._

Compatibility work remains on the `compatibility-fixes` branch until the repaired package has been tested and is ready for release.

## What qualifies as a compatibility fix

A project belongs here when its primary purpose is to:

- Update outdated patch anchors, checksums, scripts, or resource references.
- Repair crashes, installation failures, missing assets, or broken game logic caused by a newer DELTARUNE version.
- Convert an older installation format into a Deltamod-compatible package.
- Correct a defect in an existing mod without redesigning it into a substantially different project.

Large feature additions or major redesigns should be treated as a separate mod unless the original author clearly intended them as part of the update.

## Required documentation

Each fixed mod should include:

- The original mod name, author, version, and source when known.
- A list of the problems that were found.
- A clear explanation of what was changed and what was intentionally preserved.
- The DELTARUNE version, launcher version, chapters, and file checksums used for testing.
- Deltamod installation instructions.
- Known conflicts and limitations.
- A validation or test report.
- Original license and credit information.

## Compatibility standards

- Preserve the original gameplay and presentation unless a change is required for compatibility.
- Avoid bundling original game files or copyrighted assets.
- Stop safely when the target game layout or checksum is unsupported.
- Prefer patches that can coexist with unrelated Deltamod packages.
- Document overlapping resources so a merged build can be created when necessary.

## Suggested project layout

```text
compatibility fixed mods/
└── original-mod-name/
    ├── README.md
    ├── Original_Mod_Name_Compatibility_Fix_v1.0.0_Deltamod.zip
    ├── source-or-patches/
    └── tests/
```

[Return to the archive index](../README.md)
