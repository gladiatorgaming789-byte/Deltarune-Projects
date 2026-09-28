/// DCE_EditorModel
/// Editor operations. Rendering/input is kept separate from the model.

function DCE_EditorNewEnemy(_name, _template) {
    return DCE_CreateEnemy(_name, _template);
}

function DCE_EditorNewAttack(_enemy, _name) {
    var _attack = DCE_CreateAttack(_name);
    array_push(_enemy.attacks, _attack);
    return _attack;
}

function DCE_EditorAddStep(_attack, _type, _time) {
    return DCE_AddBuiltinStep(_attack, _type, _time);
}

function DCE_EditorDeleteStep(_attack, _index) {
    if (_index < 0 || _index >= array_length(_attack.steps)) return false;

    array_delete(_attack.steps, _index, 1);
    return true;
}

function DCE_EditorMoveStep(_attack, _from, _to) {
    var _count = array_length(_attack.steps);
    if (_from < 0 || _from >= _count || _to < 0 || _to >= _count) return false;

    var _step = _attack.steps[_from];
    array_delete(_attack.steps, _from, 1);
    array_insert(_attack.steps, _to, _step);
    return true;
}

function DCE_EditorSetAttackDuration(_attack, _frames) {
    _attack.duration = max(1, floor(_frames));
    return _attack;
}
