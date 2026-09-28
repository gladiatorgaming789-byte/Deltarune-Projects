/// DCE_FindEnemy(enemy_id)

DCE_Init();

for (var i = 0; i < array_length(global.dce_enemies); i++) {
    if (global.dce_enemies[i].id == argument0) {
        return global.dce_enemies[i];
    }
}

return undefined;
