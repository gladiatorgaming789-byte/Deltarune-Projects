/// DCE_BattleIntegration
/// Bridges custom Dojo enemies to the native enemy registry and attack controller.

function DCE_RegisterAllEnemies() {
    DCE_Init();

    if (!variable_global_exists("dm_enemy_info") || !ds_exists(global.dm_enemy_info, ds_type_map)) return false;
    if (!variable_global_exists("dm_enemy_list")) global.dm_enemy_list = [];

    for (var i = 0; i < array_length(global.dce_enemies); i++) {
        var _enemy = global.dce_enemies[i];
        if (!DCE_ValidateEnemy(_enemy)) continue;

        var _template = scr_dm_getenemyinfo(_enemy.template_enemy);
        var _chapter = _template.chapter;
        var _sprite = _template.sprite;
        var _object = _template.object;

        ds_map_set(global.dm_enemy_info, _enemy.id, {
            object: _object,
            chapter: _chapter,
            name: _enemy.name,
            sprite: _sprite,
            is_available: true
        });

        var _present = false;
        for (var j = 0; j < array_length(global.dm_enemy_list); j++) {
            if (global.dm_enemy_list[j] == _enemy.id) {
                _present = true;
                break;
            }
        }
        if (!_present) array_push(global.dm_enemy_list, _enemy.id);
    }

    return true;
}

function DCE_IsCustomEnemySlot(_slot) {
    DCE_Init();
    if (_slot < 0 || _slot >= global.monstermax) return false;
    if (!variable_global_exists("monstertype")) return false;

    var _type = global.monstertype[_slot];
    if (_type < 10000) return false;
    return !is_undefined(DCE_FindEnemy(_type));
}

function DCE_GetSlotEnemy(_slot) {
    if (!DCE_IsCustomEnemySlot(_slot)) return undefined;
    return DCE_FindEnemy(global.monstertype[_slot]);
}

function DCE_GetDefaultAttackIndex(_enemy) {
    if (is_undefined(_enemy)) return -1;
    if (array_length(_enemy.attacks) <= 0) return -1;

    var _index = 0;
    if (variable_struct_exists(_enemy, "default_attack")) {
        _index = clamp(floor(_enemy.default_attack), 0, array_length(_enemy.attacks) - 1);
    }
    return _index;
}

function DCE_GetAttackForSlot(_slot) {
    var _enemy = DCE_GetSlotEnemy(_slot);
    if (is_undefined(_enemy)) return undefined;

    var _index = DCE_GetDefaultAttackIndex(_enemy);
    if (_index < 0) return undefined;
    return _enemy.attacks[_index];
}

function DCE_CreateNativeAttackController(_slot, _native_type, _duration, _data) {
    if (!DCE_IsCustomEnemySlot(_slot)) return undefined;
    if (!instance_exists(global.monsterinstance[_slot])) return undefined;

    var _owner = global.monsterinstance[_slot];
    var _controller = instance_create(_owner.x, _owner.y, obj_dbulletcontroller);

    _controller.creator = _slot;
    _controller.creatorid = _owner;
    _controller.target = variable_instance_exists(_owner, "mytarget") ? _owner.mytarget : -1;
    _controller.damage = global.monsterat[_slot] * 5;
    _controller.type = _native_type;
    _controller.dce_proxy = false;
    _controller.dce_slot = _slot;
    _controller.dce_until = max(1, _duration);

    if (!is_undefined(_data)) {
        if (variable_struct_exists(_data, "side")) _controller.side = _data.side;
        if (variable_struct_exists(_data, "difficulty")) _controller.difficulty = _data.difficulty;
        if (variable_struct_exists(_data, "damage")) _controller.damage = _data.damage;
    }

    return _controller;
}

function DCE_StartAttackForSlot(_slot, _attack) {
    DCE_Init();
    if (is_undefined(_attack)) return false;
    if (!DCE_IsCustomEnemySlot(_slot)) return false;

    if (!variable_global_exists("dce_runtimes")) global.dce_runtimes = [];

    var _runtime = {
        active: true,
        slot: _slot,
        enemy_id: global.monstertype[_slot],
        attack_id: _attack.id,
        frame: -1,
        step_index: 0,
        controllers: [],
        finished: false
    };

    global.dce_runtimes[_slot] = _runtime;
    global.monsterattackname[_slot] = "__DCE_" + string(_runtime.enemy_id) + "_" + string(_attack.id);
    return true;
}

function DCE_CleanupRuntime(_runtime) {
    if (is_undefined(_runtime)) return;

    for (var i = array_length(_runtime.controllers) - 1; i >= 0; i--) {
        var _entry = _runtime.controllers[i];
        if (!is_undefined(_entry) && instance_exists(_entry.instance)) {
            with (_entry.instance) instance_destroy();
        }
    }
    _runtime.controllers = [];
    _runtime.active = false;
    _runtime.finished = true;
}

function DCE_NativeBulletSpawner(_x, _y, _object) {
    var __dc = instance_create(_x, _y, _object);
    __dc.creator = myself;
    __dc.creatorid = id;
    __dc.target = mytarget;
    __dc.damage = global.monsterat[myself] * 5;
    return __dc;
}

function DCE_BulletSpawnerHook(_x, _y, _object) {
    var _slot = v_ex("myself") ? myself : -1;
    if (_slot >= 0 && DCE_IsCustomEnemySlot(_slot) && _object == obj_dbulletcontroller) {
        var _attack = DCE_GetAttackForSlot(_slot);
        if (!is_undefined(_attack)) {
            DCE_StartAttackForSlot(_slot, _attack);

            var _proxy = instance_create(_x, _y, obj_dbulletcontroller);
            _proxy.creator = _slot;
            _proxy.creatorid = id;
            _proxy.target = mytarget;
            _proxy.damage = global.monsterat[_slot] * 5;
            _proxy.type = 999999;
            _proxy.dce_proxy = true;
            _proxy.dce_slot = _slot;
            _proxy.dce_until = _attack.duration + 30;

            if (!variable_global_exists("dce_runtimes")) global.dce_runtimes = [];
            var _runtime = global.dce_runtimes[_slot];
            if (!is_undefined(_runtime) && _runtime.active) {
                array_push(_runtime.controllers, {
                    instance: _proxy,
                    end_frame: _attack.duration + 30
                });
            }

            return _proxy;
        }
    }

    return DCE_NativeBulletSpawner(_x, _y, _object);
}

function DCE_BattleStep() {
    DCE_Init();
    if (!variable_global_exists("dce_runtimes")) global.dce_runtimes = [];

    for (var _slot = 0; _slot < global.monstermax; _slot++) {
        if (array_length(global.dce_runtimes) <= _slot) continue;

        var _runtime = global.dce_runtimes[_slot];
        if (is_undefined(_runtime) || !_runtime.active) continue;

        var _enemy = DCE_FindEnemy(_runtime.enemy_id);
        if (is_undefined(_enemy)) {
            DCE_CleanupRuntime(_runtime);
            continue;
        }

        var _attack = DCE_FindAttack(_enemy, _runtime.attack_id);
        if (is_undefined(_attack)) {
            DCE_CleanupRuntime(_runtime);
            continue;
        }

        _runtime.frame += 1;

        for (var c = array_length(_runtime.controllers) - 1; c >= 0; c--) {
            var _controller_entry = _runtime.controllers[c];
            if (is_undefined(_controller_entry) || !instance_exists(_controller_entry.instance)) {
                array_delete(_runtime.controllers, c, 1);
                continue;
            }
            if (_runtime.frame >= _controller_entry.end_frame) {
                with (_controller_entry.instance) instance_destroy();
                array_delete(_runtime.controllers, c, 1);
            }
        }

        while (_runtime.step_index < array_length(_attack.steps)) {
            var _step = _attack.steps[_runtime.step_index];
            if (_runtime.frame < _step.time) break;

            DCE_DispatchAttackStep(_runtime, _step);
            _runtime.step_index += 1;
        }

        if (_runtime.frame >= _attack.duration) {
            DCE_CleanupRuntime(_runtime);
            global.dce_runtimes[_slot] = undefined;
        }
    }
}

function DCE_DispatchAttackStep(_runtime, _step) {
    var _native_type = -1;
    var _duration = 30;
    var _data = _step.data;

    switch (_step.type) {
        case "wait":
            return true;

        case "aimed":
            _native_type = 0;
            _duration = 60;
            break;

        case "radial":
            _native_type = 31;
            _duration = 60;
            break;

        case "horizontal":
            _native_type = 30;
            _duration = 60;
            break;

        case "vertical":
            _native_type = 1;
            _duration = 60;
            break;

        case "burst":
            _native_type = 4;
            _duration = 45;
            break;

        case "native":
            _native_type = variable_struct_exists(_data, "native_type") ? floor(_data.native_type) : -1;
            _duration = variable_struct_exists(_data, "duration") ? max(1, floor(_data.duration)) : 60;
            break;

        case "repeat":
            return true;
    }

    if (_native_type < 0) return false;

    if (variable_struct_exists(_data, "duration")) {
        _duration = max(1, floor(_data.duration));
    }

    var _controller = DCE_CreateNativeAttackController(_runtime.slot, _native_type, _duration, _data);
    if (!is_undefined(_controller)) {
        array_push(_runtime.controllers, {
            instance: _controller,
            end_frame: _runtime.frame + _duration
        });
        return true;
    }

    return false;
}
