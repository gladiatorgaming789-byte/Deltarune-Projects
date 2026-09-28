/// DCE_RegisterEnemy(enemy)
/// Makes a custom enemy visible to Full Dojo Customizer's enemy registry.

DCE_Init();

if (is_undefined(argument0)) return false;

var _enemy = argument0;
if (!DCE_ValidateEnemy(_enemy)) return false;
if (_enemy.id < 10000) return false;

var _existing = DCE_FindEnemy(_enemy.id);
if (is_undefined(_existing)) {
    array_push(global.dce_enemies, _enemy);
}

DCE_RegisterAllEnemies();
return true;
