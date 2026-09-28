// FDCX v0.1 installer for Full Dojo Customizer v0.14.
using System; using System.IO; using UndertaleModLib.Compiler; using Underanalyzer.Decompiler;
EnsureDataLoaded(); if(Data.IsYYC()){ScriptError("FDCX requires non-YYC data.");return;}
string root=Path.GetDirectoryName(ScriptPath),gml=Path.Combine(root,"gml");
string[] files={"fdcx_init.gml","fdcx_attack.gml","fdcx_enemy.gml","fdcx_runtime.gml","fdcx_editor_model.gml","fdcx_editor_ui.gml","fdcx_hooks.gml"};
foreach(string f in files)if(!File.Exists(Path.Combine(gml,f))){ScriptError("Missing FDCX source: "+f);return;}
GlobalDecompileContext dc=new(Data);IDecompileSettings settings=Data.ToolInfo.DecompilerSettings;CodeImportGroup group=new(Data,dc,settings){AutoCreateAssets=true,MainThreadAction=MainThreadAction};
foreach(string f in files)group.QueueReplace("gml_GlobalScript_"+Path.GetFileNameWithoutExtension(f),File.ReadAllText(Path.Combine(gml,f)));
group.QueuePrepend("gml_GlobalScript_scr_dojamanager_initvars","fdcx_hook_init();
");
group.QueueAppend("gml_GlobalScript_scr_dojamanager_initvars","
fdcx_hook_after_init();
");
group.QueuePrepend("gml_GlobalScript_scr_dm_setup_enemy_info","fdcx_init();
");
group.QueueAppend("gml_GlobalScript_scr_dm_setup_enemy_info","
fdcx_register_custom_enemies();
");
group.QueuePrepend("gml_GlobalScript_scr_dm_generate_enemy","var _fdcx_generated = fdcx_generate_custom_enemy(argument0); if(is_struct(_fdcx_generated)) return _fdcx_generated;
");
group.QueuePrepend("gml_Object_obj_dummyenemy_Step_0","if(fdcx_custom_enemy_step())return;
");
group.QueuePrepend("gml_GlobalScript_scr_dm_save","fdcx_sync_save_data();
");
group.QueuePrepend("gml_Object_obj_dojomanage_Step_0","if(keyboard_check_pressed(vk_f8)){fdcx_editor_open();} fdcx_editor_step();
");
group.QueueAppend("gml_Object_obj_dojomanage_Draw_0","
fdcx_editor_draw();
");
group.Import(); ScriptMessage("FDCX v0.1 installed.");