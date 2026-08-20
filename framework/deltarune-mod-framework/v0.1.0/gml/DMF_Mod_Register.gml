/// @function DMF_Mod_Register(id, name, version)
function DMF_Mod_Register(id, name, version) {
    DMF_Init();
    if (!is_string(id) || string_length(id) <= 0) return undefined;
    if (!is_string(name)) name = "";
    if (!is_string(version)) version = "0.0.0";

    for (var i = 0; i < array_length(global.dmf.mods); i++) {
        if (global.dmf.mods[i].id == id) {
            global.dmf.mods[i].name = name;
            global.dmf.mods[i].version = version;
            global.dmf.mods[i].enabled = true;
            return global.dmf.mods[i];
        }
    }

    var mod = {
        id: id,
        name: name,
        version: version,
        enabled: true
    };
    global.dmf.mods[array_length(global.dmf.mods)] = mod;
    return mod;
}
