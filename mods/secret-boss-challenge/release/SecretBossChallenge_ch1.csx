using System;
using System.Collections.Generic;
using UndertaleModLib.Models;
using UndertaleModLib.Compiler;

EnsureDataLoaded();

UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, null, Data.ToolInfo.DecompilerSettings)
{
    ThrowOnNoOpFindReplace = true,
    MainThreadAction = MainThreadAction
};

int CountOccurrences(string source, string value)
{
    if (String.IsNullOrEmpty(value))
        return 0;
    int count = 0;
    int index = 0;
    while ((index = source.IndexOf(value, index, StringComparison.Ordinal)) >= 0)
    {
        count++;
        index += value.Length;
    }
    return count;
}

string ReplaceExact(string source, string oldText, string newText, string label)
{
    int oldCount = CountOccurrences(source, oldText);
    if (oldCount == 1)
        return source.Replace(oldText, newText, StringComparison.Ordinal);

    if (oldCount == 0 && CountOccurrences(source, newText) == 1)
        return source;

    throw new Exception($"Secret Boss Challenge: {label} expected one source match, found {oldCount}. The game version or another mod changed the same code.");
}

void PatchCode(string codeName, (string OldText, string NewText, string Label)[] replacements)
{
    UndertaleCode code = Data.Code.ByName(codeName);
    if (code is null)
        throw new Exception($"Secret Boss Challenge: missing code entry {codeName}.");

    string source = GetDecompiledText(code);
    string patched = source;
    foreach (var replacement in replacements)
        patched = ReplaceExact(patched, replacement.OldText, replacement.NewText, replacement.Label);

    if (!String.Equals(source, patched, StringComparison.Ordinal))
        importGroup.QueueReplace(code, patched);
}

if (Data.Code.ByName("gml_Object_obj_joker_Create_0") is null)
    throw new Exception("Secret Boss Challenge: this patch targets DELTARUNE Chapter 1.");

PatchCode("gml_Object_obj_darkcontroller_Create_0", new[]
{
    (@"mmy[0] = 0;
mmy[1] = 0;
mmy[2] = 0;
global.submenu = 0;
global.charselect = -1;
for (var i = 0; i < 36; i += 1)
{
    global.submenucoord[i] = 0;
", @"mmy[0] = 0;
mmy[1] = 0;
mmy[2] = 0;
global.submenu = 0;
if (!variable_global_exists(""secret_boss_challenge""))
{
    ossafe_ini_open(""true_config.ini"");
    global.secret_boss_challenge = ini_read_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", 0);
    ossafe_ini_close();
}
global.charselect = -1;
for (var i = 0; i < 36; i += 1)
{
    global.submenucoord[i] = 0;
", "ch1/gml_Object_obj_darkcontroller_Create_0 hunk 1"),
});

PatchCode("gml_Object_obj_darkcontroller_Step_0", new[]
{
    (@"            if (down_p())
            {
                movenoise = 1;
                global.submenucoord[30] += 1;
                if (global.submenucoord[30] > 6)
                {
                    global.submenucoord[30] = 6;
                }
            }
            if (button1_p() && onebuffer < 0)
            {
", @"            if (down_p())
            {
                movenoise = 1;
                global.submenucoord[30] += 1;
                if (global.submenucoord[30] > 7)
                {
                    global.submenucoord[30] = 7;
                }
            }
            if (button1_p() && onebuffer < 0)
            {
", "ch1/gml_Object_obj_darkcontroller_Step_0 hunk 1"),
    (@"                    {
                        global.flag[8] = 0;
                    }
                }
                if (global.is_console)
                {
                    if (global.submenucoord[30] == 3)
                    {
", @"                    {
                        global.flag[8] = 0;
                    }
                }
                if (global.submenucoord[30] == 5)
                {
                    global.secret_boss_challenge = 1 - global.secret_boss_challenge;
                    ossafe_ini_open(""true_config.ini"");
                    ini_write_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", global.secret_boss_challenge);
                    ossafe_ini_close();
                }
                if (global.is_console)
                {
                    if (global.submenucoord[30] == 3)
                    {
", "ch1/gml_Object_obj_darkcontroller_Step_0 hunk 2"),
    (@"                            check_border = 1;
                            border_select = 0;
                        }
                    }
                    if (global.submenucoord[30] == 5)
                    {
                        global.submenu = 34;
                    }
                    if (global.submenucoord[30] == 6)
                    {
                        m_quit = 1;
                    }
                }
", @"                            check_border = 1;
                            border_select = 0;
                        }
                    }
                    if (global.submenucoord[30] == 6)
                    {
                        global.submenu = 34;
                    }
                    if (global.submenucoord[30] == 7)
                    {
                        m_quit = 1;
                    }
                }
", "ch1/gml_Object_obj_darkcontroller_Step_0 hunk 3"),
    (@"                        {
                            global.flag[11] = 0;
                        }
                    }
                    if (global.submenucoord[30] == 5)
                    {
                        global.submenu = 34;
                    }
                    if (global.submenucoord[30] == 6)
                    {
                        m_quit = 1;
                    }
                }
", @"                        {
                            global.flag[11] = 0;
                        }
                    }
                    if (global.submenucoord[30] == 6)
                    {
                        global.submenu = 34;
                    }
                    if (global.submenucoord[30] == 7)
                    {
                        m_quit = 1;
                    }
                }
", "ch1/gml_Object_obj_darkcontroller_Step_0 hunk 4"),
});

PatchCode("gml_Object_obj_darkcontroller_Draw_0", new[]
{
    (@"        if (global.flag[8] == 1)
        {
            flashoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_80_1"");
        }
        shakeoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_81_0"");
        if (global.flag[12] == 1)
        {
            shakeoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_81_1"");
", @"        if (global.flag[8] == 1)
        {
            flashoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_80_1"");
        }
        challengeoff = ""OFF"";
        if (global.secret_boss_challenge == 1)
        {
            challengeoff = ""ON"";
        }
        shakeoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_81_0"");
        if (global.flag[12] == 1)
        {
            shakeoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_81_1"");
", "ch1/gml_Object_obj_darkcontroller_Draw_0 hunk 1"),
    (@"            {
                fullscreenoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_82_1"");
            }
        }
        draw_sprite(spr_heart, 0, _heartXPos, yy + 160 + (global.submenucoord[30] * 35));
        if (global.submenu == 33)
        {
            draw_set_color(c_yellow);
        }
", @"            {
                fullscreenoff = scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_82_1"");
            }
        }
        var _configHeartY = yy + 160 + (global.submenucoord[30] * 35);
        if (global.submenucoord[30] >= 5)
        {
            _configHeartY = yy + 335 + ((global.submenucoord[30] - 5) * 25);
        }
        draw_sprite(spr_heart, 0, _heartXPos, _configHeartY);
        if (global.submenu == 33)
        {
            draw_set_color(c_yellow);
        }
", "ch1/gml_Object_obj_darkcontroller_Draw_0 hunk 2"),
    (@"            }
            draw_text(_xPos, yy + 290, border_text);
            draw_text(_selectXPos, yy + 290, border_options[selected_border]);
            draw_set_color(c_white);
            draw_text(_xPos, yy + 325, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 360, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_96_0"")));
        }
        else
        {
            draw_text(_xPos, yy + 255, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_93_0"")));
            draw_text(xx + 430, yy + 255, string_hash_to_newline(fullscreenoff));
            draw_text(_xPos, yy + 290, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_94_0"")));
            draw_text(xx + 430, yy + 290, string_hash_to_newline(runoff));
            draw_text(_xPos, yy + 325, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 360, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_96_0"")));
        }
    }
    if (global.submenu == 34)
    {
", @"            }
            draw_text(_xPos, yy + 290, border_text);
            draw_text(_selectXPos, yy + 290, border_options[selected_border]);
            draw_set_color(c_white);
            draw_text(_xPos, yy + 325, ""Boss Challenge"");
            draw_text(_selectXPos, yy + 325, challengeoff);
            draw_text(_xPos, yy + 350, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 375, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_96_0"")));
        }
        else
        {
            draw_text(_xPos, yy + 255, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_93_0"")));
            draw_text(xx + 430, yy + 255, string_hash_to_newline(fullscreenoff));
            draw_text(_xPos, yy + 290, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_94_0"")));
            draw_text(xx + 430, yy + 290, string_hash_to_newline(runoff));
            draw_text(_xPos, yy + 325, ""Boss Challenge"");
            draw_text(_selectXPos, yy + 325, challengeoff);
            draw_text(_xPos, yy + 350, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 375, string_hash_to_newline(scr_84_get_lang_string(""obj_darkcontroller_slash_Draw_0_gml_96_0"")));
        }
    }
    if (global.submenu == 34)
    {
", "ch1/gml_Object_obj_darkcontroller_Draw_0 hunk 3"),
});

PatchCode("gml_Object_obj_joker_Create_0", new[]
{
    (@"beepbuffer = 0;
burstnoise = 0;
jturn = 0;
jattack = 0;
global.tempflag[4] = 1;
", @"beepbuffer = 0;
burstnoise = 0;
jturn = 0;
jattack = 0;
if (!variable_global_exists(""secret_boss_challenge""))
{
    ossafe_ini_open(""true_config.ini"");
    global.secret_boss_challenge = ini_read_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", 0);
    ossafe_ini_close();
}
hardmode = global.secret_boss_challenge;
global.tempflag[4] = 1;
", "ch1/gml_Object_obj_joker_Create_0 hunk 1"),
});

PatchCode("gml_GlobalScript_scr_monstersetup", new[]
{
    (@"        global.monstermaxhp[myself] = 3500;
        global.monsterhp[myself] = 3500;
        global.monsterat[myself] = 10;
        global.monsterdf[myself] = 5;
        global.monsterexp[myself] = 0;
        global.monstergold[myself] = 0;
        global.sparepoint[myself] = 0;
        global.mercymod[myself] = 0;
", @"        global.monstermaxhp[myself] = 3500;
        global.monsterhp[myself] = 3500;
        global.monsterat[myself] = 10;
        global.monsterdf[myself] = 5;
        if (variable_global_exists(""secret_boss_challenge"") && global.secret_boss_challenge == 1)
        {
            global.monstermaxhp[myself] = 4550;
            global.monsterhp[myself] = 4550;
            global.monsterat[myself] = 12;
            global.monsterdf[myself] = 6;
        }
        global.monsterexp[myself] = 0;
        global.monstergold[myself] = 0;
        global.sparepoint[myself] = 0;
        global.mercymod[myself] = 0;
", "ch1/gml_GlobalScript_scr_monstersetup hunk 1"),
});

PatchCode("gml_Object_obj_joker_Other_15", new[]
{
    (@"        dc.target = mytarget;
        dc.damage = global.monsterat[myself] * 4;
        global.turntimer = 300;
    }
    with (obj_dbulletcontroller)
    {
        joker = 1;
    }
", @"        dc.target = mytarget;
        dc.damage = global.monsterat[myself] * 4;
        global.turntimer = 300;
    }
    if (hardmode == 1 && instance_exists(dc))
    {
        dc.damage = ceil(dc.damage * 1.15);
        if (global.turntimer < 1000)
        {
            global.turntimer = ceil(global.turntimer * 1.15);
        }
    }
    with (obj_dbulletcontroller)
    {
        joker = 1;
    }
", "ch1/gml_Object_obj_joker_Other_15 hunk 1"),
});

PatchCode("gml_Object_obj_jokerbattleevent_Step_0", new[]
{
    (@"    instance_create(0, 0, obj_dialoguer);
}
if (con == 34 && !d_ex())
{
    if (global.flag[241] == 6)
    {
        scr_weaponget(7);
        if (noroom == 0)
        {
", @"    instance_create(0, 0, obj_dialoguer);
}
if (con == 34 && !d_ex())
{
    if (variable_global_exists(""secret_boss_challenge"") && global.secret_boss_challenge == 1)
    {
        var challenge_pending = 0;
        scr_weaponget(7);
        if (noroom == 1)
        {
            challenge_pending += 1;
        }
        scr_armorget(7);
        if (noroom == 1)
        {
            challenge_pending += 2;
        }
        global.flag[242] = challenge_pending;
        global.msg[0] = ""* (You got the Devilsknife.)/%"";
        global.msg[1] = ""* (You got the Jevilstail.)/%"";
        if (challenge_pending > 0)
        {
            global.msg[2] = ""* (... but your inventory was full.)/%"";
        }
    }
    else if (global.flag[241] == 6)
    {
        scr_weaponget(7);
        if (noroom == 0)
        {
", "ch1/gml_Object_obj_jokerbattleevent_Step_0 hunk 1"),
});

PatchCode("gml_Object_obj_treasure_room_Create_0", new[]
{
    (@"        instance_destroy();
    }
    else
    {
        if (global.flag[242] == 1)
        {
            itemflag = 112;
            itemtype = ""weapon"";
            t_itemid = 7;
", @"        instance_destroy();
    }
    else
    {
        if (global.flag[242] == 1 || global.flag[242] == 3)
        {
            itemflag = 112;
            itemtype = ""weapon"";
            t_itemid = 7;
", "ch1/gml_Object_obj_treasure_room_Create_0 hunk 1"),
});

PatchCode("gml_Object_obj_treasure_room_Other_10", new[]
{
    (@"            {
                equipcon = 1;
            }
        }
        global.flag[itemflag] = 1;
        with (obj_event_manager)
        {
            trigger_event(UnknownEnum.Value_0, UnknownEnum.Value_23, UnknownEnum.Value_939);
        }
", @"            {
                equipcon = 1;
            }
        }
        if (room == room_cc_prison_prejoker && global.flag[242] == 3)
        {
            global.flag[242] = 2;
        }
        else
        {
            global.flag[itemflag] = 1;
            if (room == room_cc_prison_prejoker)
            {
                global.flag[242] = 0;
            }
        }
        with (obj_event_manager)
        {
            trigger_event(UnknownEnum.Value_0, UnknownEnum.Value_23, UnknownEnum.Value_939);
        }
", "ch1/gml_Object_obj_treasure_room_Other_10 hunk 1"),
});

importGroup.Import();
