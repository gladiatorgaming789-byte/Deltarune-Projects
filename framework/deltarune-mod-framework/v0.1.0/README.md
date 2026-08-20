# DELTARUNE Mod Framework v0.1.0

Framework v0.1.0 is the first concrete runtime milestone for the shared DELTARUNE mod API.

## Included APIs

- Mod Registry
- Capability Registry
- Event Bus

The implementation is self-contained and uses GameMaker 2.3-compatible GML primitives. It does not add rooms, sprites, sounds, fonts, or other binary assets.

## API surface

### Mod Registry

- `DMF_Mod_Register(id, name, version)`
- `DMF_Mod_IsRegistered(id)`
- `DMF_Mod_Get(id)`
- `DMF_Mod_Unregister(id)`

### Capability Registry

- `DMF_Capability_Register(mod_id, capability_id, version)`
- `DMF_Capability_Has(mod_id, capability_id, minimum_version)`
- `DMF_Capability_GetProviders(capability_id, minimum_version)`
- `DMF_Capability_Unregister(mod_id, capability_id)`

### Event Bus

- `DMF_Event_Subscribe(event_name, callback, priority, once)`
- `DMF_Event_Unsubscribe(token)`
- `DMF_Event_Emit(event_name, payload)`
- `DMF_Event_Clear(event_name)`

Events are dispatched from highest priority to lowest priority. A callback may cancel propagation by returning `true` or by setting `payload.cancelled = true`. One-shot listeners are removed after dispatch.

## Design constraints

- Lazy initialization: no startup object or room modification is required.
- Stable global namespace: all public functions use the `DMF_` prefix.
- No sound resources or graphical assets are required.
- The framework does not alter normal story progression.
- The framework is intended to be injected as global script CodeEntries by UTMT/G3M tooling.

## Integration example

```gml
DMF_Mod_Register("github.customtrainingplaylist.gladiatorgaming", "Custom Training Playlist", "0.2.1");
DMF_Capability_Register("github.customtrainingplaylist.gladiatorgaming", "training_session", "1.0");

var token = DMF_Event_Subscribe("battle.phase_changed", function(event) {
    if (event.new_phase == "enemy") {
        // trainer logic
    }
}, 100, false);
```

This milestone intentionally does not migrate Custom Training Playlist itself. The trainer remains the stable reference consumer until the battle-state milestone is ready.
