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

if (Data.GameObjects.Any(x => x?.Name?.Content == "obj_item_giver"))
{
    ScriptMessage("Item Giver is already installed for Chapter 1.");
    return;
}

GlobalDecompileContext globalDecompileContext = new(Data);
IDecompileSettings decompilerSettings = new DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true
};

UndertaleGameObject objItemGiver = new UndertaleGameObject();
objItemGiver.Name = Data.Strings.MakeString("obj_item_giver");
objItemGiver.Visible = true;
objItemGiver.Persistent = true;
objItemGiver.Awake = true;
Data.GameObjects.Add(objItemGiver);

importGroup.QueueReplace(objItemGiver.EventHandlerFor(EventType.Create, (uint)0, Data),
@"depth = -999999;
menu_open = false;
lists_built = false;
category = 0;
selected = 0;
status_text = """";
status_timer = 0;
status_ok = true;
saved_interact = 0;
category_names = [""ITEMS"", ""WEAPONS"", ""ARMOR"", ""KEY ITEMS"", ""LIGHT ITEMS""];
item_ids = [];
item_names = [];
weapon_ids = [];
weapon_names = [];
armor_ids = [];
armor_names = [];
key_ids = [];
key_names = [];
light_ids = [];
light_names = [];
preview_id = -1;
preview_name = """";
preview_desc = """";
preview_extra = """";

get_count = function()
{
    switch (category)
    {
        case 0: return array_length(item_ids);
        case 1: return array_length(weapon_ids);
        case 2: return array_length(armor_ids);
        case 3: return array_length(key_ids);
        case 4: return array_length(light_ids);
    }
    return 0;
};

get_id = function(_index)
{
    switch (category)
    {
        case 0: return item_ids[_index];
        case 1: return weapon_ids[_index];
        case 2: return armor_ids[_index];
        case 3: return key_ids[_index];
        case 4: return light_ids[_index];
    }
    return -1;
};

get_name = function(_index)
{
    switch (category)
    {
        case 0: return item_names[_index];
        case 1: return weapon_names[_index];
        case 2: return armor_names[_index];
        case 3: return key_names[_index];
        case 4: return light_names[_index];
    }
    return """";
};

update_preview = function()
{
    var _count = get_count();
    preview_id = -1;
    preview_name = """";
    preview_desc = """";
    preview_extra = """";
    if (_count <= 0)
        return;

    selected = clamp(selected, 0, _count - 1);
    preview_id = get_id(selected);
    preview_name = get_name(selected);

    switch (category)
    {
        case 0:
            itemdescb = """";
            scr_iteminfo(preview_id);
            preview_desc = string(itemdescb);
            break;
        case 1:
            weapondesctemp = """";
            scr_weaponinfo(preview_id);
            preview_desc = string(weapondesctemp);
            preview_extra = ""AT "" + string(weaponattemp) + ""   DF "" + string(weapondftemp) + ""   MAG "" + string(weaponmagtemp);
            if (string_length(string_replace_all(string(weaponabilitytemp), "" "", """")) > 0)
                preview_extra += ""   "" + string(weaponabilitytemp);
            break;
        case 2:
            armordesctemp = """";
            scr_armorinfo(preview_id);
            preview_desc = string(armordesctemp);
            preview_extra = ""AT "" + string(armorattemp) + ""   DF "" + string(armordftemp) + ""   MAG "" + string(armormagtemp);
            if (string_length(string_replace_all(string(armorabilitytemp), "" "", """")) > 0)
                preview_extra += ""   "" + string(armorabilitytemp);
            break;
        case 3:
            tempkeyitemdesc = """";
            scr_keyiteminfo(preview_id);
            preview_desc = string(tempkeyitemdesc);
            break;
        case 4:
            preview_desc = ""Light World inventory item."";
            break;
    }
};

build_lists = function()
{
    item_ids = [];
    item_names = [];
    weapon_ids = [];
    weapon_names = [];
    armor_ids = [];
    armor_names = [];
    key_ids = [];
    key_names = [];
    light_ids = [];
    light_names = [];

    for (var _id = 1; _id <= 255; _id++)
    {
        itemnameb = "" "";
        scr_iteminfo(_id);
        var _name = string(itemnameb);
        var _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(item_ids, _id);
            array_push(item_names, _name);
        }

        weaponnametemp = "" "";
        scr_weaponinfo(_id);
        _name = string(weaponnametemp);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(weapon_ids, _id);
            array_push(weapon_names, _name);
        }

        armornametemp = "" "";
        scr_armorinfo(_id);
        _name = string(armornametemp);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(armor_ids, _id);
            array_push(armor_names, _name);
        }

        tempkeyitemname = "" "";
        scr_keyiteminfo(_id);
        _name = string(tempkeyitemname);
        _clean = string_replace_all(string_replace_all(_name, "" "", """"), ""#"", """");
        if (string_length(_clean) > 0)
        {
            array_push(key_ids, _id);
            array_push(key_names, _name);
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
            array_push(light_ids, _id);
            array_push(light_names, _name);
        }
    }
    global.litem[7] = _old_light_item;
    scr_litemname();

    lists_built = true;
    selected = 0;
    update_preview();
};

close_menu = function()
{
    menu_open = false;
    global.interact = saved_interact;
};
");

importGroup.QueueReplace(objItemGiver.EventHandlerFor(EventType.Step, (uint)0, Data),
@"if (status_timer > 0)
    status_timer--;

if (!menu_open)
{
    if (keyboard_check_pressed(vk_f8))
    {
        if (instance_exists(obj_battlecontroller))
        {
            status_text = ""Item Giver is unavailable during battle."";
            status_ok = false;
            status_timer = 120;
            exit;
        }

        if (!lists_built)
            build_lists();

        saved_interact = global.interact;
        global.interact = 1;
        menu_open = true;
        update_preview();
    }
    exit;
}

if (instance_exists(obj_battlecontroller))
{
    close_menu();
    exit;
}

global.interact = 1;

if (keyboard_check_pressed(vk_f8) || keyboard_check_pressed(vk_escape) || keyboard_check_pressed(ord(""X"")))
{
    close_menu();
    exit;
}

var _changed = false;
if (keyboard_check_pressed(vk_left))
{
    category = (category + 4) mod 5;
    selected = 0;
    _changed = true;
}
if (keyboard_check_pressed(vk_right))
{
    category = (category + 1) mod 5;
    selected = 0;
    _changed = true;
}

var _count = get_count();
if (_count > 0)
{
    if (keyboard_check_pressed(vk_up))
    {
        selected = (selected + _count - 1) mod _count;
        _changed = true;
    }
    if (keyboard_check_pressed(vk_down))
    {
        selected = (selected + 1) mod _count;
        _changed = true;
    }
    if (keyboard_check_pressed(vk_pageup))
    {
        selected = max(0, selected - 10);
        _changed = true;
    }
    if (keyboard_check_pressed(vk_pagedown))
    {
        selected = min(_count - 1, selected + 10);
        _changed = true;
    }
    if (keyboard_check_pressed(vk_home))
    {
        selected = 0;
        _changed = true;
    }
    if (keyboard_check_pressed(vk_end))
    {
        selected = _count - 1;
        _changed = true;
    }
}

if (keyboard_check_pressed(ord(""R"")))
{
    build_lists();
    status_text = ""Item tables refreshed."";
    status_ok = true;
    status_timer = 120;
    _changed = true;
}

if (_changed)
    update_preview();

if (_count > 0 && (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(ord(""Z""))))
{
    var _id = get_id(selected);
    var _name = get_name(selected);
    noroom = 0;

    switch (category)
    {
        case 0: scr_itemget(_id); break;
        case 1: scr_weaponget(_id); break;
        case 2: scr_armorget(_id); break;
        case 3: scr_keyitemget(_id); break;
        case 4: scr_litemget(_id); break;
    }

    if (norom == 1)
    {
        status_text = ""No inventory space for "" + _name + ""."";
        status_ok = false;
    }
    else
    {
        status_text = ""Added "" + _name + "" (ID "" + string(_id) + "")."";
        status_ok = true;
    }
    status_timer = 180;
    update_preview();
}
");

importGroup.QueueReplace(objItemGiver.EventHandlerFor(EventType.Draw, (uint)64, Data),
@"var _old_font = draw_get_font();
var _old_color = draw_get_color();
var _old_alpha = draw_get_alpha();
var _old_halign = draw_get_halign();
var _old_valign = draw_get_valign();

draw_set_font(fnt_main);
draw_set_halign(fa_left);
draw_set_valign(fa_top);

if (!menu_open)
{
    if (status_timer > 0)
    {
        draw_set_alpha(0.88);
        draw_set_color(c_black);
        draw_rectangle(120, 16, 520, 48, false);
        draw_set_alpha(1);
        draw_set_color(status_ok ? c_lime : c_red);
        draw_set_halign(fa_center);
        draw_text(320, 23, status_text);
    }

    draw_set_halign(_old_halign);
    draw_set_valign(_old_valign);
    draw_set_font(_old_font);
    draw_set_color(_old_color);
    draw_set_alpha(_old_alpha);
    exit;
}

draw_set_alpha(0.94);
draw_set_color(make_color_rgb(12, 12, 20));
draw_rectangle(18, 18, 622, 462, false);
draw_set_alpha(1);
draw_set_color(c_white);
draw_rectangle(18, 18, 622, 462, true);

draw_set_halign(fa_center);
draw_set_color(c_fuchsia);
draw_text(320, 31, ""ITEM GIVER"");
draw_set_color(c_white);
draw_text(320, 53, ""< "" + category_names[category] + "" >"");

var _count = get_count();
draw_set_halign(fa_left);
draw_set_color(c_gray);
draw_text(37, 73, ""ID"");
draw_text(78, 73, ""NAME"");
draw_set_halign(fa_right);
draw_text(603, 73, string(selected + 1) + "" / "" + string(_count));

var _visible = 10;
var _start = clamp(selected - 4, 0, max(0, _count - _visible));
for (var _row = 0; _row < _visible; _row++)
{
    var _index = _start + _row;
    if (_index >= _count)
        break;

    var _y = 94 + (_row * 22);
    var _id = get_id(_index);
    var _name = get_name(_index);
    if (_index == selected)
    {
        draw_set_alpha(0.25);
        draw_set_color(c_fuchsia);
        draw_rectangle(31, _y - 2, 609, _y + 18, false);
        draw_set_alpha(1);
        draw_set_color(c_yellow);
        draw_text(37, _y, ""> "" + string(_id));
        draw_text(78, _y, _name);
    }
    else
    {
        draw_set_color(c_white);
        draw_text(37, _y, string(_id));
        draw_text(78, _y, _name);
    }
}

draw_set_color(c_gray);
draw_line(31, 318, 609, 318);
draw_set_color(c_yellow);
draw_text(37, 327, preview_name + ""  [ID "" + string(preview_id) + ""]"");
draw_set_color(c_white);
draw_text(37, 350, string_hash_to_newline(preview_desc));
if (string_length(preview_extra) > 0)
{
    draw_set_color(c_aqua);
    draw_text(37, 395, preview_extra);
}

if (status_timer > 0)
{
    draw_set_color(status_ok ? c_lime : c_red);
    draw_text(37, 417, status_text);
}

draw_set_color(c_gray);
draw_set_halign(fa_center);
draw_text(320, 440, ""Arrows: Navigate   PgUp/PgDn: Jump   Z/Enter: Give   R: Refresh   X/Esc/F8: Close"");

draw_set_halign(_old_halign);
draw_set_valign(_old_valign);
draw_set_font(_old_font);
draw_set_color(_old_color);
draw_set_alpha(_old_alpha);
");

importGroup.QueueReplace(objItemGiver.EventHandlerFor(EventType.CleanUp, (uint)0, Data),
@"if (menu_open)
    global.interact = saved_interact;
");

importGroup.QueueAppend("gml_GlobalScript_scr_gamestart",
@"if (!instance_exists(obj_item_giver))
{
    instance_create(0, 0, obj_item_giver);
}
");

importGroup.Import();
ScriptMessage("Item Giver installed for Chapter 1. Press F8 outside battle.");
