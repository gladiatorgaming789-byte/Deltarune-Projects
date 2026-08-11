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

if (Data.Code.ByName("gml_Object_obj_spamton_neo_enemy_Create_0") is null)
    throw new Exception("Secret Boss Challenge: this patch targets DELTARUNE Chapter 2.");

PatchCode("gml_Object_obj_darkcontroller_Create_0", new[]
{
    (@"mmy[1] = 0;
mmy[2] = 0;
mmy[3] = 0;
global.submenu = 0;
global.charselect = -1;
for (var i = 0; i < 36; i += 1)
{
    global.submenucoord[i] = 0;
", @"mmy[1] = 0;
mmy[2] = 0;
mmy[3] = 0;
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
", "ch2/gml_Object_obj_darkcontroller_Create_0 hunk 1"),
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
", "ch2/gml_Object_obj_darkcontroller_Step_0 hunk 1"),
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
", "ch2/gml_Object_obj_darkcontroller_Step_0 hunk 2"),
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
                        cancelnoise = 1;
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
                        cancelnoise = 1;
                    }
", "ch2/gml_Object_obj_darkcontroller_Step_0 hunk 3"),
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
                        cancelnoise = 1;
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
                        cancelnoise = 1;
                    }
", "ch2/gml_Object_obj_darkcontroller_Step_0 hunk 4"),
});

PatchCode("gml_Object_obj_darkcontroller_Draw_0", new[]
{
    (@"        if (global.flag[8] == 1)
        {
            flashoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_83_1"");
        }
        shakeoff = stringsetloc(""OFF"", ""obj_darkcontroller_slash_Draw_0_gml_84_0"");
        if (global.flag[12] == 1)
        {
            shakeoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_84_1"");
", @"        if (global.flag[8] == 1)
        {
            flashoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_83_1"");
        }
        challengeoff = ""OFF"";
        if (global.secret_boss_challenge == 1)
        {
            challengeoff = ""ON"";
        }
        shakeoff = stringsetloc(""OFF"", ""obj_darkcontroller_slash_Draw_0_gml_84_0"");
        if (global.flag[12] == 1)
        {
            shakeoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_84_1"");
", "ch2/gml_Object_obj_darkcontroller_Draw_0 hunk 1"),
    (@"            {
                fullscreenoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_87_1"");
            }
        }
        draw_sprite(spr_heart, 0, _heartXPos, yy + 160 + (global.submenucoord[30] * 35));
        if (global.submenu == 33)
        {
            draw_set_color(c_yellow);
        }
", @"            {
                fullscreenoff = stringsetloc(""ON"", ""obj_darkcontroller_slash_Draw_0_gml_87_1"");
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
", "ch2/gml_Object_obj_darkcontroller_Draw_0 hunk 2"),
    (@"            }
            draw_text(_xPos, yy + 290, stringsetloc(""Border"", ""obj_darkcontroller_slash_Draw_0_gml_112_0""));
            draw_text(_selectXPos, yy + 290, border_options[selected_border]);
            draw_set_color(c_white);
            draw_text(_xPos, yy + 325, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 360, string_hash_to_newline(back_text));
        }
        else
        {
            draw_text(_xPos, yy + 255, string_hash_to_newline(stringsetloc(""Fullscreen"", ""obj_darkcontroller_slash_Draw_0_gml_93_0"")));
            draw_text(xx + 430, yy + 255, string_hash_to_newline(fullscreenoff));
            draw_text(_xPos, yy + 290, string_hash_to_newline(autorun_text));
            draw_text(xx + 430, yy + 290, string_hash_to_newline(runoff));
            draw_text(_xPos, yy + 325, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 360, string_hash_to_newline(back_text));
        }
    }
    if (global.submenu == 34)
    {
", @"            }
            draw_text(_xPos, yy + 290, stringsetloc(""Border"", ""obj_darkcontroller_slash_Draw_0_gml_112_0""));
            draw_text(_selectXPos, yy + 290, border_options[selected_border]);
            draw_set_color(c_white);
            draw_text(_xPos, yy + 325, ""Boss Challenge"");
            draw_text(_selectXPos, yy + 325, challengeoff);
            draw_text(_xPos, yy + 350, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 375, string_hash_to_newline(back_text));
        }
        else
        {
            draw_text(_xPos, yy + 255, string_hash_to_newline(stringsetloc(""Fullscreen"", ""obj_darkcontroller_slash_Draw_0_gml_93_0"")));
            draw_text(xx + 430, yy + 255, string_hash_to_newline(fullscreenoff));
            draw_text(_xPos, yy + 290, string_hash_to_newline(autorun_text));
            draw_text(xx + 430, yy + 290, string_hash_to_newline(runoff));
            draw_text(_xPos, yy + 325, ""Boss Challenge"");
            draw_text(_selectXPos, yy + 325, challengeoff);
            draw_text(_xPos, yy + 350, string_hash_to_newline(stringsetloc(""Return to Title"", ""obj_darkcontroller_slash_Draw_0_gml_95_0"")));
            draw_text(_xPos, yy + 375, string_hash_to_newline(back_text));
        }
    }
    if (global.submenu == 34)
    {
", "ch2/gml_Object_obj_darkcontroller_Draw_0 hunk 3"),
});

PatchCode("gml_Object_obj_spamton_neo_enemy_Create_0", new[]
{
    (@"funnycheattimer = 0;
funnycheattimer2 = 0;
funnycheattimer3 = 0;
headexpand = 0;
hellmode = 0;
laserflash = irandom(3);
talkmax = 90;
image_speed = 0.16666666666666666;
idlesprite = spr_sneo_example;
", @"funnycheattimer = 0;
funnycheattimer2 = 0;
funnycheattimer3 = 0;
headexpand = 0;
if (!variable_global_exists(""secret_boss_challenge""))
{
    ossafe_ini_open(""true_config.ini"");
    global.secret_boss_challenge = ini_read_real(""MODS"", ""SECRET_BOSS_CHALLENGE"", 0);
    ossafe_ini_close();
}
hellmode = global.secret_boss_challenge;
laserflash = irandom(3);
talkmax = 90;
image_speed = 0.16666666666666666;
idlesprite = spr_sneo_example;
", "ch2/gml_Object_obj_spamton_neo_enemy_Create_0 hunk 1"),
});

PatchCode("gml_GlobalScript_scr_monstersetup", new[]
{
    (@"        global.monstermaxhp[myself] = 4809;
        global.monsterhp[myself] = 4809;
        global.monsterat[myself] = 13;
        global.monsterdf[myself] = 0;
        global.monsterexp[myself] = 0;
        global.monstergold[myself] = 0;
        global.sparepoint[myself] = 0;
        global.mercymod[myself] = 0;
", @"        global.monstermaxhp[myself] = 4809;
        global.monsterhp[myself] = 4809;
        global.monsterat[myself] = 13;
        global.monsterdf[myself] = 0;
        if (variable_global_exists(""secret_boss_challenge"") && global.secret_boss_challenge == 1)
        {
            global.monstermaxhp[myself] = 6000;
            global.monsterhp[myself] = 6000;
            global.monsterat[myself] = 15;
        }
        global.monsterexp[myself] = 0;
        global.monstergold[myself] = 0;
        global.sparepoint[myself] = 0;
        global.mercymod[myself] = 0;
", "ch2/gml_GlobalScript_scr_monstersetup hunk 1"),
});

PatchCode("gml_Object_obj_ch2_sceneex2a_Step_0", new[]
{
    (@"    c_msgsetloc(0, ""* (You got ShadowCrystal.)/%"", ""obj_ch2_sceneex2a_slash_Step_0_gml_95_0"");
    c_talk_wait();
    c_wait(5);
    noroom = 0;
    if (global.flag[571] == 1)
    {
        scr_weaponget(21);
        if (noroom == 1)
        {
", @"    c_msgsetloc(0, ""* (You got ShadowCrystal.)/%"", ""obj_ch2_sceneex2a_slash_Step_0_gml_95_0"");
    c_talk_wait();
    c_wait(5);
    noroom = 0;
    if (variable_global_exists(""secret_boss_challenge"") && global.secret_boss_challenge == 1)
    {
        var pending_weapon = 0;
        var pending_armor = 0;
        scr_weaponget(21);
        if (noroom == 1)
        {
            pending_weapon = 1;
        }
        c_soundplay(snd_item);
        c_speaker(""no_name"");
        c_msgsetloc(0, ""* (You got PuppetScarf.)/%"", ""obj_ch2_sceneex2a_slash_Step_0_gml_88_0"");
        c_talk_wait();
        global.flag[454] = 1;
        scr_armorget(21);
        if (noroom == 1)
        {
            pending_armor = 1;
        }
        c_soundplay(snd_item);
        c_speaker(""no_name"");
        c_msgsetloc(0, ""* (You got Dealmaker.)/%"", ""obj_ch2_sceneex2a_slash_Step_0_gml_82_0"");
        c_talk_wait();
        if (pending_weapon == 1 && pending_armor == 1)
            global.flag[468] = 5;
        else if (pending_weapon == 1)
            global.flag[468] = 3;
        else if (pending_armor == 1)
            global.flag[468] = 4;
        else
            global.flag[468] = 0;
        noroom = max(pending_weapon, pending_armor);
    }
    else if (global.flag[571] == 1)
    {
        scr_weaponget(21);
        if (noroom == 1)
        {
", "ch2/gml_Object_obj_ch2_sceneex2a_Step_0 hunk 1"),
});

PatchCode("gml_Object_obj_treasure_room_Create_0", new[]
{
    (@"    if (global.flag[142] == 1)
    {
        qualify = 0;
    }
    if (global.flag[571] == 1 || global.flag[468] == 2)
    {
        itemtype = ""weapon"";
    }
    else
    {
        itemtype = ""armor"";
    }
    if (scr_armorcheck_equipped_party(21) > 0 || scr_armorcheck_inventory(21) > 0 || scr_weaponcheck_inventory(21) > 0 || scr_weaponcheck_equipped_party(21) > 0)
    {
        qualify = 0;
    }
    if (qualify == 0)
", @"    if (global.flag[142] == 1)
    {
        qualify = 0;
    }
    if (global.flag[468] == 3 || global.flag[468] == 5)
        itemtype = ""weapon"";
    else if (global.flag[468] == 4)
        itemtype = ""armor"";
    else if (global.flag[571] == 1 || global.flag[468] == 2)
        itemtype = ""weapon"";
    else
        itemtype = ""armor"";
    if (global.flag[468] >= 3)
    {
        if (global.flag[468] == 5 && (scr_weaponcheck_inventory(21) > 0 || scr_weaponcheck_equipped_party(21) > 0))
        {
            global.flag[468] = 4;
            itemtype = ""armor"";
        }
        if (global.flag[468] == 3 && (scr_weaponcheck_inventory(21) > 0 || scr_weaponcheck_equipped_party(21) > 0))
            qualify = 0;
        if (global.flag[468] == 4 && (scr_armorcheck_inventory(21) > 0 || scr_armorcheck_equipped_party(21) > 0))
            qualify = 0;
    }
    else if (scr_armorcheck_equipped_party(21) > 0 || scr_armorcheck_inventory(21) > 0 || scr_weaponcheck_inventory(21) > 0 || scr_weaponcheck_equipped_party(21) > 0)
    {
        qualify = 0;
    }
    if (qualify == 0)
", "ch2/gml_Object_obj_treasure_room_Create_0 hunk 1"),
});

PatchCode("gml_Object_obj_treasure_room_Other_10", new[]
{
    (@"        }
    }
    if (noroom == 0)
    {
        global.flag[itemflag] = 1;
    }
    else
    {
        close = 1;
", @"        }
    }
    if (noroom == 0)
    {
        if ((room == room_dw_castle_west_cliff || room == room_dw_mansion_b_east_transformed) && global.flag[468] == 5)
        {
            global.flag[468] = 4;
        }
        else
        {
            global.flag[itemflag] = 1;
            if ((room == room_dw_castle_west_cliff || room == room_dw_mansion_b_east_transformed) && (global.flag[468] == 3 || global.flag[468] == 4))
            {
                global.flag[468] = 0;
            }
        }
    }
    else
    {
        close = 1;
", "ch2/gml_Object_obj_treasure_room_Other_10 hunk 1"),
});

importGroup.Import();
