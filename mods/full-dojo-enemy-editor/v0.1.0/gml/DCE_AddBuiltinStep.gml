/// DCE_AddBuiltinStep(attack, type, time)
/// Adds a built-in timeline primitive backed by native Dojo attack controllers.

var _data = {};

switch (argument1) {
    case "wait":
        _data.duration = 1;
        break;

    case "aimed":
        _data.native_type = 0;
        _data.duration = 60;
        _data.damage = 10;
        break;

    case "radial":
        _data.native_type = 31;
        _data.duration = 60;
        _data.damage = 10;
        break;

    case "horizontal":
        _data.native_type = 30;
        _data.duration = 60;
        _data.damage = 10;
        break;

    case "vertical":
        _data.native_type = 1;
        _data.duration = 60;
        _data.damage = 10;
        break;

    case "burst":
        _data.native_type = 4;
        _data.duration = 45;
        _data.damage = 10;
        break;

    case "repeat":
        _data.count = 2;
        _data.interval = 20;
        break;
}

return DCE_AddStep(argument0, argument1, argument2, _data);
