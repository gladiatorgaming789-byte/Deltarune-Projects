using System;
using UndertaleModLib.Models;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

GlobalDecompileContext dc = new(Data);
IDecompileSettings ds = Data.ToolInfo.DecompilerSettings;

string ReplaceExact(string source, string oldText, string newText, int expected, string label)
{
    int count = source.Split(new[] { oldText }, StringSplitOptions.None).Length - 1;
    if (count != expected)
        throw new Exception("SBC v0.4.3: " + label + " expected " + expected + " matches, found " + count);
    return source.Replace(oldText, newText);
}

void Patch(string codeName, Func<string,string> edit)
{
    var code = Data.Code.ByName(codeName);
    if (code == null) throw new Exception("SBC v0.4.3 missing " + codeName);
    string oldSource = GetDecompiledText(code);
    string newSource = edit(oldSource);
    if (oldSource == newSource) throw new Exception("SBC v0.4.3 no-op patch: " + codeName);
    CodeImportGroup group = new(Data, dc, ds) { ThrowOnNoOpFindReplace = true, MainThreadAction = MainThreadAction };
    group.QueueReplace(code, newSource);
    group.Import();
}

void EnsureScript(string scriptName, string gml)
{
    string codeName = "gml_Script_" + scriptName;
    var code = Data.Code.ByName(codeName);
    if (code == null)
    {
        CodeImportGroup group = new(Data, dc, ds) { ThrowOnNoOpFindReplace = true, MainThreadAction = MainThreadAction };
        group.QueueReplace(codeName, gml);
        group.Import();
        code = Data.Code.ByName(codeName);
    }
    if (code == null) throw new Exception("SBC v0.4.3 failed creating " + codeName);
    if (Data.Scripts.ByName(scriptName) == null)
        Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString(scriptName), Code = code });
    Data.Functions.EnsureDefined(scriptName, Data.Strings);
}

if (Data.Code.ByName("gml_Object_obj_pink_enemy_Create_0") == null)
    throw new Exception("SBC v0.4.3 targets DELTARUNE Chapter 5 v23");

EnsureScript("scr_gg_sbc_rewards", @"
if (argument0 == 0)
{
    if (!variable_global_exists(""secret_boss_challenge""))
    {
        ossafe_ini_open(""true_config.ini"");
        global.secret_boss_challenge = ini_read_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", 0);
        ossafe_ini_close();
    }
    if (global.flag[2493] == 0)
    {
        if (global.secret_boss_challenge == 1)
            global.flag[2493] = 1;
        else
            global.flag[2493] = 2;
    }
    if (global.flag[2493] == 1 && global.flag[2491] == 0)
    {
        global.flag[1312] += 3;
        global.flag[2491] = 1;
    }
    if (global.flag[2492] == 0)
    {
        if (global.flag[2493] == 1 && global.flag[1914] == 2)
            global.flag[2492] = 2;
        else
            global.flag[2492] = 3;
    }
}

if (global.flag[2493] == 1 && global.flag[2490] == 0)
{
    if (scr_weaponcheck_inventory(38) > 0 || scr_weaponcheck_equipped_any(38) > 0)
        global.flag[2490] = 1;
    else
    {
        scr_weaponget(38);
        if (noroom == 0)
            global.flag[2490] = 1;
    }
}

if (global.flag[2492] == 2)
{
    if (scr_weaponcheck_inventory(39) > 0 || scr_weaponcheck_equipped_any(39) > 0)
        global.flag[2492] = 1;
    else
    {
        scr_weaponget(39);
        if (noroom == 0)
            global.flag[2492] = 1;
    }
}
");

EnsureScript("scr_gg_sbc_shield_apply", @"
global.secret_boss_challenge_shield_active = 1;
global.secret_boss_challenge_shield_target = -1;
for (var __sbc_shield_i = 0; __sbc_shield_i < 3; __sbc_shield_i++)
{
    if (global.char[__sbc_shield_i] != 0 && instance_exists(global.charinstance[__sbc_shield_i]))
    {
        var __sbc_shield_target = global.charinstance[__sbc_shield_i];
        var __sbc_shield_anim = instance_create(__sbc_shield_target.x, __sbc_shield_target.y, obj_healanim);
        __sbc_shield_anim.target = __sbc_shield_target;
        __sbc_shield_anim.image_blend = make_color_rgb(255, 128, 210);
        __sbc_shield_target.tu += 1;
    }
}
global.spelldelay = 20;
");

Patch("gml_Object_obj_darkcontroller_Create_0", source =>
    ReplaceExact(source,
        "global.submenu = 0;\nglobal.charselect = -1;",
        @"global.submenu = 0;
if (!variable_global_exists(""secret_boss_challenge""))
{
    ossafe_ini_open(""true_config.ini"");
    global.secret_boss_challenge = ini_read_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", 0);
    ossafe_ini_close();
}
global.charselect = -1;", 1, "CONFIG load"));

Patch("gml_Object_obj_darkcontroller_Step_0", source =>
{
    source = ReplaceExact(source, "if (global.submenucoord[30] > 6)", "if (global.submenucoord[30] > 7)", 1, "CONFIG max row");
    string anchor = @"                    }
                }
                if (global.is_console)";
    string insert = @"                    }
                }
                if (global.submenucoord[30] == 5)
                {
                    global.secret_boss_challenge = 1 - global.secret_boss_challenge;
                    ossafe_ini_open(""true_config.ini"");
                    ini_write_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", global.secret_boss_challenge);
                    ossafe_ini_close();
                }
                if (global.is_console)";
    source = ReplaceExact(source, anchor, insert, 1, "CONFIG toggle");
    source = ReplaceExact(source,
        "if (global.submenucoord[30] == 5)\n                    {\n                        global.submenu = 34;",
        "if (global.submenucoord[30] == 6)\n                    {\n                        global.submenu = 34;", 2, "CONFIG shifted title");
    source = ReplaceExact(source,
        "if (global.submenucoord[30] == 6)\n                    {\n                        m_quit = 1;",
        "if (global.submenucoord[30] == 7)\n                    {\n                        m_quit = 1;", 2, "CONFIG shifted back");
    return source;
});

Patch("gml_Object_obj_darkcontroller_Draw_0", source =>
{
    source = ReplaceExact(source,
        "draw_sprite(spr_heart, 0, _heartXPos, yy + 160 + (global.submenucoord[30] * 35));",
        "draw_sprite(spr_heart, 0, _heartXPos, (global.submenucoord[30] >= 5) ? (yy + 335 + ((global.submenucoord[30] - 5) * 25)) : (yy + 160 + (global.submenucoord[30] * 35)));", 1, "CONFIG cursor");
    source = ReplaceExact(source,
        @"            draw_text(_xPos, yy + 325, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 360, string_hash_to_newline(back_text));",
        @"            draw_text(_xPos, yy + 325, ""Boss Challenge"");
            draw_text(_selectXPos, yy + 325, (global.secret_boss_challenge == 1) ? ""ON"" : ""OFF"");
            draw_text(_xPos, yy + 350, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 375, string_hash_to_newline(back_text));", 2, "CONFIG rows");
    return source;
});

Patch("gml_GlobalScript_scr_weaponinfo", source =>
{
    string anchor = @"        case 50:
            weaponnametemp = stringsetloc(""JingleBlade"", ""scr_weaponinfo_slash_scr_weaponinfo_gml_629_0"");";
    string cases = @"        case 38:
            weaponnametemp = stringsetloc(""Pink Scarf"", ""secret_boss_challenge_pink_scarf_name"");
            weapondesctemp = stringsetloc(""A soft scarf left by Pink.#Grants Shield and improves grazing."", ""secret_boss_challenge_pink_scarf_desc"");
            wmessage2temp = stringsetloc(""Way too pink. Even for me."", ""secret_boss_challenge_pink_scarf_susie"");
            wmessage3temp = stringsetloc(""It feels warm... and a little dramatic!"", ""secret_boss_challenge_pink_scarf_ralsei"");
            wmessage4temp = stringsetloc(""It's adorable!"", ""secret_boss_challenge_pink_scarf_noelle"");
            weaponattemp = 8;
            weapondftemp = 4;
            weaponmagtemp = 12;
            weaponboltstemp = 1;
            weaponstyletemp = ""?"";
            weapongrazeamttemp = 0.1;
            weapongrazesizetemp = 0.25;
            weaponchar1temp = 0;
            weaponchar2temp = 0;
            weaponchar3temp = 1;
            weaponchar4temp = 0;
            weaponicontemp = 3;
            weaponabilityicontemp = 0;
            weaponabilitytemp = ""Shield / Area +25%, TP +10%"";
            value = 1500;
            break;
        case 39:
            weaponnametemp = stringsetloc(""Pink's Staff"", ""secret_boss_challenge_pinks_staff_name"");
            weapondesctemp = stringsetloc(""A heart-topped staff earned by facing Pink#at her meanest. It hums in Kris's hands."", ""secret_boss_challenge_pinks_staff_desc"");
            wmessage2temp = stringsetloc(""Kris, bonk something with it."", ""secret_boss_challenge_pinks_staff_susie"");
            wmessage3temp = stringsetloc(""It looks like a conductor's baton!"", ""secret_boss_challenge_pinks_staff_ralsei"");
            wmessage4temp = stringsetloc(""That is an extremely pink weapon."", ""secret_boss_challenge_pinks_staff_noelle"");
            weaponattemp = 14;
            weapondftemp = 2;
            weaponmagtemp = 4;
            weaponboltstemp = 1;
            weaponstyletemp = ""?"";
            weapongrazeamttemp = 0;
            weapongrazesizetemp = 0;
            weaponchar1temp = 1;
            weaponchar2temp = 0;
            weaponchar3temp = 0;
            weaponchar4temp = 0;
            weaponicontemp = 1;
            weaponabilityicontemp = 0;
            weaponabilitytemp = """";
            value = 2000;
            break;
        case 50:
            weaponnametemp = stringsetloc(""JingleBlade"", ""scr_weaponinfo_slash_scr_weaponinfo_gml_629_0"");";
    return ReplaceExact(source, anchor, cases, 1, "Pink weapons");
});

Patch("gml_Object_obj_dw_pink_encounter_Step_0", source =>
    ReplaceExact(source,
        "if (con == 5)\n{\n    con = 6;\n    pinkface.silhouette = false;",
        "if (con == 5)\n{\n    con = 6;\n    scr_gg_sbc_rewards(0);\n    pinkface.silhouette = false;", 1, "Pink reward call"));

Patch("gml_Object_obj_dw_fcastle_pinkshop_Create_0", source =>
{
    source = ReplaceExact(source,
        "purchasecount = 0;\ntryingtobuy = -1;",
        @"scr_gg_sbc_rewards(1);
regularpurchasegoal = (global.flag[2493] == 1) ? 4 : 3;
totalpurchasegoal = regularpurchasegoal + 1;
purchasecount = 0;
tryingtobuy = -1;", 1, "shop goals/recovery");
    source = ReplaceExact(source, "if (purchasecount == 4)", "if (purchasecount == totalpurchasegoal)", 1, "shop completed count");
    return source;
});

Patch("gml_Object_obj_dw_fcastle_pinkshop_Step_0", source =>
{
    source = ReplaceExact(source, "if (purchasecount == 3)", "if (purchasecount == regularpurchasegoal)", 2, "shop regular thresholds");
    source = ReplaceExact(source, "if (purchasecount == 4)", "if (purchasecount == totalpurchasegoal)", 2, "shop total thresholds");
    return source;
});

Patch("gml_Object_obj_dw_fcastle_pinkshop_Other_11", source =>
    ReplaceExact(source,
        "c_msgnextsubloc(\"~1* Now^1, choose 3 of us!/%\",",
        "c_msgnextsubloc((regularpurchasegoal == 4) ? \"~1* Now^1, choose 4 of us!/%\" : \"~1* Now^1, choose 3 of us!/%\",", 1, "shop choice dialogue"));

Patch("gml_Object_obj_battlecontroller_Create_0", source =>
    ReplaceExact(source,
        "global.spelldelay = 10;\nglobal.turntimer = 120;",
        "global.spelldelay = 10;\nglobal.secret_boss_challenge_shield_active = 0;\nglobal.secret_boss_challenge_shield_target = -1;\nglobal.turntimer = 120;", 1, "Shield battle init"));

Patch("gml_GlobalScript_scr_spellinfo", source =>
{
    string anchor = @"        case 13:
            spellname = stringsetloc(""Scythemare"", ""scr_spellinfo_slash_scr_spellinfo_gml_219_0"");";
    string repl = @"        case 14:
            spellname = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name"");
            spellnameb = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name_b"");
            spelldescb = stringsetloc(""Shield#party"", ""secret_boss_challenge_shield_spell_desc_b"");
            spelldesc = stringsetloc(""Reduces damage to the whole party by about 75%#until the enemy attack ends."", ""secret_boss_challenge_shield_spell_desc"");
            spelltarget = 0;
            cost = 187.5;
            spellusable = 0;
            break;
        case 13:
            spellname = stringsetloc(""Scythemare"", ""scr_spellinfo_slash_scr_spellinfo_gml_219_0"");";
    return ReplaceExact(source, anchor, repl, 1, "Shield info");
});

Patch("gml_GlobalScript_scr_spellmenu_setup", source =>
{
    int pos = source.LastIndexOf("\n}", StringComparison.Ordinal);
    if (pos < 0) throw new Exception("SBC v0.4.3 spellmenu closing brace missing");
    string block = @"
    // SBC: add Shield only for active Ralsei while Pink Scarf is equipped.
    for (__i = 0; __i < 3; __i++)
    {
        if (global.char[__i] == 3 && global.charweapon[3] == 38)
        {
            for (__fj = 0; __fj < 18; __fj++)
            {
                if (global.battlespell[__i][__fj] == 0)
                {
                    scr_spellinfo(14);
                    global.battlespell[__i][__fj] = 14;
                    global.battlespellcost[__i][__fj] = cost;
                    global.battlespellname[__i][__fj] = spellnameb;
                    global.battlespelldesc[__i][__fj] = spelldescb;
                    global.battlespelltarget[__i][__fj] = spelltarget;
                    __fj = 18;
                }
            }
        }
    }
";
    return source.Insert(pos, block);
});

Patch("gml_Object_obj_heroparent_Step_0", source =>
    ReplaceExact(source,
        "            scr_spell(global.charspecial[myself], myself);",
        @"            if (global.charspecial[myself] == 14)
                scr_gg_sbc_shield_apply();
            else
                scr_spell(global.charspecial[myself], myself);", 1, "Shield safe caller"));

Patch("gml_GlobalScript_scr_spelltext", source =>
{
    string anchor = @"        case 100:
            cancelattack = 0;";
    string repl = @"        case 14:
            global.msg[0] = stringsetsubloc(""* ~1 cast SHIELD on the party!/%"", global.charname[global.char[caster]], ""secret_boss_challenge_shield_cast"");
            break;
        case 100:
            cancelattack = 0;";
    return ReplaceExact(source, anchor, repl, 1, "Shield text");
});

Patch("gml_GlobalScript_scr_damage", source =>
{
    string direct = @"        if (!instance_exists(obj_shake))";
    string directNew = @"        if (target < 3 && variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1)
        {
            tdamage = max(1, ceil(tdamage * 0.25));
        }
        if (!instance_exists(obj_shake))";
    source = ReplaceExact(source, direct, directNew, 1, "Shield direct damage");
    string party = @"                    tdamage = ceil(tdamage * scr_element_damage_reduction(__element, chartarget));
                    if (global.charaction[hpi] == 10)";
    string partyNew = @"                    tdamage = ceil(tdamage * scr_element_damage_reduction(__element, chartarget));
                    if (variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1)
                    {
                        tdamage = max(1, ceil(tdamage * 0.25));
                    }
                    if (global.charaction[hpi] == 10)";
    source = ReplaceExact(source, party, partyNew, 1, "Shield party damage");
    return source;
});

Patch("gml_GlobalScript_scr_mnendturn", source =>
    ReplaceExact(source,
        "    if (techwon == 0)\n    {",
        @"    if (techwon == 0)
    {
        if (variable_global_exists(""secret_boss_challenge_shield_active""))
        {
            global.secret_boss_challenge_shield_active = 0;
            global.secret_boss_challenge_shield_target = -1;
        }", 1, "Shield expiry"));

Patch("gml_Object_obj_grazebox_Create_0", source =>
    ReplaceExact(source,
        "grazesizefactor = 1;\ngrazetpfactor += (scr_armorcheck_equipped_party(15) * 0.1);",
        @"grazesizefactor = 1;
if (scr_weaponcheck_equipped_party(38) > 0)
{
    grazetpfactor += 0.1;
    grazesizefactor += 0.25;
}
grazetpfactor += (scr_armorcheck_equipped_party(15) * 0.1);", 1, "Pink Scarf graze"));

ScriptMessage("Secret Boss Challenge v0.4.3 Chapter 5 installed. Shield cast is isolated from scr_spell.");
