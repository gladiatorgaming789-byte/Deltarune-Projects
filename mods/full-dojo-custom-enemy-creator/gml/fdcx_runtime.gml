function fdcx_runtime_start(arg0, arg1 = 0)
{
    fdcx_init();
    var _enemy = is_string(arg0) ? fdcx_enemy_find(arg0) : arg0;
    if (!is_struct(_enemy) || !variable_struct_exists(_enemy, "attacks"))
    {
        return false;
    }
    if (array_length(_enemy.attacks) <= 0)
    {
        return false;
    }
    var _attack_id = _enemy.attacks[clamp(arg1, 0, array_length(_enemy.attacks) - 1)];
    var _attack = fdcx_attack_find(_attack_id);
    if (!is_struct(_attack))
    {
        return false;
    }
    global.fdcx_runtime_attack = _attack;
    global.fdcx_runtime_frame = 0;
    global.fdcx_runtime_event_index = 0;
    return true;
}

function fdcx_runtime_stop()
{
    global.fdcx_runtime_attack = undefined;
    global.fdcx_runtime_frame = 0;
    global.fdcx_runtime_event_index = 0;
}

function fdcx_runtime_step()
{
    fdcx_init();
    if (!is_struct(global.fdcx_runtime_attack))
    {
        return;
    }
    var _attack = global.fdcx_runtime_attack;
    var _events = variable_struct_exists(_attack, "events") ? _attack.events : [];
    while (global.fdcx_runtime_event_index < array_length(_events))
    {
        var _event = _events[global.fdcx_runtime_event_index];
        if (!is_struct(_event))
        {
            global.fdcx_runtime_event_index++;
            continue;
        }
        var _time = variable_struct_exists(_event, "time") ? _event.time : 0;
        if (_time > global.fdcx_runtime_frame)
        {
            break;
        }
        fdcx_runtime_spawn_event(_event);
        global.fdcx_runtime_event_index++;
    }
    global.fdcx_runtime_frame++;
    var _duration = variable_struct_exists(_attack, "duration") ? _attack.duration : 180;
    if (global.fdcx_runtime_frame > _duration)
    {
        fdcx_runtime_stop();
    }
}

function fdcx_runtime_spawn_event(arg0)
{
    if (!is_struct(arg0) || !variable_struct_exists(arg0, "type"))
    {
        return;
    }
    if (arg0.type != "bullet")
    {
        return;
    }
    var _x = variable_struct_exists(arg0, "x") ? arg0.x : camerax() + 320;
    var _y = variable_struct_exists(arg0, "y") ? arg0.y : cameray() + 170;
    var _bullet = instance_create(_x, _y, obj_regularbullet);
    _bullet.damage = variable_struct_exists(arg0, "damage") ? arg0.damage : 10;
    _bullet.speed = variable_struct_exists(arg0, "speed") ? arg0.speed : 4;
    _bullet.direction = variable_struct_exists(arg0, "direction") ? arg0.direction : 90;
    _bullet.image_angle = variable_struct_exists(arg0, "image_angle") ? arg0.image_angle : _bullet.direction;
    _bullet.target = 0;
    if (variable_struct_exists(arg0, "sprite"))
    {
        var _sprite_name = arg0.sprite;
        var _sprite_asset = asset_get_index(_sprite_name);
        if (_sprite_asset >= 0)
        {
            _bullet.sprite_index = _sprite_asset;
        }
    }
    if (variable_struct_exists(arg0, "scale"))
    {
        _bullet.image_xscale = arg0.scale;
        _bullet.image_yscale = arg0.scale;
    }
}