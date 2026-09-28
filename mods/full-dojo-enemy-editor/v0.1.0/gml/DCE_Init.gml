/// DCE_Init()
/// Initializes the Full Dojo Enemy Editor runtime.

if (!variable_global_exists("dce_initialized")) {
    global.dce_initialized = false;
}

if (!global.dce_initialized) {
    global.dce_initialized = true;

    if (!variable_global_exists("dce_enemies")) {
        global.dce_enemies = [];
    }

    if (!variable_global_exists("dce_next_enemy_id")) {
        global.dce_next_enemy_id = 10000;
    }

    if (!variable_global_exists("dce_editor_open")) {
        global.dce_editor_open = false;
    }

    if (!variable_global_exists("dce_editor_mode")) {
        global.dce_editor_mode = "enemy_list";
    }

    if (!variable_global_exists("dce_selected_enemy")) {
        global.dce_selected_enemy = -1;
    }

    if (!variable_global_exists("dce_selected_attack")) {
        global.dce_selected_attack = -1;
    }

    if (!variable_global_exists("dce_selected_step")) {
        global.dce_selected_step = -1;
    }
}
