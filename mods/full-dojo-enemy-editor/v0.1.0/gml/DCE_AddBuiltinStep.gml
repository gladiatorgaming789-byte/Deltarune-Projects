/// DCE_AddBuiltinStep(attack, type, time)
/// Adds a safe built-in attack primitive.

var _data = {};

switch (argument1) {
    case "wait":
        break;

    case "aimed":
        _data.speed = 4;
        _data.damage = 10;
        _data.angle_offset = 0;
        break;

    case "radial":
        _data.count = 8;
        _data.speed = 3;
        _data.damage = 10;
        _data.rotation = 0;
        break;

    case "horizontal":
        _data.speed = 4;
        _data.damage = 10;
        break;

    case "vertical":
        _data.speed = 4;
        _data.damage = 10;
        break;

    case "burst":
        _data.count = 5;
        _data.speed = 4;
        _data.spread = 12;
        _data.damage = 10;
        break;

    case "repeat":
        _data.count = 2;
        _data.interval = 20;
        break;
}

return DCE_AddStep(argument0, argument1, argument2, _data);
