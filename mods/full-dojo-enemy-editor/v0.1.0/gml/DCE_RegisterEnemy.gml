/// DCE_RegisterEnemy(enemy)
/// Makes a custom enemy visible to Full Dojo Customizer's enemy registry.

DCE_Init();

if (is_undefined(argument0)) {
    return false;
}

var _enemy = argument0;

// Keep custom IDs out of the vanilla enemy range.
if (_enemy.id < 10000) {
    return false;
}

var _existing = DCE_FindEnemy(_enemy.id);
if (is_undefined(_existing)) {
    array_push(global.dce_enemies, _enemy);
}

// The actual dm_enemy_info registration is performed by the integration hook,
// because the exact structure is owned by Full Dojo Customizer v0.14.

return true;
