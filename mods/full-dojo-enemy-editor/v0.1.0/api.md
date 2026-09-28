# Full Dojo Enemy Editor API

## Enemy

`DCE_CreateEnemy(name, template_enemy)`

Creates an addon enemy. IDs start at 10000.

Fields:

- `id`
- `name`
- `description`
- `template_enemy`
- `hp`
- `at`
- `df`
- `sprite`
- `attacks`

## Attacks

`DCE_CreateAttack(name)`

Creates a timeline.

`DCE_AddBuiltinStep(attack, type, time)`

Built-in types:

- `wait`
- `aimed`
- `radial`
- `horizontal`
- `vertical`
- `burst`
- `repeat`

`DCE_EditorDeleteStep`, `DCE_EditorMoveStep`, and `DCE_EditorSetAttackDuration` provide editor operations.

## Runtime

`DCE_AttackStart(enemy_id, attack_id)`

Starts a timeline.

`DCE_AttackStep()`

Advances the timeline one frame.

The battle integration layer is responsible for translating a validated step into Full Dojo Customizer's existing bullet system.
