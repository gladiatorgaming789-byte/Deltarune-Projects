function fdcx_enemy_create(arg0 = "Custom Enemy")
{
    return {
        id: "fdcx_enemy_" + string(irandom(2147483646)),
        name: arg0,
        description: "",
        hp: 100,
        at: 5,
        df: 0,
        sprite: "spr_dummymonster",
        attacks: [],
        act_names: ["Check"],
        spareable: true
    };
}

function fdcx_enemy_add_attack(arg0, arg1)
{
    if (!is_struct(arg0) || !is_struct(arg1))
    {
        return false;
    }
    if (!variable_struct_exists(arg0, "attacks"))
    {
        arg0.attacks = [];
    }
    array_push(arg0.attacks, arg1.id);
    return true;
}

function fdcx_enemy_find(arg0)
{
    fdcx_init();
    for (var i = 0; i < array_length(global.fdcx_custom_enemies); i++)
    {
        if (global.fdcx_custom_enemies[i].id == arg0)
        {
            return global.fdcx_custom_enemies[i];
        }
    }
    return undefined;
}

function fdcx_attack_find(arg0)
{
    fdcx_init();
    for (var i = 0; i < array_length(global.fdcx_custom_attacks); i++)
    {
        if (global.fdcx_custom_attacks[i].id == arg0)
        {
            return global.fdcx_custom_attacks[i];
        }
    }
    return undefined;
}