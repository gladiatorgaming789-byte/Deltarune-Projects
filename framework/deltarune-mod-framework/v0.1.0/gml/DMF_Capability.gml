/// @function DMF_Capability_Register(mod_id, capability_id, version)
function DMF_Capability_Register(mod_id, capability_id, version) {
    DMF_Init();
    if (!DMF_Mod_IsRegistered(mod_id)) return false;
    if (!is_string(capability_id) || string_length(capability_id) <= 0) return false;
    if (!is_string(version)) version = "0.0.0";

    for (var i = 0; i < array_length(global.dmf.capabilities); i++) {
        var cap = global.dmf.capabilities[i];
        if (cap.mod_id == mod_id && cap.capability_id == capability_id) {
            cap.version = version;
            return true;
        }
    }

    global.dmf.capabilities[array_length(global.dmf.capabilities)] = {
        mod_id: mod_id,
        capability_id: capability_id,
        version: version
    };
    return true;
}

/// @function DMF_Capability_Unregister(mod_id, capability_id)
function DMF_Capability_Unregister(mod_id, capability_id) {
    DMF_Init();
    var removed = false;
    var next = [];
    for (var i = 0; i < array_length(global.dmf.capabilities); i++) {
        var cap = global.dmf.capabilities[i];
        if (cap.mod_id == mod_id && cap.capability_id == capability_id) {
            removed = true;
        } else {
            next[array_length(next)] = cap;
        }
    }
    global.dmf.capabilities = next;
    return removed;
}

/// @function DMF_Version_AtLeast(version, minimum)
function DMF_Version_AtLeast(version, minimum) {
    var a = string_split(version, ".");
    var b = string_split(minimum, ".");
    for (var i = 0; i < 3; i++) {
        var av = (i < array_length(a)) ? real(a[i]) : 0;
        var bv = (i < array_length(b)) ? real(b[i]) : 0;
        if (av > bv) return true;
        if (av < bv) return false;
    }
    return true;
}

/// @function DMF_Capability_Has(mod_id, capability_id, minimum_version)
function DMF_Capability_Has(mod_id, capability_id, minimum_version) {
    DMF_Init();
    if (!is_string(minimum_version)) minimum_version = "0.0.0";
    for (var i = 0; i < array_length(global.dmf.capabilities); i++) {
        var cap = global.dmf.capabilities[i];
        if (cap.mod_id == mod_id && cap.capability_id == capability_id) {
            return DMF_Version_AtLeast(cap.version, minimum_version);
        }
    }
    return false;
}

/// @function DMF_Capability_GetProviders(capability_id, minimum_version)
function DMF_Capability_GetProviders(capability_id, minimum_version) {
    DMF_Init();
    if (!is_string(minimum_version)) minimum_version = "0.0.0";
    var providers = [];
    for (var i = 0; i < array_length(global.dmf.capabilities); i++) {
        var cap = global.dmf.capabilities[i];
        if (cap.capability_id == capability_id && DMF_Version_AtLeast(cap.version, minimum_version)) {
            providers[array_length(providers)] = cap.mod_id;
        }
    }
    return providers;
}
