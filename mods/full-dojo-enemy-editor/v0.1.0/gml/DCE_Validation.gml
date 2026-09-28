/// DCE_Validation
/// Rejects malformed data before it reaches the battle system.

function DCE_ValidateEnemy(_enemy) {
    if (!is_struct(_enemy)) return false;
    if (!variable_struct_exists(_enemy, "id")) return false;
    if (_enemy.id < 10000) return false;
    if (!variable_struct_exists(_enemy, "name")) return false;
    if (!variable_struct_exists(_enemy, "template_enemy")) return false;
    if (!variable_struct_exists(_enemy, "attacks")) return false;

    for (var i = 0; i < array_length(_enemy.attacks); i++) {
        if (!DCE_ValidateAttack(_enemy.attacks[i])) return false;
    }

    return true;
}

function DCE_ValidateAttack(_attack) {
    if (!is_struct(_attack)) return false;
    if (!variable_struct_exists(_attack, "id")) return false;
    if (!variable_struct_exists(_attack, "duration")) return false;
    if (!variable_struct_exists(_attack, "steps")) return false;
    if (_attack.duration < 1) return false;

    var _last = -1;
    for (var i = 0; i < array_length(_attack.steps); i++) {
        var _step = _attack.steps[i];
        if (!is_struct(_step)) return false;
        if (!variable_struct_exists(_step, "type")) return false;
        if (!variable_struct_exists(_step, "time")) return false;
        if (_step.time < _last) return false;
        _last = _step.time;
    }

    return true;
}
