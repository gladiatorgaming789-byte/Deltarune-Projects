function fdcx_editor_new_attack()
{
    var _attack = fdcx_attack_create("Untitled Attack");
    array_push(global.fdcx_custom_attacks, _attack);
    global.fdcx_selected_attack = array_length(global.fdcx_custom_attacks) - 1;
    global.fdcx_selected_event = -1;
    return _attack;
}

function fdcx_editor_add_bullet(arg0, arg1, arg2 = 0)
{
    fdcx_init();
    if (global.fdcx_selected_attack < 0 || global.fdcx_selected_attack >= array_length(global.fdcx_custom_attacks))
    {
        return false;
    }
    var _attack = global.fdcx_custom_attacks[global.fdcx_selected_attack];
    var _event = fdcx_attack_event_bullet(arg2, arg0, arg1);
    fdcx_attack_add_event(_attack, _event);
    global.fdcx_selected_event = array_length(_attack.events) - 1;
    return true;
}

function fdcx_editor_delete_selected_event()
{
    if (global.fdcx_selected_attack < 0)
    {
        return false;
    }
    var _attack = global.fdcx_custom_attacks[global.fdcx_selected_attack];
    if (!is_struct(_attack) || global.fdcx_selected_event < 0 || global.fdcx_selected_event >= array_length(_attack.events))
    {
        return false;
    }
    array_delete(_attack.events, global.fdcx_selected_event, 1);
    global.fdcx_selected_event = -1;
    return true;
}

function fdcx_editor_duplicate_selected_event()
{
    if (global.fdcx_selected_attack < 0)
    {
        return false;
    }
    var _attack = global.fdcx_custom_attacks[global.fdcx_selected_attack];
    if (!is_struct(_attack) || global.fdcx_selected_event < 0 || global.fdcx_selected_event >= array_length(_attack.events))
    {
        return false;
    }
    var _copy = struct_copy(_attack.events[global.fdcx_selected_event]);
    _copy.time += 15;
    array_insert(_attack.events, global.fdcx_selected_event + 1, _copy);
    global.fdcx_selected_event++;
    return true;
}