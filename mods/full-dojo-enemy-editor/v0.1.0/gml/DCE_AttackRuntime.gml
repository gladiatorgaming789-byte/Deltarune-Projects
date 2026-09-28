/// DCE_AttackRuntime
/// Executes the addon attack timeline. The integration layer supplies bullet
/// creation through the existing Dojo/battle bullet primitives.

function DCE_AttackStart(_enemy_id, _attack_id) {
    DCE_Init();
    var _enemy = DCE_FindEnemy(_enemy_id);
    if (is_undefined(_enemy)) return false;

    var _attack = DCE_FindAttack(_enemy, _attack_id);
    if (is_undefined(_attack)) return false;

    global.dce_runtime = {
        enemy_id: _enemy_id,
        attack_id: _attack_id,
        frame: 0,
        step_index: 0,
        finished: false
    };

    return true;
}

function DCE_AttackStep() {
    if (!variable_global_exists("dce_runtime")) return false;
    if (is_undefined(global.dce_runtime)) return false;
    if (global.dce_runtime.finished) return false;

    var _enemy = DCE_FindEnemy(global.dce_runtime.enemy_id);
    if (is_undefined(_enemy)) return false;

    var _attack = DCE_FindAttack(_enemy, global.dce_runtime.attack_id);
    if (is_undefined(_attack)) return false;

    global.dce_runtime.frame += 1;

    while (global.dce_runtime.step_index < array_length(_attack.steps)) {
        var _step = _attack.steps[global.dce_runtime.step_index];

        if (global.dce_runtime.frame < _step.time) break;

        // Integration hook. A step is intentionally data-only here.
        // The battle-specific hook consumes _step.type and _step.data.
        DCE_DispatchAttackStep(_step);

        global.dce_runtime.step_index += 1;
    }

    if (global.dce_runtime.frame >= _attack.duration) {
        global.dce_runtime.finished = true;
    }

    return !global.dce_runtime.finished;
}

function DCE_DispatchAttackStep(_step) {
    // Supported primitives are deliberately small and deterministic.
    // The Full Dojo integration maps these to its existing bullet objects.
    switch (_step.type) {
        case "wait":
        case "aimed":
        case "radial":
        case "horizontal":
        case "vertical":
        case "burst":
        case "repeat":
            return _step;
    }

    return undefined;
}
