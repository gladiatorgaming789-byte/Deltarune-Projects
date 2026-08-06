# Item Giver Mode v0.1.4 — Test Report

## User-reported issue

The v0.1.3 menu opened, but DELTARUNE's own inventory/menu text could render over the Item Giver and stale-looking text remained around the lower/left areas of the screen.

## Root cause

v0.1.3 appended Item Giver to `obj_time`'s normal Draw GUI event (subtype 64). Other GUI objects can render later in the frame, so their text appeared on top of the Item Giver. The renderer also assumed a fixed 640×480-style layout and drew raw item names that can contain `#` line-break markers.

## Fix

v0.1.4:

- Moves Item Giver rendering to `obj_time` Draw GUI End (subtype 75).
- Draws a full-screen dim layer before the centered panel.
- Uses `display_get_gui_width()` / `display_get_gui_height()` for placement.
- Sanitizes `#` markers from list names.
- Truncates long list/preview names.
- Uses `draw_text_ext` for wrapped description, status, and toast text.

## Compilation

All five clean chapter files compiled and wrote successfully.

Rebuilt SHA-256 values:

- Chapter 1: `cfe9494854ef30ff759a877972d389a5803c8836221f1a5a2d3b22da77f77be0`
- Chapter 2: `2d99dd288df1495bb04c9a726b359401b3dc3b71412b30b57f30511643683f49`
- Chapter 3: `b6c1297c9622d2e18ce9ee335730c7c8cc568f6aaa4991608f36d6799dc64b3b`
- Chapter 4: `6566ff0357bd01ccc9ce790430fc739694a632b3c95cc8fe1825d4c93bd82167`
- Chapter 5: `2a4df04a91702660feb33e80a37466f63ace6fefe4a8ff9b8ab58ee864650fb4`

Chapter 1 round-trip decompilation confirms the Item Giver renderer is present in `gml_Object_obj_time_Draw_75`.

## Idempotency

Applying the matching v0.1.4 patch a second time produced byte-identical output in Chapters 1–5.

## Debug Mode v4.01 compatibility

Both patch orders compiled successfully across Chapters 1–5:

1. Debug Mode → Item Giver Mode
2. Item Giver Mode → Debug Mode

## Secret Boss Challenge v0.4.0 compatibility

Both patch orders compiled successfully in Chapter 5.

## Package validation

- `meta.json` parses successfully and reports version `0.1.4`.
- Package ID remains `github.itemgivermode.gladiatorgaming`.
- `modding.xml` still routes all five chapters through Deltamod CSX patches.
- ZIP integrity test passed.
- Every chapter script extracted from the final ZIP reproduced the tested chapter output byte-for-byte.
- ZIP SHA-256: `08828b77f881be8ba5093e73c56893f8161f6813fe96bd699eed9697c5bf0f30`.

## Manual verification still needed

The exact visual overlap was reported from a real Windows gameplay session. The renderer architecture causing it has been changed and compile/round-trip checks pass, but the final visual result still needs confirmation in the user's normal Deltamod/Windows setup.
