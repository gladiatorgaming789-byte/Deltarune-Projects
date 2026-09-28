/// DCE_UI
/// First playable editor UI. Intended to be appended to obj_dojomanage Step/Draw.

function DCE_UI_Open() {
    DCE_Init();
    global.dce_editor_open = true;
    global.dce_editor_mode = "enemy_list";
    global.dce_selected_enemy = -1;
    global.dce_selected_attack = -1;
    global.dce_selected_step = -1;
    keyboard_string = "";
}

function DCE_UI_Close() {
    global.dce_editor_open = false;
    keyboard_string = "";
}

function DCE_UI_EnemyCount() {
    DCE_Init();
    return array_length(global.dce_enemies);
}

function DCE_UI_GetEnemy(_index) {
    if (_index < 0 || _index >= DCE_UI_EnemyCount()) return undefined;
    return global.dce_enemies[_index];
}

function DCE_UI_OpenEnemy(_index) {
    var _enemy = DCE_UI_GetEnemy(_index);
    if (is_undefined(_enemy)) return;
    global.dce_selected_enemy = _index;
    global.dce_editor_mode = "enemy";
    global.dce_selected_attack = -1;
    global.dce_selected_step = -1;
}

function DCE_UI_OpenAttack(_index) {
    var _enemy = DCE_UI_GetEnemy(global.dce_selected_enemy);
    if (is_undefined(_enemy)) return;
    if (_index < 0 || _index >= array_length(_enemy.attacks)) return;
    global.dce_selected_attack = _index;
    global.dce_editor_mode = "attack";
    global.dce_selected_step = -1;
}

function DCE_UI_NewEnemy() {
    var _name = keyboard_string;
    if (_name == "") _name = "Custom Enemy";
    var _enemy = DCE_CreateEnemy(_name, 0);
    DCE_Save();
    global.dce_selected_enemy = array_length(global.dce_enemies) - 1;
    global.dce_editor_mode = "enemy";
    keyboard_string = "";
    return _enemy;
}

function DCE_UI_NewAttack() {
    var _enemy = DCE_UI_GetEnemy(global.dce_selected_enemy);
    if (is_undefined(_enemy)) return;
    var _name = keyboard_string;
    if (_name == "") _name = "Attack " + string(array_length(_enemy.attacks) + 1);
    var _attack = DCE_EditorNewAttack(_enemy, _name);
    DCE_Save();
    global.dce_selected_attack = array_length(_enemy.attacks) - 1;
    global.dce_editor_mode = "attack";
    keyboard_string = "";
    return _attack;
}

function DCE_UI_AddStep(_type) {
    var _enemy = DCE_UI_GetEnemy(global.dce_selected_enemy);
    if (is_undefined(_enemy)) return;
    if (global.dce_selected_attack < 0) return;

    var _attack = _enemy.attacks[global.dce_selected_attack];
    var _time = 0;

    if (array_length(_attack.steps) > 0) {
        _time = _attack.steps[array_length(_attack.steps) - 1].time + 15;
    }

    DCE_EditorAddStep(_attack, _type, _time);
    _attack.duration = max(_attack.duration, _time + 30);
    DCE_Save();
}

function DCE_UI_HandleStep() {
    if (!global.dce_editor_open) return;

    if (keyboard_check_pressed(vk_escape)) {
        if (global.dce_editor_mode == "enemy_list") {
            DCE_UI_Close();
        } else if (global.dce_editor_mode == "enemy") {
            global.dce_editor_mode = "enemy_list";
            global.dce_selected_enemy = -1;
        } else if (global.dce_editor_mode == "attack") {
            global.dce_editor_mode = "enemy";
            global.dce_selected_attack = -1;
        }
        keyboard_string = "";
        return;
    }

    if (keyboard_check_pressed(vk_f8)) {
        DCE_UI_Close();
        return;
    }

    switch (global.dce_editor_mode) {
        case "enemy_list":
            if (keyboard_check_pressed(ord("N"))) {
                keyboard_string = "";
                global.dce_editor_mode = "new_enemy";
            }
            break;

        case "new_enemy":
            if (keyboard_check_pressed(vk_enter)) DCE_UI_NewEnemy();
            break;

        case "enemy":
            if (keyboard_check_pressed(ord("N"))) {
                keyboard_string = "";
                global.dce_editor_mode = "new_attack";
            }
            break;

        case "new_attack":
            if (keyboard_check_pressed(vk_enter)) DCE_UI_NewAttack();
            break;

        case "attack":
            if (keyboard_check_pressed(ord("1"))) DCE_UI_AddStep("wait");
            if (keyboard_check_pressed(ord("2"))) DCE_UI_AddStep("aimed");
            if (keyboard_check_pressed(ord("3"))) DCE_UI_AddStep("radial");
            if (keyboard_check_pressed(ord("4"))) DCE_UI_AddStep("horizontal");
            if (keyboard_check_pressed(ord("5"))) DCE_UI_AddStep("vertical");
            if (keyboard_check_pressed(ord("6"))) DCE_UI_AddStep("burst");
            if (keyboard_check_pressed(ord("7"))) DCE_UI_AddStep("repeat");
            break;
    }
}

function DCE_Save() {
    DCE_Init();
    var _path = "dojo_manager/dce_enemies.json";
    var _file = file_text_open_write(_path);
    if (_file < 0) return false;

    file_text_write_string(_file, DCE_Encode());
    file_text_close(_file);
    return true;
}

function DCE_Load() {
    DCE_Init();
    var _path = "dojo_manager/dce_enemies.json";
    if (!file_exists(_path)) return false;

    var _file = file_text_open_read(_path);
    if (_file < 0) return false;

    var _text = file_text_read_string(_file);
    file_text_close(_file);
    return DCE_Decode(_text);
}

function DCE_UI_Draw() {
    if (!global.dce_editor_open) return;

    var _w = display_get_gui_width();
    var _h = display_get_gui_height();

    draw_set_alpha(0.94);
    draw_set_color(c_black);
    draw_rectangle(20, 20, _w - 20, _h - 20, false);
    draw_set_alpha(1);

    draw_set_color(c_white);
    draw_set_font(fnt_main);

    draw_text(40, 36, "FULL DOJO — CUSTOM ENEMY EDITOR");
    draw_text(40, 62, "F8 Close    ESC Back");

    var _y = 105;

    switch (global.dce_editor_mode) {
        case "enemy_list":
            draw_text(40, _y, "CUSTOM ENEMIES");
            draw_text(40, _y + 28, "N  New Enemy");

            for (var i = 0; i < array_length(global.dce_enemies); i++) {
                var _e = global.dce_enemies[i];
                draw_text(70, _y + 65 + i * 24,
                    string(i + 1) + ". " + _e.name + "  [" + string(_e.id) + "]");
            }
            break;

        case "new_enemy":
            draw_text(40, _y, "NEW ENEMY");
            draw_text(40, _y + 35, "Type a name, then press ENTER.");
            draw_text(40, _y + 70, keyboard_string);
            break;

        case "enemy":
            var _enemy = DCE_UI_GetEnemy(global.dce_selected_enemy);
            if (is_undefined(_enemy)) {
                global.dce_editor_mode = "enemy_list";
                break;
            }

            draw_text(40, _y, _enemy.name);
            draw_text(40, _y + 28, "ID: " + string(_enemy.id));
            draw_text(40, _y + 52, "Template: " + string(_enemy.template_enemy));
            draw_text(40, _y + 76, "HP " + string(_enemy.hp) + "   AT " + string(_enemy.at) + "   DF " + string(_enemy.df));
            draw_text(40, _y + 112, "N  New Attack");

            for (var a = 0; a < array_length(_enemy.attacks); a++) {
                draw_text(70, _y + 150 + a * 24,
                    string(a + 1) + ". " + _enemy.attacks[a].name +
                    " (" + string(array_length(_enemy.attacks[a].steps)) + " steps)");
            }
            break;

        case "new_attack":
            draw_text(40, _y, "NEW ATTACK");
            draw_text(40, _y + 35, "Type an attack name, then press ENTER.");
            draw_text(40, _y + 70, keyboard_string);
            break;

        case "attack":
            var _e2 = DCE_UI_GetEnemy(global.dce_selected_enemy);
            if (is_undefined(_e2)) {
                global.dce_editor_mode = "enemy_list";
                break;
            }

            var _a2 = _e2.attacks[global.dce_selected_attack];
            draw_text(40, _y, _a2.name);
            draw_text(40, _y + 30, "Duration: " + string(_a2.duration) + " frames");
            draw_text(40, _y + 60, "1 Wait  2 Aimed  3 Radial  4 Horizontal  5 Vertical  6 Burst  7 Repeat");

            for (var s = 0; s < array_length(_a2.steps); s++) {
                var _st = _a2.steps[s];
                draw_text(60, _y + 100 + s * 24,
                    string(s + 1) + ". @" + string(_st.time) + "  " + string(_st.type));
            }
            break;
    }

    draw_set_alpha(1);
}
