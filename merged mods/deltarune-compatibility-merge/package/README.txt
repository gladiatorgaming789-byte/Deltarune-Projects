DELTARUNE Compatibility Merge v1.0.7
=====================================

This Deltamod package targets the supplied Windows launcher and Chapters 1-5.

HOTFIX 1.0.7
------------
Fixed the cut-off bottom point on `spr_bhero_graze` in Chapters 2-4.

The sprite previously used a 99x90 canvas with:
- OriginY = 89;
- the visible heart point occupying the final row (row 89);
- no transparent row below the point.

That made the bottom of the graze heart look chopped off. This version expands the logical sprite
canvas and each frame's bounding height from 90 to 92 pixels while leaving the texture, OriginY,
collision margins, and draw position unchanged. The two new bottom rows are transparent, so the
sprite looks complete without moving on screen or changing collisions.

Audit notes:
- Chapter 1 Modernized and No-Hat imported frame PNGs and sprite metadata were compared against
  their donor files; they match their intended donors.
- The standard `spr_grazeappear` and `spr_grazemask` resources match the clean game.
- `spr_bhero_graze` was the graze resource whose visible pixels reached the exact canvas boundary.

Previous fixes remain included:
- Chapter 1 elevator choice cursor alignment;
- Better Saves sprites and INI-context repair;
- Chapter 1 Modernized asset relinking;
- Custom Difficulty menu sprites;
- Chapter 1 smart/random bullet target repair;
- the user-supplied Pink Fight `pink.ogg` override.

Fully merged:
- Custom Difficulty 1.8.3
- Better Saves v7
- Deltarune 60 FPS 1.1.20
- Chapter 1 Modernized 1.1.0
- No-Hat Ralsei 1.4
- New ACTs in the Knight Fight 2.7 (Normal variant)

Improved Pink Fight Background:
- Updated pink.ogg override included in the installable workspace release.
- Visual data patch excluded because it targets an incompatible legacy Chapter 5 data.win.

Validation:
- The repaired sprite exports as 99x92 with visible alpha ending at row 89, leaving two transparent
  rows beneath it.
- All packaged xdelta patches were decoded against their clean sources and compared byte-for-byte
  with the intended merged binaries.
