#!/usr/bin/env python3
"""Build Item Giver Mode v0.3.0 as mergeable Deltamod .g3mpatch files.

ItemGiver_Draw75.gml is the compact v0.2.0 append-body baseline. This builder
upgrades that source to v0.3.0 before compiling it, keeping the distributable
patch at one existing CodeEntry per chapter.
"""
from pathlib import Path
import argparse
import shutil
import subprocess
import tempfile
import zipfile

ROOT = Path(__file__).resolve().parent
CHAPTERS = range(1, 6)


def run(cmd):
    print('+', *map(str, cmd))
    subprocess.run([str(x) for x in cmd], check=True, stdin=subprocess.DEVNULL)


def upgrade_gml(src: str) -> str:
    src = src.replace('// Item Giver v0.2.0', '// Item Giver v0.3.0', 1)
    src = src.replace('ig_itemgiver_version = "0.2.0";', 'ig_itemgiver_version = "0.3.0";', 1)

    old = '    ig_category_names = ["ITEMS", "WEAPONS", "ARMOR", "KEY ITEMS", "LIGHT ITEMS"];'
    new = old + '''\n    // [5] keyboard binding, [6] loaded file slot, [7] rebind state, [8] debounce\n    array_push(ig_category_names, ord("I"));\n    array_push(ig_category_names, -999);\n    array_push(ig_category_names, 0);\n    array_push(ig_category_names, 0);'''
    if old not in src:
        raise RuntimeError('Could not find Item Giver state initialization anchor')
    src = src.replace(old, new, 1)

    marker = '''if (ig_status_timer > 0)\n    ig_status_timer--;'''
    load = '''// Load the Item Giver binding for the current save slot.\nif (variable_global_exists("filechoice") && ig_category_names[6] != global.filechoice)\n{\n    ig_category_names[5] = ord("I");\n    ig_category_names[6] = global.filechoice;\n    ig_category_names[7] = 0;\n    ig_category_names[8] = 0;\n    var _ig_cfg = "keyconfig_" + string(global.filechoice) + ".ini";\n    if (ossafe_file_exists(_ig_cfg))\n    {\n        ossafe_ini_open(_ig_cfg);\n        ig_category_names[5] = ini_read_real("ITEM_GIVER", "KEYBOARD", ord("I"));\n        if (!global.is_console)\n            ini_close();\n        else\n            ossafe_ini_close();\n    }\n}\n\n''' + marker
    if marker not in src:
        raise RuntimeError('Could not find status-timer anchor')
    src = src.replace(marker, load, 1)

    logic_anchor = '''var _ig_rebuild = false;\nvar _ig_need_preview = false;'''
    logic = '''var _ig_rebuild = false;\nvar _ig_need_preview = false;\nvar _ig_controls_active = (!global.is_console && variable_global_exists("submenu") && global.submenu == 35 && i_ex(obj_darkcontroller));\n\n// Add ITEM GIVER as pseudo-row 9 below the game's native Controls rows.\n// This intentionally avoids changing obj_darkcontroller, preserving the one-CodeEntry footprint.\nif (_ig_controls_active)\n{\n    if (ig_category_names[7] == 0)\n    {\n        if (global.submenucoord[35] == 8 && down_p())\n            global.submenucoord[35] = 9;\n\n        if (global.submenucoord[35] == 9 && up_p())\n            global.submenucoord[35] = 8;\n\n        if (global.submenucoord[35] == 9 && button1_p())\n        {\n            ig_category_names[7] = 1;\n            ig_category_names[8] = 2;\n            ig_status_text = "Press a keyboard key for Item Giver. Esc cancels.";\n            ig_status_ok = true;\n            ig_status_timer = 180;\n        }\n        else if (global.submenucoord[35] == 7 && button1_p())\n        {\n            ig_category_names[5] = ord("I");\n            var _ig_cfg_reset = "keyconfig_" + string(global.filechoice) + ".ini";\n            ossafe_ini_open(_ig_cfg_reset);\n            ini_write_real("ITEM_GIVER", "KEYBOARD", ig_category_names[5]);\n            if (!global.is_console)\n                ini_close();\n            else\n            {\n                ossafe_ini_close();\n                ossafe_savedata_save();\n            }\n            ig_status_text = "Item Giver key reset to I.";\n            ig_status_ok = true;\n            ig_status_timer = 120;\n        }\n    }\n    else\n    {\n        if (ig_category_names[8] > 0)\n            ig_category_names[8]--;\n        else if (keyboard_check_pressed(vk_escape))\n        {\n            ig_category_names[7] = 0;\n            ig_status_text = "Item Giver rebind cancelled.";\n            ig_status_ok = false;\n            ig_status_timer = 90;\n        }\n        else if (keyboard_check_pressed(vk_anykey))\n        {\n            var _ig_new_key = keyboard_lastkey;\n            var _ig_reserved = false;\n            for (var _ig_key_i = 0; _ig_key_i < 7; _ig_key_i++)\n            {\n                if (global.input_k[_ig_key_i] == _ig_new_key)\n                    _ig_reserved = true;\n            }\n            if (_ig_new_key == vk_enter || _ig_new_key == vk_shift || _ig_new_key == vk_control || _ig_new_key == vk_escape)\n                _ig_reserved = true;\n\n            if (_ig_reserved)\n            {\n                ig_status_text = "That key is already reserved. Choose another key.";\n                ig_status_ok = false;\n                ig_status_timer = 120;\n            }\n            else\n            {\n                ig_category_names[5] = _ig_new_key;\n                ig_category_names[7] = 0;\n                var _ig_cfg_save = "keyconfig_" + string(global.filechoice) + ".ini";\n                ossafe_ini_open(_ig_cfg_save);\n                ini_write_real("ITEM_GIVER", "KEYBOARD", ig_category_names[5]);\n                if (!global.is_console)\n                    ini_close();\n                else\n                {\n                    ossafe_ini_close();\n                    ossafe_savedata_save();\n                }\n                ig_status_text = "Item Giver key set to " + string(global.asc_def[ig_category_names[5]]) + ".";\n                ig_status_ok = true;\n                ig_status_timer = 120;\n            }\n        }\n    }\n}\nelse\n{\n    ig_category_names[7] = 0;\n    ig_category_names[8] = 0;\n}\n\nvar _ig_bind_pressed = keyboard_check_pressed(ig_category_names[5]);'''
    if logic_anchor not in src:
        raise RuntimeError('Could not find Item Giver logic anchor')
    src = src.replace(logic_anchor, logic, 1)

    if src.count('keyboard_check_pressed(vk_f7)') != 2:
        raise RuntimeError('Unexpected F7 source layout')
    src = src.replace('keyboard_check_pressed(vk_f7)', '_ig_bind_pressed')
    src = src.replace('''    if (_ig_bind_pressed)\n    {''', '''    if (_ig_bind_pressed && !_ig_controls_active)\n    {''', 1)

    footer = '"Arrows: Navigate   PgUp/PgDn: Jump   Z/Enter: Give   R: Refresh   X/Esc/F7: Close"'
    replacement = '"Arrows: Navigate   PgUp/PgDn: Jump   Z/Enter: Give   R: Refresh   X/Esc/" + string(global.asc_def[ig_category_names[5]]) + ": Close"'
    if footer not in src:
        raise RuntimeError('Could not find Item Giver footer anchor')
    src = src.replace(footer, replacement, 1)

    draw_anchor = '''if (!ig_menu_open)\n{\n    if (ig_status_timer > 0)'''
    draw_insert = '''if (!ig_menu_open)\n{\n    if (_ig_controls_active)\n    {\n        var _ig_ctrl_xx = obj_darkcontroller.xx;\n        var _ig_ctrl_yy = obj_darkcontroller.yy;\n        var _ig_ctrl_yoff = (global.lang == "en") ? 0 : -4;\n        var _ig_ctrl_linepad = (global.lang == "ja") ? 1 : 0;\n        var _ig_ctrl_dualshock = global.gamepad_type == "Sony DualShock 4" || global.gamepad_type == "DualSense Wireless Controller";\n        var _ig_ctrl_text_y;\n        if (_ig_ctrl_dualshock)\n            _ig_ctrl_text_y = ((global.lang == "en") ? (_ig_ctrl_yy + 137) : (_ig_ctrl_yy + 136)) + (9 * (29 + _ig_ctrl_linepad)) + _ig_ctrl_yoff;\n        else\n            _ig_ctrl_text_y = _ig_ctrl_yy + 140 + (9 * (28 + _ig_ctrl_linepad)) + _ig_ctrl_yoff;\n        var _ig_ctrl_voff = langopt(0, -8);\n        var _ig_ctrl_vspacing = langopt(28, 30);\n        var _ig_ctrl_key_y = _ig_ctrl_yy + _ig_ctrl_voff + 140 + (9 * _ig_ctrl_vspacing);\n        draw_set_halign(fa_left);\n        draw_set_color((global.submenucoord[35] == 9) ? c_aqua : c_white);\n        if (ig_category_names[7] == 1)\n            draw_set_color(c_red);\n        draw_text(_ig_ctrl_xx + 105, _ig_ctrl_text_y, "ITEM GIVER");\n        draw_text(_ig_ctrl_xx + 325, _ig_ctrl_key_y, (ig_category_names[7] == 1) ? "PRESS A KEY" : string(global.asc_def[ig_category_names[5]]));\n    }\n    if (ig_status_timer > 0)'''
    if draw_anchor not in src:
        raise RuntimeError('Could not find Draw GUI controls anchor')
    src = src.replace(draw_anchor, draw_insert, 1)
    return src


def make_csx(chapter: int, output: Path):
    template = (ROOT / 'ItemGiver_template.csx').read_text(encoding='utf-8')
    gml = upgrade_gml((ROOT / 'ItemGiver_Draw75.gml').read_text(encoding='utf-8'))
    gml = gml.replace('"', '""')
    text = template.replace('__CHAPTER__', str(chapter)).replace('__GML_BODY__', gml)
    output.write_text(text, encoding='utf-8', newline='\n')


def main():
    p = argparse.ArgumentParser()
    p.add_argument('--game-root', type=Path, required=True,
                   help='Folder containing chapter1_windows ... chapter5_windows')
    p.add_argument('--utmt', type=Path, required=True, help='UndertaleModCli executable')
    p.add_argument('--g3mtool', type=Path, required=True, help='Deltamod-compatible G3MTool executable')
    p.add_argument('--output', type=Path, default=ROOT / 'Item_Giver_Mode_v0.3.0_Deltamod.zip')
    args = p.parse_args()

    with tempfile.TemporaryDirectory(prefix='item_giver_build_') as temp_name:
        temp = Path(temp_name)
        stage = temp / 'package'
        patches = stage / 'patches'
        patches.mkdir(parents=True)
        for name in ('meta.json', 'modding.xml', 'README.txt', 'LICENSE'):
            shutil.copy2(ROOT / name, stage / name)

        for chapter in CHAPTERS:
            clean = args.game_root / f'chapter{chapter}_windows' / 'data.win'
            modified = temp / f'ch{chapter}_modified.win'
            csx = temp / f'ItemGiver_ch{chapter}.csx'
            patch = patches / f'ItemGiver_ch{chapter}.g3mpatch'
            if not clean.exists():
                raise FileNotFoundError(clean)
            make_csx(chapter, csx)
            run([args.utmt, 'load', clean, '--output', modified, '--scripts', csx])
            run([args.g3mtool, 'patch', 'create', clean, modified, patch])
            run([args.g3mtool, 'patch', 'validate', patch, '--data', clean])

        args.output.parent.mkdir(parents=True, exist_ok=True)
        with zipfile.ZipFile(args.output, 'w', zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
            for file in sorted(stage.rglob('*')):
                if file.is_file():
                    archive.write(file, file.relative_to(stage).as_posix())
        with zipfile.ZipFile(args.output) as archive:
            bad = archive.testzip()
            if bad:
                raise RuntimeError(f'ZIP integrity failure: {bad}')
        print('Wrote', args.output)


if __name__ == '__main__':
    main()
