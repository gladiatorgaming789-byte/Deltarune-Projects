/// DCE_CreateEnemy(name, template_enemy)
/// Returns a custom enemy data structure.

DCE_Init();

var _enemy = {
    id: global.dce_next_enemy_id,
    name: argument0,
    description: "",
    template_enemy: argument1,
    hp: 100,
    at: 5,
    df: 0,
    sprite: "",
    attacks: [],
    metadata: {
        editor_version: 1
    }
};

global.dce_next_enemy_id += 1;
array_push(global.dce_enemies, _enemy);

return _enemy;
