# Item Giver Mode

**Version:** 0.1.4  
**Target:** DELTARUNE Windows full release, launcher version `v23`  
**Chapters:** 1–5  
**Installer:** Deltamod-compatible UTMT `.csx` patches

Item Giver Mode adds a standalone debug-style inventory browser. Press **F7** outside battle to browse and grant named items defined by the current chapter.

## Version 0.1.4

- Fixed DELTARUNE's own inventory/menu text drawing over the Item Giver overlay.
- Moved the renderer from the normal Draw GUI pass to **Draw GUI End** on the native persistent `obj_time` controller.
- Added a full-screen dim layer and opaque centered panel.
- The panel now uses `display_get_gui_width()` and `display_get_gui_height()` instead of fixed screen placement.
- Item names replace `#` line-break markers with spaces and long list names are shortened.
- Preview, status, and toast text use wrapped drawing so they stay inside the interface.
- F7 remains the open/close hotkey.

## Categories

- Items
- Weapons
- Armor
- Key Items
- Light World items

## Controls

| Key | Action |
|---|---|
| F7 | Open or close |
| Left / Right | Change category |
| Up / Down | Move one entry |
| Page Up / Page Down | Jump ten entries |
| Home / End | First or last entry |
| Z / Enter | Give selected entry |
| R | Refresh item definitions |
| X / Escape | Close |

## Save warning

Some key items and unused/developer items depend on plot flags or scripted acquisition sequences. Giving an item does not automatically set every story flag associated with earning it. Back up the save before experimenting with progression-sensitive entries.

## Compatibility

- Deltamod CSX patches for Chapters 1–5.
- Debug Mode v4.01 compiles successfully before and after Item Giver Mode in all five chapters.
- **Secret Boss Challenge:** do not rely on enabling the standalone Item Giver and standalone Secret Boss Challenge packages together. Deltamod's cross-mod merge path does not reproduce the sequential UTMT test used during early compatibility checks. Use the dedicated [`Item Giver + Secret Boss Challenge`](../../merged%20mods/item-giver-secret-boss-challenge/README.md) merged package instead.
- Applying Item Giver Mode v0.1.4 twice produces byte-identical output in all five chapters.

## Installation

Install `Item_Giver_Mode_v0.1.4_Deltamod.zip` directly through Deltamod when using Item Giver by itself.

If Secret Boss Challenge is also wanted, install the dedicated merged package instead and disable/remove both standalone copies.

The GitHub project mirrors the tested package source under [`release/`](release/). Run `build_release.py` there to generate the five chapter scripts, then ZIP the release-folder contents so `meta.json` and `modding.xml` are at the archive root.

Package ID: `github.itemgivermode.gladiatorgaming`

Release SHA-256: `08828b77f881be8ba5093e73c56893f8161f6813fe96bd699eed9697c5bf0f30`

See [`tests/TEST_REPORT.md`](tests/TEST_REPORT.md) for validation details.
