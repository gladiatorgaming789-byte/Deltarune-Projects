# Full Dojo Custom Enemy Creator

Addon for Full Dojo Customizer v0.14.

## Goal

Adds a data-driven custom enemy creator with a combined visual attack canvas and timeline editor.

The editor extends Full Dojo Customizer rather than replacing its battle system:
- Custom enemies use Full Dojo Customizer's enemy registry.
- Custom definitions live in the existing Dojo JSON save.
- Custom attacks are data-driven.
- Runtime reuses DELTARUNE's existing battle, heart, and bullet objects.
- No story progression changes.
- No sounds added.
- Deltamod compatibility is a release requirement.

## Editor model

The attack editor has three synchronized views:
1. Battle canvas: click to place bullet events and drag selected events.
2. Timeline: precise event selection, movement, duplication, and deletion.
3. Properties: exact time, position, direction, speed, damage, lifetime, and sprite editing.

The serialized attack event is the canonical representation. Canvas and timeline are views over the same data.

## Initial attack event

v0.1 starts with a generic `bullet` event using `obj_regularbullet`. More event types can be added without changing the saved format: burst, aimed, spiral, laser, wall, and wait.

## Compatibility

Target: Full Dojo Customizer v0.14 / DELTARUNE Chapter 1-5 dataPATCHED base.
The addon installs on top of the Full Dojo Customizer build, not the clean game.
