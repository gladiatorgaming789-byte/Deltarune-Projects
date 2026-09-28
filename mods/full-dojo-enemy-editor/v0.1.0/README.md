# Full Dojo Enemy Editor v0.1.0

Addon for Full Dojo Customizer v0.14.

## Goal

Adds an in-game editor for creating custom Dojo enemies and authoring their bullet attacks.

The addon is designed around Full Dojo Customizer's existing systems instead of replacing its battle engine.

## Planned v0.1.0 workflow

1. Open the Full Dojo Customizer enemy editor.
2. Choose **New Custom Enemy**.
3. Set:
   - display name
   - base enemy/template
   - HP / AT / DF
   - sprite/portrait inherited from the template
4. Open **Attack Editor**.
5. Create an attack timeline.
6. Add attack steps:
   - wait
   - aimed bullet
   - radial bullets
   - horizontal/vertical wave
   - burst
   - repeat
   - clear
7. Preview the attack in the existing bullet-test environment.
8. Save the enemy.
9. The enemy becomes selectable by the existing Dojo battle editor.

## Compatibility goals

- Full Dojo Customizer v0.14 is a required dependency.
- Deltamod-compatible patch structure.
- No replacement of the original Dojo data files.
- No new sound resources.
- Custom data is stored separately from vanilla Dojo battle data.
- Existing Full Dojo Customizer enemies remain untouched.
- Custom enemy IDs use a reserved range beginning at 10000.

## Important implementation detail

A custom enemy is represented as data plus a vanilla enemy template. The template supplies the normal enemy battle object and presentation, while the addon supplies the custom stats and attack program.

This avoids generating a new GameMaker enemy object for every user-created enemy.

## Current status

v0.1.0 is the implementation foundation. The data model and attack-program API are being built first; final UTMT patch/runtime validation is still required before this is considered a release.

