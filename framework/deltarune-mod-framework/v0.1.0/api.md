# DELTARUNE Mod Framework v0.1 API Contract

Framework version: `0.1.0`.

## Mod Registry

`DMF_Mod_Register(id, name, version)` returns the registered mod struct. Re-registering an existing ID updates metadata without creating a duplicate.

Returned fields: `id`, `name`, `version`, `enabled`.

## Capability Registry

`DMF_Capability_Register(mod_id, capability_id, version)` requires the owning mod to be registered.

`DMF_Capability_Has(mod_id, capability_id, minimum_version)` returns a boolean.

`DMF_Capability_GetProviders(capability_id, minimum_version)` returns an array of provider mod IDs.

## Event Bus

`DMF_Event_Subscribe(event_name, callback, priority, once)` returns a numeric listener token.

Higher priority executes first. Equal priority preserves registration order.

Callbacks receive the payload and may stop propagation by returning `true` or setting `payload.cancelled = true`.

`DMF_Event_Emit` returns `{ event_name, delivered, cancelled, listener_count }`.

The event bus dispatches from a listener snapshot so callbacks may safely unsubscribe during dispatch.
