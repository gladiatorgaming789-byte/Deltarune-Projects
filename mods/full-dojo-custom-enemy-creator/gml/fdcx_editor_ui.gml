function fdcx_editor_open(){fdcx_init();global.fdcx_editor_mode=true;global.fdcx_editor_screen="enemies";global.fdcx_selected_event=-1;}
function fdcx_editor_close(){global.fdcx_editor_mode=false;fdcx_sync_save_data();}
function fdcx_editor_draw(){
 if(!global.fdcx_editor_mode)return;var _x=camerax(),_y=cameray();draw_set_alpha(.96);draw_set_color(c_black);draw_rectangle(_x+15,_y+15,_x+625,_y+465,false);draw_set_alpha(1);draw_set_color(c_white);
 draw_text(_x+30,_y+25,"Full Dojo Custom Enemy Creator");
 if(global.fdcx_editor_screen=="enemies"){
  draw_text(_x+30,_y+48,"N: new enemy   Up/Down: select   Enter: edit enemy   Esc: close");
  draw_text(_x+40,_y+80,"CUSTOM ENEMIES");
  for(var i=0;i<array_length(global.fdcx_custom_enemies);i++){var _e=global.fdcx_custom_enemies[i];if(!is_struct(_e))continue;draw_set_color(i==global.fdcx_selected_enemy?c_yellow:c_white);draw_text(_x+50,_y+105+i*22,_e.name);draw_set_color(c_white);}
  var _sel=fdcx_editor_selected_enemy();if(is_struct(_sel)){draw_text(_x+320,_y+80,"Selected");draw_text(_x+320,_y+105,"Name: "+_sel.name);draw_text(_x+320,_y+127,"HP: "+string(_sel.hp)+"  AT: "+string(_sel.at)+"  DF: "+string(_sel.df));draw_text(_x+320,_y+149,"A: new attack   1: name  2: HP  3: AT  4: DF");draw_text(_x+320,_y+175,"Attacks: "+string(array_length(_sel.attacks)));for(var j=0;j<array_length(_sel.attacks);j++){var _a=fdcx_attack_find(_sel.attacks[j]);if(is_struct(_a))draw_text(_x+335,_y+200+j*20,string(j+1)+". "+_a.name);}}
 }else{
  var _enemy=fdcx_editor_selected_enemy();var _attack=(global.fdcx_selected_attack>=0&&global.fdcx_selected_attack<array_length(global.fdcx_custom_attacks))?global.fdcx_custom_attacks[global.fdcx_selected_attack]:undefined;
  draw_text(_x+30,_y+48,"ATTACK EDITOR   A: add attack   D: duplicate   X: delete   E: edit event   Esc: back");
  draw_rectangle(_x+45,_y+75,_x+595,_y+300,true);draw_set_color(c_gray);for(var i=0;i<=10;i++)draw_line(_x+45+i*55,_y+75,_x+45+i*55,_y+300);for(var j=0;j<=5;j++)draw_line(_x+45,_y+75+j*45,_x+595,_y+75+j*45);draw_set_color(c_white);
  if(is_struct(_attack)){draw_text(_x+50,_y+310,_attack.name+"  duration="+string(_attack.duration));for(var k=0;k<array_length(_attack.events);k++){var _ev=_attack.events[k];if(!is_struct(_ev))continue;var _px=_x+45+clamp(_ev.x-50,0,540),_py=_y+75+clamp(_ev.y-85,0,215);draw_set_color(k==global.fdcx_selected_event?c_yellow:c_white);draw_circle(_px,_py,5,false);draw_text(_px+7,_py-7,string(_ev.time));}draw_set_color(c_white);draw_text(_x+50,_y+335,"TIMELINE");for(var m=0;m<array_length(_attack.events);m++){var _te=_attack.events[m];if(!is_struct(_te))continue;var _tx=_x+50+((_te.time/max(1,_attack.duration))*530);draw_line(_tx,_y+350,_tx,_y+390);draw_text(_tx-3,_y+395,string(m+1));}}
  draw_text(_x+50,_y+430,"Click canvas: add bullet at that position. Timeline markers show event timing.");
 }
}
function fdcx_editor_step(){
 if(!global.fdcx_editor_mode)return;
 if(keyboard_check_pressed(vk_escape)){if(global.fdcx_editor_screen=="attack")global.fdcx_editor_screen="enemies";else fdcx_editor_close();return;}
 if(global.fdcx_editor_screen=="enemies"){
  var _n=array_length(global.fdcx_custom_enemies);if(keyboard_check_pressed(ord("N")))fdcx_editor_new_enemy();
  if(_n>0){if(keyboard_check_pressed(vk_down))global.fdcx_selected_enemy=min(_n-1,global.fdcx_selected_enemy+1);if(keyboard_check_pressed(vk_up))global.fdcx_selected_enemy=max(0,global.fdcx_selected_enemy-1);
   var _e=fdcx_editor_selected_enemy();
   if(is_struct(_e)){if(keyboard_check_pressed(vk_enter)){global.fdcx_editor_screen="attack";if(array_length(_e.attacks)>0){global.fdcx_selected_attack=0;var _first=fdcx_attack_find(_e.attacks[0]);for(var q=0;q<array_length(global.fdcx_custom_attacks);q++)if(global.fdcx_custom_attacks[q].id==_first.id)global.fdcx_selected_attack=q;}}if(keyboard_check_pressed(ord("A")))fdcx_editor_new_attack_for_selected_enemy();if(keyboard_check_pressed(ord("1"))){var _v=get_string("Enemy name",_e.name);if(_v!=-1)_e.name=_v;}if(keyboard_check_pressed(ord("2"))){var _v=get_string("HP",string(_e.hp));if(_v!=-1)_e.hp=clamp(real(_v),1,99999);}if(keyboard_check_pressed(ord("3"))){var _v=get_string("AT",string(_e.at));if(_v!=-1)_e.at=real(_v);}if(keyboard_check_pressed(ord("4"))){var _v=get_string("DF",string(_e.df));if(_v!=-1)_e.df=real(_v);}fdcx_sync_save_data();}}
 }else{
  var _e=fdcx_editor_selected_enemy();if(!is_struct(_e))return;if(keyboard_check_pressed(ord("A")))fdcx_editor_new_attack_for_selected_enemy();if(keyboard_check_pressed(ord("D")))fdcx_editor_duplicate_selected_event();if(keyboard_check_pressed(ord("X")))fdcx_editor_delete_selected_event();if(keyboard_check_pressed(ord("E")))fdcx_editor_edit_selected_event();
  if(mouse_check_button_pressed(mb_left)){var _mx=mouse_x,_my=mouse_y;if(_mx>=camerax()+45&&_mx<=camerax()+595&&_my>=cameray()+75&&_my<=cameray()+300){var _a=fdcx_editor_selected_attack;if(_a<0){if(array_length(_e.attacks)>0){var _id=_e.attacks[0];for(var z=0;z<array_length(global.fdcx_custom_attacks);z++)if(global.fdcx_custom_attacks[z].id==_id)_a=z;global.fdcx_selected_attack=_a;}}if(_a>=0)fdcx_editor_add_bullet(_mx,_my,0);}}}
}
