/// @function DMF_Mod_IsRegistered(id)
function DMF_Mod_IsRegistered(id) {
    DMF_Init();
    for (var i = 0; i < array_length(global.dmf.mods); i++) {
        if (global.dmf.mods[i].id == id) return true;
    }
    return false;
}

/// @function DMF_Mod_Get(id)
function DMF_Mod_Get(id) {
    DMF_Init();
    for (var i = 0; i < array_length(global.dmf.mods); i++) {
        if (global.dmf.mods[i].id == id) return global.dmf.mods[i];
    }
    return undefined;
}

/// @function DMF_Mod_Unregister(id)
function DMF_Mod_Unregister(id) {
    DMF_Init();
    var removed = false;
    var next = [];
    for (var i = 0; i < array_length(global.dmf.mods); i++) {
        if (global.dmf.mods[i].id == id) {
            removed = true;
        } else {
            next[array_length(next)] = global.dmf.mods[i];
        }
    }
    global.dmf.mods = next;

    if (removed) {
        var caps = [];
        for (var c = 0; c < array_length(global.dmf.capabilities); c++) {
            if (global.dmf.capabilities[c].mod_id != id) {
                caps[array_length(caps)] = global.dmf.capabilities[c];
            }
        }
        global.dmf.capabilities = caps;
    }
    return removed;
}
