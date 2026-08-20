/// @function DMF_Init()
/// @description Lazily initializes the DELTARUNE Mod Framework runtime state.
function DMF_Init() {
    if (!variable_global_exists("dmf")) {
        global.dmf = {
            framework_version: "0.1.0",
            next_token: 1,
            mods: [],
            capabilities: [],
            events: {}
        };
    }
    return global.dmf;
}
