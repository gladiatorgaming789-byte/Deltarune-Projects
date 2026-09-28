# Attack Data Schema v0.1

An attack is a struct with `id`, `name`, `duration`, and `events`.

A bullet event contains `type`, `time`, `x`, `y`, `direction`, `speed`, `damage`, `lifetime`, `sprite`, `image_angle`, and `scale`.

Coordinates are battle-camera coordinates. `time` and `duration` are frames at normal 30 FPS battle timing. Sprite names are stored as asset names instead of numeric indices.

Unknown event fields are ignored by older runtimes so the schema can grow without invalidating existing attacks.
