/// DCE_SaveLoad helpers
/// Uses the Full Dojo Customizer save mechanism when available, while keeping
/// addon data under its own namespace.

function DCE_Encode() {
    DCE_Init();
    return json_stringify({
        version: 1,
        next_enemy_id: global.dce_next_enemy_id,
        enemies: global.dce_enemies
    });
}

function DCE_Decode(_text) {
    DCE_Init();

    if (is_undefined(_text) || _text == "") return false;

    var _data = json_parse(_text);
    if (!is_struct(_data)) return false;

    if (variable_struct_exists(_data, "enemies")) {
        global.dce_enemies = _data.enemies;
    }

    if (variable_struct_exists(_data, "next_enemy_id")) {
        global.dce_next_enemy_id = max(10000, _data.next_enemy_id);
    }

    return true;
}
