// Item Giver Mode v0.3.1 runtime.
// Runs from a tiny call appended to obj_time Draw GUI End.

// Mode 1 is used by the real Controls handler to persist a changed binding.
if (argument_count > 0 && argument0 == 1)
{
    if (!variable_global_exists("ig_itemgiver_key"))
        global.ig_itemgiver_key = ord("I");
    if (variable_global_exists("filechoice"))
    {
        ossafe_ini_open("keyconfig_" + string(global.filechoice) + ".ini");
        ini_write_real("ITEM_GIVER", "KEYBOARD", global.ig_itemgiver_key);
        if (!global.is_console)
            ini_close();
        else
        {
            ossafe_ini_close();
            ossafe_savedata_save();
        }
    }
    return;
}

if (!variable_instance_exists(id, "ig_itemgiver_version"))
{
    ig_itemgiver_version = "0.3.1";
    ig_menu_open = false;
    ig_lists_built = false;
    ig_category = 0;
    ig_selected = 0;
    ig_status_text = "";
    ig_status_timer = 0;
    ig_status_ok = true;
    ig_saved_interact = 0;
    ig_loaded_slot = -999999;
    ig_category_names = ["ITEMS", "WEAPONS", "ARMOR", "KEY ITEMS", "LIGHT ITEMS"];
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
    ig_preview_name = "";
    ig_preview_desc = "";
    ig_preview_extra = "";
}

if (!variable_global_exists("ig_itemgiver_key"))
    global.ig_itemgiver_key = ord("I");

// Load the per-slot binding once the game has a real save slot selected.
if (variable_global_exists("filechoice") && ig_loaded_slot != global.filechoice)
{
    ig_loaded_slot = global.filechoice;
    global.ig_itemgiver_key = ord("I");
    var _ig_cfg = "keyconfig_" + string(global.filechoice) + ".ini";
    if (ossafe_file_exists(_ig_cfg))
    {
        ossafe_ini_open(_ig_cfg);
        global.ig_itemgiver_key = ini_read_real("ITEM_GIVER", "KEYBOARD", ord("I"));
        if (!global.is_console)
            ini_close();
        else
            ossafe_ini_close();
    }
}

if (ig_status_timer > 0)
    ig_status_timer--;

var _ig_rebuild = false;
var _ig_need_preview = false;
var _ig_bind_pressed = keyboard_check_pressed(global.ig_itemgiver_key);
var _ig_controls_active = variable_global_exists("submenu") && global.submenu == 35 && instance_exists(obj_darkcontroller);

if (!ig_menu_open)
{
    if (_ig_bind_pressed && !_ig_controls_active)
    {
        if (instance_exists(obj_battlecontroller))
        {
            ig_status_text = "Item Giver is unavailable during battle.";
            ig_status_ok = false;
            ig_status_timer = 120;
        }
        else if (!variable_global_exists("item"))
        {
            ig_status_text = "Load a save before opening Item Giver.";
            ig_status_ok = false;
            ig_status_timer = 120;
        }
        else
        {
            if (!ig_lists_built)
                _ig_rebuild = true;

            if (variable_global_exists("interact"))
            {
                ig_saved_interact = global.interact;
                global.interact = 1;
            }
            ig_menu_open = true;
            _ig_need_preview = true;
        }
    }
}
else
{
    if (instance_exists(obj_battlecontroller))
    {
        ig_menu_open = false;
        if (variable_global_exists("interact"))
            global.interact = ig_saved_interact;
    }
    else
    {
        if (variable_global_exists("interact"))
            global.interact = 1;

        if (_ig_bind_pressed || keyboard_check_pressed(vk_escape) || keyboard_check_pressed(ord("X")))
        {
            ig_menu_open = false;
            if (variable_global_exists("interact"))
                global.interact = ig_saved_interact;
        }
        else
        {
            var _ig_ids = ig_item_ids;
            switch (ig_category)
            {
                case 1: _ig_ids = ig_weapon_ids; break;
                case 2: _ig_ids = ig_armor_ids; break;
                case 3: _ig_ids = ig_key_ids; break;
                case 4: _ig_ids = ig_light_ids; break;
            }
            var _ig_count = array_length(_ig_ids);

            if (keyboard_check_pressed(vk_left))
            {
                ig_category = (ig_category + 4) mod 5;
                ig_selected = 0;
                _ig_need_preview = true;
            }
            if (keyboard_check_pressed(vk_right))
            {
                ig_category = (ig_category + 1) mod 5;
                ig_selected = 0;
                _ig_need_preview = true;
            }

            _ig_ids = ig_item_ids;
            switch (ig_category)
            {
                case 1: _ig_ids = ig_weapon_ids; break;
                case 2: _ig_ids = ig_armor_ids; break;
                case 3: _ig_ids = ig_key_ids; break;
                case 4: _ig_ids = ig_light_ids; break;
            }
            _ig_count = array_length(_ig_ids);

            if (_ig_count > 0)
            {
                if (keyboard_check_pressed(vk_up))
                {
                    ig_selected = (ig_selected + _ig_count - 1) mod _ig_count;
                    _ig_need_preview = true;
                }
                if (keyboard_check_pressed(vk_down))
                {
                    ig_selected = (ig_selected + 1) mod _ig_count;
                    _ig_need_preview = true;
                }
                if (keyboard_check_pressed(vk_pageup))
                {
                    ig_selected = max(0, ig_selected - 10);
                    _ig_need_preview = true;
                }
                if (keyboard_check_pressed(vk_pagedown))
                {
                    ig_selected = min(_ig_count - 1, ig_selected + 10);
                    _ig_need_preview = true;
                }
                if (keyboard_check_pressed(vk_home))
                {
                    ig_selected = 0;
                    _ig_need_preview = true;
                }
                if (keyboard_check_pressed(vk_end))
                {
                    ig_selected = _ig_count - 1;
                    _ig_need_preview = true;
                }
            }

            if (keyboard_check_pressed(ord("R")))
            {
                _ig_rebuild = true;
                ig_status_text = "Item tables refreshed.";
                ig_status_ok = true;
                ig_status_timer = 120;
            }

            if (_ig_count > 0 && (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(ord("Z"))))
            {
                var _ig_names = ig_item_names;
                switch (ig_category)
                {
                    case 1: _ig_names = ig_weapon_names; break;
                    case 2: _ig_names = ig_armor_names; break;
                    case 3: _ig_names = ig_key_names; break;
                    case 4: _ig_names = ig_light_names; break;
                }
                var _ig_id = _ig_ids[ig_selected];
                var _ig_name = string_replace_all(_ig_names[ig_selected], "#", " ");
                noroom = 0;

                switch (ig_category)
                {
                    case 0: scr_itemget(_ig_id); break;
                    case 1: scr_weaponget(_ig_id); break;
                    case 2: scr_armorget(_ig_id); break;
                    case 3: scr_keyitemget(_ig_id); break;
                    case 4: scr_litemget(_ig_id); break;
                }

                if (noroom == 1)
                {
                    ig_status_text = "No inventory space for " + _ig_name + ".";
                    ig_status_ok = false;
                }
                else
                {
                    ig_status_text = "Added " + _ig_name + " (ID " + string(_ig_id) + ").";
                    ig_status_ok = true;
                }
                ig_status_timer = 180;
                _ig_need_preview = true;
            }
        }
    }
}

if (_ig_rebuild)
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

    for (var _ig_scan_id = 1; _ig_scan_id <= 255; _ig_scan_id++)
    {
        itemnameb = " ";
        scr_iteminfo(_ig_scan_id);
        var _ig_name_scan = string(itemnameb);
        var _ig_clean = string_replace_all(string_replace_all(_ig_name_scan, " ", ""), "#", "");
        if (string_length(_ig_clean) > 0)
        {
            array_push(ig_item_ids, _ig_scan_id);
            array_push(ig_item_names, _ig_name_scan);
        }

        weaponnametemp = " ";
        scr_weaponinfo(_ig_scan_id);
        _ig_name_scan = string(weaponnametemp);
        _ig_clean = string_replace_all(string_replace_all(_ig_name_scan, " ", ""), "#", "");
        if (string_length(_ig_clean) > 0)
        {
            array_push(ig_weapon_ids, _ig_scan_id);
            array_push(ig_weapon_names, _ig_name_scan);
        }

        armornametemp = " ";
        scr_armorinfo(_ig_scan_id);
        _ig_name_scan = string(armornametemp);
        _ig_clean = string_replace_all(string_replace_all(_ig_name_scan, " ", ""), "#", "");
        if (string_length(_ig_clean) > 0)
        {
            array_push(ig_armor_ids, _ig_scan_id);
            array_push(ig_armor_names, _ig_name_scan);
        }

        tempkeyitemname = " ";
        scr_keyiteminfo(_ig_scan_id);
        _ig_name_scan = string(tempkeyitemname);
        _ig_clean = string_replace_all(string_replace_all(_ig_name_scan, " ", ""), "#", "");
        if (string_length(_ig_clean) > 0)
        {
            array_push(ig_key_ids, _ig_scan_id);
            array_push(ig_key_names, _ig_name_scan);
        }
    }

    var _ig_old_light_item = global.litem[7];
    for (var _ig_scan_light_id = 1; _ig_scan_light_id <= 255; _ig_scan_light_id++)
    {
        global.litem[7] = _ig_scan_light_id;
        global.litemname[7] = " ";
        scr_litemname();
        var _ig_light_name = string(global.litemname[7]);
        var _ig_light_clean = string_replace_all(string_replace_all(_ig_light_name, " ", ""), "#", "");
        if (string_length(_ig_light_clean) > 0)
        {
            array_push(ig_light_ids, _ig_scan_light_id);
            array_push(ig_light_names, _ig_light_name);
        }
    }
    global.litem[7] = _ig_old_light_item;
    scr_litemname();

    ig_lists_built = true;
    ig_selected = 0;
    _ig_need_preview = true;
}

if (_ig_need_preview)
{
    var _ig_preview_ids = ig_item_ids;
    var _ig_preview_names = ig_item_names;
    switch (ig_category)
    {
        case 1: _ig_preview_ids = ig_weapon_ids; _ig_preview_names = ig_weapon_names; break;
        case 2: _ig_preview_ids = ig_armor_ids; _ig_preview_names = ig_armor_names; break;
        case 3: _ig_preview_ids = ig_key_ids; _ig_preview_names = ig_key_names; break;
        case 4: _ig_preview_ids = ig_light_ids; _ig_preview_names = ig_light_names; break;
    }
    var _ig_preview_count = array_length(_ig_preview_ids);
    ig_preview_id = -1;
    ig_preview_name = "";
    ig_preview_desc = "";
    ig_preview_extra = "";

    if (_ig_preview_count > 0)
    {
        ig_selected = clamp(ig_selected, 0, _ig_preview_count - 1);
        ig_preview_id = _ig_preview_ids[ig_selected];
        ig_preview_name = string_replace_all(_ig_preview_names[ig_selected], "#", " ");

        switch (ig_category)
        {
            case 0:
                itemdescb = "";
                scr_iteminfo(ig_preview_id);
                ig_preview_desc = string(itemdescb);
                break;
            case 1:
                weapondesctemp = "";
                scr_weaponinfo(ig_preview_id);
                ig_preview_desc = string(weapondesctemp);
                ig_preview_extra = "AT " + string(weaponattemp) + "   DF " + string(weapondftemp) + "   MAG " + string(weaponmagtemp);
                if (string_length(string_replace_all(string(weaponabilitytemp), " ", "")) > 0)
                    ig_preview_extra += "   " + string(weaponabilitytemp);
                break;
            case 2:
                armordesctemp = "";
                scr_armorinfo(ig_preview_id);
                ig_preview_desc = string(armordesctemp);
                ig_preview_extra = "AT " + string(armorattemp) + "   DF " + string(armordftemp) + "   MAG " + string(armormagtemp);
                if (string_length(string_replace_all(string(armorabilitytemp), " ", "")) > 0)
                    ig_preview_extra += "   " + string(armorabilitytemp);
                break;
            case 3:
                tempkeyitemdesc = "";
                scr_keyiteminfo(ig_preview_id);
                ig_preview_desc = string(tempkeyitemdesc);
                break;
            case 4:
                ig_preview_desc = "Light World inventory item.";
                break;
        }
    }
}

var _ig_old_font = draw_get_font();
var _ig_old_color = draw_get_color();
var _ig_old_alpha = draw_get_alpha();
var _ig_old_halign = draw_get_halign();
var _ig_old_valign = draw_get_valign();

draw_set_font(fnt_main);
draw_set_halign(fa_left);
draw_set_valign(fa_top);

var _ig_gw = display_get_gui_width();
var _ig_gh = display_get_gui_height();

if (!ig_menu_open)
{
    if (ig_status_timer > 0 && !_ig_controls_active)
    {
        var _ig_toast_w = min(440, _ig_gw - 24);
        var _ig_toast_x = (_ig_gw - _ig_toast_w) * 0.5;
        draw_set_alpha(0.94);
        draw_set_color(c_black);
        draw_rectangle(_ig_toast_x, 12, _ig_toast_x + _ig_toast_w, 45, false);
        draw_set_alpha(1);
        draw_set_color(ig_status_ok ? c_lime : c_red);
        draw_set_halign(fa_center);
        draw_text_ext(_ig_gw * 0.5, 20, ig_status_text, 16, _ig_toast_w - 20);
    }
}
else
{
    var _ig_panel_w = min(616, _ig_gw - 24);
    var _ig_panel_h = min(456, _ig_gh - 24);
    var _ig_px = (_ig_gw - _ig_panel_w) * 0.5;
    var _ig_py = (_ig_gh - _ig_panel_h) * 0.5;
    var _ig_left = _ig_px + 18;
    var _ig_right = _ig_px + _ig_panel_w - 18;
    var _ig_inner_w = _ig_panel_w - 36;

    draw_set_alpha(0.72);
    draw_set_color(c_black);
    draw_rectangle(0, 0, _ig_gw, _ig_gh, false);

    draw_set_alpha(0.985);
    draw_set_color(make_color_rgb(12, 12, 20));
    draw_rectangle(_ig_px, _ig_py, _ig_px + _ig_panel_w, _ig_py + _ig_panel_h, false);
    draw_set_alpha(1);
    draw_set_color(c_white);
    draw_rectangle(_ig_px, _ig_py, _ig_px + _ig_panel_w, _ig_py + _ig_panel_h, true);

    draw_set_halign(fa_center);
    draw_set_color(c_fuchsia);
    draw_text(_ig_gw * 0.5, _ig_py + 14, "ITEM GIVER");
    draw_set_color(c_white);
    draw_text(_ig_gw * 0.5, _ig_py + 37, "<  " + ig_category_names[ig_category] + "  >");

    var _ig_draw_ids = ig_item_ids;
    var _ig_draw_names = ig_item_names;
    switch (ig_category)
    {
        case 1: _ig_draw_ids = ig_weapon_ids; _ig_draw_names = ig_weapon_names; break;
        case 2: _ig_draw_ids = ig_armor_ids; _ig_draw_names = ig_armor_names; break;
        case 3: _ig_draw_ids = ig_key_ids; _ig_draw_names = ig_key_names; break;
        case 4: _ig_draw_ids = ig_light_ids; _ig_draw_names = ig_light_names; break;
    }
    var _ig_draw_count = array_length(_ig_draw_ids);

    draw_set_halign(fa_left);
    draw_set_color(c_gray);
    draw_text(_ig_left + 6, _ig_py + 65, "ID");
    draw_text(_ig_left + 55, _ig_py + 65, "NAME");
    draw_set_halign(fa_right);
    draw_text(_ig_right - 4, _ig_py + 65, string(ig_selected + 1) + " / " + string(_ig_draw_count));

    var _ig_visible = 9;
    var _ig_row_h = 21;
    var _ig_list_y = _ig_py + 86;
    var _ig_start = clamp(ig_selected - 4, 0, max(0, _ig_draw_count - _ig_visible));
    for (var _ig_row = 0; _ig_row < _ig_visible; _ig_row++)
    {
        var _ig_index = _ig_start + _ig_row;
        if (_ig_index >= _ig_draw_count)
            break;

        var _ig_y = _ig_list_y + (_ig_row * _ig_row_h);
        var _ig_id_draw = _ig_draw_ids[_ig_index];
        var _ig_name_draw = string_replace_all(_ig_draw_names[_ig_index], "#", " ");
        if (string_length(_ig_name_draw) > 30)
            _ig_name_draw = string_copy(_ig_name_draw, 1, 27) + "...";

        if (_ig_index == ig_selected)
        {
            draw_set_alpha(0.28);
            draw_set_color(c_fuchsia);
            draw_rectangle(_ig_left, _ig_y - 2, _ig_right, _ig_y + 18, false);
            draw_set_alpha(1);
            draw_set_color(c_yellow);
            draw_set_halign(fa_left);
            draw_text(_ig_left + 6, _ig_y, "> " + string(_ig_id_draw));
            draw_text(_ig_left + 55, _ig_y, _ig_name_draw);
        }
        else
        {
            draw_set_color(c_white);
            draw_set_halign(fa_left);
            draw_text(_ig_left + 6, _ig_y, string(_ig_id_draw));
            draw_text(_ig_left + 55, _ig_y, _ig_name_draw);
        }
    }

    var _ig_sep_y = _ig_py + 284;
    draw_set_color(c_gray);
    draw_line(_ig_left, _ig_sep_y, _ig_right, _ig_sep_y);

    draw_set_halign(fa_left);
    draw_set_color(c_yellow);
    var _ig_preview_title = ig_preview_name;
    if (string_length(_ig_preview_title) > 34)
        _ig_preview_title = string_copy(_ig_preview_title, 1, 31) + "...";
    draw_text(_ig_left + 6, _ig_sep_y + 10, _ig_preview_title + "  [ID " + string(ig_preview_id) + "]");

    draw_set_color(c_white);
    var _ig_desc = string_hash_to_newline(ig_preview_desc);
    draw_text_ext(_ig_left + 6, _ig_sep_y + 33, _ig_desc, 16, _ig_inner_w - 12);

    if (string_length(ig_preview_extra) > 0)
    {
        draw_set_color(c_aqua);
        draw_text_ext(_ig_left + 6, _ig_py + _ig_panel_h - 76, string_replace_all(ig_preview_extra, "#", " "), 16, _ig_inner_w - 12);
    }

    if (ig_status_timer > 0)
    {
        draw_set_color(ig_status_ok ? c_lime : c_red);
        draw_text_ext(_ig_left + 6, _ig_py + _ig_panel_h - 55, string_replace_all(ig_status_text, "#", " "), 16, _ig_inner_w - 12);
    }

    draw_set_color(c_gray);
    draw_set_halign(fa_center);
    var _ig_key_name = "I";
    if (variable_global_exists("asc_def"))
        _ig_key_name = string(global.asc_def[global.ig_itemgiver_key]);
    draw_text(_ig_gw * 0.5, _ig_py + _ig_panel_h - 24, "Arrows: Navigate   PgUp/PgDn: Jump   Z/Enter: Give   R: Refresh   X/Esc/" + _ig_key_name + ": Close");
}

draw_set_halign(_ig_old_halign);
draw_set_valign(_ig_old_valign);
draw_set_font(_ig_old_font);
draw_set_color(_ig_old_color);
draw_set_alpha(_ig_old_alpha);
