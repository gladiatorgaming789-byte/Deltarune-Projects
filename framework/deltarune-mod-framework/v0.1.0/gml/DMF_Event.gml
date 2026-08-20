/// @function DMF_Event_Subscribe(event_name, callback, priority, once)
function DMF_Event_Subscribe(event_name, callback, priority, once) {
    DMF_Init();
    if (!is_string(event_name) || string_length(event_name) <= 0) return -1;
    if (!is_callable(callback)) return -1;
    if (!is_real(priority)) priority = 0;
    if (!is_bool(once)) once = false;

    if (!variable_struct_exists(global.dmf.events, event_name)) {
        variable_struct_set(global.dmf.events, event_name, []);
    }

    var token = global.dmf.next_token;
    global.dmf.next_token += 1;
    var listener = {
        token: token,
        event_name: event_name,
        callback: callback,
        priority: priority,
        once: once,
        active: true
    };

    var listeners = variable_struct_get(global.dmf.events, event_name);
    var inserted = false;
    var next = [];
    for (var i = 0; i < array_length(listeners); i++) {
        if (!inserted && priority > listeners[i].priority) {
            next[array_length(next)] = listener;
            inserted = true;
        }
        next[array_length(next)] = listeners[i];
    }
    if (!inserted) next[array_length(next)] = listener;
    variable_struct_set(global.dmf.events, event_name, next);
    return token;
}

/// @function DMF_Event_Unsubscribe(token)
function DMF_Event_Unsubscribe(token) {
    DMF_Init();
    var changed = false;
    var names = variable_struct_get_names(global.dmf.events);
    for (var n = 0; n < array_length(names); n++) {
        var name = names[n];
        var listeners = variable_struct_get(global.dmf.events, name);
        for (var i = 0; i < array_length(listeners); i++) {
            if (listeners[i].token == token && listeners[i].active) {
                listeners[i].active = false;
                changed = true;
            }
        }
        variable_struct_set(global.dmf.events, name, listeners);
    }
    return changed;
}

/// @function DMF_Event_Emit(event_name, payload)
function DMF_Event_Emit(event_name, payload) {
    DMF_Init();
    if (!is_struct(payload)) payload = { value: payload };
    if (!variable_struct_exists(global.dmf.events, event_name)) {
        return { event_name: event_name, delivered: 0, cancelled: false, listener_count: 0 };
    }

    var listeners = variable_struct_get(global.dmf.events, event_name);
    var delivered = 0;
    var cancelled = false;
    var snapshot = array_copy(listeners, 0, array_length(listeners));

    for (var i = 0; i < array_length(snapshot); i++) {
        var listener = snapshot[i];
        if (!listener.active) continue;
        delivered += 1;
        var result = listener.callback(payload);
        if (listener.once) DMF_Event_Unsubscribe(listener.token);
        if (result == true) {
            cancelled = true;
            break;
        }
        if (variable_struct_exists(payload, "cancelled") && payload.cancelled) {
            cancelled = true;
            break;
        }
    }

    return {
        event_name: event_name,
        delivered: delivered,
        cancelled: cancelled,
        listener_count: array_length(snapshot)
    };
}

/// @function DMF_Event_Clear(event_name)
function DMF_Event_Clear(event_name) {
    DMF_Init();
    if (!variable_struct_exists(global.dmf.events, event_name)) return false;
    variable_struct_set(global.dmf.events, event_name, []);
    return true;
}
