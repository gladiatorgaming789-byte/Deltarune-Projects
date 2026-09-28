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
function fdcx_editor_new_enemy(){fdcx_init();var _enemy=fdcx_enemy_create("Custom Enemy "+string(array_length(global.fdcx_custom_enemies)+1));array_push(global.fdcx_custom_enemies,_enemy);global.fdcx_selected_enemy=array_length(global.fdcx_custom_enemies)-1;global.fdcx_selected_attack=-1;fdcx_sync_save_data();return _enemy;}
function fdcx_editor_selected_enemy(){if(global.fdcx_selected_enemy>=0&&global.fdcx_selected_enemy<array_length(global.fdcx_custom_enemies))return global.fdcx_custom_enemies[global.fdcx_selected_enemy];return undefined;}
function fdcx_editor_new_attack_for_selected_enemy(){var _enemy=fdcx_editor_selected_enemy();if(!is_struct(_enemy))return undefined;var _attack=fdcx_editor_new_attack();fdcx_enemy_add_attack(_enemy,_attack);fdcx_sync_save_data();return _attack;}
function fdcx_editor_select_attack(arg0){global.fdcx_selected_attack=arg0;global.fdcx_selected_event=-1;}
function fdcx_editor_edit_selected_event(){if(global.fdcx_selected_attack<0)return;var _a=global.fdcx_custom_attacks[global.fdcx_selected_attack];if(!is_struct(_a)||global.fdcx_selected_event<0||global.fdcx_selected_event>=array_length(_a.events))return;var _e=_a.events[global.fdcx_selected_event];var _v=get_string("Event time (frames)",string(_e.time));if(_v!=-1)_e.time=max(0,real(_v));_v=get_string("Speed",string(_e.speed));if(_v!=-1)_e.speed=real(_v);_v=get_string("Damage",string(_e.damage));if(_v!=-1)_e.damage=max(0,real(_v));fdcx_sync_save_data();}
