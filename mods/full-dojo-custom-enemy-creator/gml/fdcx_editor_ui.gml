function fdcx_editor_open(){fdcx_init();global.fdcx_editor_mode=true;global.fdcx_selected_event=-1;}
function fdcx_editor_close(){global.fdcx_editor_mode=false;}
function fdcx_editor_draw(){
    if(!global.fdcx_editor_mode)return;
    var _x=camerax(),_y=cameray(); draw_set_alpha(.96);draw_set_color(c_black);draw_rectangle(_x+20,_y+20,_x+620,_y+460,false);draw_set_alpha(1);draw_set_color(c_white);
    draw_text(_x+36,_y+30,"FDCX Attack Editor");draw_text(_x+36,_y+50,"Canvas: click to add bullet | D: duplicate | X: delete");
    draw_rectangle(_x+50,_y+85,_x+590,_y+315,true);draw_set_color(c_gray);
    for(var i=0;i<=10;i++)draw_line(_x+50+i*54,_y+85,_x+50+i*54,_y+315);
    for(var j=0;j<=5;j++)draw_line(_x+50,_y+85+j*46,_x+590,_y+85+j*46);
    draw_set_color(c_white);
    if(global.fdcx_selected_attack>=0&&global.fdcx_selected_attack<array_length(global.fdcx_custom_attacks)){
        var _attack=global.fdcx_custom_attacks[global.fdcx_selected_attack];draw_text(_x+50,_y+325,_attack.name+" duration="+string(_attack.duration));
        for(var k=0;k<array_length(_attack.events);k++){var _e=_attack.events[k];if(!is_struct(_e))continue;draw_circle(_x+50+(_e.x-50),_y+85+(_e.y-85),5,false);draw_text(_x+57+(_e.x-50),_y+78+(_e.y-85),string(_e.time));}
        draw_text(_x+50,_y+350,"Timeline");
        for(var m=0;m<array_length(_attack.events);m++){var _te=_attack.events[m];if(!is_struct(_te))continue;var _tx=_x+50+((_te.time/max(1,_attack.duration))*540);draw_line(_tx,_y+370,_tx,_y+410);draw_text(_tx-4,_y+415,string(_te.time));}
    }
    draw_text(_x+50,_y+440,"Enter: new attack | Esc: close");
}
function fdcx_editor_step(){
    if(!global.fdcx_editor_mode)return;
    if(keyboard_check_pressed(vk_escape)){fdcx_editor_close();return;}
    if(keyboard_check_pressed(vk_enter)){fdcx_editor_new_attack();}
    if(global.fdcx_selected_attack>=0&&global.fdcx_selected_attack<array_length(global.fdcx_custom_attacks)){
        if(keyboard_check_pressed(ord("X")))fdcx_editor_delete_selected_event();
        if(keyboard_check_pressed(ord("D")))fdcx_editor_duplicate_selected_event();
        if(mouse_check_button_pressed(mb_left)){var _mx=mouse_x,_my=mouse_y;if(_mx>=camerax()+50&&_mx<=camerax()+590&&_my>=cameray()+85&&_my<=cameray()+315)fdcx_editor_add_bullet(_mx,_my,0);}
    }
}