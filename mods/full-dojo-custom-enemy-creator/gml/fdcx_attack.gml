function fdcx_attack_create(arg0 = "Untitled Attack")
{
    return {
        id: "fdcx_attack_" + string(irandom(2147483646)),
        name: arg0,
        duration: 180,
        events: []
    };
}

function fdcx_attack_event_bullet(arg0 = 0, arg1 = 320, arg2 = 170)
{
    return {
        type: "bullet",
        time: arg0,
        x: arg1,
        y: arg2,
        direction: 90,
        speed: 4,
        damage: 10,
        lifetime: 600,
        sprite: "spr_smallbullet",
        image_angle: 90,
        scale: 1
    };
}

function fdcx_attack_add_event(arg0, arg1)
{
    if (!is_struct(arg0))
    {
        return false;
    }
    if (!variable_struct_exists(arg0, "events"))
    {
        arg0.events = [];
    }
    array_push(arg0.events, arg1);
    return true;
}

function fdcx_attack_copy(arg0)
{
    if (!is_struct(arg0))
    {
        return fdcx_attack_create();
    }
    var _copy = {
        id: variable_struct_exists(arg0, "id") ? arg0.id : "fdcx_attack_" + string(irandom(2147483646)),
        name: variable_struct_exists(arg0, "name") ? arg0.name : "Untitled Attack",
        duration: variable_struct_exists(arg0, "duration") ? arg0.duration : 180,
        events: []
    };
    if (variable_struct_exists(arg0, "events") && is_array(arg0.events))
    {
        for (var i = 0; i < array_length(arg0.events); i++)
        {
            var _event = arg0.events[i];
            if (is_struct(_event))
            {
                array_push(_copy.events, struct_copy(_event));
            }
        }
    }
    return _copy;
}