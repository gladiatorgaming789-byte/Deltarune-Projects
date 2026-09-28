function fdcx_hook_init(){ fdcx_init(); }

function fdcx_hook_after_init(){
    fdcx_init();
    if (!variable_struct_exists(global.dojo_data, "fdcx")) global.dojo_data.fdcx = { enemies: [], attacks: [] };
    global.fdcx_custom_enemies = global.dojo_data.fdcx.enemies;
    global.fdcx_custom_attacks = global.dojo_data.fdcx.attacks;
    fdcx_register_custom_enemies();
}

function fdcx_sync_save_data(){
    fdcx_init();
    if (!variable_global_exists("dojo_data") || !is_struct(global.dojo_data)) return;
    global.dojo_data.fdcx = { enemies: global.fdcx_custom_enemies, attacks: global.fdcx_custom_attacks };
}

function fdcx_custom_type_id(arg0){
    var _hash=0;
    for(var i=1;i<=string_length(arg0);i++) _hash=((_hash*31)+ord(string_char_at(arg0,i))) mod 100000;
    return 7000+_hash;
}

function fdcx_register_custom_enemies(){
    fdcx_init();
    if(!variable_global_exists("dm_enemy_info") || !ds_exists(global.dm_enemy_info,ds_type_map)) return;
    for(var i=0;i<array_length(global.fdcx_custom_enemies);i++){
        var _enemy=global.fdcx_custom_enemies[i]; if(!is_struct(_enemy)) continue;
        var _id=fdcx_custom_type_id(_enemy.id);
        var _sprite=asset_get_index(_enemy.sprite); if(_sprite<0) _sprite=spr_dummymonster;
        ds_map_set(global.dm_enemy_info,_id,{object:obj_dummyenemy,chapter:9999,name:_enemy.name,sprite:_sprite,is_available:true});
        var _exists=false;
        for(var j=0;j<array_length(global.dm_enemy_list);j++) if(global.dm_enemy_list[j]==_id){_exists=true;break;}
        if(!_exists) array_push(global.dm_enemy_list,_id);
    }
}

function fdcx_override_generated_enemy(arg0,arg1){
    fdcx_init(); var _id=is_struct(arg0)?arg0.type:arg0;
    if(!variable_global_exists("dm_enemy_info") || !ds_exists(global.dm_enemy_info,ds_type_map) || !ds_map_exists(global.dm_enemy_info,_id)) return arg1;
    var _info=ds_map_find_value(global.dm_enemy_info,_id); if(_info.object!=obj_dummyenemy) return arg1;
    for(var i=0;i<array_length(global.fdcx_custom_enemies);i++){
        var _enemy=global.fdcx_custom_enemies[i];
        if(is_struct(_enemy) && fdcx_custom_type_id(_enemy.id)==_id){ arg1.type=_id;arg1.hp=clamp(_enemy.hp,1,99999);arg1.at=_enemy.at;arg1.df=_enemy.df;return arg1; }
    }
    return arg1;
}

function fdcx_current_custom_enemy(){
    if(!v_ex("myself") || !variable_global_exists("monstertype")) return undefined;
    var _type=global.monstertype[myself];
    for(var i=0;i<array_length(global.fdcx_custom_enemies);i++){var _enemy=global.fdcx_custom_enemies[i];if(is_struct(_enemy)&&fdcx_custom_type_id(_enemy.id)==_type)return _enemy;}
    return undefined;
}

function fdcx_custom_enemy_step(){
    var _enemy=fdcx_current_custom_enemy(); if(!is_struct(_enemy)) return false;
    if(global.monster[myself]!=1) return true;
    if(scr_isphase("enemytalk") && !talked){global.typer=50;global.battlemsg[0]="* "+_enemy.name+" prepares an attack.";talked=1;}
    if(global.mnfight==1.5 && scr_attackpriority(1)){
        if(!instance_exists(obj_growtangle)) instance_create(camerax()+320,cameray()+170,obj_growtangle);
        if(!instance_exists(obj_moveheart)) scr_moveheart();
        global.mnfight=2;scr_turntimer(90);
    }
    if(scr_isphase("bullets") && !attacked){
        attacked=1;
        var _count=max(1,array_length(_enemy.attacks));
        var _attack_index=turns mod _count;
        if(fdcx_runtime_start(_enemy,_attack_index)){
            global.monsterattackname[myself]="FDCX_"+_enemy.id;
            var _attack=global.fdcx_runtime_attack;
            scr_turntimer(variable_struct_exists(_attack,"duration")?_attack.duration+30:210);
        } else scr_turntimer(30);
        turns++;
    }
    if(global.mnfight==2 && global.turntimer<=0){attacked=0;setbattlemsg=false;}
    return true;
}