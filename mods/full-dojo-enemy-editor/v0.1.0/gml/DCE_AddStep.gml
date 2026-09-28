/// DCE_AddStep(attack, type, time, data)
/// Adds one timeline step to an attack.

var _attack = argument0;

var _step = {
    type: argument1,
    time: argument2,
    data: argument3
};

array_push(_attack.steps, _step);
return _attack;
