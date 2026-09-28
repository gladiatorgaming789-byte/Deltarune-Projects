/// DCE_AttackRuntime
/// Public runtime API. Battle-frame execution lives in DCE_BattleIntegration.

function DCE_AttackStart(_enemy_id, _attack_id, _slot = 0) {
    DCE_Init();
    var _enemy = DCE_FindEnemy(_enemy_id);
    if (is_undefined(_enemy)) return false;
    var _attack = DCE_FindAttack(_enemy, _attack_id);
    if (is_undefined(_attack)) return false;
    if (!DCE_IsCustomEnemySlot(_slot)) return false;
    if (global.monstertype[_slot] != _enemy_id) return false;
    return DCE_StartAttackForSlot(_slot, _attack);
}

function DCE_AttackStep() {
    DCE_BattleStep();
    return true;
}
