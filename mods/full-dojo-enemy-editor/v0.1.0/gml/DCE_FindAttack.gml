/// DCE_FindAttack(enemy, attack_id)

if (is_undefined(argument0)) {
    return undefined;
}

var _enemy = argument0;

for (var i = 0; i < array_length(_enemy.attacks); i++) {
    if (_enemy.attacks[i].id == argument1) {
        return _enemy.attacks[i];
    }
}

return undefined;
