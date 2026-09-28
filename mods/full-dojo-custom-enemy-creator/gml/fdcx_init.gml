function fdcx_init()
{
    if (variable_global_exists("fdcx_initialized") && global.fdcx_initialized)
    {
        return;
    }
    global.fdcx_initialized = true;
    global.fdcx_version = "0.1.0-dev";
    global.fdcx_custom_enemies = [];
    global.fdcx_custom_attacks = [];
    global.fdcx_editor_mode = false;
    global.fdcx_selected_enemy = -1;
    global.fdcx_selected_attack = -1;
    global.fdcx_selected_event = -1;
    global.fdcx_runtime_attack = undefined;
    global.fdcx_runtime_event_index = 0;
    global.fdcx_runtime_frame = 0;
}