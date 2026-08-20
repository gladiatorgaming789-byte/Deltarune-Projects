/// @function DMF_SelfTest()
/// @description Runtime self-test for Framework v0.1. Returns a report struct.
function DMF_SelfTest() {
    DMF_Init();

    var report = {
        passed: true,
        failures: []
    };
    var fail = function(message) {
        report.passed = false;
        report.failures[array_length(report.failures)] = message;
    };

    var mod = DMF_Mod_Register("dmf.test", "Framework Self Test", "1.0.0");
    if (!is_struct(mod) || !DMF_Mod_IsRegistered("dmf.test")) fail("Mod registration/query failed");

    if (!DMF_Capability_Register("dmf.test", "self_test", "1.2.0")) fail("Capability registration failed");
    if (!DMF_Capability_Has("dmf.test", "self_test", "1.1.0")) fail("Capability version check failed");
    if (DMF_Capability_Has("dmf.test", "self_test", "1.3.0")) fail("Capability rejected minimum version incorrectly");

    var providers = DMF_Capability_GetProviders("self_test", "1.0.0");
    if (array_length(providers) != 1 || providers[0] != "dmf.test") fail("Capability provider discovery failed");

    var order = [];
    var high = function(payload) {
        order[array_length(order)] = "high";
    };
    var low = function(payload) {
        order[array_length(order)] = "low";
    };
    var once = function(payload) {
        order[array_length(order)] = "once";
    };

    DMF_Event_Clear("dmf.test.event");
    DMF_Event_Subscribe("dmf.test.event", low, 0, false);
    DMF_Event_Subscribe("dmf.test.event", high, 100, false);
    DMF_Event_Subscribe("dmf.test.event", once, 50, true);

    var first = DMF_Event_Emit("dmf.test.event", {});
    if (first.delivered != 3 || first.cancelled) fail("Event dispatch count/cancellation failed");
    if (array_length(order) != 3 || order[0] != "high" || order[1] != "once" || order[2] != "low") fail("Event priority ordering failed");

    order = [];
    var second = DMF_Event_Emit("dmf.test.event", {});
    if (second.delivered != 2 || array_length(order) != 2 || order[0] != "high" || order[1] != "low") fail("One-shot listener removal failed");

    var cancel = function(payload) {
        payload.cancelled = true;
    };
    DMF_Event_Subscribe("dmf.test.cancel", cancel, 100, false);
    var cancelled = DMF_Event_Emit("dmf.test.cancel", {});
    if (!cancelled.cancelled) fail("Payload cancellation failed");

    DMF_Event_Clear("dmf.test.event");
    DMF_Event_Clear("dmf.test.cancel");
    DMF_Mod_Unregister("dmf.test");

    if (DMF_Mod_IsRegistered("dmf.test")) fail("Mod unregister failed");

    return report;
}
