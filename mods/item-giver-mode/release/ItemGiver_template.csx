using System;
using System.Linq;
using UndertaleModLib.Models;
using UndertaleModLib.Decompiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Item Giver requires the DELTARUNE full release.");
    return;
}

string displayName = Data?.GeneralInfo?.DisplayName?.Content?.ToLowerInvariant() ?? "";
if (!displayName.Contains("chapter 1") && !displayName.Contains("chapitre 1"))
{
    ScriptError("This Item Giver patch is for Chapter 1 only.");
    return;
}

const string marker = "__ITEM_GIVER_MODE_V014__";
if (Data.Strings.Any(x => x?.Content == marker))
{
    ScriptMessage("Item Giver v0.1.4 is already installed for Chapter 1.");
    return;
}
Data.Strings.MakeString(marker);

GlobalDecompileContext globalDecompileContext = new(Data);
IDecompileSettings decompilerSettings = new DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true
};

UndertaleGameObject objTime = Data.GameObjects.ByName("obj_time");
if (objTime == null)
{
    ScriptError("Item Giver could not find DELTARUNE's persistent obj_time controller.");
    return;
}

importGroup.QueueAppend(objTime.EventHandlerFor(EventType.Create, (uint)0, Data),
@"ig_itemgiver_version = ""0.1.4"";
ig_menu_open = false;
ig_lists_built = false;
ig_category = 0;
ig_selected = 0;
ig_status_text = """";
ig_status_timer = 0;
ig_status_ok = true;
ig_saved_interact = 0;
ig_category_names = [""ITEMS"", ""WEAPONS"", ""ARMOR"", ""KEY ITEMS"", ""LIGHT ITEMS""];
ig_item_ids = [];
ig_item_names = [];
ig_weapon_ids = [];
ig_weapon_names = [];
ig_armor_ids = [];
ig_armor_names = [];
ig_key_ids = [];
ig_key_names = [];
ig_light_ids = [];
ig_light_names = [];
ig_preview_id = -1;
ig_preview_name = """";
ig_preview_desc = """";
ig_preview_extra = """";

ig_get_count = function()
{
    switch (ig_category)
    {
        case 0: return array_length(ig_item_ids);
        case 1: return array_length(ig_weapon_ids);
        case 2: return array_length(ig_armor_ids);
        case 3: return array_length(ig_key_ids);
        case 4: return array_length(ig_light_ids);
    }
    return 0;
};

ig_get_id = function(_index)
{
    switch (ig_category)
    {
        case 0: return ig_item_ids[_index];
        case 1: return ig_weapon_ids[_index];
        case 2: return ig_armor_ids[_index];
        case 3: return ig_key_ids[_index];
        case 4: return ig_light_ids[_index];
    }
    return -1;
};

ig_get_name = function(_index)
{
    switch (ig_category)
    {
        case 0: return string_replace_all(ig_item_names[_index], ""#"", "" "");
        case 1: return string_replace_all(ig_weapon_names[_index], ""#"", "" "");
        case 2: return string_replace_all(ig_armor_names[_index], ""#"", "" "");
        case 3: return string_replace_all(ig_key_names[_index], ""#"", "" "");
        case 4: return string_replace_all(ig_light_names[_index], ""#"", "" "");
    }
    return """";
};

ig_update_preview = function()
{
    var _count = ig_get_count();
    ig_preview_id = -1;
    ig_preview_name = """";
    ig_preview_desc = """";
    ig_preview_extra = """";
    if (_count <= 0)
        return;

    ig_selected = clamp(ig_selected, 0, _count - 1);
    ig_preview_id = ig_get_id(ig_selected);
    ig_preview_name = ig_get_name(ig_selected);

    switch (ig_category)
    {
        case 0:
            itemdescb = """";
            scr_iteminfo(ig_preview_id);
            ig_preview_desc = string(itemdescb);
            break;
        case 1:
            weapondesctemp = """";
            scr_weaponinfo(ig_preview_id);
            ig_preview_desc = string(weapondesctemp);
            ig_preview_extra = ""AT "" + string(weaponattemp) + ""   DF "" + string(weapondftemp) + ""   MAG "" + string(weaponmagtemp);
            if (string_length(string_replace_all(string(weaponabilitytemp), "" "", """")) > 0)
                ig_preview_extra += ""   "" + string(weaponabilitytemp);
            break;
        case 2:
            armordesctemp = """";
            scr_armorinfo(ig_preview_id);
            ig_preview_desc = string(armordesctemp);
            ig_preview_extra = ""AT "" + string(armorattemp) + ""   DF "" + string(armordftemp) + ""   MAG "" + string(armormagtemp);
            if (string_length(string_replace_all(string(armorabilitytemp), "" "", """")) > 0)
                ig_preview_extra += ""   "" + string(armorabilitytemp);
            break;
        case 3:
            tempkeyitemdesc = """";
            scr_keyiteminfo(ig_preview_id);
            ig_preview_desc = string(tempkeyitemdesc);
            break;
        case 4:
            ig_preview_desc = ""Light World inventory item."";
            break;
    }
};

ig_build_lists = function()
{
    ig_item_ids = [];
    ig_item_names = [];
    ig_weapon_ids = [];
    ig_weapon_names = [];
    ig_armor_ids = [];
    ig_armor_names = [];
    ig_key_ids = [];
    ig_key_names = [];
    ig_light_ids = [];
    ig_light_names = [];

    for (var _id = 1; _id <= 255; _id++)
    {
        itemnameb = "" "";
        scr_iteminfo(_id);
        var _name = string(itemnameb);
        var _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(ig_item_ids, _id);
            array_push(ig_item_names, _name);
        }

        weaponnametemp = "" "";
        scr_weaponinfo(_id);
        _name = string(weaponnametemp);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(ig_weapon_ids, _id);
            array_push(ig_weapon_names, _name);
        }

        armornametemp = "" "";
        scr_armorinfo(_id);
        _name = string(armornametemp);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(ig_armor_ids, _id);
            array_push(ig_armor_names, _name);
        }

        tempkeyitemname = "" "";
        scr_keyiteminfo(_id);
        _name = string(tempkeyitemname);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(ig_key_ids, _id);
            array_push(ig_key_names, _name);
        }
    }

    var _old_light_item = global.litem[7];
    for (var _id = 1; _id <= 255; _id++)
    {
        global.litem[7] = _id;
        global.litemname[7] = "" "";
        scr_litemname();
        var _name = string(global.litemname[7]);
        var _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(ig_light_ids, _id);
            array_push(ig_light_names, _name);
        }
    }
    global.litem[7] = _old_light_item;
    scr_litemname();

    ig_lists_built = true;
    ig_selected = 0;
    ig_update_preview();
};

ig_close_menu = function()
{
    ig_menu_open = false;
    if (variable_global_exists(""interact""))
        global.interact = ig_saved_interact;
};
");

importGroup.QueueAppend(objTime.EventHandlerFor(EventType.Step, (uint)0, Data),
@"if (ig_status_timer > 0)
    ig_status_timer--;

if (!ig_menu_open)
{
    if (keyboard_check_pressed(vk_f7))
    {
        if (instance_exists(obj_battlecontroller))
        {
            ig_status_text = ""Item Giver is unavailable during battle."";
            ig_status_ok = false;
            ig_status_timer = 120;
        }
        else if (!variable_global_exists(""item""))
        {
            ig_status_text = ""Load a save before opening Item Giver."";
            ig_status_ok = false;
            ig_status_timer = 120;
        }
        else
        {
            if (!ig_lists_built)
                ig_build_lists();

            if (variable_global_exists(""interact""))
            {
                ig_saved_interact = global.interact;
                global.interact = 1;
            }
            ig_menu_open = true;
            ig_update_preview();
        }
    }
}
else
{
    if (instance_exists(obj_battlecontroller))
    {
        ig_close_menu();
    }
    else
    {
        if (variable_global_exists(""interact""))
            global.interact = 1;

        if (keyboard_check_pressed(vk_f7) || keyboard_check_pressed(vk_escape) || keyboard_check_pressed(ord(""X"")))
        {
            ig_close_menu();
        }
        else
        {
            var _changed = false;
            if (keyboard_check_pressed(vk_left))
            {
                ig_category = (ig_category + 4) mod 5;
                ig_selected = 0;
                _changed = true;
            }
            if (keyboard_check_pressed(vk_right))
            {
                ig_category = (ig_category + 1) mod 5;
                ig_selected = 0;
                _changed = true;
            }

            var _count = ig_get_count();
            if (_count > 0)
            {
                if (keyboard_check_pressed(vk_up))
                {
                    ig_selected = (ig_selected + _count - 1) mod _count;
                    _changed = true;
                }
                if (keyboard_check_pressed(vk_down))
                {
                    ig_selected = (ig_selected + 1) mod _count;
                    _changed = true;
                }
                if (keyboard_check_pressed(vk_pageup))
                {
                    ig_selected = max(0, ig_selected - 10);
                    _changed = true;
                }
                if (keyboard_check_pressed(vk_pagedown))
                {
                    ig_selected = min(_count - 1, ig_selected + 10);
                    _changed = true;
                }
                if (keyboard_check_pressed(vk_home))
                {
                    ig_selected = 0;
                    _changed = true;
                }
                if (keyboard_check_pressed(vk_end))
                {
                    ig_selected = _count - 1;
                    _changed = true;
                }
            }

            if (keyboard_check_pressed(ord(""R"")))
            {
                ig_build_lists();
                ig_status_text = ""Item tables refreshed."";
                ig_status_ok = true;
                ig_status_timer = 120;
                _changed = true;
            }

            if (_changed)
                ig_update_preview();

            _count = ig_get_count();
            if (_count > 0 && (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(ord(""Z""))))
            {
                var _id = ig_get_id(ig_selected);
                var _name = ig_get_name(ig_selected);
                noroom = 0;

                switch (ig_category)
                {
                    case 0: scr_itemget(_id); break;
                    case 1: scr_weaponget(_id); break;
                    case 2: scr_armorget(_id); break;
                    case 3: scr_keyitemget(_id); break;
                    case 4: scr_litemget(_id); break;
                }

                if (noroom == 1)
                {
                    ig_status_text = ""No inventory space for "" + _name + ""."";
                    ig_status_ok = false;
                }
                else
                {
                    ig_status_text = ""Added "" + _name + "" (ID "" + string(_id) + "")."";
                    ig_status_ok = true;
                }
                ig_status_timer = 180;
                ig_update_preview();
            }
        }
    }
}");

importGroup.QueueAppend(objTime.EventHandlerFor(EventType.Draw, (uint)75, Data),
@"var _old_font = draw_get_font();
var _old_color = draw_get_color();
var _old_alpha = draw_get_alpha();
var _old_halign = draw_get_halign();
var _old_valign = draw_get_valign();

draw_set_font(fnt_main);
draw_set_halign(fa_left);
draw_set_valign(fa_top);

var _gw = display_get_gui_width();
var _gh = display_get_gui_height();

if (!ig_menu_open)
{
    if (ig_status_timer > 0)
    {
        var _toast_w = min(440, _gw - 24);
        var _toast_x = (_gw - _toast_w) * 0.5;
        draw_set_alpha(0.94);
        draw_set_color(c_black);
        draw_rectangle(_toast_x, 12, _toast_x + _toast_w, 45, false);
        draw_set_alpha(1);
        draw_set_color(ig_status_ok ? c_lime : c_red);
        draw_set_halign(fa_center);
        draw_text_ext(_gw * 0.5, 20, ig_status_text, 16, _toast_w - 20);
    }
}
else
{
    var _panel_w = min(616, _gw - 24);
    var _panel_h = min(456, _gh - 24);
    var _px = (_gw - _panel_w) * 0.5;
    var _py = (_gh - _panel_h) * 0.5;
    var _left = _px + 18;
    var _right = _px + _panel_w - 18;
    var _inner_w = _panel_w - 36;

    draw_set_alpha(0.72);
    draw_set_color(c_black);
    draw_rectangle(0, 0, _gw, _gh, false);

    draw_set_alpha(0.985);
    draw_set_color(make_color_rgb(12, 12, 20));
    draw_rectangle(_px, _py, _px + _panel_w, _py + _panel_h, false);
    draw_set_alpha(1);
    draw_set_color(c_white);
    draw_rectangle(_px, _py, _px + _panel_w, _py + _panel_h, true);

    draw_set_halign(fa_center);
    draw_set_color(c_fuchsia);
    draw_text(_gw * 0.5, _py + 14, ""ITEM GIVER"");
    draw_set_color(c_white);
    draw_text(_gw * 0.5, _py + 37, ""<  "" + ig_category_names[ig_category] + ""  >"");

    var _count = ig_get_count();
    draw_set_halign(fa_left);
    draw_set_color(c_gray);
    draw_text(_left + 6, _py + 65, ""ID"");
    draw_text(_left + 55, _py + 65, ""NAME"");
    draw_set_halign(fa_right);
    draw_text(_right - 4, _py + 65, string(ig_selected + 1) + "" / "" + string(_count));

    var _visible = 9;
    var _row_h = 21;
    var _list_y = _py + 86;
    var _start = clamp(ig_selected - 4, 0, max(0, _count - _visible));
    for (var _row = 0; _row < _visible; _row++)
    {
        var _index = _start + _row;
        if (_index >= _count)
            break;

        var _y = _list_y + (_row * _row_h);
        var _id = ig_get_id(_index);
        var _name = ig_get_name(_index);
        if (string_length(_name) > 30)
            _name = string_copy(_name, 1, 27) + ""..."";

        if (_index == ig_selected)
        {
            draw_set_alpha(0.28);
            draw_set_color(c_fuchsia);
            draw_rectangle(_left, _y - 2, _right, _y + 18, false);
            draw_set_alpha(1);
            draw_set_color(c_yellow);
            draw_set_halign(fa_left);
            draw_text(_left + 6, _y, ""> "" + string(_id));
            draw_text(_left + 55, _y, _name);
        }
        else
        {
            draw_set_color(c_white);
            draw_set_halign(fa_left);
            draw_text(_left + 6, _y, string(_id));
            draw_text(_left + 55, _y, _name);
        }
    }

    var _sep_y = _py + 284;
    draw_set_color(c_gray);
    draw_line(_left, _sep_y, _right, _sep_y);

    draw_set_halign(fa_left);
    draw_set_color(c_yellow);
    var _preview_title = ig_preview_name;
    if (string_length(_preview_title) > 34)
        _preview_title = string_copy(_preview_title, 1, 31) + ""..."";
    draw_text(_left + 6, _sep_y + 10, _preview_title + ""  [ID "" + string(ig_preview_id) + ""]"");

    draw_set_color(c_white);
    var _desc = string_hash_to_newline(ig_preview_desc);
    draw_text_ext(_left + 6, _sep_y + 33, _desc, 16, _inner_w - 12);

    if (string_length(ig_preview_extra) > 0)
    {
        draw_set_color(c_aqua);
        draw_text_ext(_left + 6, _py + _panel_h - 76, string_replace_all(ig_preview_extra, ""#"", "" ""), 16, _inner_w - 12);
    }

    if (ig_status_timer > 0)
    {
        draw_set_color(ig_status_ok ? c_lime : c_red);
        draw_text_ext(_left + 6, _py + _panel_h - 55, string_replace_all(ig_status_text, ""#"", "" ""), 16, _inner_w - 12);
    }

    draw_set_color(c_gray);
    draw_set_halign(fa_center);
    draw_text(_gw * 0.5, _py + _panel_h - 24, ""Arrows: Navigate   PgUp/PgDn: Jump   Z/Enter: Give   R: Refresh   X/Esc/F7: Close"");
}

draw_set_halign(_old_halign);
draw_set_valign(_old_valign);
draw_set_font(_old_font);
draw_set_color(_old_color);
draw_set_alpha(_old_alpha);
");

importGroup.QueueAppend(objTime.EventHandlerFor(EventType.CleanUp, (uint)0, Data),
@"if (ig_menu_open && variable_global_exists(""interact""))
    global.interact = ig_saved_interact;
");

importGroup.Import();
ScriptMessage("Item Giver v0.1.4 installed for Chapter 1. Press F7 outside battle.");
